using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class RenamePCProcessor : PostCommandProcessor
  {
    public RenamePCProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(string[] parameters)
    {
      int userID;
      int pcID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      if (parameters.Length < 3)
        return ErrorWrapper.SendError("Command parameters missing.");

     if (!int.TryParse(parameters[1], out pcID))
       return ErrorWrapper.SendError("Cannot parse PC ID");

      BLClient client = null;

      try
      {
        client = new BLClient();

        client.RenamePC(userID, pcID, parameters[2]);
      }
      catch (Exception ex)
      {
        return ErrorWrapper.SendError(ex.Message);
      }
      finally
      {
        client.Dispose();
      }

      return "1";
    }
  }
}
