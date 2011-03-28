using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.SOAStructures
{
  /// <summary>
  /// Enumeration of possible options for a channel with changing privacy settings or password
  /// </summary>
  [DataContract]
  public enum ChannelPrivacyOptions
  {
    /// <summary>
    /// Users already authorized remain authorized.
    /// </summary>
    [EnumMember]
    KeepAuthorizedUsers,

    /// <summary>
    /// Users who were previously authorized are to be unauthorized on this changing of privacy.
    /// </summary>
    [EnumMember]
    UnauthorizeExistingAuthorizedUsers,

    /// <summary>
    /// Authorize all followers to stream that is about to be turned private.
    /// </summary>
    [EnumMember]
    AuthorizeAllFollowers,

    /// <summary>
    /// Privacy settings have no bearing on followers/authorized users.
    /// This will happen if public channels remain public in this changing of channel details
    /// or if private channels remain private and have no password change.
    /// </summary>
    [EnumMember]
    Unchanged
  }
}
