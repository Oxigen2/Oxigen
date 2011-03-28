using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.Exceptions
{
  public class RemoteServerException : ApplicationException, ISerializable
  {
    public RemoteServerException()
    {
    }

    public RemoteServerException(string message)
      : base(message)
    {
    }

    public RemoteServerException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    public RemoteServerException(string errorCode, string errorSeverity, string message)
      : base("Error Code: " + errorCode + "\r\n Error Severity: " + errorSeverity + "\r\n Message: " + message)
    {
    }

    public RemoteServerException(SerializationInfo info, StreamingContext context)
    {
    }
  }
}
