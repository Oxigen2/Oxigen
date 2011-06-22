using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.AssetScheduling;
using System.Security.Cryptography;
using System.IO;
using OxigenIIAdvertising.LoggerInfo;

namespace OxigenIIAdvertising.PlaylistLogic
{
  /// <summary>
  /// Provides methods to pick a random asset from the user's subscribed channels
  /// </summary>
  public class PlaylistAssetPicker
  {
    private HashSet<ChannelBucket> _consumedChannelBuckets = null;
    private HashSet<ContentPlaylistAsset> _consumedContentPlaylistAssets = null;
    private RNGCryptoServiceProvider _random = null;
    private Playlist _playlist= null;
    private AssetScheduler _assetScheduler;
    private int _noChannelBuckets = -1;
    private int _noPlaylistAdverts = -1;
    private string _assetFilePath = "";
    private string _password = "";
    private float _displayMessageAssetDisplayLength = -1;
    private byte[] _randomNumbers = null;
    private int _requestTimeout = -1;
    private bool _bIncludeContentWebAssets = true;
    private bool _bErrorOnSetup = false;
    private Logger _logger = null;
    private bool _bInsufficientMemoryForLargeFiles = false;

    /// <summary>
    /// User to set the playlist to pick assets from when screensaver impressions are in progress (constructor sets the playlist at startup).
    /// Not thread safe. Use a lock around this property to make thread safe.
    /// </summary>
    public Playlist Playlist
    {
      set
      {
        _playlist = value;
      }
    }

    /// <summary>
    /// Creates a playlist asset picker
    /// </summary>
    /// <param name="playlist">The playlist to search in</param>
    /// <param name="assetScheduler">AssetScheduler object to determine if an asset has temporal availability</param>
    /// <param name="displayMessageAssetDisplayLength">Display length of the "no assets" asset</param>
    /// <param name="assetFilePath">Directory path of the playlist assets</param>
    /// <param name="password">password to use when checking for decryption</param>
    /// <param name="requestTimeout">The number of seconds to wait before the request for testing net/website availability times out</param>
    /// <param name="bErrorOnSetup">If an error occurred on setup, an asset won't be attempted</param>
    public PlaylistAssetPicker(Playlist playlist, 
      AssetScheduler assetScheduler, 
      float displayMessageAssetDisplayLength, 
      string assetFilePath, 
      string password, 
      int requestTimeout,
      bool bErrorOnSetup,
      int screenNo,
      Logger logger,
      bool bInsufficientMemoryForLargeFiles)
    {
      _consumedChannelBuckets = new HashSet<ChannelBucket>();
      _consumedContentPlaylistAssets = new HashSet<ContentPlaylistAsset>();
      _random = new RNGCryptoServiceProvider();
      _logger = logger;

      _bErrorOnSetup = bErrorOnSetup;

      _displayMessageAssetDisplayLength = displayMessageAssetDisplayLength;

      if (_bErrorOnSetup)
      {
        _logger.WriteMessage("There was an error on initial setup; remaining Picker members will not be initialised.");
        return;
      }

      _playlist = playlist;
      _assetScheduler = assetScheduler;
      _noChannelBuckets = playlist.ChannelBuckets.Count;
      _noPlaylistAdverts = playlist.AdvertBucket.AdvertAssets.Count;
      _assetFilePath = assetFilePath;
      _password = password;
      _requestTimeout = requestTimeout;
      _randomNumbers = new byte[8];
      _bInsufficientMemoryForLargeFiles = bInsufficientMemoryForLargeFiles;

      _logger.WriteMessage("Picker members all initialised.");
    }

