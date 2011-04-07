using System.Collections.Generic;
using System;
using System.IO;
using SharpArch.Core;
using Oxigen.Core;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;
 

namespace Oxigen.ApplicationServices
{
    public class TemplateManagementService : ITemplateManagementService
    {
        public TemplateManagementService(ITemplateRepository templateRepository) {
            Check.Require(templateRepository != null, "templateRepository may not be null");

            this.templateRepository = templateRepository;
        }

        public Template Get(int id) {
            return templateRepository.Get(id);
        }

        public IList<Template> GetAll() {
            return templateRepository.GetAll();
        }

        public IList<TemplateDto> GetTemplateSummaries() {
            return templateRepository.GetTemplateSummaries();
        }

        public TemplateFormViewModel CreateFormViewModel() {
            TemplateFormViewModel viewModel = new TemplateFormViewModel();
            return viewModel;
        }

        public TemplateFormViewModel CreateFormViewModelFor(int templateId) {
            Template template = templateRepository.Get(templateId);
            return CreateFormViewModelFor(template);
        }

        public TemplateFormViewModel CreateFormViewModelFor(Template template) {
            TemplateFormViewModel viewModel = CreateFormViewModel();
            viewModel.Template = template;
            return viewModel;
        }

        public ActionConfirmation SaveOrUpdate(Template template) {
            if (template.IsValid()) {
                templateRepository.SaveOrUpdate(template);

                ActionConfirmation saveOrUpdateConfirmation = ActionConfirmation.CreateSuccessConfirmation(
                    "The template was successfully saved.");
                saveOrUpdateConfirmation.Value = template;

                return saveOrUpdateConfirmation;
            }
            else {
                templateRepository.DbContext.RollbackTransaction();

                return ActionConfirmation.CreateFailureConfirmation(
                    "The template could not be saved due to missing or invalid information.");
            }
        }

        public ActionConfirmation UpdateWith(Template templateFromForm, int idOfTemplateToUpdate, string fileName, byte[] fileByteArray) {
            Template templateToUpdate = 
                templateRepository.Get(idOfTemplateToUpdate);
            TransferFormValuesTo(templateToUpdate, templateFromForm);

            if (fileByteArray != null) 
                SaveFile(templateToUpdate, fileName, fileByteArray);

            if (templateToUpdate.IsValid()) {
                ActionConfirmation updateConfirmation = ActionConfirmation.CreateSuccessConfirmation(
                    "The template was successfully updated.");
                updateConfirmation.Value = templateToUpdate;

                return updateConfirmation;
            }
            else {
                templateRepository.DbContext.RollbackTransaction();

                return ActionConfirmation.CreateFailureConfirmation(
                    "The template could not be saved due to missing or invalid information.");
            }
        }

        public ActionConfirmation Delete(int id) {
            Template templateToDelete = templateRepository.Get(id);

            if (templateToDelete != null) {
                templateRepository.Delete(templateToDelete);

                try {
                    templateRepository.DbContext.CommitChanges();
                    
                    return ActionConfirmation.CreateSuccessConfirmation(
                        "The template was successfully deleted.");
                }
                catch {
                    templateRepository.DbContext.RollbackTransaction();

                    return ActionConfirmation.CreateFailureConfirmation(
                        "A problem was encountered preventing the template from being deleted. " +
                        "Another item likely depends on this template.");
                }
            }
            else {
                return ActionConfirmation.CreateFailureConfirmation(
                    "The template could not be found for deletion. It may already have been deleted.");
            }
        }

        public ActionConfirmation Create(Template template, string fileName, byte[] fileByteArray)
        {
            Template newTemplate = new Template(".swf");
            SaveFile(newTemplate, fileName, fileByteArray);
            newTemplate.MetaData = template.MetaData;
            newTemplate.Publisher = template.Publisher;
            templateRepository.SaveOrUpdate(newTemplate);
            return ActionConfirmation.CreateSuccessConfirmation("The template was successfully uploaded");

        }

        private void SaveFile(Template template, string fileName, byte[] fileByteArray)
        {
            FileStream fs = new FileStream(template.FileFullPathName, FileMode.Create, FileAccess.Write);
            fs.Write(fileByteArray, 0, fileByteArray.Length);
            fs.Close();
            template.Name = Path.GetFileNameWithoutExtension(fileName);
        }

        private void TransferFormValuesTo(Template templateToUpdate, Template templateFromForm) {
		    templateToUpdate.MetaData = templateFromForm.MetaData;
            templateToUpdate.Publisher = templateFromForm.Publisher;
        }

        ITemplateRepository templateRepository;
    }
}
