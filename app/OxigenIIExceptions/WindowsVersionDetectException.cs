using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.Exceptions
{
  public class WindowsVersionDetectException : ApplicationException, ISerializable
  {
    public WindowsVersionDetectException()
    {
    }

    public WindowsVersionDetectException(string message)
      : base(message)
    {
    }

    public WindowsVersionDetectException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    public WindowsVersionDetectException(SerializationInfo info, StreamingContext context)
    {
    }
  }
}
