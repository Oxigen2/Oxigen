using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.SOAStructures;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class EditChannelProcessor : PostCommandProcessor
  {
    public EditChannelProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(string[] parameters)
    {
      int userID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      if (parameters.Length < 10)
        return ErrorWrapper.SendError("Command parameters missing.");

      int streamID;
      int? categoryID = null;
      bool bPrivate;
      bool bAcceptPasswordRequests;

      string channelName = parameters[3].Trim();
      string description = parameters[4].Trim();
      string longDescription = parameters[5].Trim();
      string keywords = parameters[6].Trim();
      string password = parameters[8].Trim();
      ChannelPrivacyOptions unlockOptions;

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

      if (!int.TryParse(parameters[2].Trim(), out streamID))
        return ErrorWrapper.SendError("Cannot parse stream");

      if (!bool.TryParse(parameters[7], out bPrivate))
        return ErrorWrapper.SendError("Cannot parse channel privacy.");

      if (!bool.TryParse(parameters[9], out bAcceptPasswordRequests))
        return ErrorWrapper.SendError("Cannot parse channel acceptance of password requests.");

      if (parameters.Length > 10)
      {
        switch (parameters[10])
        {
          case "0":
            unlockOptions = ChannelPrivacyOptions.AuthorizeAllFollowers;
            break;
          case "1":
            unlockOptions = ChannelPrivacyOptions.UnauthorizeExistingAuthorizedUsers;
            break;
          case "2":
            unlockOptions = ChannelPrivacyOptions.KeepAuthorizedUsers;
            break;
          default:
            unlockOptions = ChannelPrivacyOptions.Unchanged;
            break;
        }
      }
      else
        unlockOptions = ChannelPrivacyOptions.Unchanged;

      if (bPrivate && string.IsNullOrEmpty(password))
        return "-2";

      BLClient client = null;
      
      try
      {
        client = new BLClient();

        client.EditChannel(userID, streamID, categoryID, channelName, description, longDescription, 
          keywords, bPrivate, password, bAcceptPasswordRequests, unlockOptions);
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
