using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.AppData
{
  /// <summary>
  /// Class describing content (non-advertising) assets
  /// </summary>
  [Serializable]
  public class ChannelAsset : Asset
  {
    private ChannelDataAssetLevel _assetLevel;

    /// <summary>
    /// Normal or Premium level
    /// </summary>
    public ChannelDataAssetLevel AssetLevel
    {
      get { return _assetLevel; }
      set { _assetLevel = value; }
    }

    public ChannelAsset() { }

    public ChannelAsset(long assetID, PlayerType playerType, string assetFileName,
          float displayDuration, string clickDestination, string assetWebSite, string[] scheduleInfo, ChannelDataAssetLevel assetLevel,
          DateTime startDateTime, DateTime endDateTime)
    {
      _assetID = assetID;
      _playerType = playerType;
      _assetFilename = assetFileName;
      _displayDuration = displayDuration;
      _clickDestination = clickDestination;
      _assetWebSite = assetWebSite;
      _scheduleInfo = scheduleInfo;
      _assetLevel = assetLevel;
      _startDateTime = startDateTime;
      _endDateTime = endDateTime;
    }
  }
}
