using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.Exceptions
{
  public class LogDateTimeException : ApplicationException, ISerializable
  {
    public LogDateTimeException()
    {
    }

    public LogDateTimeException(string message)
      : base(message)
    {
    }

    public LogDateTimeException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    public LogDateTimeException(SerializationInfo info, StreamingContext context)
    {
    }
  }
}
