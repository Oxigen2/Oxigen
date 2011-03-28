using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class EditChannelWeightingProcessor : PostCommandProcessor
  {
    public EditChannelWeightingProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(string[] parameters)
    {
      int userID;
      int pcID;
      int channelID;
      int channelWeighting;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      if (parameters.Length < 4)
        return ErrorWrapper.SendError("Command parameters missing.");

      if (!int.TryParse(parameters[1], out pcID))
        return ErrorWrapper.SendError("Cannot parse PC ID.");

      if (!int.TryParse(parameters[2], out channelID))
        return ErrorWrapper.SendError("Cannot parse Channel ID.");

      if (!int.TryParse(parameters[3], out channelWeighting))
        return ErrorWrapper.SendError("Cannot parse Channel Weighting.");

      BLClient client = null;

      try
      {
        client = new BLClient();

        client.EditChannelWeighting(userID, pcID, channelID, channelWeighting);
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
