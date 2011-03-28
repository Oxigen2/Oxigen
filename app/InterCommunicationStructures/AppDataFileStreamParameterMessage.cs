using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;

namespace InterCommunicationStructures
{
  /// <summary>
  /// Web method holding information and a stream of an application data file to be transferred (pushed) 
  /// from the master servers to the relay servers.
  /// </summary>
  [MessageContract]
  public class AppDataFileStreamParameterMessage : StreamMasterParameterMessage
  {
    private DataFileType _dataFileType;
    private long _channelID;

    /// <summary>
    /// Type of data to process
    /// </summary>
    [MessageHeader]
    public DataFileType DataFileType
    {
      get
      {
        return _dataFileType;
      }

      set
      {
        _dataFileType = value;
      }
    }
        
    /// <summary>
    /// ChannelID, if a channel data file is to be transferred
    /// </summary>
    [MessageHeader]
    public long ChannelID
    {
      get
      {
        return _channelID;
      }

      set
      {
        _channelID = value;
      }
    }
  }
}
