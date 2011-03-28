using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OxigenIIPresentation
{
  /// <summary>
  /// Provides static methods to wrap an error message
  /// </summary>
  public class ErrorWrapper
  {
    /// <summary>
    /// Returns an error string in a format undestandable by the client-end JavaScript
    /// </summary>
    /// <param name="errorMessage">Error message to return</param>
    /// <returns>an error string in a format undestandable by the client-end JavaScript</returns>
    public static string SendError(string errorMessage)
    {
      return "Error: " + errorMessage;
    }
  }
}
