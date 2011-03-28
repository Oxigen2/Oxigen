using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.DataContracts.UserFileMarshaller
{
  /// <summary>
  /// Object to hold version and download information for a single software component
  /// </summary>
  [DataContract]
  public class ComponentInfo
  {
    private ComponentType _componentType;
    private string _componentVersion;
    private HashSet<string> _otherURLs;
    private string _componentURL;

    /// <summary>
    /// Instantiating this class also instantiates a list with other possible URLs for download
    /// </summary>
    public ComponentInfo()
    {
      _otherURLs = new HashSet<string>();
    }

    /// <summary>
    /// Type that uniquely identifies the component (application's name)
    /// </summary>
    [DataMember]
    public ComponentType ComponentType
    {
      get { return _componentType; }
      set { _componentType = value; }
    }

    /// <summary>
    /// Version of the component
    /// </summary>
    [DataMember]
    public string ComponentVersion
    {
      get { return _componentVersion; }
      set { _componentVersion = value; }
    }

    /// <summary>
    /// Primary download URL of the component
    /// </summary>
    [DataMember]
    public string ComponentURL
    {
      get { return _componentURL; }
      set { _componentURL = value; }
    }

    /// <summary>
    /// A HashSet of other URLs to download the component
    /// </summary>
    [DataMember]
    public HashSet<string> OtherURLs
    {
      get { return _otherURLs; }
      set { _otherURLs = value; }
    }
  }

}
