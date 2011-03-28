using System;
using System.Collections.Generic;
using System.Text;

namespace InstallCustomSteps.DuplicateLibrary
{
  /// <summary>
  /// Cut-down version of OxigenIIAdvertising.AppData.ChannelSubscription
  /// for use on local machine during Setup
  /// </summary>
  public class ChannelSubscription
  {
    private long _channelID;
    private string _channelName;
    private string _channelGUID;
    private int _channelWeightingUnnormalised;
    private float _channelWeightingNormalised;

    /// <summary>
    /// Gets the unique ID of the channel in the database
    /// </summary>
    public long ChannelID
    {
      get { return _channelID; }
      set { _channelID = value; }
    }

    /// <summary>
    /// Gets the name of the channel
    /// </summary>
    public string ChannelName
    {
      get { return _channelName; }
      set { _channelName = value; }
    }

    /// <summary>
    /// Gets or sets the unnormalized channel weighting
    /// </summary>
    public int ChannelWeightingUnnormalised
    {
      get { return _channelWeightingUnnormalised; }
      set { _channelWeightingUnnormalised = value; }
    }

    /// <summary>
    /// Gets or sets the normalized channel weighting
    /// </summary>
    public float ChannelWeightingNormalised
    {
      get { return _channelWeightingNormalised; }
      set { _channelWeightingNormalised = value; }
    }

    /// <summary>
    /// Gets the channel GUID that identifies the channel's location across the relay servers
    /// </summary>
    public string ChannelGUID
    {
      get { return _channelGUID; }
      set { _channelGUID = value; }
    }
  }
}
