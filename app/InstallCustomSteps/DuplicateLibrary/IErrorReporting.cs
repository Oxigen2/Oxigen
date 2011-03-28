using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.IO;
using System.Collections.ObjectModel;

namespace ServiceErrorReporting
{
  /// <summary>
  /// Provides properties for Service error return objects
  /// </summary>
  public interface IErrorReporting
  {
    /// <summary>
    /// Error Status
    /// </summary>
    ErrorStatus ErrorStatus { get; set; }

    /// <summary>
    /// A code to be returned with the error
    /// </summary>
    string ErrorCode { get; set; }

    /// <summary>
    /// Custom message to be returned with the error
    /// </summary>
    string Message { get; set; }

    /// <summary>
    /// Retriable or Severe
    /// </summary>
    ErrorSeverity ErrorSeverity { get; set; }
  }
}
