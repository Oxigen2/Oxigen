using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.Exceptions
{
  public class TypeDiscrepancyException : ApplicationException, ISerializable
  {
    public TypeDiscrepancyException()
    {
    }

    public TypeDiscrepancyException(string message)
      : base(message)
    {
    }

    public TypeDiscrepancyException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    public TypeDiscrepancyException(SerializationInfo info, StreamingContext context)
    {
    }
  }
}
