using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class MovePCStreamProcessor : PostCommandProcessor
  {
    public MovePCStreamProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(string[] parameters)
    {
      int oldFolderID;
      int newFolderID;
      int userID;
      List<int> channelIDList;

      string error = Helper.GetIDsMove(_session, parameters, out userID, out oldFolderID, out newFolderID, out channelIDList);

      if (error != "1")
        return error;

      BLClient client = null;

      try
      {
        client = new BLClient();

        client.MovePCChannels(userID, oldFolderID, newFolderID, channelIDList);
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
