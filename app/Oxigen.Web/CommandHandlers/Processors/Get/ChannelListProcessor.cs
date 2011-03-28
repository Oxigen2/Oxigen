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
  public class ChannelListProcessor : GetCommandProcessor
  {
    public ChannelListProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(NameValueCollection commandParameters)
    {
      int categoryID;
      int startPageNo;
      int endPageNo;
      string sortBy;
      string sortByEnumString;
      int userID;

      if (!Helper.TryGetUserID(_session, out userID))
        userID = -1;

      string error = ValidateIntParameter(commandParameters, "categoryId", out categoryID);

      if (error != String.Empty)
        return error;

      error = ValidateIntParameter(commandParameters, "startPageNo", out startPageNo);

      if (error != String.Empty)
        return error;

      error = ValidateIntParameter(commandParameters, "endPageNo", out endPageNo);

      if (error != String.Empty)
        return error;

      error = ValidateStringParameter(commandParameters, "sortBy", out sortBy);

      if (error != String.Empty)
        return error;

      error = GetSortChannelsByFromConfig(sortBy, out sortByEnumString);

      if (error != String.Empty)
        return error;

      // validate Sort By enum
      SortChannelsBy sortChannelsBy;

      error = TryParseSortChannelsBy(typeof(SortChannelsBy), sortByEnumString, out sortChannelsBy);

      if (error != String.Empty)
        return error;

      PageChannelData pageChannelData;

      BLClient client = null;

      // call WCF BLL Method
      try
      {
        client = new BLClient();

        pageChannelData = client.GetChannelListByCategoryID(userID, categoryID, startPageNo, endPageNo, sortChannelsBy);
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

    private string GetSortChannelsByFromConfig(string sortByValue, out string sortByEnumString)
    {
      QueryStringParameterValueConfiguration queryStringParameterValueConfiguration = (QueryStringParameterValueConfiguration)System.Configuration.ConfigurationManager.GetSection("queryStringParameterGroup/enumQueryStringParameterSet");

      if (queryStringParameterValueConfiguration.QueryStringParameters[sortByValue] == null)
      {
        sortByEnumString = String.Empty;
        return ErrorWrapper.SendError("Enum parameter " + sortByValue + " does not exist.");
      }

      sortByEnumString = queryStringParameterValueConfiguration.QueryStringParameters[sortByValue].Value;

      return String.Empty;
    }

    private string TryParseSortChannelsBy(Type type, string sortBy, out SortChannelsBy sortChannelBy)
    {
      try
      {
        sortChannelBy = (SortChannelsBy)Enum.Parse(type, sortBy);
      }
      catch
      {
        sortChannelBy = SortChannelsBy.Alphabetical;
        return ErrorWrapper.SendError(sortBy + " cannot be parsed.");
      }

      return String.Empty;
    }
  }
}
