using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using OxigenIIAdvertising.SOAStructures;
using OxigenIIAdvertising.BLClients;
using System.Text;
using System.Web.SessionState;

namespace OxigenIIPresentation.CommandHandlers.Processors.Get
{
  public class SearchProcessor : GetCommandProcessor
  {
    public SearchProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(NameValueCollection commandParameters)
    {
      string keyword;
      int userID;

      if (!Helper.TryGetUserID(_session, out userID))
        userID = -1;

      string error = ValidateStringParameter(commandParameters, "keyword", out keyword);

      if (error != String.Empty)
        return error;

      List<ChannelListChannel> channelList;

      BLClient client = null;

      // call WCF BLL Method
      try
      {
        client = new BLClient();

        channelList = client.Search(userID, keyword);
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

      return Flatten(channelList);
    }

    private string Flatten(List<ChannelListChannel> channelList)
    {
      if (channelList == null)
        return String.Empty;

      StringBuilder sb = new StringBuilder();

      foreach (ChannelListChannel channel in channelList)
      {
        sb.Append(channel.ChannelID);
        sb.Append(",,");
        sb.Append(channel.ChannelName);
        sb.Append(",,");
        sb.Append(System.Configuration.ConfigurationSettings.AppSettings["thumbnailChannelRelativePath"] + channel.ImagePath);
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
        
        sb.Append("||");
      }

      return sb.ToString().TrimEnd(new char[] { '|' });
    }
  }
}
