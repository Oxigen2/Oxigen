using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class SetCategoryProcessor : PostCommandProcessor
  {
    public SetCategoryProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(string[] parameters)
    {
      int userID;
      int streamID;
      int categoryID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      if (!int.TryParse(parameters[1], out streamID))
        return ErrorWrapper.SendError("Parsing Stream ID");

      if (!int.TryParse(parameters[2], out categoryID))
        return ErrorWrapper.SendError("Parsing Category ID");
      
      BLClient client = null;

      try
      {
        client = new BLClient();

        client.SetCategory(userID, streamID, categoryID);
      }
      catch (Exception exception)
      {
        return ErrorWrapper.SendError(exception.Message);
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return "1";
    }
  }
}
