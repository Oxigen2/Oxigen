using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class AddChannelProcessor : PostCommandProcessor
  {
    public AddChannelProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(string[] parameters)
    {
      int userID;
      int? categoryID = null;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      if (parameters.Length < 9)
        return ErrorWrapper.SendError("Command parameters missing.");

      bool bPrivate;
      bool bAcceptPasswordRequests;

      string channelName = parameters[2].Trim();
      string description = parameters[3].Trim();
      string longDescription = parameters[4].Trim();
      string keywords = parameters[5].Trim();
      string password = parameters[7].Trim();

      if (string.IsNullOrEmpty(channelName))
        return "-1";

      if (string.IsNullOrEmpty(description))
        description = null;

      if (string.IsNullOrEmpty(longDescription))
        longDescription = null;

      if (string.IsNullOrEmpty(keywords))
        keywords = null;
      else
        keywords = keywords.Replace("|", ",");

      if (string.IsNullOrEmpty(password))
        password = null;

      if (!Helper.NullableIntTryParse(parameters[1], out categoryID))
        return ErrorWrapper.SendError("Cannot parse category.");

      if (categoryID == -1)
        categoryID = null;

      if (!bool.TryParse(parameters[6], out bPrivate))
        return ErrorWrapper.SendError("Cannot parse channel privacy.");

      if (!bool.TryParse(parameters[8], out bAcceptPasswordRequests))
        return ErrorWrapper.SendError("Cannot parse channel acceptance of password requests.");

      if (bPrivate && string.IsNullOrEmpty(password))
        return "-2";

      BLClient client = null;

      try
      {
        client = new BLClient();

        client.AddChannel(userID, categoryID, channelName, description, longDescription, keywords, bPrivate, password, bAcceptPasswordRequests);
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
