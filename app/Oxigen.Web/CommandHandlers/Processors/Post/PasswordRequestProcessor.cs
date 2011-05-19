using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Text.RegularExpressions;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation.CommandHandlers.Processors.Get
{
  public class PasswordRequestProcessor : PostCommandProcessor
  {
    private Regex _emailRegex = new Regex(@"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", RegexOptions.Compiled);

    public PasswordRequestProcessor(HttpSessionState session) : base(session) { }

    private Oxigen.LoggerInfoServer.Logger logger = new Oxigen.LoggerInfoServer.Logger("Password Requests", @"E:\Client Sites\OxigenIIAdvertisingSystem\debug.txt", Oxigen.LoggerInfoServer.LoggingMode.Debug);

    internal override string Execute(string[] parameters)
    {
      int channelID;
      string name = parameters[2];
      string emailAddress = parameters[3];
      string message = parameters[4];
      string captchaString = parameters[5];
      BLClient client = null;

      logger.WriteTimestampedMessage("no of parameters: " + parameters.Length);

      int counter = 0;

      foreach (string param in parameters)
      {
        logger.WriteTimestampedMessage("Parameter " + counter + " = " + param);

        counter++;
      }

      if (!int.TryParse(parameters[1], out channelID))
      {
        logger.WriteMessage(parameters[1] + " is not a number.");
        throw new FormatException("Parameter must be a number");
      }

      if (string.IsNullOrEmpty(emailAddress) || !_emailRegex.IsMatch(emailAddress))
      {
        logger.WriteTimestampedMessage("0");

        return "0";
      }

      if (captchaString != (string)_session["CaptchaImageText"])
      {
        logger.WriteTimestampedMessage("-1");

        return "-1";
      }

      if (string.IsNullOrEmpty(name))
      {
        logger.WriteTimestampedMessage("-2");

        return "-2";
      }

      try
      {
        client = new BLClient();

        client.SendPasswordRequest(channelID, name, emailAddress, message);
      }
      catch (Exception ex)
      {
        logger.WriteTimestampedMessage(ex.ToString());
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return "1";
    }
  }
}
