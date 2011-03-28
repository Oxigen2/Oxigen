using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;

namespace InterCommunicationStructures
{
  /// <summary>
  /// Web method parameter containing information and the binary of an asset file to be transferred (pushed)
  /// from the master servers to the relay servers
  /// </summary>
  [MessageContract]
  public class AssetFileStreamParameterMessage : StreamMasterParameterMessage
  {
    private long _channelID;
    private AssetType _assetType;

    /// <summary>
    /// The unique database ID for the channel
    /// </summary>
    [MessageHeader]
    public long ChannelID
    {
      get { return _channelID; }
      set { _channelID = value; }
    }

    /// <summary>
    /// Content or Advert
    /// </summary>
    [MessageHeader]
    public AssetType AssetType
    {
      get { return _assetType; }
      set { _assetType = value; }
    }
  }
}
