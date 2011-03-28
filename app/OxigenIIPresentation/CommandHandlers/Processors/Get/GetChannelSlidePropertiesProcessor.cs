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
  public class GetChannelSlidePropertiesProcessor : GetCommandProcessor
  {
    public GetChannelSlidePropertiesProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(System.Collections.Specialized.NameValueCollection commandParameters)
    {
      int channelsSlideID;
      int userID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      string error = ValidateIntParameter(commandParameters, "slideID", out channelsSlideID);

      if (error != String.Empty)
        return error;

      ChannelSlideProperties channelSlideProperties = null;

      BLClient client = null;

      // call WCF BLL Method
      try
      {
        client = new BLClient();

        channelSlideProperties = client.GetChannelSlideProperties(userID, channelsSlideID);
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

      return Flatten(channelSlideProperties);
    }

    private string Flatten(ChannelSlideProperties channelSlideProperties)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("[[\"");
      sb.Append(channelSlideProperties.URL.Replace(",,", "{a001}"));
      sb.Append("\",\"");

      if (channelSlideProperties.DisplayDuration == -1F)
        sb.Append(Resource.UserDefinedDisplayDuration);
      else
        sb.Append(channelSlideProperties.DisplayDuration);     
      
      sb.Append("\"],");
      sb.Append(channelSlideProperties.PresentationSchedule);
      sb.Append("]");

      return sb.ToString();
    }
  }
}
