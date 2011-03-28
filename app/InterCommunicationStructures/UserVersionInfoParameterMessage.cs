using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;

namespace InterCommunicationStructures
{
  /// <summary>
  /// Web method parameter that holds user software version information, to be transferred to the relay servers
  /// from the client machine
  /// </summary>
  [MessageContract]
  public class UserVersionInfoParameterMessage : UserParameterMessage
  {
    private Stream _componentInfoStream;

    /// <summary>
    /// Stream to hold the serialized ComponentInfo Array or aggregated Component Info Information
    /// </summary>
    [MessageBodyMember]
    public Stream ComponentInfoStream
    {
      get { return _componentInfoStream; }
      set { _componentInfoStream = value; }
    }
  }
}
