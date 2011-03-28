using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class EditChannelThumbnailProcessor : PostCommandProcessor
  {
    public EditChannelThumbnailProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(string[] parameters)
    {
      int userID;
      int channelID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      if (parameters.Length < 3)
        return ErrorWrapper.SendError("Command parameters missing.");

      if (!int.TryParse(parameters[1], out channelID))
        return ErrorWrapper.SendError("Cannot parse channel ID.");

      string thumbnailSlideRelativePath = System.Configuration.ConfigurationSettings.AppSettings["thumbnailSlideRelativePath"];
      int thumbnailSlideRelativePathLength = thumbnailSlideRelativePath.Length;

      string imagePath = parameters[2].Substring(thumbnailSlideRelativePathLength, parameters[2].Length - thumbnailSlideRelativePathLength);

      BLClient client = null;

      try
      {
        client = new BLClient();

        client.EditChannelThumbnail(userID, channelID, imagePath);
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
