using System.Collections.Generic;
using Oxigen.Core.Syndication;
using Oxigen.ApplicationServices.ViewModels.Syndication;
using Oxigen.Core.QueryDtos.Syndication;
using Oxigen.Core; 

namespace Oxigen.ApplicationServices.Syndication
{
    public interface IRSSFeedManagementService
    {
        RSSFeedFormViewModel CreateFormViewModel();
        RSSFeedFormViewModel CreateFormViewModelFor(int rSSFeedId);
        RSSFeedFormViewModel CreateFormViewModelFor(RSSFeed rSSFeed);
        RSSFeed Get(int id);
        IList<RSSFeed> GetAll();
        IList<RSSFeedDto> GetRSSFeedSummaries();
        ActionConfirmation SaveOrUpdate(RSSFeed rSSFeed);
        ActionConfirmation UpdateWith(RSSFeed rSSFeedFromForm, int idOfRSSFeedToUpdate);
        ActionConfirmation Delete(int id);
        ActionConfirmation Run(int id);
        ActionConfirmation Refresh();
    }
}
