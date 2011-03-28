using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Collections.Specialized;
using OxigenIIAdvertising.SOAStructures;
using OxigenIIAdvertising.BLClients;
using System.Text;

namespace OxigenIIPresentation.CommandHandlers.Processors.Get
{
  public class UserStreamsProcessor : GetCommandProcessor
  {
    public UserStreamsProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(NameValueCollection commandParameters)
    {
      int folderID;
      int userID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      string error = ValidateIntParameter(commandParameters, "folderID", out folderID);

      if (error != String.Empty)
        return error;

      PageSlideData pageSlideData;

      BLClient client = null;

      // call WCF BLL Method
      try
      {
        client = new BLClient();

        pageSlideData = client.GetUserStreams(userID, folderID);
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

      return Flatten(pageSlideData);
    }

    private string Flatten(PageSlideData pageSlideData)
    {
      if (pageSlideData == null)
        return String.Empty;

      StringBuilder sb = new StringBuilder();

      sb.Append(pageSlideData.NoPages);
      sb.Append(",,");
      sb.Append(System.Configuration.ConfigurationSettings.AppSettings["thumbnailChannelRelativePath"] + pageSlideData.ChannelThumbnailPath);
      
      if (pageSlideData.Slides.Count > 0)
        sb.Append("||");

      foreach (SlideListSlide slide in pageSlideData.Slides)
      {
        sb.Append(slide.SlideID);
        sb.Append(",,");
        sb.Append(slide.SlideName);
        sb.Append(",,");
        sb.Append(System.Configuration.ConfigurationSettings.AppSettings["thumbnailSlideRelativePath"] + slide.ImagePath);
        sb.Append(",,");
        sb.Append(slide.Locked);
        sb.Append("||");
      }

      return sb.ToString().TrimEnd(new char[] { '|' });
    }
  }
}