    public ChannelAssetAssociation SelectAsset(float totalDisplayTime, float totalAdvertDisplayTime, float protectedContentTime,
      float advertDisplayThreshold, string displayMessage, string appToRun)
    {
      ChannelAssetAssociation channelAssetAssociation = null;

      if (_bErrorOnSetup)
      {
        PlaylistAsset playlistAsset = new ContentPlaylistAsset(_displayMessageAssetDisplayLength, displayMessage, appToRun);

        _logger.WriteError("There was an error on initial setup; \"No Assets\" asset selected.");

        return new ChannelAssetAssociation(0, playlistAsset);
      }

      // is the next asset content or advert?
      _logger.WriteMessage("Deciding whether the next asset will be a content or an advert.");

      bool bIsNextAssetAdvert = IsNextAssetAdvert(totalDisplayTime, totalAdvertDisplayTime, protectedContentTime, advertDisplayThreshold);

      _logger.WriteMessage("will the next asset be an advert: " + bIsNextAssetAdvert);

      if (bIsNextAssetAdvert)
        channelAssetAssociation = PickRandomAdvertPlaylistAsset();
      else
        channelAssetAssociation = PickRandomContentPlaylistAsset();

      if (bIsNextAssetAdvert && channelAssetAssociation == null)
      {
        _logger.WriteMessage("Next asset is an advert but an advert could not be selected at this time. Attempting to select a content.");

        channelAssetAssociation = PickRandomContentPlaylistAsset();

        if (channelAssetAssociation != null)
          _logger.WriteMessage("playlist asset " + channelAssetAssociation.PlaylistAsset.AssetID + " was selected.");
      }

      // if no asset was found, create a "No assets" asset
      if (channelAssetAssociation == null)
      {
        channelAssetAssociation = new ChannelAssetAssociation(0, new ContentPlaylistAsset(_displayMessageAssetDisplayLength, displayMessage, "app://ContentExchanger"));

        _logger.WriteMessage("no asset was found, \"No Assets\" asset selected.");
      }

      return channelAssetAssociation;
    }

    private bool IsNextAssetAdvert(float totalDisplayTime, float totalAdvertDisplayTime, float protectedContentTime, float advertDisplayThreshold)
    {
      _logger.WriteMessage("totalDisplayTime = " + totalDisplayTime + ", totalAdvertDisplayTime = " + totalAdvertDisplayTime + ", protectedContentTime = " + protectedContentTime + ", advertDisplayThreashold = " + advertDisplayThreshold);

      // screensaver has not played any assets yet, cannot apply equation so select a content
      if (totalDisplayTime <= protectedContentTime)
      {
        _logger.WriteMessage("totalDisplayTime < protectedContentTime so the next asset will be a content.");
        return false;
      }
      
      _logger.WriteMessage("totalAdvertDisplayTime / totalDisplayTime = " + (float)totalAdvertDisplayTime / (float)totalDisplayTime);

      return ((float)totalAdvertDisplayTime / (float)totalDisplayTime) < advertDisplayThreshold;
    }
    
    /// <summary>
    /// Picks a random asset from the content exchanger-generated Playlist
    /// </summary>
    /// <param name="playlist">The content exchanger-generated Playlist</param>
    /// <param name="assetScheduler">Asset Scheduler object that determines which assets are temporally capable of being played</param>
    /// <returns>The information on the content playlist asset, null if no available asset found</returns>
    private ChannelAssetAssociation PickRandomContentPlaylistAsset()
    {
      // this counts the number of buckets we have already tried and have not found any content in.
      int noConsumedChannelBuckets = _consumedChannelBuckets.Count;

      _logger.WriteMessage("Attempted Channel Buckets: " + noConsumedChannelBuckets);

      // if user has not subscribed to any channels or if all channel buckets have been searched, return null
      // clear consumed buckets to search again on the next run
      if (_noChannelBuckets == 0 || _noChannelBuckets == noConsumedChannelBuckets)
      {
        _consumedChannelBuckets.Clear();
        _consumedContentPlaylistAssets.Clear();

        if (_noChannelBuckets > 0)
          _logger.WriteMessage("All channel buckets searched. No content asset can be played at this time.");
        else
          _logger.WriteMessage("No channel buckets in playlist. No content asset can be played at this time.");

        return null;
      }

      ChannelBucket cb = GetRandomChannelBucket(_playlist);

      _logger.WriteMessage("channel bucket selected successfully");

      ChannelAssetAssociation caa = GetRandomContentPlaylistAsset(cb);

      if (caa == null)
      {
        _consumedChannelBuckets.Add(cb);

        _logger.WriteMessage("no content in this channel was selected. Channel bucket will not be attempted again in this run. Attempting another channel bucket.");

        caa = PickRandomContentPlaylistAsset();
      }
      else
        _logger.WriteMessage("content asset selected: " + caa.PlaylistAsset.AssetID + " in channel: " + caa.ChannelID);

      // at this point the algorithm has succeeded or has processed all buckets and assets and found no content available
      // clear consumed
      _consumedChannelBuckets.Clear();
      _consumedContentPlaylistAssets.Clear();

      // next pick will include content web assets if due to network/website unavailability those were excluded
      _bIncludeContentWebAssets = true;

      return caa; // if this is null, there is no content asset available for playing
    }

