using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.AppData
{
  /// <summary>
  /// A single channel subscription object
  /// </summary>
  [Serializable]
  [DataContract]
  public class ChannelSubscription
  {
    private long _channelID;
    private string _channelName;
    private bool _bLocked;
    private string _channelGUID;
    private int _channelWeightingUnnormalised;
    private float _channelWeightingNormalised;

    /// <summary>
    /// Gets the unique ID of the channel in the database
    /// </summary>
    [DataMember]
    public long ChannelID
    {
      get { return _channelID; }
      set { _channelID = value; }
    }

    /// <summary>
    /// Gets the name of the channel
    /// </summary>
    [DataMember]
    public string ChannelName
    {
      get { return _channelName; }
      set { _channelName = value; }
    }

    /// <summary>
    /// Gets or sets the unnormalized channel weighting
    /// </summary>
    [DataMember]
    public int ChannelWeightingUnnormalised
    {
      get { return _channelWeightingUnnormalised; }
      set { _channelWeightingUnnormalised = value; }
    }

    /// <summary>
    /// Gets or sets the normalized channel weighting
    /// </summary>
    [DataMember]
    public float ChannelWeightingNormalised
    {
      get { return _channelWeightingNormalised; }
      set { _channelWeightingNormalised = value; }
    }

    /// <summary>
    /// Gets the channel GUID that identifies the channel's location across the relay servers
    /// </summary>
    [DataMember]
    public string ChannelGUID
    {
      get { return _channelGUID; }
      set { _channelGUID = value; }
    }

    /// <summary>
    /// Gets or sets whether the stream is locked
    /// </summary>
    [DataMember]
    public bool Locked
    {
      get { return _bLocked; }
      set { _bLocked = value; }
    }

    /// <summary>
    /// Gets the letter which suffixes the channel's GUID
    /// </summary>
    /// <returns>The letter which suffixes the channel's GUID</returns>
    public string GetGUIDSuffix()
    {
      return _channelGUID.Substring(_channelGUID.LastIndexOf("_") + 1, 1);
    }

    public ChannelSubscription() { }
  }
}
