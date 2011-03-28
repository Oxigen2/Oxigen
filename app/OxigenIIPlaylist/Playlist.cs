using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.InclusionExclusionRules;

namespace OxigenIIAdvertising.AppData
{
  /// <summary>
  /// End user's playlist
  /// </summary>
  public class Playlist
  {
    private HashSet<ChannelBucket> _channelBuckets;
    private AdvertBucket _advertBucket;

    /// <summary>
    /// Hashset to hold Channel Bucket objects
    /// </summary>
    public HashSet<ChannelBucket> ChannelBuckets
    {
      get { return _channelBuckets; }
      set { _channelBuckets = value; }
    }

    /// <summary>
    /// Advert Bucket object
    /// </summary>
    public AdvertBucket AdvertBucket
    {
      get { return _advertBucket; }
      set { _advertBucket = value; }
    }

    /// <summary>
    /// Instantiating a Playlist will also instantiate a HashSet to hold the underlying channel buckets
    /// and an AdvertBucket to hold the underlying advert assets
    /// </summary>
    public Playlist()
    {
      _channelBuckets = new HashSet<ChannelBucket>();
      _advertBucket = new AdvertBucket();
    }

    /// <summary>
    /// Copy constructor
    /// </summary>
    /// <param name="otherPlaylist">Playlist to copy</param>
    public Playlist(Playlist otherPlaylist)
    {
      if (otherPlaylist != null)
      {
        this._advertBucket = otherPlaylist._advertBucket;
        this._channelBuckets = otherPlaylist._channelBuckets;
      }
    }
  }
}
