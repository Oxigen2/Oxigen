using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.LogStats
{
  /// <summary>
  /// a single object holding the calculated operation (show or click) weighting and the matching channel ID
  /// </summary>
  public class ChannelAdvertOperationStat
  {
    private long _channelID;
    private float _advertOperationProportion;

    /// <summary>
    /// the unique database ID of the channel
    /// </summary>
    public long ChannelID
    {
      get { return _channelID; }
      set { _channelID = value; }
    }

    /// <summary>
    /// the weighted operation (show or click) that corresponds to the channel
    /// </summary>
    public float AdvertOperationProportion
    {
      get { return _advertOperationProportion; }
      set { _advertOperationProportion = value; }
    }

    /// <summary>
    /// a single object holding the calculated operation (show or click) weighting and the matching channel ID
    /// </summary>
    public ChannelAdvertOperationStat()
    {
    }

    /// <summary>
    /// a single object holding the calculated operation (show or click) weighting and the matching channel ID
    /// </summary>
    /// <param name="channelID">the unique database ID of the channel</param>
    /// <param name="advertOperationProportion">the weighted operation (show or click) that corresponds to the channel</param>
    public ChannelAdvertOperationStat(long channelID, float advertOperationProportion)
    {
      _channelID = channelID;
      _advertOperationProportion = advertOperationProportion;
    }
  }
}
