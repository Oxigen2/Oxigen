using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Microsoft.Practices.ServiceLocation;
using Oxigen.ApplicationServices;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation.CommandHandlers.Processors.Post
{
  public class TemplateConvertProcessor : PostCommandProcessor
  {
    public TemplateConvertProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(string[] parameters)
    {
        int userID;
        List<int> contentIDList = null;
        int folderId;
        int templateId;
        string field1;
        string field2;

        string error;

        if (!Helper.TryGetUserID(_session, out userID))
            return String.Empty;
        if (!int.TryParse(parameters[1], out folderId))
            return ErrorWrapper.SendError("Invalid FolderId");
        if (!int.TryParse(parameters[2], out templateId))
            return ErrorWrapper.SendError("Invalid Template ID");
        field1 = parameters[3];
        field2 = parameters[4];

        error = Helper.GetContentIDs(parameters[5], out contentIDList);
        if (error != "1") return error;
        if (templateId == 0)
        {
            var client = new BLClient();
            client.AddSlideContent(userID, folderId, contentIDList);            
        }
        else
        {
            var slideManagementService = ServiceLocator.Current.GetInstance<ISlideManagementService>();
            slideManagementService.CreateFromTemplate(userID, folderId, contentIDList, templateId, field1,
                                                      field2);
           
        }
 
      return "1";
    }
  }
}
