using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ServiceErrorReporting
{
  /// <summary>
  /// Object to hold error information, if error is applicable
  /// </summary>
  [MessageContract]
  public class SimpleErrorWrapperMessage : IErrorReporting
  {
    private ErrorStatus _errorStatus;
    private string _errorCode;
    private string _message;
    private ErrorSeverity _errorSeverity;

    /// <summary>
    /// Status pertaining to error conditions
    /// </summary>
    [MessageHeader]
    public ErrorStatus ErrorStatus
    {
      get { return _errorStatus; }
      set { _errorStatus = value; }
    }

    /// <summary>
    /// A unique error code to identify errors across the system
    /// </summary>
    [MessageHeader]
    public string ErrorCode
    {
      get { return _errorCode; }
      set { _errorCode = value; }
    }

    /// <summary>
    /// A message to include with the error
    /// </summary>
    [MessageHeader]
    public string Message
    {
      get { return _message; }
      set { _message = value; }
    }

    /// <summary>
    /// Retriable or Severe
    /// </summary>
    [MessageHeader]
    public ErrorSeverity ErrorSeverity
    {
      get { return _errorSeverity; }
      set { _errorSeverity = value; }
    }
  }
}
