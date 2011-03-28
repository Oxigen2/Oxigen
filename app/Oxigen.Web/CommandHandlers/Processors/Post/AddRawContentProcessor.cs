using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OxigenIIAdvertising.BLClients;
using System.Web.SessionState;
using System.Text;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class AddRawContentProcessor : PostCommandProcessor
  {
    public AddRawContentProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(string[] parameters)
    {
      int userID;

      if (!Helper.TryGetUserID(_session, out userID))
        return String.Empty;

      int folderID;
      List<string[]> rawContentInfo;
            
      if (error != "1")
        return error;

      BLClient client = null;
      List<int> addedIDs;

      try
      {
        client = new BLClient();

        addedIDs = client.AddAssetContent(userID, folderID, contentIDList);
      }
      catch (Exception ex)
      {
        return ErrorWrapper.SendError(ex.Message);
      }
      finally
      {
        client.Dispose();
      }

      return Flatten(addedIDs);
    }

    private string Flatten(List<int> addedIDs)
    {
      StringBuilder sb = new StringBuilder();

      foreach (int addedID in addedIDs)
      {
        sb.Append(addedID);
        sb.Append(",,");
      }

      return sb.ToString().TrimEnd(new char[] { ',' });
    }
  }
}
