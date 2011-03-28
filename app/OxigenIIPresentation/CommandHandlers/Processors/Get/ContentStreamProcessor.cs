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
  public class ContentStreamProcessor : GetCommandProcessor
  {
    public ContentStreamProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(System.Collections.Specialized.NameValueCollection commandParameters)
    {
      int streamID;
      int userID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      string error = ValidateIntParameter(commandParameters, "streamID", out streamID);

      if (error != String.Empty)
        return error;

      ChannelProperties contentStream;

      BLClient client = null;

      // call WCF BLL Method
      try
      {
        client = new BLClient();

        contentStream = client.GetChannelProperties(userID, streamID);
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

      return Flatten(contentStream);
    }

    private string Flatten(ChannelProperties channelProperties)
    {
      if (channelProperties == null)
        return String.Empty;

      StringBuilder sb = new StringBuilder();

      sb.Append(channelProperties.CategoryID);
      sb.Append(",,");
      sb.Append(channelProperties.CategoryName);
      sb.Append(",,");
      sb.Append(channelProperties.ChannelID);
      sb.Append(",,");
      sb.Append(channelProperties.Name);
      sb.Append(",,");
      sb.Append(channelProperties.Description);
      sb.Append(",,");
      sb.Append(channelProperties.LongDescription);
      sb.Append(",,");
      sb.Append(channelProperties.Keywords.Replace(",", "|"));
      sb.Append(",,");
      sb.Append(channelProperties.Locked);
      sb.Append(",,");
      sb.Append(channelProperties.ChannelPassword);
      sb.Append(",,");
      sb.Append(channelProperties.AcceptPasswordRequests);
      sb.Append(",,");
      sb.Append(channelProperties.HasAuthorizedUsers);

      return sb.ToString();
    }
  }
}
