using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OxigenIIAdvertising.BLClients;
using System.Web.SessionState;
using OxigenIIAdvertising.SOAStructures;
using System.Text.RegularExpressions;
using OxigenIIAdvertising.LoggerInfo;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class SignupProcessor : PostCommandProcessor
  {
    public SignupProcessor(HttpSessionState session) : base(session) { }

    private Logger _logger = new Logger("Sign up", @"\\iis6-server\Client Sites\OxigenIIAdvertisingSystem\debug.txt", LoggingMode.Debug);
    private Regex emailPattern = new Regex(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}\b", RegexOptions.Compiled);

    internal override string Execute(string[] parameters)
    {
      // is there a command name, username, password, firstname, lastname?
      if (parameters.Length < 5)
        return ErrorWrapper.SendError("Command parameters missing.");       
  
      if (!emailPattern.IsMatch(parameters[1]))
        return "-3";

      BLClient client = null;

      User user = null;

      foreach (string parameter in parameters)
      {
        if (parameter.Trim() == String.Empty)
          return "-1";
      }

      // call WCF BLL layer
      try
      {
        client = new BLClient();

        user = client.Signup(parameters[1], parameters[2], parameters[3], parameters[4]);
      }
      catch (Exception exception)
      {
          _logger.WriteMessage(exception.ToString());
        return ErrorWrapper.SendError(exception.Message);
      }
      finally
      {
        client.Dispose();
      }

      if (user == null)
        return "-2";
      
      _session.Add("User", user);
      return "1";     
    }
  }
}
