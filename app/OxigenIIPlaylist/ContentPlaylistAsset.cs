using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.AppData
{
  /// <summary>
  /// Class that represents an content playlist asset
  /// </summary>
  public class ContentPlaylistAsset : PlaylistAsset
  {
    private PlaylistAssetLevel _assetLevel;
    private string _message;

    /// <summary>
    /// Normal or Premium
    /// </summary>
    public PlaylistAssetLevel AssetLevel
    {
      get { return _assetLevel; }
      set { _assetLevel = value; }
    }

    /// <summary>
    /// Message to display on screen if the asset is not a normal content asset but
    /// an exceptional assets shown when no assets are available
    /// </summary>
    public string Message
    {
      get { return _message; }
      set { _message = value; }
    }

    public ContentPlaylistAsset() { }

    /// <summary>
    /// Constructs a special asset to display when no assets are available are available due to an error condition
    /// or due to scheduling or no assets in subscriptions.
    /// </summary>
    /// <param name="displayLength">Seconds to display the asset for</param>
    /// <param name="message">Information message to appear on the screen</param>
    public ContentPlaylistAsset(float displayLength, string message, string clickDestination)
    {
      _assetID = 0;
      _message = message;
      _displayLength = displayLength;
      _clickDestination = clickDestination;
      _playerType = PlayerType.NoAssetsAnimator;
      _assetLevel = PlaylistAssetLevel.Premium;
    }

    /// <summary>
    /// Copy constructor
    /// </summary>
    /// <param name="otherContentPlaylistAsset">Content Playlist Asset to copy</param>
    public ContentPlaylistAsset(ContentPlaylistAsset otherContentPlaylistAsset)
    {
      this._assetFilename = otherContentPlaylistAsset._assetFilename;
      this._assetID = otherContentPlaylistAsset._assetID;
      this._assetWebSite = otherContentPlaylistAsset._assetWebSite;
      this._clickDestination = otherContentPlaylistAsset._clickDestination;
      this._displayLength = otherContentPlaylistAsset._displayLength;
      this._endDateTime = otherContentPlaylistAsset._endDateTime;
      this._playerType = otherContentPlaylistAsset._playerType;
      this._scheduleInfo = otherContentPlaylistAsset._scheduleInfo;
      this._startDateTime = otherContentPlaylistAsset._startDateTime;
      this._assetLevel = otherContentPlaylistAsset._assetLevel;
      this._message = otherContentPlaylistAsset._message;
    }
  }
}
