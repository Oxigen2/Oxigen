using System.Collections.Generic;
using Oxigen.Core;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
 

namespace Oxigen.ApplicationServices
{
    public interface ITemplateManagementService
    {
        TemplateFormViewModel CreateFormViewModel();
        TemplateFormViewModel CreateFormViewModelFor(int templateId);
        TemplateFormViewModel CreateFormViewModelFor(Template template);
        Template Get(int id);
        IList<Template> GetAll();
        IList<TemplateDto> GetTemplateSummaries();
        ActionConfirmation SaveOrUpdate(Template template);
        ActionConfirmation UpdateWith(Template templateFromForm, int idOfTemplateToUpdate, string fileName, byte[] fileByteArray);
        ActionConfirmation Delete(int id);
        ActionConfirmation Create(Template template, string fileName, byte[] fileByteArray);
        IList<TemplateDto> GetByPublisher(int id);
    }
}
