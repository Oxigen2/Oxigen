using NUnit.Framework;
using Rhino.Mocks;
using SharpArch.Testing.NUnit;
using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport;
using Oxigen.Core.Syndication;
using Oxigen.ApplicationServices.Syndication;
using Oxigen.ApplicationServices.ViewModels.Syndication;
using Oxigen.Core.QueryDtos.Syndication;
using Oxigen.Core.RepositoryInterfaces.Syndication;
using Tests.Oxigen.Core.Syndication;
using Oxigen.Core; 

namespace Tests.Oxigen.ApplicationServices.Syndication
{
    [TestFixture]
    public class RSSFeedManagementServiceTests
    {
        [SetUp]
        public void SetUp() {
            ServiceLocatorInitializer.Init();

            rSSFeedRepository = 
                MockRepository.GenerateMock<IRSSFeedRepository>();
            rSSFeedRepository.Stub(r => r.DbContext)
                .Return(MockRepository.GenerateMock<IDbContext>());
            
            rSSFeedManagementService =
                new RSSFeedManagementService(rSSFeedRepository, null, null);
        }

        [Test]
        public void CanGetRSSFeed() {
            // Establish Context
            RSSFeed rSSFeedToExpect = 
                RSSFeedInstanceFactory.CreateValidTransientRSSFeed();

            rSSFeedRepository.Expect(r => r.Get(1))
                .Return(rSSFeedToExpect);

            // Act
            RSSFeed rSSFeedRetrieved = 
                rSSFeedManagementService.Get(1);

            // Assert
            rSSFeedRetrieved.ShouldNotBeNull();
            rSSFeedRetrieved.ShouldEqual(rSSFeedToExpect);
        }

        [Test]
        public void CanGetAllRSSFeeds() {
            // Establish Context
            IList<RSSFeed> rSSFeedsToExpect = new List<RSSFeed>();

            RSSFeed rSSFeed = 
                RSSFeedInstanceFactory.CreateValidTransientRSSFeed();

            rSSFeedsToExpect.Add(rSSFeed);

            rSSFeedRepository.Expect(r => r.GetAll())
                .Return(rSSFeedsToExpect);

            // Act
            IList<RSSFeed> rSSFeedsRetrieved =
                rSSFeedManagementService.GetAll();

            // Assert
            rSSFeedsRetrieved.ShouldNotBeNull();
            rSSFeedsRetrieved.Count.ShouldEqual(1);
            rSSFeedsRetrieved[0].ShouldNotBeNull();
            rSSFeedsRetrieved[0].ShouldEqual(rSSFeed);
        }

        [Test]
        public void CanGetRSSFeedSummaries() {
            // Establish Context
            IList<RSSFeedDto> rSSFeedSummariesToExpect = new List<RSSFeedDto>();

            RSSFeedDto rSSFeedDto = new RSSFeedDto();
            rSSFeedSummariesToExpect.Add(rSSFeedDto);

            rSSFeedRepository.Expect(r => r.GetRSSFeedSummaries())
                .Return(rSSFeedSummariesToExpect);

            // Act
            IList<RSSFeedDto> rSSFeedSummariesRetrieved =
                rSSFeedManagementService.GetRSSFeedSummaries();

            // Assert
            rSSFeedSummariesRetrieved.ShouldNotBeNull();
            rSSFeedSummariesRetrieved.Count.ShouldEqual(1);
            rSSFeedSummariesRetrieved[0].ShouldNotBeNull();
            rSSFeedSummariesRetrieved[0].ShouldEqual(rSSFeedDto);
        }

        [Test]
        public void CanCreateFormViewModel() {
            // Establish Context
            RSSFeedFormViewModel viewModelToExpect = new RSSFeedFormViewModel();

            // Act
            RSSFeedFormViewModel viewModelRetrieved =
                rSSFeedManagementService.CreateFormViewModel();

            // Assert
            viewModelRetrieved.ShouldNotBeNull();
            viewModelRetrieved.RSSFeed.ShouldBeNull();
        }

        [Test]
        public void CanCreateFormViewModelForRSSFeed() {
            // Establish Context
            RSSFeedFormViewModel viewModelToExpect = new RSSFeedFormViewModel();

            RSSFeed rSSFeed = 
                RSSFeedInstanceFactory.CreateValidTransientRSSFeed();

            rSSFeedRepository.Expect(r => r.Get(1))
                .Return(rSSFeed);

            // Act
            RSSFeedFormViewModel viewModelRetrieved =
                rSSFeedManagementService.CreateFormViewModelFor(1);

            // Assert
            viewModelRetrieved.ShouldNotBeNull();
            viewModelRetrieved.RSSFeed.ShouldNotBeNull();
            viewModelRetrieved.RSSFeed.ShouldEqual(rSSFeed);
        }

        [Test]
        public void CanSaveOrUpdateValidRSSFeed() {
            // Establish Context
            RSSFeed validRSSFeed = 
                RSSFeedInstanceFactory.CreateValidTransientRSSFeed();

            // Act
            ActionConfirmation confirmation =
                rSSFeedManagementService.SaveOrUpdate(validRSSFeed);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldNotBeNull();
            confirmation.Value.ShouldEqual(validRSSFeed);
        }

        [Test]
        public void CannotSaveOrUpdateInvalidRSSFeed() {
            // Establish Context
            RSSFeed invalidRSSFeed = new RSSFeed();

            // Act
            ActionConfirmation confirmation =
                rSSFeedManagementService.SaveOrUpdate(invalidRSSFeed);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CanUpdateWithValidRSSFeedFromForm() {
            // Establish Context
            RSSFeed validRSSFeedFromForm = 
                RSSFeedInstanceFactory.CreateValidTransientRSSFeed();
            
            // Intentionally empty to ensure successful transfer of values
            RSSFeed rSSFeedFromDb = new RSSFeed();

            rSSFeedRepository.Expect(r => r.Get(1))
                .Return(rSSFeedFromDb);

            // Act
            ActionConfirmation confirmation =
                rSSFeedManagementService.UpdateWith(validRSSFeedFromForm, 1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldNotBeNull();
            confirmation.Value.ShouldEqual(rSSFeedFromDb);
            confirmation.Value.ShouldEqual(validRSSFeedFromForm);
        }

        [Test]
        public void CannotUpdateWithInvalidRSSFeedFromForm() {
            // Establish Context
            RSSFeed invalidRSSFeedFromForm = new RSSFeed();

            // Intentionally empty to ensure successful transfer of values
            RSSFeed rSSFeedFromDb = new RSSFeed();

            rSSFeedRepository.Expect(r => r.Get(1))
                .Return(rSSFeedFromDb);

            // Act
            ActionConfirmation confirmation =
                rSSFeedManagementService.UpdateWith(invalidRSSFeedFromForm, 1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CanDeleteRSSFeed() {
            // Establish Context
            RSSFeed rSSFeedToDelete = new RSSFeed();

            rSSFeedRepository.Expect(r => r.Get(1))
                .Return(rSSFeedToDelete);

            // Act
            ActionConfirmation confirmation =
                rSSFeedManagementService.Delete(1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CannotDeleteNonexistentRSSFeed() {
            // Establish Context
            rSSFeedRepository.Expect(r => r.Get(1))
                .Return(null);

            // Act
            ActionConfirmation confirmation =
                rSSFeedManagementService.Delete(1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        private IRSSFeedRepository rSSFeedRepository;
        private IRSSFeedManagementService rSSFeedManagementService;
    }
}
