using System.Collections.Generic;
using System;
using SharpArch.Core;
using Oxigen.Core;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;
 

namespace Oxigen.ApplicationServices
{
    public class SlideFolderManagementService : ISlideFolderManagementService
    {
        public SlideFolderManagementService(ISlideFolderRepository slideFolderRepository) {
            Check.Require(slideFolderRepository != null, "slideFolderRepository may not be null");

            this.slideFolderRepository = slideFolderRepository;
        }

        public SlideFolder Get(int id) {
            return slideFolderRepository.Get(id);
        }

        public IList<SlideFolder> GetAll() {
            return slideFolderRepository.GetAll();
        }

        public IList<SlideFolderDto> GetSlideFolderSummaries() {
            return slideFolderRepository.GetSlideFolderSummaries();
        }

        public SlideFolderFormViewModel CreateFormViewModel() {
            SlideFolderFormViewModel viewModel = new SlideFolderFormViewModel();
            return viewModel;
        }

        public SlideFolderFormViewModel CreateFormViewModelFor(int slideFolderId) {
            SlideFolder slideFolder = slideFolderRepository.Get(slideFolderId);
            return CreateFormViewModelFor(slideFolder);
        }

        public SlideFolderFormViewModel CreateFormViewModelFor(SlideFolder slideFolder) {
            SlideFolderFormViewModel viewModel = CreateFormViewModel();
            viewModel.SlideFolder = slideFolder;
            return viewModel;
        }

        public ActionConfirmation SaveOrUpdate(SlideFolder slideFolder) {
            if (slideFolder.IsValid()) {
                slideFolderRepository.SaveOrUpdate(slideFolder);

                ActionConfirmation saveOrUpdateConfirmation = ActionConfirmation.CreateSuccessConfirmation(
                    "The slideFolder was successfully saved.");
                saveOrUpdateConfirmation.Value = slideFolder;

                return saveOrUpdateConfirmation;
            }
            else {
                slideFolderRepository.DbContext.RollbackTransaction();

                return ActionConfirmation.CreateFailureConfirmation(
                    "The slideFolder could not be saved due to missing or invalid information.");
            }
        }

        public ActionConfirmation UpdateWith(SlideFolder slideFolderFromForm, int idOfSlideFolderToUpdate) {
            SlideFolder slideFolderToUpdate = 
                slideFolderRepository.Get(idOfSlideFolderToUpdate);
            TransferFormValuesTo(slideFolderToUpdate, slideFolderFromForm);

            if (slideFolderToUpdate.IsValid()) {
                ActionConfirmation updateConfirmation = ActionConfirmation.CreateSuccessConfirmation(
                    "The slideFolder was successfully updated.");
                updateConfirmation.Value = slideFolderToUpdate;

                return updateConfirmation;
            }
            else {
                slideFolderRepository.DbContext.RollbackTransaction();

                return ActionConfirmation.CreateFailureConfirmation(
                    "The slideFolder could not be saved due to missing or invalid information.");
            }
        }

        public ActionConfirmation Delete(int id) {
            SlideFolder slideFolderToDelete = slideFolderRepository.Get(id);

            if (slideFolderToDelete != null) {
                slideFolderRepository.Delete(slideFolderToDelete);

                try {
                    slideFolderRepository.DbContext.CommitChanges();
                    
                    return ActionConfirmation.CreateSuccessConfirmation(
                        "The slideFolder was successfully deleted.");
                }
                catch {
                    slideFolderRepository.DbContext.RollbackTransaction();

                    return ActionConfirmation.CreateFailureConfirmation(
                        "A problem was encountered preventing the slideFolder from being deleted. " +
                        "Another item likely depends on this slideFolder.");
                }
            }
            else {
                return ActionConfirmation.CreateFailureConfirmation(
                    "The slideFolder could not be found for deletion. It may already have been deleted.");
            }
        }

        private void TransferFormValuesTo(SlideFolder slideFolderToUpdate, SlideFolder slideFolderFromForm) {
		    slideFolderToUpdate.SlideFolderName = slideFolderFromForm.SlideFolderName;
			slideFolderToUpdate.Publisher = slideFolderFromForm.Publisher;
        }

        ISlideFolderRepository slideFolderRepository;
    }
}
