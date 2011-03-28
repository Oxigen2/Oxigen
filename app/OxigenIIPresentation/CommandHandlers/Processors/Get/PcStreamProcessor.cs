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
  public class PcStreamProcessor : GetCommandProcessor
  {
    public PcStreamProcessor(HttpSessionState session) : base(session) { }
    
    internal override string Execute(System.Collections.Specialized.NameValueCollection commandParameters)
    {
      int pcID;
      int userID;

      Helper.TryGetUserID(_session, out userID);

      string error = ValidateIntParameter(commandParameters, "pcID", out pcID);

      if (error != String.Empty)
        return error;

      PageChannelData pageChannelData;

      BLClient client = null;
      
      // call WCF BLL Method
      try
      {
        client = new BLClient();

        pageChannelData = client.GetPcStreams(userID, pcID);
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

      return Flatten(pageChannelData);
    }

    private string Flatten(PageChannelData pageChannelData)
    {
      if (pageChannelData == null)
        return String.Empty;

      StringBuilder sb = new StringBuilder();

      sb.Append(pageChannelData.NoPages);
      sb.Append("||");

      foreach (ChannelListChannel channel in pageChannelData.Channels)
      {
        sb.Append(channel.ChannelID);
        sb.Append(",,");
        sb.Append(channel.ChannelName);
        sb.Append(",,");
        sb.Append(System.Configuration.ConfigurationSettings.AppSettings["thumbnailChannelRelativePath"] + channel.ImagePath);
        sb.Append(",,");
        sb.Append(channel.ChannelWeightingUnnormalised);
        sb.Append(",,");

        switch (channel.PrivacyStatus)
        {
          case ChannelPrivacyStatus.Locked:
            sb.Append("1");
            break;
          case ChannelPrivacyStatus.Unlocked:
            sb.Append("2");
            break;
          default:
            sb.Append("0");
            break;
        }
        sb.Append(",,");
        sb.Append(channel.AcceptPasswordRequests ? "1" : "0");
        
        sb.Append("||");
      }

      return sb.ToString().TrimEnd(new char[] { '|' });
    }
  }
}
