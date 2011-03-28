using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using OxigenIIAdvertising.DataAccess;
using System.Data.SqlClient;

namespace OxigenIIAdvertising.SOAStructures
{
  /// <summary>
  /// Represents an end user's custom content stream
  /// </summary>
  [Serializable]
  [DataContract]
  public class ChannelProperties
  {
    private int _channelID;
    private int _categoryID;
    private string _categoryName;
    private string _name;
    private string _description;
    private string _longDescription;
    private string _keywords;
    private bool _bLocked;
    private string _channelPassword;
    private bool _bAcceptPasswordRequests;
    private bool _bHasAuthorizedUsers;

    /// <summary>
    /// The database unique ID for the stream
    /// </summary>
    [DataMember]
    public int ChannelID
    {
      get { return _channelID; }
      set { _channelID = value; }
    }

    /// <summary>
    /// The database unique ID for the stream
    /// </summary>
    [DataMember]
    public int CategoryID
    {
      get { return _categoryID; }
      set { _categoryID = value; }
    }

    /// <summary>
    /// The name of the category the stream is part of
    /// </summary>
    [DataMember]
    public string CategoryName
    {
      get { return _categoryName; }
      set { _categoryName = value; }
    }

    /// <summary>
    /// The stream's name
    /// </summary>
    [DataMember]
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    /// <summary>
    /// The stream's short description
    /// </summary>
    [DataMember]
    public string Description
    {
      get { return _description; }
      set { _description = value; }
    }

    /// <summary>
    /// The stream's long description
    /// </summary>
    [DataMember]
    public string LongDescription
    {
      get { return _longDescription; }
      set { _longDescription = value; }
    }

    /// <summary>
    /// A set of keywords for the stream to be found with
    /// </summary>
    [DataMember]
    public string Keywords
    {
      get { return _keywords; }
      set { _keywords = value; }
    }

    /// <summary>
    /// Public or private
    /// </summary>
    [DataMember]
    public bool Locked
    {
      get { return _bLocked; }
      set { _bLocked = value; }
    }

    /// <summary>
    /// A set of keywords for the stream to be found with
    /// </summary>
    [DataMember]
    public string ChannelPassword
    {
      get { return _channelPassword; }
      set { _channelPassword = value; }
    }

    /// <summary>
    /// Public or private
    /// </summary>
    [DataMember]
    public bool AcceptPasswordRequests
    {
      get { return _bAcceptPasswordRequests; }
      set { _bAcceptPasswordRequests = value; }
    }

    /// <summary>
    /// Stream is / was private and there are users who have entered the right password
    /// </summary>
    [DataMember]
    public bool HasAuthorizedUsers
    {
      get { return _bHasAuthorizedUsers; }
      set { _bHasAuthorizedUsers = value; }
    }

    public ChannelProperties() { }

    public ChannelProperties(string name, string description, string longDescription, string keywords, bool bLocked)
    {
      _name = name;
      _description = description;
      _longDescription = longDescription;
      _keywords = keywords;
      _bLocked = bLocked;
    }
  }
}
