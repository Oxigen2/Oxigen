using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;
using System.Text;
using OxigenIIAdvertising.SOAStructures;

namespace OxigenIIPresentation.CommandHandlers.Processors.Get
{
  public class GetPreviewProcessor : GetCommandProcessor
  {
    public GetPreviewProcessor(HttpSessionState session) : base(session) { }

    OxigenIIAdvertising.LoggerInfo.Logger log = new OxigenIIAdvertising.LoggerInfo.Logger("Preview", @"E:\Client Sites\OxigenIIAdvertisingSystem\Debug.txt", OxigenIIAdvertising.LoggerInfo.LoggingMode.Debug);

    internal override string Execute(System.Collections.Specialized.NameValueCollection commandParameters)
    {
      int mediaID;
      int userID = -1;
      string subDir;
      string[] files;
      PreviewType previewType;

      string error = ValidateIntParameter(commandParameters, "ID", out mediaID);

      if (error != String.Empty)
        return error;

      if (!MediaTypeValid(commandParameters["type"]))
        return ErrorWrapper.SendError("Invalid Type");
      
      string mediaType = commandParameters["type"];

      log.WriteMessage("1");

      // for raw content and slide content, also check if the user is logged in.
      if (mediaType != "C")
      {
        if (!Helper.TryGetUserID(_session, out userID))
          return String.Empty;
      }

      BLClient client = null;

      try
      {
        client = new BLClient();

        client.GetPreviewFrames(userID, mediaType, mediaID, out subDir, out files, out previewType);
      }
      catch (Exception exception)
      {
        log.WriteMessage(exception.ToString());
        return ErrorWrapper.SendError(exception.Message);
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      if (files == null)
      {
        log.WriteMessage("files is null");
        return "A,,/ViewMedia.aspx?noPreview=true";
      }

      log.WriteMessage(Flatten(subDir, files, mediaType, previewType));

      return Flatten(subDir, files, mediaType, previewType);
    }

    private string Flatten(string subDir, string[] files, string mediaType, PreviewType previewType)
    {
      StringBuilder sb = new StringBuilder();
      
      if (previewType == PreviewType.Flash)
        sb.Append("F,,");
      else
        sb.Append("A,,");

      foreach (string file in files)
      {
        sb.Append("/ViewMedia.aspx?mediaType=");
        sb.Append(mediaType);
        sb.Append("&previewType=");
        sb.Append(previewType.ToString());
        sb.Append("&fileName=");
        sb.Append(file);
        sb.Append("&subDir=");
        sb.Append(subDir);
        sb.Append("||");
      }

      return sb.Remove(sb.Length - 2, 2).ToString();
    }

    private bool MediaTypeValid(string s)
    {
      if (s != "R" && s != "S" && s != "C")
        return false;

      return true;
    }
  }
}