    // picks a random channel bucket from the ones that haven't been picked before, using probability rules
    private ChannelBucket GetRandomChannelBucket(Playlist playlist)
    {
      // run a loop 25 times then just randomly pick a bucket from the remain non-consumed list.
      // reason: channel buckets with large probability may have been selected so far;
      // miniscule channel buckets will take longer,
      // method can theoretically engage in an infinite loop
      for (int i = 0; i < 25; i++)
      {
        // get double from random
        double randomDouble = GetRandomDouble();

        foreach (ChannelBucket cb in playlist.ChannelBuckets)
        {
          // if the channel bucket hasn't been selected in a previous run and if the random number falls within its boundaries, return it
          if (!_consumedChannelBuckets.Contains(cb)
            && randomDouble < cb.HigherThresholdNormalised
            && randomDouble >= cb.LowerThresholdNormalised)
          {
            _logger.WriteMessage("channel bucket with probability selected.");
            return cb;
          }
        }
      }

      _logger.WriteMessage("channel bucket with probability failed. Attempting to select a channel bucket randomly and without probability.");

      return GetRandomChannelBucketWithoutProbability(playlist);
    }

    // picks a random channel bucket from the ones that haven't been picked before, without using probability rules
    // it is assumed that this method is called when there is at least one unselected channel bucket in the playlist
    // as outer method checks for this before execution thread reaches this point here.
    private ChannelBucket GetRandomChannelBucketWithoutProbability(Playlist playlist)
    {
      // get all the channel buckets that have not been selected, into a collection.
      var unselectedChannelBuckets = from ChannelBucket cb in playlist.ChannelBuckets
                                     where !_consumedChannelBuckets.Contains(cb)
                                     select cb;

      return GetRandomElement<ChannelBucket>(unselectedChannelBuckets);
    }

