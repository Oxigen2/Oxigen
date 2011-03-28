using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OxigenIIAdvertising.SOAStructures;
using System.Text;
using OxigenIIAdvertising.BLClients;
using System.Web.SessionState;

namespace OxigenIIPresentation.CommandHandlers.Processors.Get
{
  public class StreamSlidesProcessor : GetCommandProcessor
  {
    public StreamSlidesProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(System.Collections.Specialized.NameValueCollection commandParameters)
    {
      int folderID;
      int userID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      string error = ValidateIntParameter(commandParameters, "folderID", out folderID);

      if (error != String.Empty)
        return error;

      PageSlideData slideList;

      BLClient client = null;

      // call WCF BLL Method
      try
      {
        client = new BLClient();

        slideList = client.GetStreamSlides(userID, folderID);
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

      return Flatten(slideList);
    }

    private string Flatten(PageSlideData slideList)
    {
      if (slideList == null)
        return String.Empty;

      StringBuilder sb = new StringBuilder();

      sb.Append(slideList.NoPages);
      sb.Append("||");

      foreach (SlideListSlide slide in slideList.Slides)
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
