using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.SOAStructures;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class EditChannelSlidePropertiesProcessor : PostCommandProcessor
  {
    private int _minDisplayDuration;
    private int _maxDisplayDuration;

    public EditChannelSlidePropertiesProcessor(HttpSessionState session) : base(session) 
    {
      _minDisplayDuration = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["minDisplayDuration"]);
      _maxDisplayDuration = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["maxDisplayDuration"]);
    }

    internal override string Execute(string[] parameters)
    {
      int userID;
      int slideID;
      string url;
      string trimmedDisplayDuration;
      float displayDuration;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      int parametersLength = parameters.Length;

      if (parametersLength < 5)
        return ErrorWrapper.SendError("Command parameters missing.");

      if (!int.TryParse(parameters[1], out slideID))
        return ErrorWrapper.SendError("Invalid Slide ID");

      url = parameters[2].Trim().Replace("{a001}", ",,");
      trimmedDisplayDuration = parameters[3].Trim();

      if (string.IsNullOrEmpty(url))
        url = null;

      if (!string.IsNullOrEmpty(trimmedDisplayDuration) && trimmedDisplayDuration != Resource.UserDefinedDisplayDuration)
      {
        if (!float.TryParse(trimmedDisplayDuration, out displayDuration))
          return "-1";

        if (displayDuration < _minDisplayDuration || displayDuration > _maxDisplayDuration)
          return "-3"; // display duration out of bounds
      }
      else
        displayDuration = -1F;

      string[] startEndDateTimes = null;

      if (!Validate(parametersLength, parameters, ref startEndDateTimes))
        return "-2";

      BLClient client = null;

      try
      {
        client = new BLClient();

        client.EditChannelSlideProperties(userID, slideID, url, displayDuration, parameters[4], startEndDateTimes);
      }
      catch (Exception ex)
      {
        return ErrorWrapper.SendError(ex.ToString());
      }
      finally
      {
        client.Dispose();
      }

      return "1";
    }

    private bool Validate(int parametersLength, string[] parameters, ref string[] startEndDateTimes)
    {
      if (parametersLength > 5)
      {
        startEndDateTimes = new string[parametersLength - 5];

        for (int i = 5; i < parametersLength; i++)
        {
          string[] dateTimesIndividual = parameters[i].Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);

          foreach (string dateTime in dateTimesIndividual)
          {
            DateTime dt;

            if (!string.IsNullOrEmpty(dateTime) && !DateTime.TryParse(dateTime, out dt))
              return false;
          }

          startEndDateTimes[i - 5] = parameters[i];
        }
      }

      return true;
    }
  }
}
