using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.Singletons;
using OxigenIIAdvertising.AppData;

namespace OxigenIIAdvertising.ScreenSaver
{
  /// <summary>
  /// Object to access the logging singleton
  /// </summary>
  public class LogSingletonAccessor
  {
    private DateTime _assetImpressionStartDateTime = new DateTime();

    /// <summary>
    /// The start date and time of the playlist asset as set at the ScreenSaver objects
    /// </summary>
    public DateTime AssetImpressionStartDateTime
    {
      get { return _assetImpressionStartDateTime; }
      set { _assetImpressionStartDateTime = value; }
    }
    
    /// <summary>
    /// Adds an impression log entry for the last played asset. Calculates the time at invocation then subtracts to find the duration
    /// </summary>
    /// <param name="channelAssetAssociation">the pair of channelID - asset for which to keep an impression log</param>
    public void AddImpressionLog(ChannelAssetAssociation channelAssetAssociation)
    {
      // if this is a content asset check if it's premium content (error/no assets asset is premium content)
      if (channelAssetAssociation.PlaylistAsset is ContentPlaylistAsset && ((ContentPlaylistAsset)channelAssetAssociation.PlaylistAsset).AssetLevel != PlaylistAssetLevel.Premium)
        return;

      // playlist asset can be null if screensaver has just started and no asset
      // had had the time to load yet.
      if (channelAssetAssociation == null)
        return;

      // declare the string builder as local to make it thread safe
      StringBuilder logSb = new StringBuilder();

      logSb.Append(channelAssetAssociation.ChannelID);
      logSb.Append("|");
      logSb.Append(channelAssetAssociation.PlaylistAsset.AssetID);
      logSb.Append("|");
      logSb.Append(MakeDateToString(_assetImpressionStartDateTime));
      logSb.Append("|");
      logSb.Append(_assetImpressionStartDateTime.ToLongTimeString());
      logSb.Append("|");
      logSb.Append(GetDurationInSeconds());

      if (channelAssetAssociation.PlaylistAsset is AdvertPlaylistAsset)
        LogEntriesRawSingleton.Instance.AdvertImpressionLogEntries.Add(logSb.ToString());
      else // premium content or error/no assets asset
        LogEntriesRawSingleton.Instance.ContentImpressionLogEntries.Add(logSb.ToString());
    }

    /// <summary>
    /// Adds click log to log file
    /// </summary>
    /// <param name="ChannelAssetAssociation">the pair of channelID - asset for which to keep an click log</param>
    public void AddClickLog(ChannelAssetAssociation channelAssetAssociation)
    {
      // playlist asset can be null if screensaver has just started and no asset
      // had had the time to load yet.
      if (channelAssetAssociation == null)
        return;

      // declare the string builder as local to make it thread safe
      StringBuilder logSb = new StringBuilder();

      DateTime now = DateTime.Now;

      logSb.Append(channelAssetAssociation.ChannelID);
      logSb.Append("|");
      logSb.Append(channelAssetAssociation.PlaylistAsset.AssetID);
      logSb.Append("|");
      logSb.Append(MakeDateToString(now));
      logSb.Append("|");
      logSb.Append(now.ToLongTimeString());

      if (channelAssetAssociation.PlaylistAsset is AdvertPlaylistAsset)
        LogEntriesRawSingleton.Instance.AdvertClickLogEntries.Add(logSb.ToString());
      else // content or "no assets"
        LogEntriesRawSingleton.Instance.ContentClickLogEntries.Add(logSb.ToString());
    }

    private string MakeDateToString(DateTime dateTime)
    {
      string year = dateTime.Year.ToString();
      string month = dateTime.Month.ToString();
      string day = dateTime.Day.ToString();

      if (month.Length == 1)
        month = "0" + month;

      if (day.Length == 1)
        day = "0" + day;

      return year + month + day;
    }

    private string GetDurationInSeconds()
    {
      DateTime now = DateTime.Now;

      TimeSpan duration = now.Subtract(_assetImpressionStartDateTime);

      return (Math.Round(duration.TotalSeconds, 0)).ToString();
    }
  }
}
