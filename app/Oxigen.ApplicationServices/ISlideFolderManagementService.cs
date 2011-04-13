using System.Collections.Generic;
using Oxigen.Core;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
 

namespace Oxigen.ApplicationServices
{
    public interface ISlideFolderManagementService
    {
        SlideFolderFormViewModel CreateFormViewModel();
        SlideFolderFormViewModel CreateFormViewModelFor(int slideFolderId);
        SlideFolderFormViewModel CreateFormViewModelFor(SlideFolder slideFolder);
        SlideFolder Get(int id);
        IList<SlideFolder> GetAll();
        IList<SlideFolderDto> GetSlideFolderSummaries();
        ActionConfirmation SaveOrUpdate(SlideFolder slideFolder);
        ActionConfirmation UpdateWith(SlideFolder slideFolderFromForm, int idOfSlideFolderToUpdate);
        ActionConfirmation Delete(int id);
        IList<SlideFolderDto> GetByProducer(int producerId);
    }
}
