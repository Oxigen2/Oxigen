using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace OxigenIIPresentation.CommandHandlers.Processors
{
  public abstract class PostCommandProcessor : CommandProcessor
  {
    public PostCommandProcessor(HttpSessionState session) : base(session) { }

    /// <summary>
    /// When implemented in child classes, it executes a user request and returns a result
    /// </summary>
    /// <param name="commandParameters">a string array with the command's parameters</param>
    /// <returns>a string with the result or an error</returns>
    internal abstract string Execute(string[] parameters);
  }
}
