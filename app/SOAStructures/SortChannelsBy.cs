using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.SOAStructures
{
  /// <summary>
  /// Sort returned channels by this attribute
  /// </summary>
  [DataContract]
  public enum SortChannelsBy
  {
    /// <summary>
    /// Sort channels by popularity
    /// </summary>
    [EnumMember]
    Popularity,

    /// <summary>
    /// Sort channels by date (most recent to least recent)
    /// </summary>
    [EnumMember]
    MostRecent,

    /// <summary>
    /// Sort channels by alphabetical order
    /// </summary>
    [EnumMember]
    Alphabetical
  }
}
