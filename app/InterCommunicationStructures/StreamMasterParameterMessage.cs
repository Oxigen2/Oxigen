using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;

namespace InterCommunicationStructures
{
  /// <summary>
  /// Web method parameter containing information and the binary of an file to be transferred (pushed)
  /// with its checksum from the master servers to the relay servers
  /// </summary>
  [MessageContract]
  public class StreamMasterParameterMessage : MasterParameterMessage
  {
    protected string _filename;
    protected string _checksum;
    protected Stream _stream;

    /// <summary>
    /// The file name
    /// </summary>
    [MessageHeader]
    public string Filename
    {
      get { return _filename; }
      set { _filename = value; }
    }

    /// <summary>
    /// The file's checksum calculated at the sender's end
    /// </summary>
    [MessageHeader]
    public string Checksum
    {
      get { return _checksum; }
      set { _checksum = value; }
    }

    /// <summary>
    /// The stream calculated at the sender's end
    /// </summary>
    [MessageBodyMember]
    public Stream Stream
    {
      get { return _stream; }
      set { _stream = value; }
    }
  }
}
