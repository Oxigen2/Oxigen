using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace InterCommunicationStructures
{
  [MessageContract]
  public class VersionParameterMessage
  {
    private string _systemPassPhrase;
    private string _version;

    [MessageHeader]
    public string SystemPassPhrase
    {
      get { return _systemPassPhrase; }
      set { _systemPassPhrase = value; }
    }

    [MessageHeader]
    public string Version
    {
      get { return _version; }
      set { _version = value; }
    }
  }
}
