using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using InterCommunicationStructures;

namespace InterCommunicationStructures
{
  /// <summary>
  /// Web method holding information for an application data file to be requested from the relay servers by a client.
  /// </summary>
  [MessageContract]
  public class AppDataFileParameterMessage : UserParameterMessage
  {
    private DataFileType _dataFileType;
    private string _checksum;
    private long _channelID;

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

    [MessageHeader]
    public string Checksum
    {
      get
      {
        return _checksum;
      }

      set
      {
        _checksum = value;
      }
    }

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
