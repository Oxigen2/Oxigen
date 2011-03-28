using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.AppData
{
  /// <summary>
  /// Bucket to hold advert playlist assets
  /// </summary>
  public class AdvertBucket
  {
    private HashSet<AdvertPlaylistAsset> _advertAssets;

    /// <summary>
    /// HashSet to hold Playlist Assets
    /// </summary>
    public HashSet<AdvertPlaylistAsset> AdvertAssets
    {
      get { return _advertAssets; }
      set { _advertAssets = value; }
    }
    
    /// <summary>
    /// This will also instantiate a HashSet to hold advert assets
    /// </summary>
    public AdvertBucket()
    {
      _advertAssets = new HashSet<AdvertPlaylistAsset>();
    }
  }
}
