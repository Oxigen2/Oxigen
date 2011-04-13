using System.Collections.Generic;
using System;
using SharpArch.Core;
using Oxigen.Core.Syndication;
using Oxigen.ApplicationServices.ViewModels.Syndication;
using Oxigen.Core.QueryDtos.Syndication;
using Oxigen.Core.RepositoryInterfaces.Syndication;
using Oxigen.Core; 

namespace Oxigen.ApplicationServices.Syndication
{
    public class RSSFeedManagementService : IRSSFeedManagementService
    {
        public RSSFeedManagementService(IRSSFeedRepository rSSFeedRepository) {
            Check.Require(rSSFeedRepository != null, "rSSFeedRepository may not be null");

            this.rSSFeedRepository = rSSFeedRepository;
        }

        public RSSFeed Get(int id) {
            return rSSFeedRepository.Get(id);
        }

        public IList<RSSFeed> GetAll() {
            return rSSFeedRepository.GetAll();
        }

        public IList<RSSFeedDto> GetRSSFeedSummaries() {
            return rSSFeedRepository.GetRSSFeedSummaries();
        }

        public RSSFeedFormViewModel CreateFormViewModel() {
            RSSFeedFormViewModel viewModel = new RSSFeedFormViewModel();
            return viewModel;
        }

        public RSSFeedFormViewModel CreateFormViewModelFor(int rSSFeedId) {
            RSSFeed rSSFeed = rSSFeedRepository.Get(rSSFeedId);
            return CreateFormViewModelFor(rSSFeed);
        }

        public RSSFeedFormViewModel CreateFormViewModelFor(RSSFeed rSSFeed) {
            RSSFeedFormViewModel viewModel = CreateFormViewModel();
            viewModel.RSSFeed = rSSFeed;
            return viewModel;
        }

        public ActionConfirmation SaveOrUpdate(RSSFeed rSSFeed) {
            if (rSSFeed.IsValid()) {
                rSSFeedRepository.SaveOrUpdate(rSSFeed);

                ActionConfirmation saveOrUpdateConfirmation = ActionConfirmation.CreateSuccessConfirmation(
                    "The rSSFeed was successfully saved.");
                saveOrUpdateConfirmation.Value = rSSFeed;

                return saveOrUpdateConfirmation;
            }
            else {
                rSSFeedRepository.DbContext.RollbackTransaction();

                return ActionConfirmation.CreateFailureConfirmation(
                    "The rSSFeed could not be saved due to missing or invalid information.");
            }
        }

        public ActionConfirmation UpdateWith(RSSFeed rSSFeedFromForm, int idOfRSSFeedToUpdate) {
            RSSFeed rSSFeedToUpdate = 
                rSSFeedRepository.Get(idOfRSSFeedToUpdate);
            TransferFormValuesTo(rSSFeedToUpdate, rSSFeedFromForm);

            if (rSSFeedToUpdate.IsValid()) {
                ActionConfirmation updateConfirmation = ActionConfirmation.CreateSuccessConfirmation(
                    "The rSSFeed was successfully updated.");
                updateConfirmation.Value = rSSFeedToUpdate;

                return updateConfirmation;
            }
            else {
                rSSFeedRepository.DbContext.RollbackTransaction();

                return ActionConfirmation.CreateFailureConfirmation(
                    "The rSSFeed could not be saved due to missing or invalid information.");
            }
        }

        public ActionConfirmation Delete(int id) {
            RSSFeed rSSFeedToDelete = rSSFeedRepository.Get(id);

            if (rSSFeedToDelete != null) {
                rSSFeedRepository.Delete(rSSFeedToDelete);

                try {
                    rSSFeedRepository.DbContext.CommitChanges();
                    
                    return ActionConfirmation.CreateSuccessConfirmation(
                        "The rSSFeed was successfully deleted.");
                }
                catch {
                    rSSFeedRepository.DbContext.RollbackTransaction();

                    return ActionConfirmation.CreateFailureConfirmation(
                        "A problem was encountered preventing the rSSFeed from being deleted. " +
                        "Another item likely depends on this rSSFeed.");
                }
            }
            else {
                return ActionConfirmation.CreateFailureConfirmation(
                    "The rSSFeed could not be found for deletion. It may already have been deleted.");
            }
        }

        private void TransferFormValuesTo(RSSFeed rSSFeedToUpdate, RSSFeed rSSFeedFromForm) {
		    rSSFeedToUpdate.URL = rSSFeedFromForm.URL;
			rSSFeedToUpdate.LastChecked = rSSFeedFromForm.LastChecked;
			rSSFeedToUpdate.LastItem = rSSFeedFromForm.LastItem;
			rSSFeedToUpdate.Template = rSSFeedFromForm.Template;
			rSSFeedToUpdate.XSLT = rSSFeedFromForm.XSLT;
        }

        IRSSFeedRepository rSSFeedRepository;
    }
}
