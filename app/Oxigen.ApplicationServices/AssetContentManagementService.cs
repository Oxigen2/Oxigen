using System.Collections.Generic;
using System;
using SharpArch.Core;
using Oxigen.Core;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;
 

namespace Oxigen.ApplicationServices
{
    public class AssetContentManagementService : IAssetContentManagementService
    {
        public AssetContentManagementService(IAssetContentRepository assetContentRepository) {
            Check.Require(assetContentRepository != null, "assetContentRepository may not be null");

            this.assetContentRepository = assetContentRepository;
        }

        public AssetContent Get(int id) {
            return assetContentRepository.Get(id);
        }

        public IList<AssetContent> GetAll() {
            return assetContentRepository.GetAll();
        }

        public IList<AssetContentDto> GetAssetContentSummaries() {
            return assetContentRepository.GetAssetContentSummaries();
        }

        public AssetContentFormViewModel CreateFormViewModel() {
            AssetContentFormViewModel viewModel = new AssetContentFormViewModel();
            return viewModel;
        }

        public AssetContentFormViewModel CreateFormViewModelFor(int assetContentId) {
            AssetContent assetContent = assetContentRepository.Get(assetContentId);
            return CreateFormViewModelFor(assetContent);
        }

        public AssetContentFormViewModel CreateFormViewModelFor(AssetContent assetContent) {
            AssetContentFormViewModel viewModel = CreateFormViewModel();
            viewModel.AssetContent = assetContent;
            return viewModel;
        }

        public ActionConfirmation SaveOrUpdate(AssetContent assetContent) {
            if (assetContent.IsValid()) {
                assetContentRepository.SaveOrUpdate(assetContent);

                ActionConfirmation saveOrUpdateConfirmation = ActionConfirmation.CreateSuccessConfirmation(
                    "The assetContent was successfully saved.");
                saveOrUpdateConfirmation.Value = assetContent;

                return saveOrUpdateConfirmation;
            }
            else {
                assetContentRepository.DbContext.RollbackTransaction();

                return ActionConfirmation.CreateFailureConfirmation(
                    "The assetContent could not be saved due to missing or invalid information.");
            }
        }

        public ActionConfirmation UpdateWith(AssetContent assetContentFromForm, int idOfAssetContentToUpdate) {
            AssetContent assetContentToUpdate = 
                assetContentRepository.Get(idOfAssetContentToUpdate);
            TransferFormValuesTo(assetContentToUpdate, assetContentFromForm);

            if (assetContentToUpdate.IsValid()) {
                ActionConfirmation updateConfirmation = ActionConfirmation.CreateSuccessConfirmation(
                    "The assetContent was successfully updated.");
                updateConfirmation.Value = assetContentToUpdate;

                return updateConfirmation;
            }
            else {
                assetContentRepository.DbContext.RollbackTransaction();

                return ActionConfirmation.CreateFailureConfirmation(
                    "The assetContent could not be saved due to missing or invalid information.");
            }
        }

        public ActionConfirmation Delete(int id) {
            AssetContent assetContentToDelete = assetContentRepository.Get(id);

            if (assetContentToDelete != null) {
                assetContentRepository.Delete(assetContentToDelete);

                try {
                    assetContentRepository.DbContext.CommitChanges();
                    
                    return ActionConfirmation.CreateSuccessConfirmation(
                        "The assetContent was successfully deleted.");
                }
                catch {
                    assetContentRepository.DbContext.RollbackTransaction();

                    return ActionConfirmation.CreateFailureConfirmation(
                        "A problem was encountered preventing the assetContent from being deleted. " +
                        "Another item likely depends on this assetContent.");
                }
            }
            else {
                return ActionConfirmation.CreateFailureConfirmation(
                    "The assetContent could not be found for deletion. It may already have been deleted.");
            }
        }

        private void TransferFormValuesTo(AssetContent assetContentToUpdate, AssetContent assetContentFromForm) {
		    assetContentToUpdate.Name = assetContentFromForm.Name;
			assetContentToUpdate.Caption = assetContentFromForm.Caption;
			assetContentToUpdate.GUID = assetContentFromForm.GUID;
			assetContentToUpdate.ImageName = assetContentFromForm.ImageName;
			assetContentToUpdate.ImagePath = assetContentFromForm.ImagePath;
			assetContentToUpdate.ImagePathWinFS = assetContentFromForm.ImagePathWinFS;
			assetContentToUpdate.AddDate = assetContentFromForm.AddDate;
			assetContentToUpdate.Creator = assetContentFromForm.Creator;
			assetContentToUpdate.DisplayDuration = assetContentFromForm.DisplayDuration;
			assetContentToUpdate.EditDate = assetContentFromForm.EditDate;
			assetContentToUpdate.Filename = assetContentFromForm.Filename;
			assetContentToUpdate.FilenameExtension = assetContentFromForm.FilenameExtension;
			assetContentToUpdate.FilenameNoPath = assetContentFromForm.FilenameNoPath;
			assetContentToUpdate.Length = assetContentFromForm.Length;
            assetContentToUpdate.PreviewType = assetContentFromForm.PreviewType;
            assetContentToUpdate.SubDir = assetContentFromForm.SubDir;
            assetContentToUpdate.URL = assetContentFromForm.URL;
            assetContentToUpdate.UserGivenDate = assetContentFromForm.UserGivenDate;
        }

        IAssetContentRepository assetContentRepository;
    }
}
