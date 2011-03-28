using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class MoveRawContentProcessor : PostCommandProcessor
  {
    public MoveRawContentProcessor(HttpSessionState session) : base(session) { }
    
    internal override string Execute(string[] parameters)
    {
      int oldFolderID;
      int newFolderID;
      int userID;
      List<int> contentIDList;

      string error = Helper.GetIDsMove(_session, parameters, out userID, out oldFolderID, out newFolderID, out contentIDList);

      if (error != "1")
        return error;

      BLClient client = null;

      try
      {
        client = new BLClient();

        client.MoveRawContent(userID, oldFolderID, newFolderID, contentIDList);
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
