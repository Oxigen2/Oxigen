using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OxigenIIAdvertising.SOAStructures;
using OxigenIIAdvertising.BLClients;
using System.Web.SessionState;
using System.Text;

namespace OxigenIIPresentation.CommandHandlers.Processors.Get
{
  public class RawContentByFolderIDProcessor : GetCommandProcessor
  {
    public RawContentByFolderIDProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(System.Collections.Specialized.NameValueCollection commandParameters)
    {
      int userID;
      int folderID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      string error = ValidateIntParameter(commandParameters, "folderID", out folderID);

      if (error != String.Empty)
        return error;

      PageAssetContentData contentList;

      BLClient client = null;

      // call WCF BLL Method
      try
      {
        client = new BLClient();

        contentList = client.GetRawContent(userID, folderID);
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

      return Flatten(contentList);
    }    

    private string Flatten(PageAssetContentData contentList)
    {
      if (contentList == null)
        return String.Empty;

      StringBuilder sb = new StringBuilder();

      sb.Append(contentList.NoPages);
      sb.Append("||");

      foreach (AssetContentListContent content in contentList.AssetContents)
      {
        sb.Append(content.AssetContentID);
        sb.Append(",,");
        sb.Append(content.Name);
        sb.Append(",,");
        sb.Append(System.Configuration.ConfigurationSettings.AppSettings["thumbnailAssetContentRelativePath"] + content.ImagePath);
        sb.Append("||");
      }

      return sb.ToString().TrimEnd(new char[] { '|' });
    }
  }
}
