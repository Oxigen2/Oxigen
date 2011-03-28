using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;
using OxigenIIAdvertising.SOAStructures;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class RemoveAssetContentProcessor : PostCommandProcessor
  {
    public RemoveAssetContentProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(string[] parameters)
    {
      int userID;
      int assetContentFolderID;
      int assetContentID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      if (parameters.Length < 3)
        return ErrorWrapper.SendError("Command parameters missing.");

      if (!int.TryParse(parameters[1], out assetContentFolderID))
        return ErrorWrapper.SendError("Cannot parse raw content folder ID.");

      if (!int.TryParse(parameters[2], out assetContentID))
        return ErrorWrapper.SendError("Cannot parse raw content ID.");

      BLClient client = null;
      int fileLength = -1;

      try
      {
        client = new BLClient();

        fileLength = client.RemoveAssetContent(userID, assetContentFolderID, assetContentID);
      }
      catch (Exception ex)
      {
        return ErrorWrapper.SendError(ex.Message);
      }
      finally
      {
        client.Dispose();
      }

      if (fileLength != -1)
        ((User)_session["User"]).UsedBytes -= fileLength;

      return "1";
    }
  }
}
