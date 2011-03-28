using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.Exceptions
{
  public class DataNotFoundException : Exception, ISerializable
  {
    public DataNotFoundException() : base("Data was expected in database but was not found.")
    {
    }

    public DataNotFoundException(string message)
      : base(message)
    {
    }

    public DataNotFoundException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    public DataNotFoundException(SerializationInfo info, StreamingContext context)
    {
    }
  }
}
