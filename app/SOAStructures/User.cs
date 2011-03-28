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
  /// Represents a user structure
  /// </summary>
  [Serializable]
  [DataContract]
  public class User
  {
    private int _userID;
    private string _firstName;
    private string _lastName;
    private string _displayName;
    private string _emailAddress;
    private string _password;
    private long _usedBytes;
    private long _totalAvailableBytes;

    /// <summary>
    /// The unique database ID of the user
    /// </summary>
    [DataMember]
    public int UserID
    {
      get { return _userID; }
      set { _userID = value; }
    }

    /// <summary>
    /// User's first name
    /// </summary>
    [DataMember]
    public string FirstName
    {
      get { return _firstName; }
      set { _firstName = value; }
    }

    /// <summary>
    /// User's last name
    /// </summary>
    [DataMember]
    public string LastName
    {
      get { return _lastName; }
      set { _lastName = value; }
    }

    /// <summary>
    /// User's first and last name
    /// </summary>
    [DataMember]
    public string DisplayName
    {
      get { return _displayName; }
      set { _displayName = value; }
    }

    /// <summary>
    /// User's e-mail address
    /// </summary>
    [DataMember]
    public string EmailAddress
    {
      get { return _emailAddress; }
      set { _emailAddress = value; }
    }

    /// <summary>
    /// User's password
    /// </summary>
    [DataMember]
    public string Password
    {
      get { return _password; }
      set { _password = value; }
    }

    /// <summary>
    /// User's bytes used for asset data
    /// </summary>
    [DataMember]
    public long UsedBytes
    {
      get { return _usedBytes; }
      set { _usedBytes = value; }
    }

    /// <summary>
    /// User's total available bytes used for asset data
    /// </summary>
    [DataMember]
    public long TotalAvailableBytes
    {
      get { return _totalAvailableBytes; }
      set { _totalAvailableBytes = value; }
    }

    public User(int userID)
    {
      _userID = userID;
    }

    public User(string emailAddress, string password, string firstName, string lastName, long usedBytes, long totalAvailableBytes)
    {
      _emailAddress = emailAddress;
      _password = password;
      _firstName = firstName;
      _lastName = lastName;
      _displayName = firstName + " " + lastName;
      _usedBytes = usedBytes;
      _totalAvailableBytes = totalAvailableBytes;
    }

    public User(int userID, string emailAddress, string password, string firstName, string lastName, long usedBytes, long totalAvailableBytes)
      : this(emailAddress, password, firstName, lastName, usedBytes, totalAvailableBytes)
    {
      _userID = userID;
    }
  }
}
