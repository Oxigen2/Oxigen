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
  public class PcStreamAllProcessor : GetCommandProcessor
  {
    public PcStreamAllProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(System.Collections.Specialized.NameValueCollection commandParameters)
    {
      int userID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      BLClient client = null;
      List<List<ChannelListChannel>> pcs = null;

      try
      {
        client = new BLClient();

        pcs = client.GetPCStreamsAll(userID);
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

      return Flatten(pcs);
    }

    private string Flatten(List<List<ChannelListChannel>> pcs)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("[");

      foreach (List<ChannelListChannel> channels in pcs)
      {
        sb.Append("[");

        foreach (ChannelListChannel channel in channels)
        {
          sb.Append("[");

          sb.Append("\"");
          sb.Append(channel.ChannelID);
          sb.Append("\",\"");
          sb.Append(channel.ChannelName.Replace("\"", "||"));
          sb.Append("\",\"");
          sb.Append(System.Configuration.ConfigurationSettings.AppSettings["thumbnailAssetContentRelativePath"] + channel.ImagePath);
          sb.Append("\"");

          sb.Append("],");
        }

        if (channels.Count > 0)
          sb.Remove(sb.Length - 1, 1);

        sb.Append("],");
      }

      if (pcs.Count > 0)
        sb.Remove(sb.Length - 1, 1);

      sb.Append("]");

      return sb.ToString();
    }    
  }
}
