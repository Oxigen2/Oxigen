using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToHashSet;
using System.IO;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.XMLSerializer;
using OxigenIIAdvertising.DemographicRange;
using OxigenIIAdvertising.Demographic;
using OxigenIIAdvertising.TaxonomySearch;
using OxigenIIAdvertising.LoggerInfo;

namespace OxigenIIAdvertising.PlaylistLogic
{
  /// <summary>
  /// Static class to read downloaded encrypted channel subscriptions, channel data for subscribed channels
  /// and advertisement list and generate the playlist file for the user.
  /// </summary>
  public static class PlaylistMaker
  {
    /// <summary>
    /// Creates the playlist for the user
    /// </summary>
    /// <param name="channelDataPath">Directory on the end user's computer where channel content files reside</param>
    /// <param name="advertListPath">Fully qualified path of the advert list file on the end user's computer</param>
    /// <param name="demographicDataPath">Fully qualified path of the demographic data path on the end user's computer</param>
    /// <param name="playlistDataPath">Fully qualified path on the end user's computer to save the playlist to</param>
    /// <param name="password">Encryption password</param>
    /// <param name="channelSubscriptions">ChannelSubscriptions object with the user's channel subscriptions</param>
    /// <param name="logger">Logger to log debug/error messages</param>
    /// <exception cref="System.Runtime.Serialization.SerializationException">Thrown when an error occurs during serialization/deserializaton</exception>
    /// <exception cref="InvalidOperationException">Thrown when file to be serialized don't match the deserializer's expected type</exception>
    /// <exception cref="DirectoryNotFoundException">thrown when directory to save or load a file from is not found</exception>
    /// <exception cref="FileNotFoundException">thrown when a file is not found</exception>
    /// <exception cref="CryptographicException">thrown when a serialized file is corrupted</exception>
    public static Playlist CreatePlaylist(string channelDataPath, 
                                          string advertListPath, 
                                          string demographicDataPath, 
                                          string playlistDataPath, 
                                          string password,
                                          float defaultDisplayDuration,
                                          ChannelSubscriptions channelSubscriptions,
                                          Logger logger)
    {
      Playlist playlist = new Playlist();

      // holds the channels to which the user is subscribed
      ChannelData channelData = new ChannelData();

      // check if channel data directory exists!
      if (!Directory.Exists(channelDataPath))
        return null;

      string[] fileEntries = Directory.GetFiles(channelDataPath, "*.dat");

      if (channelSubscriptions.SubscriptionSet.Count == 0 || fileEntries.Length == 0)
        return playlist;

      foreach (string file in fileEntries)
        channelData.Channels.Add((Channel)Serializer.Deserialize(typeof(Channel), file, "password"));

      if (!File.Exists(demographicDataPath))
        return null;

      DateTime now = DateTime.Now;

      DemographicData demographicData = (DemographicData)Serializer.Deserialize(typeof(DemographicData), demographicDataPath, password);

      DemographicRangeVerifier demographicRangeVerifier = new DemographicRangeVerifier(demographicData);

      GetNonAdvertisingPlaylist(channelData, 
        password, 
        playlist, 
        demographicRangeVerifier, 
        channelSubscriptions, 
        now,
        defaultDisplayDuration,
        logger);

      // advertising playlist
      GetAdvertisingPlaylist(advertListPath, 
        demographicDataPath,
        demographicRangeVerifier, 
        password, 
        playlist, 
        channelData, 
        channelSubscriptions, 
        now, 
        logger);

      Serializer.Serialize(playlist, playlistDataPath, password);
     
      return playlist;
    }

