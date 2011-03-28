using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ServiceErrorReporting
{
  /// <summary>
  /// Object to hold error information, if error is applicable
  /// </summary>
  [DataContract]
  public class SimpleErrorWrapper : IErrorReporting
  {
    private ErrorStatus _errorStatus;
    private string _errorCode;
    private string _message;
    private ErrorSeverity _errorSeverity;

    /// <summary>
    /// Status pertaining to error conditions
    /// </summary>
    [DataMember]
    public ErrorStatus ErrorStatus
    {
      get { return _errorStatus; }
      set { _errorStatus = value; }
    }

    /// <summary>
    /// A unique error code to identify errors across the system
    /// </summary>
    [DataMember]
    public string ErrorCode
    {
      get { return _errorCode; }
      set { _errorCode = value; }
    }

    /// <summary>
    /// A message to include with the error
    /// </summary>
    [DataMember]
    public string Message
    {
      get { return _message; }
      set { _message = value; }
    }

    /// <summary>
    /// Retriable or Severe
    /// </summary>
    [DataMember]
    public ErrorSeverity ErrorSeverity
    {
      get { return _errorSeverity; }
      set { _errorSeverity = value; }
    }
  }
}
