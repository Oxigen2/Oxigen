using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using System.Data.SqlClient;

namespace OxigenIIAdvertising.SOAStructures
{
  /// <summary>
  /// Channel structure for the thumbnail pages
  /// </summary>
  [DataContract]
  public class ChannelListChannel
  {
    private int _channelID;
    private string _channelGUID;
    private string _channelName;
    private string _imagePath;
    private int _channelWeightingUnnormalised;
    private ChannelPrivacyStatus _privacyStatus;
    private bool _bAcceptPasswordRequests;

    /// <summary>
    /// The channel's unique database identifier
    /// </summary>
    [DataMember]
    public int ChannelID
    {
      get { return _channelID; }
      set { _channelID = value; }
    }

    /// <summary>
    /// Channel's Global Unique Identifier
    /// </summary>
    [DataMember]
    public string ChannelGUID
    {
      get { return _channelGUID; }
      set { _channelGUID = value; }
    }

    /// <summary>
    /// The channel's unique database identifier
    /// </summary>
    [DataMember]
    public string ChannelName
    {
      get { return _channelName; }
      set { _channelName = value; }
    }

    /// <summary>
    /// Thumbnail image path
    /// </summary>
    [DataMember]
    public string ImagePath
    {
      get { return _imagePath; }
      set { _imagePath = value; }
    }

    /// <summary>
    /// Gets or sets the privacy status of the channel
    /// </summary>
    [DataMember]
    public ChannelPrivacyStatus PrivacyStatus
    {
      get { return _privacyStatus; }
      set { _privacyStatus = value; }
    }

    /// <summary>
    /// Channel Weighting
    /// </summary>
    [DataMember]
    public int ChannelWeightingUnnormalised
    {
      get { return _channelWeightingUnnormalised; }
      set { _channelWeightingUnnormalised = value; }
    }

    /// <summary>
    /// User accepts password e-mail requests
    /// </summary>
    [DataMember]
    public bool AcceptPasswordRequests
    {
      get { return _bAcceptPasswordRequests; }
      set { _bAcceptPasswordRequests = value; }
    }

    public ChannelListChannel(int channelID, string channelName, string imagePath, ChannelPrivacyStatus privacyStatus)
    {
      _channelID = channelID;
      _channelName = channelName;
      _imagePath = imagePath;
      _privacyStatus = privacyStatus;
    }

    public ChannelListChannel(int channelID, string channelName, string imagePath, ChannelPrivacyStatus privacyStatus, int channelWeightingUnnormalised)
      : this(channelID, channelName, imagePath, privacyStatus)
    {
      _channelWeightingUnnormalised = channelWeightingUnnormalised;
    }

    public ChannelListChannel() { }
  }

  [DataContract]
  public enum ChannelPrivacyStatus
  {
    [EnumMember]
    Locked,

    [EnumMember]
    Unlocked,

    [EnumMember]
    Public
  }
}
