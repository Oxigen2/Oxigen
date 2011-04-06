using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using System.Data.SqlClient;

namespace OxigenIIAdvertising.SOAStructures
{
  /// <summary>
  /// Represent a content channel
  /// </summary>
  [DataContract]
  public class Channel
  {
    private long _channelID;
    private string _channelName;
    private string _channelGUID;
    private List<SlideListSlide> _slides;
    private DateTime _addDate;
    private DateTime _editDate;
    private DateTime _contentLastAddedDate;
    private string _channelDescription;
    private string _channelLongDescription;
    private string _publisherDisplayName;
    private string _imagePath;
    private ChannelPrivacyStatus _privacyStatus;
    private int _noContents;
    private int _noFollowers;

    /// <summary>
    /// The channel's unique database key
    /// </summary>
    [DataMember]
    public long ChannelID
    {
      get { return _channelID; }
      set { _channelID = value; }
    }

    /// <summary>
    /// Channel's unique identifier
    /// </summary>
    [DataMember]
    public string ChannelGUID
    {
      get { return _channelGUID; }
      set { _channelGUID = value; }
    }

    /// <summary>
    /// The channel's name
    /// </summary>
    [DataMember]
    public string ChannelName
    {
      get { return _channelName; }
      set { _channelName = value; }
    }

    /// <summary>
    /// Channel's description
    /// </summary>
    [DataMember]
    public string ChannelDescription
    {
      get { return _channelDescription; }
      set { _channelDescription = value; }
    }

    /// <summary>
    /// Channel's long description
    /// </summary>
    [DataMember] 
    public string ChannelLongDescription
    {
      get { return _channelLongDescription; }
      set { _channelLongDescription = value; }
    }

    /// <summary>
    /// Date and time the channel was created at
    /// </summary>
    [DataMember]
    public DateTime AddDate
    {
      get { return _addDate; }
      set { _addDate = value; }
    }

    /// <summary>
    /// Date and time the channel was last updated at
    /// </summary>
    [DataMember]
    public DateTime EditDate
    {
      get { return _editDate; }
      set { _editDate = value; }
    }

    /// <summary>
    /// Name of the channel's creator
    /// </summary>
    [DataMember]
    public string PublisherDisplayName
    {
      get { return _publisherDisplayName; }
      set { _publisherDisplayName = value; }
    }

    /// <summary>
    /// The channel's image thumbnail for display
    /// </summary>
    [DataMember]
    public string ImagePath
    {
      get { return _imagePath; }
      set { _imagePath = value; }
    }

    /// <summary>
    /// Number of channel content
    /// </summary>
    [DataMember]
    public int NoContents
    {
      get { return _noContents; }
      set { _noContents = value; }
    }

    /// <summary>
    /// Number of subscribers
    /// </summary>
    [DataMember]
    public int NoFollowers
    {
      get { return _noFollowers; }
      set { _noFollowers = value; }
    }

    /// <summary>
    /// Slides under this channel
    /// </summary>
    [DataMember]
    public List<SlideListSlide> Slides
    {
      get { return _slides; }
      set { _slides = value; }
    }

    /// <summary>
    /// Gets or sets the channel's privacy status
    /// </summary>
    [DataMember]
    public ChannelPrivacyStatus PrivacyStatus
    {
      get { return _privacyStatus; }
      set { _privacyStatus = value; }
    }

    /// <summary>
    /// Date content was last added to the Channel
    /// </summary>
    [DataMember]
    public DateTime ContentLastAddedDate
    {
      get { return _contentLastAddedDate; }
      set { _contentLastAddedDate = value; }
    }

    public Channel(long channelID, DateTime createdAt, string channelDescription,
      string publisherDisplayName, int noContent, int noFollowers)
    {
      _channelID = channelID;
      _addDate = createdAt;
      _channelDescription = channelDescription;
      _publisherDisplayName = publisherDisplayName;
      _noContents = noContent;
      _noFollowers = noFollowers;
    }

    public Channel() { }
  }
}