    // picks a random content asset from channel bucket
    private ChannelAssetAssociation GetRandomContentPlaylistAsset(ChannelBucket cb)
    {
      // Randomly pick asset from full bucket list
      // check whether selected asset can be played according to temporal scheduling
      // if not pick again. This will be repeated 20 times.
      for (int i = 0; i < 20; i++)
      {
        ContentPlaylistAsset contentPlaylistAsset = GetRandomElement<ContentPlaylistAsset>(cb.ContentAssets);

        if (contentPlaylistAsset == null)
        {
          _logger.WriteMessage("no asset found in this channel bucket.");
          return null;
        }
        else
          _logger.WriteMessage("content asset: " + contentPlaylistAsset.AssetID + " selected randomly. Determining if it can be played at this time.");

        if (!_consumedContentPlaylistAssets.Contains(contentPlaylistAsset))
        {
          _logger.WriteMessage("content asset has not been attempted on this run.");

          if (contentPlaylistAsset.StartDateTime <= DateTime.Now
            && contentPlaylistAsset.EndDateTime >= DateTime.Now
            && (contentPlaylistAsset.PlayerType != PlayerType.WebSite || _bIncludeContentWebAssets && contentPlaylistAsset.PlayerType == PlayerType.WebSite))
          {
            if (contentPlaylistAsset.ScheduleInfo.Length == 0)
            {
              _logger.WriteMessage("content asset has no schedule info. Checking if it is available.");

              if (contentPlaylistAsset.PlayerType == PlayerType.WebSite)
              {
                _logger.WriteMessage("content asset is a website. Checking if it is available online.");

                if (AssetWebsiteExists(contentPlaylistAsset.AssetWebSite))
                {
                  _logger.WriteMessage("content asset " + contentPlaylistAsset.AssetWebSite + " is available online and will be selected.");
                  return new ChannelAssetAssociation(cb.ChannelID, contentPlaylistAsset);
                }
                else
                {
                  _logger.WriteMessage("content asset " + contentPlaylistAsset.AssetWebSite + " is not available online. Disabling selection of web content for this run.");
                  _bIncludeContentWebAssets = false;
                }
              }
              else if (contentPlaylistAsset.PlayerType != PlayerType.WebSite)
              {
               _logger.WriteMessage("content asset is a non-website. Checking if it is available on disk.");

                if (AssetFileExists(contentPlaylistAsset.GetAssetFilenameGUIDSuffix(), contentPlaylistAsset.AssetFilename))
                {
                  _logger.WriteMessage("content asset " + contentPlaylistAsset.AssetFilename + " exists on disk.");

                  if (!_bInsufficientMemoryForLargeFiles || IsNonLargeFile(contentPlaylistAsset))
                  {
                    _logger.WriteTimestampedMessage("Content asset " + contentPlaylistAsset.AssetFilename + " can be played.");
                    return new ChannelAssetAssociation(cb.ChannelID, contentPlaylistAsset);
                  }
                  else
                    _logger.WriteTimestampedMessage("There are physical memory restrictions and content asset " + contentPlaylistAsset.AssetFilename + " cannot be played");
                }
                else
                  _logger.WriteMessage("content asset " + contentPlaylistAsset.AssetFilename + " does not exist on disk.");
              }
              else
                return new ChannelAssetAssociation(cb.ChannelID, contentPlaylistAsset);
            }

            if (_assetScheduler.IsAssetPlayable(contentPlaylistAsset.ScheduleInfo))
            {
              _logger.WriteMessage("content asset has no schedule info. Checking if it is available.");

              if (contentPlaylistAsset.PlayerType == PlayerType.WebSite)
              {
                _logger.WriteMessage("content asset is a website. Checking if it is available online.");

                if (AssetWebsiteExists(contentPlaylistAsset.AssetWebSite))
                {
                  _logger.WriteMessage("content asset " + contentPlaylistAsset.AssetWebSite + " is available online and will be selected.");
                  return new ChannelAssetAssociation(cb.ChannelID, contentPlaylistAsset);
                }
                else
                {
                  _logger.WriteMessage("content asset " + contentPlaylistAsset.AssetWebSite + " is not available online. Disabling selection of web content for this run.");
                  _bIncludeContentWebAssets = false;
                }
              }
              else if (contentPlaylistAsset.PlayerType != PlayerType.WebSite)
              {
                _logger.WriteMessage("content asset is a non-website. Checking if it is available on disk.");

                if (AssetFileExists(contentPlaylistAsset.GetAssetFilenameGUIDSuffix(), contentPlaylistAsset.AssetFilename))
                {
                  _logger.WriteMessage("content asset " + contentPlaylistAsset.AssetFilename + " exists on disk.");

                  if (!_bInsufficientMemoryForLargeFiles || IsNonLargeFile(contentPlaylistAsset))
                  {
                    _logger.WriteTimestampedMessage("Content asset " + contentPlaylistAsset.AssetFilename + " can be played.");
                    return new ChannelAssetAssociation(cb.ChannelID, contentPlaylistAsset);
                  }
                  else
                    _logger.WriteTimestampedMessage("There are physical memory restrictions and content asset " + contentPlaylistAsset.AssetFilename + " cannot be played");
                }
                else
                  _logger.WriteMessage("content asset " + contentPlaylistAsset.AssetFilename + " does not exist on disk.");
              }
              else
                return new ChannelAssetAssociation(cb.ChannelID, contentPlaylistAsset);
            }
          }
        }

        _consumedContentPlaylistAssets.Add(contentPlaylistAsset);
      }
                
      // if nothing found go through Channel Bucket's entire list (excluding the consumed ones), 
      // checking what CAN be displayed and create a shortlist
      // then randomly pick one from the shortlist.
      ContentPlaylistAsset cpa = GetRandomElement<ContentPlaylistAsset>(GetPlayableContentPlaylistAssetsExcludingConsumed(cb));

      if (cpa == null)
        return null;

      return new ChannelAssetAssociation(cb.ChannelID, cpa);
    }

