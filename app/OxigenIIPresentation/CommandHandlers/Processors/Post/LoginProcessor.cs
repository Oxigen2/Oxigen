using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using OxigenIIAdvertising.BLClients;
using System.Web.SessionState;
using OxigenIIAdvertising.SOAStructures;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class LoginProcessor : PostCommandProcessor
  {
    public LoginProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(string[] parameters)
    {
      // is there a command name, username, password and a "remember me" setting?
      if (parameters.Length < 3)
        return ErrorWrapper.SendError("Command parameters missing.");

      BLClient client = null;

      User user = null;

      // call WCF BLL layer
      try
      {
        client = new BLClient();

        user = client.Login(parameters[1], parameters[2]);
      }
      catch (Exception exception)
      {
        return ErrorWrapper.SendError(exception.Message);
      }
      finally
      {
        client.Dispose();
      }

      if (user != null)
      {
        _session.Add("User", user);

        // in case user was poking around the Download page while logged off, we need to clear
        // log-off PC Creation operation parameters
        _session.Remove("PcProfileToken");

        if (string.IsNullOrEmpty(user.FirstName))
          return "-";

        return user.FirstName;
      }

      return "0";
    }
  }
}