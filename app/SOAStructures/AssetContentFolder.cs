using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.SOAStructures
{
  /// <summary>
  /// Represents a Raw Content Folder
  /// </summary>
  [DataContract]
  public class AssetContentFolder
  {
    private int _assetContentFolderID;
    private string _assetContentFolderName;
    private List<AssetContentListContent> _assetContents;

    /// <summary>
    /// The unique database ID for the Asset Content Folder
    /// </summary>
    [DataMember]
    public int AssetContentFolderID
    {
      get { return _assetContentFolderID; }
      set { _assetContentFolderID = value; }
    }

    /// <summary>
    /// The name of the folder
    /// </summary>
    [DataMember]
    public string AssetContentFolderName
    {
      get { return _assetContentFolderName; }
      set { _assetContentFolderName = value; }
    }

    /// <summary>
    /// The asset contents inside the folder
    /// </summary>
    [DataMember]
    public List<AssetContentListContent> AssetContents
    {
      get { return _assetContents; }
      set { _assetContents = value; }
    }
  }
}