    // filter non consumed content assets
    private IEnumerable<ContentPlaylistAsset> GetPlayableContentPlaylistAssetsExcludingConsumed(ChannelBucket cb)
    {
      _logger.WriteMessage("shortlisting non attempted content assets that can be displayed and selecting one randomly.");

      // filter by whether content assets are consumed
      var channelBucketContentNonConsumedPlayable = from ContentPlaylistAsset contentPlaylistAsset in cb.ContentAssets
                                                        where !_consumedContentPlaylistAssets.Contains(contentPlaylistAsset)
                                                        select contentPlaylistAsset;

      _logger.WriteMessage("successfully selected the non attempted content.");

      // further filter by date bounds
      var channelBucketContentDateNonConsumedPlayable = from ContentPlaylistAsset contentPlaylistAsset in channelBucketContentNonConsumedPlayable
                                                        where contentPlaylistAsset.StartDateTime <= DateTime.Now
                                                        && contentPlaylistAsset.EndDateTime >= DateTime.Now                                                        
                                                        select contentPlaylistAsset;

      _logger.WriteMessage("successfully selected the non attempted content whose start and end date/time surround today/now.");

      // further filter assets that passed date bound filtering by content assets with no scheduling information
      var channelBucketContentDateNonConsumedNoScheduleInfoPlayable = from ContentPlaylistAsset contentPlaylistAsset in channelBucketContentDateNonConsumedPlayable
                                                                      where contentPlaylistAsset.ScheduleInfo.Length == 0
                                                                      select contentPlaylistAsset;

      _logger.WriteMessage("successfully selected the non attempted content with no scheduling info.");

      // further filter assets that passed date bound filtering by content assets with scheduling information that permits them to be played at this time
      var channelBucketContentDateNonConsumedWithScheduleInfoPlayable = from ContentPlaylistAsset contentPlaylistAsset in channelBucketContentDateNonConsumedPlayable
                                                                        where _assetScheduler.IsAssetPlayable(contentPlaylistAsset.ScheduleInfo)
                                                                        select contentPlaylistAsset;

      _logger.WriteMessage("successfully selected the non attempted content with permitted scheduling info.");

      // union of assets passed scheduling information and assets with no scheduling information
      var playableContentAssets = channelBucketContentDateNonConsumedNoScheduleInfoPlayable.Union(channelBucketContentDateNonConsumedWithScheduleInfoPlayable);

      _logger.WriteMessage("successfully combined playable content with permitted scheduling info and without scheduling info.");

      // of those assets found, which ones are available and can be played due to memory restrictions if any
      var playableContentAssetsExisting = from ContentPlaylistAsset contentPlaylistAsset in playableContentAssets
                                          where ((contentPlaylistAsset.PlayerType == PlayerType.WebSite && AssetWebsiteExists(contentPlaylistAsset.AssetWebSite))
                                                  || contentPlaylistAsset.PlayerType != PlayerType.WebSite && AssetFileExists(contentPlaylistAsset.GetAssetFilenameGUIDSuffix(), contentPlaylistAsset.AssetFilename) &&
                                                  (!_bInsufficientMemoryForLargeFiles || IsNonLargeFile(contentPlaylistAsset)))
                                          select contentPlaylistAsset;

      _logger.WriteMessage("successfully selected content that is available locally or online.");

      return playableContentAssetsExisting;
    }

    private ChannelAssetAssociation PickRandomAdvertPlaylistAsset()
    {
      // Does playlist have assets?
      // Unlike with the channel buckets, must check this inside the method as it is publicly accessible
      if (_noPlaylistAdverts == 0)
      {
        _logger.WriteMessage("No Adverts exist on playlist. Returning null.");
        return null;
      }

      bool bIncludeAdvertWebAssets = true;

      // Pick an advert - try 20 times
      for (int i = 0; i < 20; i++)
      {
        // get double from random
        double randomDouble = GetRandomDouble();

        foreach (AdvertPlaylistAsset advertPlaylistAsset in _playlist.AdvertBucket.AdvertAssets)
        {
          _logger.WriteMessage("Attempting to select advert: " + advertPlaylistAsset.AssetID);

          if (randomDouble < advertPlaylistAsset.HigherThresholdNormalised
            && randomDouble >= advertPlaylistAsset.LowerThresholdNormalised
            && advertPlaylistAsset.StartDateTime <= DateTime.Now
            && advertPlaylistAsset.EndDateTime >= DateTime.Now
            && _assetScheduler.IsAssetPlayable(advertPlaylistAsset.ScheduleInfo)            
            && ((advertPlaylistAsset.PlayerType != PlayerType.WebSite || bIncludeAdvertWebAssets && advertPlaylistAsset.PlayerType == PlayerType.WebSite)))
          {
            if (advertPlaylistAsset.PlayerType == PlayerType.WebSite)
            {
              _logger.WriteMessage("Advert is a website. Checking if it is available online.");
              if (AssetWebsiteExists(advertPlaylistAsset.AssetWebSite))
              {
                _logger.WriteMessage("Advert " + advertPlaylistAsset.AssetWebSite + " is available online and will be selected.");
                return new ChannelAssetAssociation(0, advertPlaylistAsset);
              }
              else
              {
                bIncludeAdvertWebAssets = false;
                _logger.WriteMessage("Advert " + advertPlaylistAsset.AssetWebSite + " is not available online. Disabling selection of web adverts for this run.");
              }
            }
            else if (advertPlaylistAsset.PlayerType != PlayerType.WebSite)
            {
              _logger.WriteMessage("Advert is a non-website. Checking if it is available on disk.");

              if (AssetFileExists(advertPlaylistAsset.GetAssetFilenameGUIDSuffix(), advertPlaylistAsset.AssetFilename))
              {
                _logger.WriteMessage("Advert " + advertPlaylistAsset.AssetFilename + " exists on disk. Checking if it can be picked due to physical memory restrictions.");

                if (!_bInsufficientMemoryForLargeFiles || IsNonLargeFile(advertPlaylistAsset))
                {
                  _logger.WriteTimestampedMessage("Advert " + advertPlaylistAsset.AssetFilename + " can be played.");
                  return new ChannelAssetAssociation(0, advertPlaylistAsset);
                }
                else
                  _logger.WriteMessage("Advert " + advertPlaylistAsset.AssetFilename + " cannot be played on this PC due to physical memory restrictions.");
              }
              else
                _logger.WriteMessage("Advert " + advertPlaylistAsset.AssetFilename + " does not exist on disk.");
            }
            else
              return new ChannelAssetAssociation(0, advertPlaylistAsset);
          }
          else
          {
            _logger.WriteMessage("advert " + advertPlaylistAsset.AssetID + " does not fall within the scheduling or random criteria.\r\n" +
              "Random double: " + randomDouble + "\r\n" +
              "advertPlaylistAsset.HigherThresholdNormalised: " + advertPlaylistAsset.HigherThresholdNormalised + "\r\n" +
              "advertPlaylistAsset.LowerThresholdNormalised: " + advertPlaylistAsset.LowerThresholdNormalised + "\r\n" +
              "advertPlaylistAsset.StartDateTime: " + advertPlaylistAsset.StartDateTime + "\r\n" +
              "advertPlaylistAsset.EndDateTime: " + advertPlaylistAsset.EndDateTime + "\r\n" +
              "advertPlaylistAsset is playable: " + _assetScheduler.IsAssetPlayable(advertPlaylistAsset.ScheduleInfo) + "\r\n");
          }
        }
      }

      return null; // no asset found or has temporal scheduling that permits its impression at this time
    }

