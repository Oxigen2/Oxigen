using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;
using OxigenIIAdvertising.SOAStructures;
using System.Text;

namespace OxigenIIPresentation.CommandHandlers.Processors.Get
{
  public class SlideContentAllProcessor : GetCommandProcessor
  {
    public SlideContentAllProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(System.Collections.Specialized.NameValueCollection commandParameters)
    {
      int userID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      BLClient client = null;
      List<List<SlideListSlide>> slideFolders = null;

      try
      {
        client = new BLClient();

        slideFolders = client.GetSlideFoldersAll(userID);
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

      return Flatten(slideFolders);
    }

    private string Flatten(List<List<SlideListSlide>> slideFolders)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("[");

      foreach (List<SlideListSlide> slideFolder in slideFolders)
      {
        sb.Append("[");

        foreach (SlideListSlide slide in slideFolder)
        {
          sb.Append("[");

          sb.Append("\"");
          sb.Append(slide.SlideID);
          sb.Append("\",\"");
          sb.Append(slide.SlideName.Replace("\"", "||"));
          sb.Append("\",\"");
          sb.Append(System.Configuration.ConfigurationSettings.AppSettings["thumbnailSlideRelativePath"] + slide.ImagePath);
          sb.Append("\"");

          sb.Append("],");
        }

        if (slideFolder.Count > 0)
          sb.Remove(sb.Length - 1, 1);

        sb.Append("],");
      }

      if (slideFolders.Count > 0)
        sb.Remove(sb.Length - 1, 1);

      sb.Append("]");

      return sb.ToString();
    }
  }
}
