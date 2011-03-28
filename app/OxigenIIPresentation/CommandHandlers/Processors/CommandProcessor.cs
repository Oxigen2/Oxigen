using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace OxigenIIPresentation.CommandHandlers.Processors
{
  public abstract class CommandProcessor
  {
    protected HttpSessionState _session;

    protected CommandProcessor(HttpSessionState session)
    {
      _session = session;
    }

    /// <summary>
    /// Checks whether a given long exists in a Query string and then extracts it
    /// </summary>
    /// <param name="commandParameters">the Query String to check</param>
    /// <param name="parameter">the parameter to validate and extract</param>
    /// <param name="numerical">the reference of the long to extract the parameter to<</param>
    /// <returns>An empty string if the parameter exists, an error message otherwise</returns>
    protected string ValidateLongParameter(NameValueCollection commandParameters, string parameter, out long numerical)
    {
      string error = ValidateParameterExists(commandParameters, parameter);

      if (error != String.Empty)
      {
        numerical = -1;
        return error;
      }

      if (!long.TryParse(commandParameters[parameter], out numerical))
        return ErrorWrapper.SendError("invalid " + parameter);

      return String.Empty;
    }

    /// <summary>
    /// Checks whether a given int exists in a Query string and then extracts it
    /// </summary>
    /// <param name="commandParameters">the Query String to check</param>
    /// <param name="parameter">the parameter to validate and extract</param>
    /// <param name="numerical">the reference of the int to extract the parameter to</param>
    /// <returns>An empty string if the parameter exists, an error message otherwise</returns>
    protected string ValidateIntParameter(NameValueCollection commandParameters, string parameter, out int numerical)
    {
      string error = ValidateParameterExists(commandParameters, parameter);

      if (error != String.Empty)
      {
        numerical = -1;
        return error;
      }

      if (!int.TryParse(commandParameters[parameter], out numerical))
        return ErrorWrapper.SendError("invalid  parameter:" + parameter);

      return String.Empty;
    }

    /// <summary>
    /// Checks whether a given string exists in a Query string and then extracts it
    /// </summary>
    /// <param name="commandParameters">the Query String to check</param>
    /// <param name="parameter">the parameter to validate and extract</param>
    /// <param name="parameterValue">the reference of the string to extract the parameter to</param>
    /// <returns>An empty string if the parameter exists, an error message otherwise</returns>
    protected string ValidateStringParameter(NameValueCollection commandParameters, string parameter, out string parameterValue)
    {
      string error = ValidateParameterExists(commandParameters, parameter);

      if (error != String.Empty)
      {
        parameterValue = String.Empty;
        return error;
      }

      parameterValue = commandParameters[parameter];

      return String.Empty;
    }

    /// <summary>
    /// Checks whether a given string is "0" or "1", else returns an error message
    /// </summary>
    /// <param name="intSwitchParam">the parameter to check</param>
    /// <returns>An empty string if the intSwitchParam is "0" or "1", an error message otherwise</returns>
    protected string ValidateIntSwitchParameter(string intSwitchParam)
    {
      switch (intSwitchParam)
      {
        case "1":
          goto case "0";
        case "0":
          return String.Empty;
        default:
          return ErrorWrapper.SendError("Cannot parse int switch");
      }
    }

    /// <summary>
    /// Validates if a Query String parameter exists in the Query String
    /// </summary>
    /// <param name="commandParameters">the Query String to check</param>
    /// <param name="parameter">the parameter to validate</param>
    /// <returns>An empty string if the parameter exists, an error message otherwise</returns>
    protected string ValidateParameterExists(NameValueCollection commandParameters, string parameter)
    {
      if (commandParameters[parameter] == null)
        return ErrorWrapper.SendError("Parameter " + parameter + " not supplied");

      return String.Empty;
    }
  }
}
