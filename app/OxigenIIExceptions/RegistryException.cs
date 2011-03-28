using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.Exceptions
{
  public class RegistryException : ApplicationException, ISerializable
  {
    public RegistryException()
    {
    }

    public RegistryException(string message)
      : base(message)
    {
    }

    public RegistryException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    public RegistryException(SerializationInfo info, StreamingContext context)
    {
    }
  }
}
