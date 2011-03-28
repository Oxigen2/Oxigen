using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.Exceptions
{
  /// <summary>
  /// Thrown when an object member is meant to be defined only once but was re-defined.
  /// </summary>
  public class AlreadyDefinedException : ApplicationException, ISerializable
  {
    /// <summary>
    /// Default constructor for AlreadyDefinedException
    /// </summary>
    public AlreadyDefinedException()
    {
    }

    /// <summary>
    /// Constructor with message to give
    /// </summary>
    /// <param name="message">the message to accompany the exception</param>
    public AlreadyDefinedException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Constructor with message and inner exception
    /// </summary>
    /// <param name="message">the message to accompany the exception</param>
    /// <param name="innerException">the inner exception that was thrown</param>
    public AlreadyDefinedException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    /// Serialization constructor
    /// </summary>
    /// <param name="info">SerializationInfo object</param>
    /// <param name="context">StreamingContext object</param>
    public AlreadyDefinedException(SerializationInfo info, StreamingContext context)
    {
    }
  }
}
