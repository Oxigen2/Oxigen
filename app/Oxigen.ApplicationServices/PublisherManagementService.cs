using System.Collections.Generic;
using System;
using SharpArch.Core;
using Oxigen.Core;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;
 

namespace Oxigen.ApplicationServices
{
    public class PublisherManagementService : IPublisherManagementService
    {
        public PublisherManagementService(IPublisherRepository publisherRepository) {
            Check.Require(publisherRepository != null, "publisherRepository may not be null");

            this.publisherRepository = publisherRepository;
        }

        public Publisher Get(int id) {
            return publisherRepository.Get(id);
        }

        public IList<Publisher> GetAll() {
            return publisherRepository.GetAll();
        }

        public IList<PublisherDto> GetPublisherSummaries() {
            return publisherRepository.GetPublisherSummaries();
        }

        public IList<PublisherLookupDto> GetPublishersByPartialName(string partialName) {
            return publisherRepository.GetPublishersByPartialName(partialName);
        }

        public PublisherFormViewModel CreateFormViewModel() {
            PublisherFormViewModel viewModel = new PublisherFormViewModel();
            return viewModel;
        }

        public PublisherFormViewModel CreateFormViewModelFor(int publisherId) {
            Publisher publisher = publisherRepository.Get(publisherId);
            return CreateFormViewModelFor(publisher);
        }

        public PublisherFormViewModel CreateFormViewModelFor(Publisher publisher) {
            PublisherFormViewModel viewModel = CreateFormViewModel();
            viewModel.Publisher = publisher;
            return viewModel;
        }

        public ActionConfirmation SaveOrUpdate(Publisher publisher) {
            if (publisher.IsValid()) {
                publisherRepository.SaveOrUpdate(publisher);

                ActionConfirmation saveOrUpdateConfirmation = ActionConfirmation.CreateSuccessConfirmation(
                    "The publisher was successfully saved.");
                saveOrUpdateConfirmation.Value = publisher;

                return saveOrUpdateConfirmation;
            }
            else {
                publisherRepository.DbContext.RollbackTransaction();

                return ActionConfirmation.CreateFailureConfirmation(
                    "The publisher could not be saved due to missing or invalid information.");
            }
        }

        public ActionConfirmation UpdateWith(Publisher publisherFromForm, int idOfPublisherToUpdate) {
            Publisher publisherToUpdate = 
                publisherRepository.Get(idOfPublisherToUpdate);
            TransferFormValuesTo(publisherToUpdate, publisherFromForm);

            if (publisherToUpdate.IsValid()) {
                ActionConfirmation updateConfirmation = ActionConfirmation.CreateSuccessConfirmation(
                    "The publisher was successfully updated.");
                updateConfirmation.Value = publisherToUpdate;

                return updateConfirmation;
            }
            else {
                publisherRepository.DbContext.RollbackTransaction();

                return ActionConfirmation.CreateFailureConfirmation(
                    "The publisher could not be saved due to missing or invalid information.");
            }
        }

        public ActionConfirmation Delete(int id) {
            Publisher publisherToDelete = publisherRepository.Get(id);

            if (publisherToDelete != null) {
                publisherRepository.Delete(publisherToDelete);

                try {
                    publisherRepository.DbContext.CommitChanges();
                    
                    return ActionConfirmation.CreateSuccessConfirmation(
                        "The publisher was successfully deleted.");
                }
                catch {
                    publisherRepository.DbContext.RollbackTransaction();

                    return ActionConfirmation.CreateFailureConfirmation(
                        "A problem was encountered preventing the publisher from being deleted. " +
                        "Another item likely depends on this publisher.");
                }
            }
            else {
                return ActionConfirmation.CreateFailureConfirmation(
                    "The publisher could not be found for deletion. It may already have been deleted.");
            }
        }

        public Publisher GetByUserId(int userId)
        {
            return publisherRepository.GetByUserId(userId);
        }

        private void TransferFormValuesTo(Publisher publisherToUpdate, Publisher publisherFromForm) {
		    publisherToUpdate.UserID = publisherFromForm.UserID;
			publisherToUpdate.FirstName = publisherFromForm.FirstName;
			publisherToUpdate.LastName = publisherFromForm.LastName;
			publisherToUpdate.DisplayName = publisherFromForm.DisplayName;
			publisherToUpdate.EmailAddress = publisherFromForm.EmailAddress;
			publisherToUpdate.UsedBytes = publisherFromForm.UsedBytes;
			publisherToUpdate.TotalAvailableBytes = publisherFromForm.TotalAvailableBytes;
        }

        IPublisherRepository publisherRepository;
    }
}
