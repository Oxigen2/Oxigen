using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.LoggingStructures;
using OxigenIIAdvertising.AppData;

namespace OxigenIIAdvertising.LogStats
{
  /// <summary>
  /// Provides static method for log calculations
  /// </summary>
  public static class LogStatsCalculator
  {
    /// <summary>
    /// Calculates the Advert show proportions per channel according to channel weighting
    /// </summary>
    /// <param name="advertImpressionLog">advert show log</param>
    /// <param name="channelSubscriptions">user channel subscriptions with normalized channel weightings</param>
    /// <returns>a List with the proportions</returns>
    public static List<AdvertChannelOperationProportions> GetAdvertChannelImpressionProportions(List<ImpressionLogEntry> advertImpressionLog,
      ChannelSubscriptions channelSubscriptions)
    {
      var advertTotalPlayTimes = from ImpressionLogEntry impressionLogEntry in advertImpressionLog
                                 group impressionLogEntry by impressionLogEntry.AssetID into groupedAdvertsByID
                                 select new
                                 {
                                   AdvertID = groupedAdvertsByID.Key,
                                   AdvertTotalPlayTime = groupedAdvertsByID.Sum(impressionLogEntry => impressionLogEntry.Duration)
                                 };

      var advertChannelPlayProportions = from advert in advertTotalPlayTimes
                                         select new AdvertChannelOperationProportions
                                         {
                                           AdvertAssetID = advert.AdvertID,
                                           ChannelAdvertOperationStats =(from ChannelSubscription channelSubscription in channelSubscriptions.SubscriptionSet
                                                                         select new ChannelAdvertOperationStat
                                                                         {
                                                                           ChannelID = channelSubscription.ChannelID,
                                                                           AdvertOperationProportion = (float)advert.AdvertTotalPlayTime * channelSubscription.ChannelWeightingNormalised
                                                                         }).ToList<ChannelAdvertOperationStat>()
                                         };

      return advertChannelPlayProportions.ToList<AdvertChannelOperationProportions>();
    }

    /// <summary>
    /// Calculates the Advert click proportions per channel according to channel weighting
    /// </summary>
    /// <param name="advertClickLog">advert click log</param>
    /// <param name="channelSubscriptions">user channel subscriptions with normalized channel weightings</param>
    /// <returns>a List with the proportions</returns>
    public static List<AdvertChannelOperationProportions> GetAdvertChannelClickProportions(List<ClickLogEntry> advertClickLog,
      ChannelSubscriptions channelSubscriptions)
    {
      var advertTotalClicks = from ClickLogEntry clickLogEntry in advertClickLog
                              group clickLogEntry by clickLogEntry.AssetID into groupedAdvertsByID
                              select new
                              {
                                AdvertID = groupedAdvertsByID.Key,
                                AdvertTotalClicks = groupedAdvertsByID.Count()
                              };

      var advertChannelClickProportions = from advert in advertTotalClicks
                                          select new AdvertChannelOperationProportions
                                          {
                                            AdvertAssetID = advert.AdvertID,
                                            ChannelAdvertOperationStats = (from ChannelSubscription channelSubscription in channelSubscriptions.SubscriptionSet
                                                                           select new ChannelAdvertOperationStat
                                                                           {
                                                                             ChannelID = channelSubscription.ChannelID,
                                                                             AdvertOperationProportion = (float)advert.AdvertTotalClicks * channelSubscription.ChannelWeightingNormalised
                                                                           }).ToList<ChannelAdvertOperationStat>()
                                          };

      return advertChannelClickProportions.ToList<AdvertChannelOperationProportions>();
    }

    /// <summary>
    /// Gets the number of Clicks per asset per day in the following structure
    /// List of Dates (DateTime with only Date information) containing a List of Assets and the cumulative number of clicks or impressions for that day.
    /// Uses LINQ
    /// </summary>
    /// <typeparam name="T">derivative of LogEntry</typeparam>
    /// <param name="logEntries">the Hashset Containing the LogEntries to aggregate</param>
    /// <returns>An IEnumerable product of the aggregation LINQ Query</returns>
    public static List<DateOperationsPerAsset> AggregateClicksPerDay(List<ClickLogEntry> clickLogEntries, long channelID)
    {
      var query = from ClickLogEntry clickLogEntry in clickLogEntries
                  where clickLogEntry.ChannelID == channelID // 0 for adverts
                  group clickLogEntry by clickLogEntry.ClickDateTime.Date into logByDay
                  select new DateOperationsPerAsset
                  {
                    OperationDateTime = logByDay.Key,
                    OperationsPerDateTime = (from logEntryByDayAsset in logByDay
                                             group logEntryByDayAsset by logEntryByDayAsset.AssetID into logByAssetByDay
                                             select new OperationsPerDate
                                             {
                                               AssetID = logByAssetByDay.Key,                                               
                                               NoOperations = logByAssetByDay.Count()
                                             }).ToList<OperationsPerDate>()
                  };

      return query.ToList<DateOperationsPerAsset>();
    }

    /// <summary>
    /// Gets the number of Clicks per asset per day in the following structure
    /// List of Dates (DateTime with only Date information) containing a List of Assets and the cumulative number of clicks or impressions for that day.
    /// Uses LINQ
    /// </summary>
    /// <typeparam name="T">derivative of LogEntry</typeparam>
    /// <param name="logEntries">the Hashset Containing the LogEntries to aggregate</param>
    /// <param name="assetType">Content or Advert</param>
    /// <returns>An IEnumerable product of the aggregation LINQ Query</returns>
    public static List<DateOperationsPerAsset> AggregateImpressionsPerDay(List<ImpressionLogEntry> impressionLogEntries, long channelID)
    {
      var query = from ImpressionLogEntry impressionLogEntry in impressionLogEntries
                  where impressionLogEntry.ChannelID == channelID
                  group impressionLogEntry by impressionLogEntry.StartDateTime.Date into logByDay
                  select new DateOperationsPerAsset
                  {
                    OperationDateTime = logByDay.Key,
                    OperationsPerDateTime = (from logEntryByDayAsset in logByDay
                                             group logEntryByDayAsset by logEntryByDayAsset.AssetID into logByAssetByDay
                                             select new OperationsPerDate
                                             {
                                               AssetID = logByAssetByDay.Key,
                                               NoOperations = logByAssetByDay.Count()
                                             }).ToList<OperationsPerDate>()
                  };

      return query.ToList<DateOperationsPerAsset>();
    }
  }
}
