using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.SOAStructures
{
  /// <summary>
  /// Represents a structure of Raw content under a folder and number of pages under the same folder
  /// </summary>
  [DataContract]
  public class PageAssetContentData
  {
    private int _noPages;
    private List<AssetContentListContent> _assetContents;
    
    [DataMember]
    public int NoPages
    {
      get { return _noPages; }
      set { _noPages = value; }
    }

    [DataMember]
    public List<AssetContentListContent> AssetContents
    {
      get { return _assetContents; }
      set { _assetContents = value; }
    }
  }
}
