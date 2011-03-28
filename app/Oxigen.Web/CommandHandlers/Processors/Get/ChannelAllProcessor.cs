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
  public class ChannelAllProcessor : GetCommandProcessor
  {
    public ChannelAllProcessor(HttpSessionState session) : base(session) { }
   
    internal override string Execute(System.Collections.Specialized.NameValueCollection commandParameters)
    {
      int userID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      BLClient client = null;
      List<ChannelSimple> channels = null;

      try
      {
        client = new BLClient();

        channels = client.GetChannelsAll(userID);
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

      return Flatten(channels);
    }

    private string Flatten(List<ChannelSimple> channels)
    {
      StringBuilder sb = new StringBuilder();

      foreach (ChannelSimple channel in channels)
      {
        sb.Append(channel.ChannelID);
        sb.Append(",,");
        sb.Append(channel.ChannelName);
        sb.Append("((");

        foreach (SlideListSlide slide in channel.Slides)
        {
          sb.Append(slide.SlideID);
          sb.Append(",,");
          sb.Append(slide.SlideName);
          sb.Append(",,");
        }

        sb.Replace(",,", "||", sb.Length - 2, 2);
      }

      return sb.ToString().TrimEnd(new char[] { '|' });
    }
  }
}
