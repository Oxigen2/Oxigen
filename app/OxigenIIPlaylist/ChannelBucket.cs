using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.AppData
{
  /// <summary>
  /// Bucket to hold content (non-advertising) playlist assets and channel information
  /// </summary>
  public class ChannelBucket
  {
    private float _playingProbabilityUnnormalized;
    private float _PlayingProbabilityNormalised;
    private float _averagePlayTime;
    private float _lowerThresholdNormalised;
    private float _higherThresholdNormalised;
    private string _channelName;
    private long _channelID;
    private HashSet<ContentPlaylistAsset> _contentAssets;

    /// <summary>
    /// Normalized playing probability of the channel from which the bucket derives
    /// </summary>
    public float PlayingProbabilityNormalised
    {
      get { return _PlayingProbabilityNormalised; }
      set { _PlayingProbabilityNormalised = value; }
    }

    /// <summary>
    /// Unnormalized playing probability of the channel from which the bucket derives
    /// </summary>
    public float PlayingProbabilityUnnormalized
    {
      get { return _playingProbabilityUnnormalized; }
      set { _playingProbabilityUnnormalized = value; }
    }

    /// <summary>
    /// The average playing time of all the assets in the channel
    /// </summary>
    public float AveragePlayTime
    {
      get { return _averagePlayTime; }
      set { _averagePlayTime = value; }
    }

    /// <summary>
    /// The lower threshold of the bucket (for random play)
    /// </summary>
    public float LowerThresholdNormalised
    {
      get { return _lowerThresholdNormalised; }
      set { _lowerThresholdNormalised = value; }
    }

    /// <summary>
    /// The higher threshold of the bucket (for random play)
    /// </summary>
    public float HigherThresholdNormalised
    {
      get { return _higherThresholdNormalised; }
      set { _higherThresholdNormalised = value; }
    }

    /// <summary>
    /// Channel Weighting
    /// </summary>
    //public float ChannelWeighting
    //{
    //  get { return _channelWeighting; }
    //  set { _channelWeighting = value; }
    //}

    /// <summary>
    /// The channel ID of the channel the channel bucket derives from
    /// </summary>
    public long ChannelID
    {
      get { return _channelID; }
      set { _channelID = value; }
    }

    /// <summary>
    /// Holds the playlist assets of the channel
    /// </summary>
    public HashSet<ContentPlaylistAsset> ContentAssets
    {
      get { return _contentAssets; }
      set { _contentAssets = value; }
    }

    /// <summary>
    /// Gets or sets the name of the Channel
    /// </summary>
    public string ChannelName
    {
      get { return _channelName; }
      set { _channelName = value; }
    }

    public ChannelBucket()
    {
      _contentAssets = new HashSet<ContentPlaylistAsset>();
    }
  }
}
