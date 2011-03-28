using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class RemovePCStreamProcessor : PostCommandProcessor
  {
    public RemovePCStreamProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(string[] parameters)
    {
      int pcID;
      int userID = -1;
      List<int> contentIDList;

      string error = null;

      if (_session["User"] != null)
        error = Helper.GetIDs(_session, parameters, out userID, out pcID, out contentIDList);
      else
        error = GetIDs(parameters, out pcID, out contentIDList);

      if (error != "1")
        return error;

      BLClient client = null;

      try
      {
        client = new BLClient();

        client.RemovePCStream(userID, pcID, contentIDList);
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

    private string GetIDs(string[] parameters, out int pcID, out List<int> contentIDList)
    {
      contentIDList = null;
      pcID = -1;

      if (parameters.Length < 3)
        return ErrorWrapper.SendError("Command parameters missing.");

      if (!int.TryParse(parameters[1], out pcID))
        return ErrorWrapper.SendError("Can't parse PC ID");

      return Helper.GetContentIDs(parameters[2], out contentIDList);
    }
  }
}
