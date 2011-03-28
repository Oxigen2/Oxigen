using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;

namespace InterCommunicationStructures
{
  /// <summary>
  /// Web method parameter for log data processing between the client and the user data marshaller
  /// </summary>
  [MessageContract]
  public class LogDataParameterMessage : UserParameterMessage
  {
    private string _fileName;
    private string _channelID;
    private Stream _logFileStream;

    /// <summary>
    /// Gets or sets the file name to read or write
    /// </summary>
    [MessageHeader]
    public string FileName
    {
      get { return _fileName; }
      set { _fileName = value; }
    }

    /// <summary>
    /// Gets or sets the channel ID for the logs to upload
    /// </summary>
    [MessageHeader(Name="Channel_ID")]
    public string ChannelID
    {
      get { return _channelID; }
      set { _channelID = value; }
    }

    /// <summary>
    /// Gets or sets the stream containing a log file.
    /// </summary>
    [MessageBodyMember]
    public Stream LogFileStream
    {
      get { return _logFileStream; }
      set { _logFileStream = value; }
    }
  }
}
