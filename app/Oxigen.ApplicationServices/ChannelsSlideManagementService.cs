using System.Collections.Generic;
using System;
using SharpArch.Core;
using Oxigen.Core;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;
 

namespace Oxigen.ApplicationServices
{
    public class ChannelsSlideManagementService : IChannelsSlideManagementService
    {
        public ChannelsSlideManagementService(IChannelsSlideRepository channelsSlideRepository) {
            Check.Require(channelsSlideRepository != null, "channelsSlideRepository may not be null");

            this.channelsSlideRepository = channelsSlideRepository;
        }

        public ChannelsSlide Get(int id) {
            return channelsSlideRepository.Get(id);
        }

        public IList<ChannelsSlide> GetAll() {
            return channelsSlideRepository.GetAll();
        }

        public IList<ChannelsSlideDto> GetChannelsSlideSummaries() {
            return channelsSlideRepository.GetChannelsSlideSummaries();
        }

        public ChannelsSlideFormViewModel CreateFormViewModel() {
            ChannelsSlideFormViewModel viewModel = new ChannelsSlideFormViewModel();
            return viewModel;
        }

        public ChannelsSlideFormViewModel CreateFormViewModelFor(int channelsSlideId) {
            ChannelsSlide channelsSlide = channelsSlideRepository.Get(channelsSlideId);
            return CreateFormViewModelFor(channelsSlide);
        }

        public ChannelsSlideFormViewModel CreateFormViewModelFor(ChannelsSlide channelsSlide) {
            ChannelsSlideFormViewModel viewModel = CreateFormViewModel();
            viewModel.ChannelsSlide = channelsSlide;
            return viewModel;
        }

        public ActionConfirmation SaveOrUpdate(ChannelsSlide channelsSlide) {
            if (channelsSlide.IsValid()) {
                channelsSlideRepository.SaveOrUpdate(channelsSlide);

                ActionConfirmation saveOrUpdateConfirmation = ActionConfirmation.CreateSuccessConfirmation(
                    "The channelsSlide was successfully saved.");
                saveOrUpdateConfirmation.Value = channelsSlide;

                return saveOrUpdateConfirmation;
            }
            else {
                channelsSlideRepository.DbContext.RollbackTransaction();

                return ActionConfirmation.CreateFailureConfirmation(
                    "The channelsSlide could not be saved due to missing or invalid information.");
            }
        }

        public ActionConfirmation UpdateWith(ChannelsSlide channelsSlideFromForm, int idOfChannelsSlideToUpdate) {
            ChannelsSlide channelsSlideToUpdate = 
                channelsSlideRepository.Get(idOfChannelsSlideToUpdate);
            TransferFormValuesTo(channelsSlideToUpdate, channelsSlideFromForm);

            if (channelsSlideToUpdate.IsValid()) {
                ActionConfirmation updateConfirmation = ActionConfirmation.CreateSuccessConfirmation(
                    "The channelsSlide was successfully updated.");
                updateConfirmation.Value = channelsSlideToUpdate;

                return updateConfirmation;
            }
            else {
                channelsSlideRepository.DbContext.RollbackTransaction();

                return ActionConfirmation.CreateFailureConfirmation(
                    "The channelsSlide could not be saved due to missing or invalid information.");
            }
        }

        public ActionConfirmation Delete(int id) {
            ChannelsSlide channelsSlideToDelete = channelsSlideRepository.Get(id);

            if (channelsSlideToDelete != null) {
                channelsSlideRepository.Delete(channelsSlideToDelete);

                try {
                    channelsSlideRepository.DbContext.CommitChanges();
                    
                    return ActionConfirmation.CreateSuccessConfirmation(
                        "The channelsSlide was successfully deleted.");
                }
                catch {
                    channelsSlideRepository.DbContext.RollbackTransaction();

                    return ActionConfirmation.CreateFailureConfirmation(
                        "A problem was encountered preventing the channelsSlide from being deleted. " +
                        "Another item likely depends on this channelsSlide.");
                }
            }
            else {
                return ActionConfirmation.CreateFailureConfirmation(
                    "The channelsSlide could not be found for deletion. It may already have been deleted.");
            }
        }

        private void TransferFormValuesTo(ChannelsSlide channelsSlideToUpdate, ChannelsSlide channelsSlideFromForm) {
		    channelsSlideToUpdate.Channel = channelsSlideFromForm.Channel;
			channelsSlideToUpdate.Slide = channelsSlideFromForm.Slide;
			channelsSlideToUpdate.ClickThroughURL = channelsSlideFromForm.ClickThroughURL;
			channelsSlideToUpdate.DisplayDuration = channelsSlideFromForm.DisplayDuration;
			channelsSlideToUpdate.Schedule = channelsSlideFromForm.Schedule;
			channelsSlideToUpdate.PresentationConvertedSchedule = channelsSlideFromForm.PresentationConvertedSchedule;
        }

        IChannelsSlideRepository channelsSlideRepository;
    }
}
