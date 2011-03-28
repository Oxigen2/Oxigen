using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class EditSlideFolderProcessor : PostCommandProcessor
  {
    public EditSlideFolderProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(string[] parameters)
    {
      int userID;
      int folderID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      if (parameters.Length < 3)
        return ErrorWrapper.SendError("Command parameters missing.");

      if (!int.TryParse(parameters[1], out folderID))
        return ErrorWrapper.SendError("Cannot parse folder ID.");

      BLClient client = null;

      try
      {
        client = new BLClient();

        client.EditSlideFolder(userID, folderID, parameters[2]);
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
