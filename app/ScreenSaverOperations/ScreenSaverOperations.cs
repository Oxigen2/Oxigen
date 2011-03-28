using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.AssetScheduling;
using OxigenIIAdvertising.PlaylistLogic;
using System.Windows.Forms;

namespace OxigenIIAdvertising.ScreenSaver
{
  /// <summary>
  /// Provides static methods for the screen saver operations
  /// </summary>
  public static class ScreenSaverOperations
  {
    /// <summary>
    /// Shows a content asset on the screensaver and sets the timer to its length
    /// </summary>
    /// <param name="cpp">ContentPlaylistAssetPicker to determine a random content asset to pick</param>
    /// <param name="timer">Timer object to check whether the asset display length has elapsed</param>
    /// <returns>The information on the content playlist asset, null if no available asset found</returns>
    public static ContentPlaylistAsset ShowContentAsset(PlaylistAssetPicker playlistAssetPicker, Timer timer)
    {
      ContentPlaylistAsset cpa = playlistAssetPicker.PickRandomContentPlaylistAsset();

      if (cpa == null)
        return null;

      timer.Interval = cpa.DisplayLength * 1000; // convert to seconds

      return cpa;
    }
  }
}
