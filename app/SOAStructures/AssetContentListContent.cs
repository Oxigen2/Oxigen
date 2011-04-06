using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using System.Data.SqlClient;

namespace OxigenIIAdvertising.SOAStructures
{
  /// <summary>
  /// Represents a user content
  /// </summary>
  [Serializable]
  [DataContract]
  public class AssetContentListContent
  {
    private int _assetContentID;
    private string _name;
    private string _imagePath;

    /// <summary>
    /// The unique database ID of the Content
    /// </summary>
    [DataMember]
    public int AssetContentID
    {
      get { return _assetContentID; }
      set { _assetContentID = value; }
    }

    /// <summary>
    /// Content Title
    /// </summary>
    [DataMember]
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    /// <summary>
    /// Content thumbnail's path
    /// </summary>
    [DataMember]
    public string ImagePath
    {
      get { return _imagePath; }
      set { _imagePath = value; }
    }

    public AssetContentListContent() { }

    public AssetContentListContent(int contentID, string name, string imagePath)
    {
      _assetContentID = contentID;
      _name = name;
      _imagePath = imagePath;
    }
  }
}
