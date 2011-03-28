using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Setup
{
  public class RemoteServiceException : Exception, ISerializable
  {
    public RemoteServiceException() : base("An error occurred when communicating to a remote service.")
    {
    }

    public RemoteServiceException(string message)
      : base(message)
    {
    }

    public RemoteServiceException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    public RemoteServiceException(SerializationInfo info, StreamingContext context)
    {
    }
  }
}
