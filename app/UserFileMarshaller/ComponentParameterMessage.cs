using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace InterCommunicationStructures
{
  [MessageContract]
  public class ComponentParameterMessage
  {
    private string _systemPassPhrase;
    private string _versionNumber;
    private string _componentFileName;

    /// <summary>
    /// Gets or sets the file name to read or write
    /// </summary>
    [MessageHeader]
    public string SystemPassPhrase
    {
      get { return _systemPassPhrase; }
      set { _systemPassPhrase = value; }
    }

    /// <summary>
    /// Component's Major Version Number
    /// </summary>
    [MessageHeader]
    public string VersionNumber
    {
      get { return _versionNumber; }
      set { _versionNumber = value; }
    }

    /// <summary>
    /// Component's File Name
    /// </summary>
    [MessageHeader]
    public string ComponentFileName
    {
      get { return _componentFileName; }
      set { _componentFileName = value; }
    }
  }
}