using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.LogStats
{
  /// <summary>
  /// a single object holding a list of calculated operation (show or click) weightings for an advert asset
  /// </summary>
  public class AdvertChannelOperationProportions
  {
    private long _advertAssetID;
    private List<ChannelAdvertOperationStat> _channelAdvertOperationStats;

    /// <summary>
    /// the unique database ID of the advert
    /// </summary>
    public long AdvertAssetID
    {
      get { return _advertAssetID; }
      set { _advertAssetID = value; }
    }

    /// <summary>
    /// a collection of the user subscribed channels and their corresponding weightings for that advert
    /// </summary>
    public List<ChannelAdvertOperationStat> ChannelAdvertOperationStats
    {
      get { return _channelAdvertOperationStats; }
      set { _channelAdvertOperationStats = value; }
    }

    /// <summary>
    /// a collection of the user subscribed channels and their corresponding weightings for that advert
    /// </summary>
    public AdvertChannelOperationProportions() 
    {
      _channelAdvertOperationStats = new List<ChannelAdvertOperationStat>();
    }

    /// <summary>
    /// a single object holding a list of calculated operation (show or click) weightings for an advert asset
    /// </summary>
    /// <param name="advertAssetID">the unique database ID of the advert</param>
    /// <param name="channelAdvertOperationStats">a collection of the user subscribed channels and their corresponding weightings for that advert</param>
    public AdvertChannelOperationProportions(long advertAssetID, List<ChannelAdvertOperationStat> channelAdvertOperationStats)
    {
      _advertAssetID = advertAssetID;
      _channelAdvertOperationStats = channelAdvertOperationStats;
    }
  }
}