    private static void GetNonAdvertisingPlaylist(ChannelData channelData, 
      string password, 
      Playlist playlist, 
      DemographicRangeVerifier demographicRangeVerifier, 
      ChannelSubscriptions channelSubscriptions, 
      DateTime now, 
      float defaultDisplayDuration,
      Logger logger)
    {
      //
      // calculate content playlist
      //
      // calculate average play times
      var avgPlayTimeQuery = from Channel channel in channelData.Channels
                             join channelSubscription in channelSubscriptions.SubscriptionSet on channel.ChannelID equals channelSubscription.ChannelID
                             from ChannelAsset channelAsset in channel.ChannelAssets
                             group channelAsset by channel.ChannelID into groupedAssets
                             select new 
                             { 
                               ChannelID = groupedAssets.Key, 
                               AveragePlayTime = (float)groupedAssets.Average(channelAsset => channelAsset.DisplayDuration == -1F ? defaultDisplayDuration : channelAsset.DisplayDuration) 
                             };

      float sumAveragePlayTimes = 0F;

      foreach (var channel in avgPlayTimeQuery)
        sumAveragePlayTimes += channel.AveragePlayTime;

      // calculate playing probability of each channel
      var channelBuckets = HashSetLinqAccess.ToHashSet<ChannelBucket>(from ChannelSubscription channelSubscription in channelSubscriptions.SubscriptionSet
                                                                     join channel in avgPlayTimeQuery on channelSubscription.ChannelID equals channel.ChannelID
                                                                     join channelDataChannel in channelData.Channels on channel.ChannelID equals channelDataChannel.ChannelID
                                                                     select new ChannelBucket 
                                                                     { 
                                                                       ChannelID = channel.ChannelID,
                                                                       ChannelName = channelSubscription.ChannelName,
                                                                       AveragePlayTime = channel.AveragePlayTime,
                                                                       PlayingProbabilityUnnormalized = channel.AveragePlayTime > 0 && sumAveragePlayTimes > 0 ? (float)channelSubscription.ChannelWeightingUnnormalised / (channel.AveragePlayTime / sumAveragePlayTimes) : 0,
                                                                       ContentAssets = ConvertChannelAssetsToPlayListAssetsIfDateTimeDemoPermits(channelDataChannel, demographicRangeVerifier, now)
                                                                     });

      if (channelBuckets.Count == 0)
        return;

      playlist.ChannelBuckets = channelBuckets;

      // normalize playing probabilities
      NormalizeChannelBucketPlayingProbabilities(playlist.ChannelBuckets, logger);
    }

    /// <summary>
    /// Creates a HashSet of assets to be included in the user's playlist.
    /// Uses the Channel's ChannelAsset HashSet. 
    /// </summary>
    /// <returns>a HashSet of PlayListAsset objects</returns>
    private static HashSet<ContentPlaylistAsset> ConvertChannelAssetsToPlayListAssetsIfDateTimeDemoPermits(Channel channel, 
      DemographicRangeVerifier demographicRangeVerifier, DateTime now)
    {
      HashSet<ContentPlaylistAsset> playlistAssets = new HashSet<ContentPlaylistAsset>();

      ContentPlaylistAsset playlistAsset = null;

      foreach (ChannelAsset chAsset in channel.ChannelAssets)
      {
        if (now >= chAsset.StartDateTime && now <= chAsset.EndDateTime && 
          demographicRangeVerifier.IsAssetDemoSyntaxPlayable(chAsset.DemoRequirements))
        {
          playlistAsset = new ContentPlaylistAsset
          {
            AssetID = chAsset.AssetID,
            AssetFilename = chAsset.AssetFilename,
            ClickDestination = chAsset.ClickDestination,
            AssetWebSite = chAsset.AssetWebSite,
            DisplayLength = chAsset.DisplayDuration,
            PlayerType = chAsset.PlayerType,
            ScheduleInfo = chAsset.ScheduleInfo,
            AssetLevel = chAsset.AssetLevel == ChannelDataAssetLevel.Normal ? PlaylistAssetLevel.Normal : PlaylistAssetLevel.Premium,
            StartDateTime = chAsset.StartDateTime,
            EndDateTime = chAsset.EndDateTime
          };

          playlistAssets.Add(playlistAsset);
        }
      }

      return playlistAssets;
    }

    public static void NormalizeChannelBucketPlayingProbabilities(HashSet<ChannelBucket> channelBuckets, Logger logger)
    {
      float sumPlayingProbabilities = 0F;
      float normalisedPlayingProbability = -1F;
      float previousLowerThresholdNormalised = 0F;

      foreach (ChannelBucket cb in channelBuckets)
        sumPlayingProbabilities += cb.PlayingProbabilityUnnormalized;

      // normalize playing probabilities if there is at least a playing probability greater than 0.
      if (sumPlayingProbabilities <= 0)
        return;
      
      int noChannelBuckets = channelBuckets.Count;

      for (int i = 0; i < noChannelBuckets; i++)
      {
        ChannelBucket cb = channelBuckets.ElementAt(i);

        normalisedPlayingProbability = cb.PlayingProbabilityUnnormalized / sumPlayingProbabilities;

        cb.PlayingProbabilityNormalised = normalisedPlayingProbability;

        // calculate lower and upper threshold for random play
        cb.LowerThresholdNormalised = previousLowerThresholdNormalised;

        // make last content asset's higher normalized threshold equal to 1, else add the newly calculated normalized weighting.
        if (i < noChannelBuckets - 1)
          cb.HigherThresholdNormalised = previousLowerThresholdNormalised + normalisedPlayingProbability;
        else
          cb.HigherThresholdNormalised = 1;

        // increment lower threshold for next loop run
        previousLowerThresholdNormalised += normalisedPlayingProbability;
      }
    }

