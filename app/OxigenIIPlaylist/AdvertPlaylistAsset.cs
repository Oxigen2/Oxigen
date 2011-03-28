using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.AppData
{
  /// <summary>
  /// Class that represents an advert playlist asset
  /// </summary>
  public class AdvertPlaylistAsset : PlaylistAsset
  {
    private float _weightingUnnormalized;
    private float _weightingNormalised;
    private float _lowerThresholdNormalised;
    private float _higherThresholdNormalised;

    /// <summary>
    /// Unnormalized weighting of the playlist advert
    /// </summary>
    public float WeightingUnnormalized
    {
      get { return _weightingUnnormalized; }
      set { _weightingUnnormalized = value; }
    }

    /// <summary>
    /// Normalised weighting of the playlist advert
    /// </summary>
    public float WeightingNormalised
    {
      get { return _weightingNormalised; }
      set { _weightingNormalised = value; }
    }

    /// <summary>
    /// The lower threshold of the advert asset (for random play)
    /// </summary>
    public float LowerThresholdNormalised
    {
      get { return _lowerThresholdNormalised; }
      set { _lowerThresholdNormalised = value; }
    }
    
    /// <summary>
    /// The higher threshold of the advert asset (for random play)
    /// </summary>
    public float HigherThresholdNormalised
    {
      get { return _higherThresholdNormalised; }
      set { _higherThresholdNormalised = value; }
    }

    public AdvertPlaylistAsset() {}
        
    /// <summary>
    /// Copy constructor
    /// </summary>
    /// <param name="otherAdvertPlaylistAsset">Advert Playlist Asset Asset to copy</param>
    public AdvertPlaylistAsset(AdvertPlaylistAsset otherAdvertPlaylistAsset)
    {
      this._assetFilename = otherAdvertPlaylistAsset._assetFilename;
      this._assetID = otherAdvertPlaylistAsset._assetID;
      this._assetWebSite = otherAdvertPlaylistAsset._assetWebSite;
      this._clickDestination = otherAdvertPlaylistAsset._clickDestination;
      this._displayLength = otherAdvertPlaylistAsset._displayLength;
      this._endDateTime = otherAdvertPlaylistAsset._endDateTime;
      this._playerType = otherAdvertPlaylistAsset._playerType;
      this._scheduleInfo = otherAdvertPlaylistAsset._scheduleInfo;
      this._startDateTime = otherAdvertPlaylistAsset._startDateTime;
      this._weightingUnnormalized = otherAdvertPlaylistAsset._weightingUnnormalized;
      this._weightingNormalised = otherAdvertPlaylistAsset._weightingNormalised;
      this._lowerThresholdNormalised = otherAdvertPlaylistAsset._lowerThresholdNormalised;
      this._higherThresholdNormalised = otherAdvertPlaylistAsset._higherThresholdNormalised;
    }
  }
}
