using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OxigenIIAdvertising.BLClients;
using System.Web.SessionState;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class RemoveSlideContentProcessor : PostCommandProcessor
  {
    public RemoveSlideContentProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(string[] parameters)
    {
      int slideFolderID;
      int slideID;
      int userID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      if (parameters.Length < 3)
        return ErrorWrapper.SendError("Command parameters missing.");

      if (!int.TryParse(parameters[1], out slideFolderID))
        return ErrorWrapper.SendError("Cannot parse Slide Folder ID");

      if (!int.TryParse(parameters[2], out slideID))
        return ErrorWrapper.SendError("Cannot parse Slide ID");

      BLClient client = null;
      bool bCanRemove = false;

      try
      {
        client = new BLClient();

        bCanRemove = client.RemoveSlideContent(userID, slideFolderID, slideID);
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
