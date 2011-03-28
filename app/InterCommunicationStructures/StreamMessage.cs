using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;

namespace InterCommunicationStructures
{
  /// <summary>
  /// Simple stream message to be used as a message contract
  /// </summary>
  [MessageContract]
  public class StreamMessage
  {
    private Stream _stream;

    [MessageHeader]
    public Stream Stream
    {
      get { return _stream; }
      set { _stream = value; }
    }

    public StreamMessage(Stream stream)
    {
      _stream = stream;
    }
  }
}
