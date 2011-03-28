using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.SOAStructures;
using OxigenIIAdvertising.BLClients;
using System.Text;

namespace OxigenIIPresentation.CommandHandlers.Processors.Get
{
  public class GetSlidePropertiesProcessor : GetCommandProcessor
  {
    public GetSlidePropertiesProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(System.Collections.Specialized.NameValueCollection commandParameters)
    {
      int slideID;
      int userID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      string error = ValidateIntParameter(commandParameters, "slideID", out slideID);

      if (error != String.Empty)
        return error;

      SlideProperties slideProperties;

      BLClient client = null;

      // call WCF BLL Method
      try
      {
        client = new BLClient();

        slideProperties = client.GetSlideProperties(userID, slideID);
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

      return Flatten(slideProperties);
    }

    private string Flatten(SlideProperties slideProperties)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append(slideProperties.Name);
      sb.Append(",,");
      sb.Append(slideProperties.Creator);
      sb.Append(",,");
      sb.Append(slideProperties.Caption);
      sb.Append(",,");
      sb.Append(slideProperties.UserGivenDate.HasValue ? slideProperties.UserGivenDate.Value.ToShortDateString() : "");
      sb.Append(",,");
      sb.Append(slideProperties.URL.Replace(",,", "{a001}"));
      sb.Append(",,");

      if (slideProperties.DisplayDuration == -1F)
        sb.Append(Resource.UserDefinedDisplayDuration);
      else
        sb.Append(slideProperties.DisplayDuration);

      return sb.ToString();
    }
  }
}