    private bool IsNonLargeFile(PlaylistAsset playlistAsset)
    {
      string filePath = _assetFilePath + playlistAsset.GetAssetFilenameGUIDSuffix() + "\\" + playlistAsset.AssetFilename;

      if (!File.Exists(filePath))
        return false;

      FileInfo fi = new FileInfo(filePath);

      _logger.WriteTimestampedMessage("Length of file " + playlistAsset.AssetFilename + " is " + fi.Length + ". Is it small enough? " + (fi.Length < 62914560));

      return fi.Length < 62914560;
    }

    // checks if file exists and check that it decrypts
    private bool AssetFileExists(string assetFolder, string assetFileName)
    {
      string filePath = _assetFilePath + "\\" + assetFolder + "\\" + assetFileName;

     _logger.WriteMessage("FilePath: " + filePath);

      bool bFileExists = File.Exists(filePath);

      bool bFileIsDecryptable = bFileExists && EncryptionDecryption.EncryptionDecryptionHelper.TryIfFileDecryptable(filePath, _password);

      _logger.WriteMessage("File " + assetFileName + " exists: " + bFileExists);

      if (bFileExists)
        _logger.WriteMessage("File " + assetFileName + " is decryptable: " + bFileIsDecryptable);

      return (bFileExists && bFileIsDecryptable);
    }

    private bool AssetWebsiteExists(string targetURL)
    {
      return InternetConnectionCheck.WebChecker.IsWebSiteAvailable(targetURL, _requestTimeout);
    }

    // picks a random element from an IEnumerable type
    public T GetRandomElement<T>(IEnumerable<T> iEnumerable)
    {
      int noElements = iEnumerable.Count();
      
      if (noElements == 0)
        return default(T);

      // get double from random
      int randomInt = GetRandomInt(noElements);

      int counter = 0;

      foreach (T element in iEnumerable)
      {
        if (counter == randomInt)
          return element;

        counter++;
      }
            
      return default(T);
    }

    private double GetRandomDouble()
    {
      _random.GetBytes(_randomNumbers);

      return (double)BitConverter.ToUInt64(_randomNumbers, 0) / ulong.MaxValue;
    }

    private int GetRandomInt(int maxValue)
    {
      _random.GetBytes(_randomNumbers);

      uint u = (uint)BitConverter.ToUInt32(_randomNumbers, 0) % (uint)maxValue;

      return (int)u;
    }
  }
}
