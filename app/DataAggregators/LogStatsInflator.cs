using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OxigenIIAdvertising.FlatAggregatedLogStructures;

namespace OxigenIIAdvertising.DataAggregators
{
  public static class LogStatsInflator
  {
    public static List<DateOperationsPerAsset> InflateDateOperationsPerAsset(string logFilePath, string channelID)
    {
      string line = "";

      List<DateOperationsPerAsset> dateOperations = new List<DateOperationsPerAsset>();
            
      StreamReader streamReader = new StreamReader(logFilePath);

      while ((line = streamReader.ReadLine()) != null)
      {
        string[] lineElements = line.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

        DateOperationsPerAsset aggregateOperationsPerAsset = new DateOperationsPerAsset
        {
          OperationDate = DateTime.Parse(lineElements[0]),
          AssetType = channelID == "0" ? AssetType.Advert : AssetType.Content,
          AssetID = long.Parse(lineElements[1]),
          NoOperations = int.Parse(lineElements[2])
        };

        dateOperations.Add(aggregateOperationsPerAsset);
      }

      streamReader.Close();

      return dateOperations;
    }

    public static List<AdvertChannelOperationProportions> InflateAdvertChannelOperationProportions(string logFilePath)
    {
      string line = "";

      List<AdvertChannelOperationProportions> advertChannelOperations = new List<AdvertChannelOperationProportions>();

      StreamReader streamReader = new StreamReader(logFilePath);

      while ((line = streamReader.ReadLine()) != null)
      {
        string[] lineElements = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

        AdvertChannelOperationProportions advertChannelOperationProportions = new AdvertChannelOperationProportions
        {
          AdvertAssetID = long.Parse(lineElements[0]),
          ChannelID = long.Parse(lineElements[1]),
          AdvertOperationProportion = float.Parse(lineElements[2])
        };

        advertChannelOperations.Add(advertChannelOperationProportions);
      }

      streamReader.Close();

      return advertChannelOperations;
    }
  }
}
