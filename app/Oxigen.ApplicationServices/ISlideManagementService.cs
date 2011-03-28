using System.Collections.Generic;
using Oxigen.Core;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
 

namespace Oxigen.ApplicationServices
{
    public interface ISlideManagementService
    {
        SlideFormViewModel CreateFormViewModel();
        SlideFormViewModel CreateFormViewModelFor(int slideId);
        SlideFormViewModel CreateFormViewModelFor(Slide slide);
        Slide Get(int id);
        IList<Slide> GetAll();
        IList<SlideDto> GetSlideSummaries();
        ActionConfirmation SaveOrUpdate(Slide slide);
        ActionConfirmation UpdateWith(Slide slideFromForm, int idOfSlideToUpdate);
        ActionConfirmation Delete(int id);
        ActionConfirmation CreateFromTemplate(int userId, int slidefolderId, IList<int> assetContentIds, int templateId, string caption, string credit);
    }
}
