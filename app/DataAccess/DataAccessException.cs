using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.DataAccess
{
  /// <summary>
  /// Represents an error that has occurred at the Data Access Layer when performing a database operation
  /// </summary>
  public class DataAccessException : ApplicationException, ISerializable
  {
    /// <summary>
    /// Default constructor for DataAccessException
    /// </summary>
    public DataAccessException()
    {
    }

    /// <summary>
    /// Constructor with message to give
    /// </summary>
    /// <param name="message">the message to accompany the exception</param>
    public DataAccessException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Constructor with message and inner exception
    /// </summary>
    /// <param name="message">the message to accompany the exception</param>
    /// <param name="innerException">the inner exception that was thrown</param>
    public DataAccessException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    /// Serialization constructor
    /// </summary>
    /// <param name="info">SerializationInfo object</param>
    /// <param name="context">StreamingContext object</param>
    public DataAccessException(SerializationInfo info, StreamingContext context)
    {
    }
  }
}
