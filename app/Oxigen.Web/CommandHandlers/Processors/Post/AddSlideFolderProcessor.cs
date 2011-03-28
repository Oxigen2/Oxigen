using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class AddSlideFolderProcessor : PostCommandProcessor
  {
    public AddSlideFolderProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(string[] parameters)
    {
      int userID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      if (parameters.Length < 2)
        return ErrorWrapper.SendError("Command parameters missing.");

      BLClient client = null;
      int folderID;

      try
      {
        client = new BLClient();

        folderID = client.AddSlideFolder(userID, parameters[1]);
      }
      catch (Exception ex)
      {
        return ErrorWrapper.SendError(ex.Message);
      }
      finally
      {
        client.Dispose();
      }

      return folderID.ToString();
    }
  }
}
