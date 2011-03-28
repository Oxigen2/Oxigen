using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace InterCommunicationStructures
{
  /// <summary>
  /// The minimum amount of information to be used as a web method parameter for methods that use message contracts between the relay and master servers.
  /// </summary>
  [MessageContract]
  public class MasterParameterMessage
  {
    protected string _systemPassPhrase;

    /// <summary>
    /// Pass phrase that identifies the consumer to the web service
    /// </summary>
    [MessageHeader]
    public string SystemPassPhrase
    {
      get { return _systemPassPhrase; }
      set { _systemPassPhrase = value; }
    }
  }
}
