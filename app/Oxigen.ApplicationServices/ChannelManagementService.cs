using System.Collections.Generic;
using System;
using SharpArch.Core;
using Oxigen.Core;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;
 

namespace Oxigen.ApplicationServices
{
    public class ChannelManagementService : IChannelManagementService
    {
        public ChannelManagementService(IChannelRepository channelRepository) {
            Check.Require(channelRepository != null, "channelRepository may not be null");

            this.channelRepository = channelRepository;
        }

        public Channel Get(int id) {
            return channelRepository.Get(id);
        }

        public IList<Channel> GetAll() {
            return channelRepository.GetAll();
        }

        public IList<ChannelDto> GetChannelSummaries() {
            return channelRepository.GetChannelSummaries();
        }

        public ChannelFormViewModel CreateFormViewModel() {
            ChannelFormViewModel viewModel = new ChannelFormViewModel();
            return viewModel;
        }

        public ChannelFormViewModel CreateFormViewModelFor(int channelId) {
            Channel channel = channelRepository.Get(channelId);
            return CreateFormViewModelFor(channel);
        }

        public ChannelFormViewModel CreateFormViewModelFor(Channel channel) {
            ChannelFormViewModel viewModel = CreateFormViewModel();
            viewModel.Channel = channel;
            return viewModel;
        }

        public ActionConfirmation SaveOrUpdate(Channel channel) {
            if (channel.IsValid()) {
                channelRepository.SaveOrUpdate(channel);

                ActionConfirmation saveOrUpdateConfirmation = ActionConfirmation.CreateSuccessConfirmation(
                    "The channel was successfully saved.");
                saveOrUpdateConfirmation.Value = channel;

                return saveOrUpdateConfirmation;
            }
            else {
                channelRepository.DbContext.RollbackTransaction();

                return ActionConfirmation.CreateFailureConfirmation(
                    "The channel could not be saved due to missing or invalid information.");
            }
        }

        public ActionConfirmation UpdateWith(Channel channelFromForm, int idOfChannelToUpdate) {
            Channel channelToUpdate = 
                channelRepository.Get(idOfChannelToUpdate);
            TransferFormValuesTo(channelToUpdate, channelFromForm);

            if (channelToUpdate.IsValid()) {
                ActionConfirmation updateConfirmation = ActionConfirmation.CreateSuccessConfirmation(
                    "The channel was successfully updated.");
                updateConfirmation.Value = channelToUpdate;

                return updateConfirmation;
            }
            else {
                channelRepository.DbContext.RollbackTransaction();

                return ActionConfirmation.CreateFailureConfirmation(
                    "The channel could not be saved due to missing or invalid information.");
            }
        }

        public ActionConfirmation Delete(int id) {
            Channel channelToDelete = channelRepository.Get(id);

            if (channelToDelete != null) {
                channelRepository.Delete(channelToDelete);

                try {
                    channelRepository.DbContext.CommitChanges();
                    
                    return ActionConfirmation.CreateSuccessConfirmation(
                        "The channel was successfully deleted.");
                }
                catch {
                    channelRepository.DbContext.RollbackTransaction();

                    return ActionConfirmation.CreateFailureConfirmation(
                        "A problem was encountered preventing the channel from being deleted. " +
                        "Another item likely depends on this channel.");
                }
            }
            else {
                return ActionConfirmation.CreateFailureConfirmation(
                    "The channel could not be found for deletion. It may already have been deleted.");
            }
        }

        public IList<ChannelDto> GetByPublisher(int id)
        {
            return channelRepository.GetByPublisher(id);
        }

        private void TransferFormValuesTo(Channel channelToUpdate, Channel channelFromForm) {
		    channelToUpdate.CategoryID = channelFromForm.CategoryID;
			channelToUpdate.Publisher = channelFromForm.Publisher;
			channelToUpdate.ChannelName = channelFromForm.ChannelName;
			channelToUpdate.ChannelGUID = channelFromForm.ChannelGUID;
			channelToUpdate.ChannelDescription = channelFromForm.ChannelDescription;
			channelToUpdate.ChannelLongDescription = channelFromForm.ChannelLongDescription;
			channelToUpdate.Keywords = channelFromForm.Keywords;
			channelToUpdate.ImagePath = channelFromForm.ImagePath;
			channelToUpdate.bHasDefaultThumbnail = channelFromForm.bHasDefaultThumbnail;
			channelToUpdate.bLocked = channelFromForm.bLocked;
			channelToUpdate.bAcceptPasswordRequests = channelFromForm.bAcceptPasswordRequests;
			channelToUpdate.ChannelPassword = channelFromForm.ChannelPassword;
			channelToUpdate.NoContent = channelFromForm.NoContent;
			channelToUpdate.NoFollowers = channelFromForm.NoFollowers;
			channelToUpdate.AddDate = channelFromForm.AddDate;
			channelToUpdate.EditDate = channelFromForm.EditDate;
			channelToUpdate.MadeDirtyLastDate = channelFromForm.MadeDirtyLastDate;
			channelToUpdate.ContentLastAddedDate = channelFromForm.ContentLastAddedDate;
        }

        IChannelRepository channelRepository;
    }
}
