using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.SOAStructures
{
  /// <summary>
  /// Represents the properties of a raw asset content
  /// </summary>
  [DataContract]
  public class AssetContentProperties
  {
    private int _assetContentID;
    private string _name;
    private string _creator;
    private string _caption;
    private DateTime? _userGivenDate;
    private string _url;
    private float _displayDuration;

    /// <summary>
    /// The content's ID
    /// </summary>
    [DataMember]
    public int AssetContentID
    {
      get { return _assetContentID; }
      set { _assetContentID = value; }
    }

    /// <summary>
    /// The content's displayed name
    /// </summary>
    [DataMember]
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    /// <summary>
    /// Raw asset's caption
    /// </summary>
    [DataMember]
    public string Caption
    {
      get { return _caption; }
      set { _caption = value; }
    }

    /// <summary>
    /// Raw asset's creator's name
    /// </summary>
    [DataMember]
    public string Creator
    {
      get { return _creator; }
      set { _creator = value; }
    }

    /// <summary>
    /// User's given date
    /// </summary>
    [DataMember]
    public DateTime? UserGivenDate
    {
      get { return _userGivenDate; }
      set { _userGivenDate = value; }
    }

    /// <summary>
    /// URL associated with the raw content
    /// </summary>
    [DataMember]
    public string URL
    {
      get { return _url; }
      set { _url = value; }
    }

    /// <summary>
    /// Content's display duration
    /// </summary>
    [DataMember]
    public float DisplayDuration
    {
      get { return _displayDuration; }
      set { _displayDuration = value; }
    }

    public AssetContentProperties() { }

    public AssetContentProperties(string name, string creator, string caption, DateTime userGivenDate, string url, float displayDuration)
    {
      _name = name;
      _creator = creator;
      _caption = caption;
      _userGivenDate = userGivenDate;
      _url = url;
      _displayDuration = displayDuration;
    }
  }
}
