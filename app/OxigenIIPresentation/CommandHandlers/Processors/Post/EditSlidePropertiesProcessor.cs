using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class EditSlidePropertiesProcessor : PostCommandProcessor
  {
    private int _minDisplayDuration;
    private int _maxDisplayDuration;

    public EditSlidePropertiesProcessor(HttpSessionState session) : base(session) 
    {
      _minDisplayDuration = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["minDisplayDuration"]);
      _maxDisplayDuration = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["maxDisplayDuration"]);
    }

    internal override string Execute(string[] parameters)
    {
      int userID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      if (parameters.Length < 8)
        return ErrorWrapper.SendError("Command parameters missing.");

      int slideID;
      DateTime? date = null;
      float displayDuration;

      string trimmedTitle = parameters[2].Trim();
      string trimmedCreator = parameters[3].Trim();
      string trimmedCaption = parameters[4].Trim();
      string trimmedDate = parameters[5].Trim();
      string trimmedURL = parameters[6].Trim().Replace("{a001}", ",,");
      string trimmedDisplayDuration = parameters[7].Trim();

      if (!int.TryParse(parameters[1], out slideID))
        return ErrorWrapper.SendError("Invalid slide ID");

      if (string.IsNullOrEmpty(trimmedTitle))
        return "-4";

      if (!string.IsNullOrEmpty(trimmedDate) && !Helper.NullableDateTryParse(trimmedDate, out date))
        return "-1"; // invalid date

      if (date != null && !Helper.DateWithinLimits(date))
        return "-1"; // invalid date

      if (string.IsNullOrEmpty(trimmedDate))
        date = null;

      if (!string.IsNullOrEmpty(trimmedDisplayDuration) && trimmedDisplayDuration != Resource.UserDefinedDisplayDuration)
      {
        if (!float.TryParse(trimmedDisplayDuration, out displayDuration))
          return "-2"; // invalid display duration

        if (displayDuration < _minDisplayDuration || displayDuration > _maxDisplayDuration)
          return "-3"; // display duration out of bounds
      }
      else
        displayDuration = -1F;

      if (string.IsNullOrEmpty(trimmedCaption))
        trimmedCaption = null;

      if (string.IsNullOrEmpty(trimmedCreator))
        trimmedCreator = null;

      if (string.IsNullOrEmpty(trimmedURL))
        trimmedURL = null;

      BLClient client = null;

      try
      {
        client = new BLClient();

        client.EditSlideContentProperties(userID, slideID, trimmedTitle, trimmedCreator, trimmedCaption,
          date, trimmedURL, displayDuration);
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
