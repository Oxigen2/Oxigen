using System.Collections.Generic;
using System;
using System.IO;
using Oxigen.Core.RepositoryInterfaces;
using SharpArch.Core;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;
using Oxigen.Core;
using SharpArch.Data.NHibernate;

namespace Oxigen.ApplicationServices
{
    public class RSSFeedManagementService : IRSSFeedManagementService
    {
        public RSSFeedManagementService(IRSSFeedRepository rSSFeedRepository, IChannelsSlideRepository channelsSlideRepository, ISlideRepository slideRepository, ISlideFolderRepository slideFolderRepository)
        {
            Check.Require(rSSFeedRepository != null, "rSSFeedRepository may not be null");
            this.rSSFeedRepository = rSSFeedRepository;

            Check.Require(channelsSlideRepository != null, "channelsSlideRepository may not be null");
            this.channelsSlideRepository = channelsSlideRepository;

            Check.Require(slideRepository != null, "channelsSlideRepository may not be null");
            this.slideRepository = slideRepository;

            Check.Require(slideFolderRepository != null, "channelsSlideRepository may not be null");
            this.slideFolderRepository = slideFolderRepository;
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
            //viewModel.SlideFolders = rSSFeed.Publisher.SlideFolders;
            //viewModel.Templates = rSSFeed.Publisher.AssignedTemplates;
            //viewModel.Channels = rSSFeed.Publisher.Channels;
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

        public ActionConfirmation Run(int id)
        {
            var rssFeed = rSSFeedRepository.Get(id);
            Run(rssFeed);
            return ActionConfirmation.CreateSuccessConfirmation("Success");
        }

        private void Run(RSSFeed rssFeed)
        {
            rssFeed.Run();
            foreach (var channelSlide in rssFeed.Channel.AssignedSlides)
            {
                slideRepository.SaveOrUpdate(channelSlide.Slide);
                channelsSlideRepository.SaveOrUpdate(channelSlide);
            }
            rSSFeedRepository.SaveOrUpdate(rssFeed);
        }

        public ActionConfirmation Refresh()
        {
            try
            {
                //NHibernateSession.GetDefaultSessionFactory().OpenSession();
                var rssFeeds = rSSFeedRepository.GetAll();
                foreach (var rssFeed in rssFeeds)
                {
                    rSSFeedRepository.DbContext.BeginTransaction();
                    Run(rssFeed);
                    rSSFeedRepository.DbContext.CommitChanges();
                    rSSFeedRepository.DbContext.CommitTransaction();
                }

                NHibernateSession.Current.Clear();

                List<string> filesToDelete = new List<string>();
                var slideFolders = slideFolderRepository.GetSlideFoldersWithTooManySlides();
                slideFolderRepository.DbContext.BeginTransaction();

                foreach (var slideFolder in slideFolders)
                {
                    int numberOfSlideToRemove = slideFolder.SlideCount - slideFolder.MaxSlideCount;
                    for (int x = 0; x < numberOfSlideToRemove; x++)
                    {
                        var slide = slideFolder.Slides[0];
                        filesToDelete.Add(slide.FileFullPathName);
                        slideFolder.Slides.RemoveAt(0);
                        slideRepository.Delete(slide);
                    }
                }
                slideFolderRepository.DbContext.CommitChanges();
                rSSFeedRepository.DbContext.CommitTransaction();

                foreach (var filePath in filesToDelete)
                {
                    if (File.Exists(filePath)) File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                return ActionConfirmation.CreateFailureConfirmation(ex.ToString());
            }
            return ActionConfirmation.CreateSuccessConfirmation("Success");
        }

        private void TransferFormValuesTo(RSSFeed rSSFeedToUpdate, RSSFeed rSSFeedFromForm) {
            rSSFeedToUpdate.Name = rSSFeedFromForm.Name;
		    rSSFeedToUpdate.URL = rSSFeedFromForm.URL;
			rSSFeedToUpdate.Template = rSSFeedFromForm.Template;
            rSSFeedToUpdate.Channel = rSSFeedFromForm.Channel;
            rSSFeedToUpdate.SlideFolder = rSSFeedFromForm.SlideFolder;
			rSSFeedToUpdate.XSLT = rSSFeedFromForm.XSLT;
        }

        IRSSFeedRepository rSSFeedRepository;
        IChannelsSlideRepository channelsSlideRepository;
        ISlideRepository slideRepository;
        ISlideFolderRepository slideFolderRepository;
    }
}
