using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Web.SessionState;
using OxigenIIAdvertising.BLClients;
using OxigenIIAdvertising.SOAStructures;
using System.Text;

namespace OxigenIIPresentation.CommandHandlers.Processors.Get
{
  public class ContentFoldersProcessor : GetCommandProcessor
  {
    public ContentFoldersProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(NameValueCollection commandParameters)
    {
      int userID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;
      
      List<List<CreateContentGenericFolder>> folderList;

      BLClient client = null;

      try
      {
        client = new BLClient();

        folderList = client.GetFolders(userID);
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

      return Flatten(folderList);
    }
    
    private string Flatten(List<List<CreateContentGenericFolder>> folderList)
    {
      if (folderList == null)
        return String.Empty;

      StringBuilder sb = new StringBuilder();

      foreach (List<CreateContentGenericFolder> list in folderList)
      {
        foreach (CreateContentGenericFolder folder in list)
        {
          sb.Append(folder.FolderID);
          sb.Append(",,");
          sb.Append(folder.FolderName);
          sb.Append(",,");
        }

        sb.Replace(",,", "||", sb.Length - 2, 2);
      }

      return sb.ToString().TrimEnd(new char[] { '|' });
    }
  }
}
