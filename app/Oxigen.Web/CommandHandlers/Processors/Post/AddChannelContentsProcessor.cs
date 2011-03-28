using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class AddChannelContentsProcessor : PostCommandProcessor
  {
    private int _minDisplayDuration;
    private int _maxDisplayDuration;

    public AddChannelContentsProcessor(HttpSessionState session) : base(session) 
    {
      _minDisplayDuration = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["minDisplayDuration"]);
      _maxDisplayDuration = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["maxDisplayDuration"]);
    }

    internal override string Execute(string[] parameters)
    {
      int channelID;
      string trimmedURL = null;
      string trimmedDisplayDuration = null;
      float displayDuration;
      int userID;
      DateTime date;
      List<int> slideIDList;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      if (parameters.Length < 10)
        return ErrorWrapper.SendError("Command parameters missing.");

      if (!int.TryParse(parameters[1], out channelID))
        return ErrorWrapper.SendError("Cannot parse stream ID.");

      trimmedURL = parameters[2].Trim().Replace("{a001}", ",,");
      trimmedDisplayDuration = parameters[3].Trim();

      if (trimmedURL.StartsWith("["))
        trimmedURL = "-2"; // copy from slides
      else if (string.IsNullOrEmpty(trimmedURL))
        trimmedURL = null;

      if (trimmedDisplayDuration.StartsWith("["))
        displayDuration = -2F; // copy from slides
      else if (!string.IsNullOrEmpty(trimmedDisplayDuration))
      {
        if (!float.TryParse(trimmedDisplayDuration, out displayDuration))
          return "-1";

        if (displayDuration < _minDisplayDuration || displayDuration > _maxDisplayDuration)
          return "-3"; // display duration out of bounds
      }
      else
        displayDuration = -1F;

      for (int i = 4; i < 8; i++ )
      {
        if (!string.IsNullOrEmpty(parameters[i]) && !DateTime.TryParse(parameters[i], out date))
          return "-2";
      }

      string error = Helper.GetContentIDs(parameters[9], out slideIDList);

      if (error != "1")
        return error;

      BLClient client = null;

      try
      {
        client = new BLClient();

        client.AddChannelContent(userID, channelID, trimmedURL, displayDuration, parameters[4].Trim(),
          parameters[5].Trim(), parameters[6].Trim(), parameters[7].Trim(), parameters[8], slideIDList);
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
