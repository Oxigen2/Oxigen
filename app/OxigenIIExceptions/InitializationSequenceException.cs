using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.Exceptions
{
  public class InitializationSequenceException : ApplicationException, ISerializable
  {
    public InitializationSequenceException()
    {
    }

    public InitializationSequenceException(string message)
      : base(message)
    {
    }

    public InitializationSequenceException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    public InitializationSequenceException(SerializationInfo info, StreamingContext context)
    {
    }
  }

}
