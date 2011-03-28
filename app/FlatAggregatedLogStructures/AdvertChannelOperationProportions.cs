using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.FlatAggregatedLogStructures
{
  /// <summary>
  /// Represents an aggregated click/log per advert asset per channel.
  /// In a single file, the pair of AdvertAssetID and ChannelID should be unique
  /// </summary>
  public struct AdvertChannelOperationProportions
  {
    private long _advertAssetID;
    private long _channelID;
    private float _advertOperationProportion;

    /// <summary>
    /// The unique ID of the advert asset for which the proportions are calculated
    /// </summary>
    public long AdvertAssetID
    {
      get { return _advertAssetID; }
      set { _advertAssetID = value; }
    }

    /// <summary>
    /// The Channel ID for this advert asset for which the proportions are calculated
    /// </summary>
    public long ChannelID
    {
      get { return _channelID; }
      set { _channelID = value; }
    }

    /// <summary>
    /// Product of total play time or total number of clicks for the channel with ChannelID times the Channel's weighting
    /// </summary>
    public float AdvertOperationProportion
    {
      get { return _advertOperationProportion; }
      set { _advertOperationProportion = value; }
    }
  }
}
