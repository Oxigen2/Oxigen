using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using OxigenIIAdvertising.SOAStructures;
using System.Text;
using OxigenIIAdvertising.BLClients;
using System.Web.SessionState;

namespace OxigenIIPresentation.CommandHandlers.Processors.Get
{
  public class ChannelDetailsProcessor : GetCommandProcessor
  {
    public ChannelDetailsProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(NameValueCollection commandParameters)
    {
      int channelID;

      string error = ValidateIntParameter(commandParameters, "channelId", out channelID);

      if (error != string.Empty)
        return error;

      Channel channel = null;

      BLClient client = null;

      try
      {
        client = new BLClient();

        channel = client.GetChannelDetailsSimple(channelID);
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

      return Flatten(channel);
    }

    private string Flatten(Channel channel)
    {
      if (channel == null)
        return String.Empty;

      StringBuilder sb = new StringBuilder();

      sb.Append(string.IsNullOrEmpty(channel.ChannelDescription) ? "No Description Available" : channel.ChannelDescription);
      sb.Append(",,");
      sb.Append(channel.AddDate.ToShortDateString());
      sb.Append(",,");
      sb.Append(string.IsNullOrEmpty(channel.PublisherDisplayName) ? "-" : channel.PublisherDisplayName);
      sb.Append(",,");
      sb.Append(channel.NoContents);
      sb.Append(",,");
      sb.Append(channel.NoFollowers);

      return sb.ToString();
    }
  }
}
