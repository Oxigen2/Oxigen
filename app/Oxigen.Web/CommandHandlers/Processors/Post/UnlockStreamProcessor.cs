using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class UnlockStreamProcessor : PostCommandProcessor
  {
    public UnlockStreamProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(string[] parameters)
    {
      int userID;
      int channelID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      if (parameters.Length < 3)
        return ErrorWrapper.SendError("Command parameters missing.");

      if (!int.TryParse(parameters[1], out channelID))
        return ErrorWrapper.SendError("Cannot parse channel ID.");

      BLClient client = null;
      bool bUnlocked = false;

      try
      {
        client = new BLClient();

        bUnlocked = client.UnlockStream(userID, channelID, parameters[2]);
      }
      catch (Exception ex)
      {
        return ErrorWrapper.SendError(ex.Message);
      }
      finally
      {
        client.Dispose();
      }

      return bUnlocked ? "1" : "0";
    }
  }
}
