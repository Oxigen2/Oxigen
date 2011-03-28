using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.AppData
{
  /// <summary>
  /// Represents a pair of a playlist asset and the channel it belongs to.
  /// Used when picking a asset and there is a need to keep a reference to its channel's ID
  /// </summary>
  public class ChannelAssetAssociation
  {
    private long _channelID;
    private PlaylistAsset _playlistAsset;

    /// <summary>
    /// The channelID the playlist asset was picked from
    /// </summary>
    public long ChannelID
    {
      get { return _channelID; }
      set { _channelID = value; }
    }

    /// <summary>
    /// The picked playlist asset
    /// </summary>
    public PlaylistAsset PlaylistAsset
    {
      get { return _playlistAsset; }
      set { _playlistAsset = value; }
    }

    public ChannelAssetAssociation(long channelID, PlaylistAsset playlistAsset)
    {
      _channelID = channelID;
      _playlistAsset = playlistAsset;
    }
  }
}
