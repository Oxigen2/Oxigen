using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OxigenIIAdvertising.BLClients;
using System.Web.SessionState;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class AddSlideContentsProcessor : PostCommandProcessor
  {
    public AddSlideContentsProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(string[] parameters)
    {
      int folderID;
      int userID;
      List<int> contentIDList;

      string error = Helper.GetIDs(_session, parameters, out userID, out folderID, out contentIDList);

      if (error != "1")
        return error;

      BLClient client = null;

      try
      {
        client = new BLClient();

        client.AddSlideContent(userID, folderID, contentIDList);
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
