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
  public class RawContentProcessor : GetCommandProcessor
  {
    public RawContentProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(System.Collections.Specialized.NameValueCollection commandParameters)
    {
      int userID;
      int contentID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      string error = ValidateIntParameter(commandParameters, "contentID", out contentID);

      if (error != String.Empty)
        return error;

      AssetContentProperties contentProperties = null;
      BLClient client = null;

      // call WCF BLL Method
      try
      {
        client = new BLClient();

        contentProperties = client.GetRawContentProperties(userID, contentID);
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

      return Flatten(contentProperties);
    }

    private string Flatten(AssetContentProperties contentProperties)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append(contentProperties.Name);
      sb.Append(",,");
      sb.Append(contentProperties.Creator);
      sb.Append(",,");
      sb.Append(contentProperties.Caption);
      sb.Append(",,");
      sb.Append(contentProperties.UserGivenDate.HasValue ? contentProperties.UserGivenDate.Value.ToShortDateString() : "");
      sb.Append(",,");
      sb.Append(contentProperties.URL.Replace(",,", "{a001}"));
      sb.Append(",,");

      if (contentProperties.DisplayDuration == -1F)
        sb.Append(Resource.UserDefinedDisplayDuration);
      else
        sb.Append(contentProperties.DisplayDuration);    

      return sb.ToString().TrimEnd(new char[] { '|' });
    }
  }
}
