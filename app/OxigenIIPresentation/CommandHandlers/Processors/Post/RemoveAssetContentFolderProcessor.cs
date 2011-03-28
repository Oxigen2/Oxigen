using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;
using OxigenIIAdvertising.SOAStructures;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class RemoveAssetContentFolderProcessor : PostCommandProcessor
  {
    public RemoveAssetContentFolderProcessor(HttpSessionState session) : base(session) { }

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
      int filesLength = -1;

      try
      {
        client = new BLClient();

        filesLength = client.RemoveAssetContentFolder(userID, folderID);
      }
      catch (Exception ex)
      {
        return ErrorWrapper.SendError(ex.Message);
      }
      finally
      {
        client.Dispose();
      }

      ((User)_session["User"]).UsedBytes -= filesLength;

      return "1";
    }
  }
}
