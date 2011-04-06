using NUnit.Framework;
using Rhino.Mocks;
using SharpArch.Testing.NUnit;
using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport;
using Oxigen.Core;
using Oxigen.ApplicationServices;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;
using Tests.Oxigen.Core;
 

namespace Tests.Oxigen.ApplicationServices
{
    [TestFixture]
    public class PublisherManagementServiceTests
    {
        [SetUp]
        public void SetUp() {
            ServiceLocatorInitializer.Init();

            publisherRepository = 
                MockRepository.GenerateMock<IPublisherRepository>();
            publisherRepository.Stub(r => r.DbContext)
                .Return(MockRepository.GenerateMock<IDbContext>());
            
            publisherManagementService =
                new PublisherManagementService(publisherRepository);
        }

        [Test]
        public void CanGetPublisher() {
            // Establish Context
            Publisher publisherToExpect = 
                PublisherInstanceFactory.CreateValidTransientPublisher();

            publisherRepository.Expect(r => r.Get(1))
                .Return(publisherToExpect);

            // Act
            Publisher publisherRetrieved = 
                publisherManagementService.Get(1);

            // Assert
            publisherRetrieved.ShouldNotBeNull();
            publisherRetrieved.ShouldEqual(publisherToExpect);
        }

        [Test]
        public void CanGetAllPublishers() {
            // Establish Context
            IList<Publisher> publishersToExpect = new List<Publisher>();

            Publisher publisher = 
                PublisherInstanceFactory.CreateValidTransientPublisher();

            publishersToExpect.Add(publisher);

            publisherRepository.Expect(r => r.GetAll())
                .Return(publishersToExpect);

            // Act
            IList<Publisher> publishersRetrieved =
                publisherManagementService.GetAll();

            // Assert
            publishersRetrieved.ShouldNotBeNull();
            publishersRetrieved.Count.ShouldEqual(1);
            publishersRetrieved[0].ShouldNotBeNull();
            publishersRetrieved[0].ShouldEqual(publisher);
        }

        [Test]
        public void CanGetPublisherSummaries() {
            // Establish Context
            IList<PublisherDto> publisherSummariesToExpect = new List<PublisherDto>();

            PublisherDto publisherDto = new PublisherDto();
            publisherSummariesToExpect.Add(publisherDto);

            publisherRepository.Expect(r => r.GetPublisherSummaries())
                .Return(publisherSummariesToExpect);

            // Act
            IList<PublisherDto> publisherSummariesRetrieved =
                publisherManagementService.GetPublisherSummaries();

            // Assert
            publisherSummariesRetrieved.ShouldNotBeNull();
            publisherSummariesRetrieved.Count.ShouldEqual(1);
            publisherSummariesRetrieved[0].ShouldNotBeNull();
            publisherSummariesRetrieved[0].ShouldEqual(publisherDto);
        }

        [Test]
        public void CanGetPublishersByPartialName() {
            // Establish Context
            IList<PublisherLookupDto> publisherListToExpect = new List<PublisherLookupDto>();

            PublisherLookupDto publisherLookupDto = new PublisherLookupDto();
            publisherListToExpect.Add(publisherLookupDto);
            
            const string partialName = "John";

            publisherRepository.Expect(r => r.GetPublishersByPartialName(partialName)).Return(publisherListToExpect);

            // Act
            IList<PublisherLookupDto> publisherListRetrieved =
                publisherManagementService.GetPublishersByPartialName(partialName);

            // Assert
            publisherListRetrieved.ShouldNotBeNull();
            publisherListRetrieved.Count.ShouldEqual(1);
            publisherListRetrieved[0].ShouldNotBeNull();
            publisherListRetrieved[0].ShouldEqual(publisherLookupDto);
        }

        [Test]
        public void CanCreateFormViewModel() {
            // Establish Context
            PublisherFormViewModel viewModelToExpect = new PublisherFormViewModel();

            // Act
            PublisherFormViewModel viewModelRetrieved =
                publisherManagementService.CreateFormViewModel();

            // Assert
            viewModelRetrieved.ShouldNotBeNull();
            viewModelRetrieved.Publisher.ShouldBeNull();
        }

        [Test]
        public void CanCreateFormViewModelForPublisher() {
            // Establish Context
            PublisherFormViewModel viewModelToExpect = new PublisherFormViewModel();

            Publisher publisher = 
                PublisherInstanceFactory.CreateValidTransientPublisher();

            publisherRepository.Expect(r => r.Get(1))
                .Return(publisher);

            // Act
            PublisherFormViewModel viewModelRetrieved =
                publisherManagementService.CreateFormViewModelFor(1);

            // Assert
            viewModelRetrieved.ShouldNotBeNull();
            viewModelRetrieved.Publisher.ShouldNotBeNull();
            viewModelRetrieved.Publisher.ShouldEqual(publisher);
        }

        [Test]
        public void CanSaveOrUpdateValidPublisher() {
            // Establish Context
            Publisher validPublisher = 
                PublisherInstanceFactory.CreateValidTransientPublisher();

            // Act
            ActionConfirmation confirmation =
                publisherManagementService.SaveOrUpdate(validPublisher);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldNotBeNull();
            confirmation.Value.ShouldEqual(validPublisher);
        }

        [Test]
        public void CannotSaveOrUpdateInvalidPublisher() {
            // Establish Context
            Publisher invalidPublisher = new Publisher();

            // Act
            ActionConfirmation confirmation =
                publisherManagementService.SaveOrUpdate(invalidPublisher);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CanUpdateWithValidPublisherFromForm() {
            // Establish Context
            Publisher validPublisherFromForm = 
                PublisherInstanceFactory.CreateValidTransientPublisher();
            
            // Intentionally empty to ensure successful transfer of values
            Publisher publisherFromDb = new Publisher();

            publisherRepository.Expect(r => r.Get(1))
                .Return(publisherFromDb);

            // Act
            ActionConfirmation confirmation =
                publisherManagementService.UpdateWith(validPublisherFromForm, 1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldNotBeNull();
            confirmation.Value.ShouldEqual(publisherFromDb);
            confirmation.Value.ShouldEqual(validPublisherFromForm);
        }

        [Test]
        public void CannotUpdateWithInvalidPublisherFromForm() {
            // Establish Context
            Publisher invalidPublisherFromForm = new Publisher();

            // Intentionally empty to ensure successful transfer of values
            Publisher publisherFromDb = new Publisher();

            publisherRepository.Expect(r => r.Get(1))
                .Return(publisherFromDb);

            // Act
            ActionConfirmation confirmation =
                publisherManagementService.UpdateWith(invalidPublisherFromForm, 1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CanDeletePublisher() {
            // Establish Context
            Publisher publisherToDelete = new Publisher();

            publisherRepository.Expect(r => r.Get(1))
                .Return(publisherToDelete);

            // Act
            ActionConfirmation confirmation =
                publisherManagementService.Delete(1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CannotDeleteNonexistentPublisher() {
            // Establish Context
            publisherRepository.Expect(r => r.Get(1))
                .Return(null);

            // Act
            ActionConfirmation confirmation =
                publisherManagementService.Delete(1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        private IPublisherRepository publisherRepository;
        private IPublisherManagementService publisherManagementService;
    }
}
