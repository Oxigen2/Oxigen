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
  public class RawContentAllProcessor : GetCommandProcessor
  {
    public RawContentAllProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(System.Collections.Specialized.NameValueCollection commandParameters)
    {
      int userID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      BLClient client = null;
      List<List<AssetContentListContent>> assetContentFolders = null;

      try
      {
        client = new BLClient();

        assetContentFolders = client.GetAssetContentAll(userID);
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

      return Flatten(assetContentFolders);
    }

    private string Flatten(List<List<AssetContentListContent>> assetContentFolders)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("[");

      foreach (List<AssetContentListContent> assetContentFolder in assetContentFolders)
      {
        sb.Append("[");

        foreach (AssetContentListContent assetContent in assetContentFolder)
        {
          sb.Append("[");

          sb.Append("\"");
          sb.Append(assetContent.AssetContentID);
          sb.Append("\",\"");
          sb.Append(assetContent.Name.Replace("\"", "||"));
          sb.Append("\",\"");
          sb.Append(System.Configuration.ConfigurationSettings.AppSettings["thumbnailAssetContentRelativePath"] + assetContent.ImagePath);
          sb.Append("\"");

          sb.Append("],");
        }

        if (assetContentFolder.Count > 0)
          sb.Remove(sb.Length - 1, 1);

        sb.Append("],");
      }

      if (assetContentFolders.Count > 0)
        sb.Remove(sb.Length - 1, 1);
      
      sb.Append("]");

      return sb.ToString();
    }
  }
}
