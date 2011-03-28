using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class RemoveSlideFolderProcessor : PostCommandProcessor
  {
    public RemoveSlideFolderProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(string[] parameters)
    {
      int userID;
      int folderID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      if (parameters.Length < 2)
        return ErrorWrapper.SendError("Command parameters missing.");

      if (!int.TryParse(parameters[1], out folderID))
        return ErrorWrapper.SendError("Cannot parse folder ID.");

      BLClient client = null;
      bool bCanRemove = false;

      try
      {
        client = new BLClient();

        bCanRemove = client.RemoveSlideFolder(userID, folderID);
      }
      catch (Exception ex)
      {
        return ErrorWrapper.SendError(ex.Message);
      }
      finally
      {
        client.Dispose();
      }

      if (!bCanRemove)
        return "-1";

      return "1";
    }
  }
}
