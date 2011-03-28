using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ServiceErrorReporting
{
  /// <summary>
  /// Error Status
  /// </summary>
  [DataContract]
  public enum ErrorStatus
  {
    /// <summary>
    /// No error. Successful operation
    /// </summary>
    [EnumMember]
    Success,

    /// <summary>
    /// Error raised
    /// </summary>
    [EnumMember]
    Failure,

    /// <summary>
    /// The comparison of two file checksums are equal. Used for file retrieval operations
    /// </summary>
    [EnumMember]
    ChecksumEqual,

    /// <summary>
    /// Web method has returned no data.
    /// </summary>
    [EnumMember]
    NoData
  }

  /// <summary>
  /// Enumeration to hold error severity cases
  /// </summary>
  [DataContract]
  public enum ErrorSeverity
  {
    /// <summary>
    /// Severe. Halts program execution
    /// </summary>
    [EnumMember]
    Severe,

    /// <summary>
    /// Retriable. Application will continue working and operation can be attempted again later
    /// </summary>
    [EnumMember]
    Retriable
  }
}
