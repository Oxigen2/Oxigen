using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.LogStats;

namespace OxigenIIAdvertising.LogExchanger
{
  public static class LogFlattener
  {
    /// <summary>
    /// Flattens a List of Advert click/impression 
    /// </summary>
    /// <param name="advertChannelOperationProportionsSet">A List of AdvertChannelOperationProportions (advert distribution of clicks or logs across channels)</param>
    /// <returns>a string with comma delimited lines of the click/impression stats</returns>
    public static string FlattenAdvertChannelOperationProportions(List<AdvertChannelOperationProportions> advertChannelOperationProportionsSet)
    {
      if (advertChannelOperationProportionsSet.Count == 0)
        return "";

      StringBuilder deflatedStats = new StringBuilder();

      foreach (AdvertChannelOperationProportions advertChannelOperationProportions in advertChannelOperationProportionsSet)
      {
        foreach (ChannelAdvertOperationStat channelAdvertOperationStat in advertChannelOperationProportions.ChannelAdvertOperationStats)
        {
          deflatedStats.Append(advertChannelOperationProportions.AdvertAssetID);
          deflatedStats.Append("|");
          deflatedStats.Append(channelAdvertOperationStat.ChannelID);
          deflatedStats.Append("|");
          deflatedStats.Append(channelAdvertOperationStat.AdvertOperationProportion);
          deflatedStats.AppendLine();
        }
      }

      return deflatedStats.ToString();
    }

    /// <summary>
    /// Flattens a List of click/impression times per day per asset
    /// </summary>
    /// <param name="dateOperationsPerAssetSet">List of click/impression times per day per asset</param>
    /// <returns>a string with comma delimited lines of the click/impression stats</returns>
    public static string FlattenOperationsPerDatePerAsset(List<DateOperationsPerAsset> dateOperationsPerAssetSet)
    {
      if (dateOperationsPerAssetSet.Count == 0)
        return "";

      StringBuilder deflatedStats = new StringBuilder();

      foreach (DateOperationsPerAsset dateOperationsPerAsset in dateOperationsPerAssetSet)
      {
        foreach (OperationsPerDate operationsPerDate in dateOperationsPerAsset.OperationsPerDateTime)
        {
          deflatedStats.Append(dateOperationsPerAsset.OperationDateTime.ToString());
          deflatedStats.Append("|");
          deflatedStats.Append(operationsPerDate.AssetID);
          deflatedStats.Append("|");
          deflatedStats.Append(operationsPerDate.NoOperations);
          deflatedStats.AppendLine();
        }
      }

      return deflatedStats.ToString();
    }
  }
}