    private static void GetAdvertisingPlaylist(string advertListPath, string demographicDataPath,
      DemographicRangeVerifier demographicRangeVerifier, string password, Playlist playlist, ChannelData channelData,
      ChannelSubscriptions channelSubscriptions, DateTime now, Logger logger)
    {
      // if the advertisment or the demographic data file is not found, an exception will be thrown
      // if user has corrupted any of the files, an exception will be thrown
      AdvertList advertList = (AdvertList)Serializer.Deserialize(typeof(AdvertList), advertListPath, password);

      // flatten channel definitions
      HashSet<string> channelDefinitions = new HashSet<string>();

      foreach (Channel channel in channelData.Channels)
        SplitAndInsertChannelDefinitions(ref channelDefinitions, channel.ChannelDefinitions);

      // flatten advert definitions
      HashSet<string> advertDefinitions = new HashSet<string>();
      
      foreach (AdvertAsset advertasset in advertList.Adverts)
        SplitAndInsertChannelDefinitions(ref advertDefinitions, advertasset.AdvertDefinitions);

      var advertListsFiltered = HashSetLinqAccess.ToHashSet<AdvertPlaylistAsset>(from AdvertAsset advertAsset in advertList.Adverts
                                                                           where demographicRangeVerifier.IsAssetDemoSyntaxPlayable(advertAsset.DemoRequirements)
                                                                           && TreeSearch.IncludeByChannelClassifications(channelDefinitions, advertAsset.InclusionExclusionList) == true
                                                                           && TreeSearch.IncludeByAdvertClassifications(advertDefinitions, channelData, channelSubscriptions) == true
                                                                           && now >= advertAsset.StartDateTime 
                                                                           && now <= advertAsset.EndDateTime
                                                                           select new AdvertPlaylistAsset
                                                                           {                                                                             
                                                                             AssetID = advertAsset.AssetID,
                                                                             AssetFilename = advertAsset.AssetFilename,
                                                                             ClickDestination = advertAsset.ClickDestination,
                                                                             AssetWebSite = advertAsset.AssetWebSite,
                                                                             PlayerType = advertAsset.PlayerType,
                                                                             ScheduleInfo = advertAsset.ScheduleInfo,
                                                                             DisplayLength = advertAsset.DisplayDuration,
                                                                             WeightingUnnormalized = advertAsset.Weighting,
                                                                             StartDateTime = advertAsset.StartDateTime,
                                                                             EndDateTime = advertAsset.EndDateTime
                                                                           });
     
      playlist.AdvertBucket.AdvertAssets = advertListsFiltered;
    }

    private static void NormalizeAdvertWeightings(HashSet<AdvertPlaylistAsset> advertAssets)
    {
      float sumWeightings = 0F;
      float normalisedWeighting = -1F;
      float previousLowerThresholdNormalised = 0F;

      foreach (AdvertPlaylistAsset advertPlaylistAsset in advertAssets)
        sumWeightings += advertPlaylistAsset.WeightingUnnormalized;

      // normalize advert weightings if there is at least a weighting greater than 0
      if (sumWeightings <= 0)
        return;

      int noAdvertPlaylistAssets = advertAssets.Count;

      for (int i = 0; i < noAdvertPlaylistAssets; i++)
      {
        AdvertPlaylistAsset advertPlaylistAsset = advertAssets.ElementAt(i);

        normalisedWeighting = advertPlaylistAsset.WeightingUnnormalized / sumWeightings;

        advertPlaylistAsset.WeightingNormalised = normalisedWeighting;

        // calculate lower and upper threshold for random play
        advertPlaylistAsset.LowerThresholdNormalised = previousLowerThresholdNormalised;

        // make last advert asset's higher normalized threshold equal to 1, else add the newly calculated normalized weighting.
        if (i < noAdvertPlaylistAssets - 1)
          advertPlaylistAsset.HigherThresholdNormalised = previousLowerThresholdNormalised + normalisedWeighting;
        else
          advertPlaylistAsset.HigherThresholdNormalised = 1;

        // increment lower threshold for next loop run
        previousLowerThresholdNormalised += normalisedWeighting;
      }
    }

    private static void SplitAndInsertChannelDefinitions(ref HashSet<string> definitions, string playableItemDefinitions)
    {
      if (playableItemDefinitions == null)
        return;

      string[] definitionsArray = playableItemDefinitions.Split();

      foreach (string definition in definitionsArray)
      {
        if (!definitions.Contains<string>(definition))
          definitions.Add(definition);
      }
    }
  }
}
