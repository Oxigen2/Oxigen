using System.Collections.Generic;
using Oxigen.Core;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
 

namespace Oxigen.ApplicationServices
{
    public interface IPublisherManagementService
    {
        PublisherFormViewModel CreateFormViewModel();
        PublisherFormViewModel CreateFormViewModelFor(int publisherId);
        PublisherFormViewModel CreateFormViewModelFor(Publisher publisher);
        Publisher Get(int id);
        IList<Publisher> GetAll();
        IList<PublisherDto> GetPublisherSummaries();
        ActionConfirmation SaveOrUpdate(Publisher publisher);
        ActionConfirmation UpdateWith(Publisher publisherFromForm, int idOfPublisherToUpdate);
        ActionConfirmation Delete(int id);
        Publisher GetByUserId(int userId);
    }
}
