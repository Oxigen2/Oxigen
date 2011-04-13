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
    public class SlideFolderManagementServiceTests
    {
        [SetUp]
        public void SetUp() {
            ServiceLocatorInitializer.Init();

            slideFolderRepository = 
                MockRepository.GenerateMock<ISlideFolderRepository>();
            slideFolderRepository.Stub(r => r.DbContext)
                .Return(MockRepository.GenerateMock<IDbContext>());
            
            slideFolderManagementService =
                new SlideFolderManagementService(slideFolderRepository);
        }

        [Test]
        public void CanGetSlideFolder() {
            // Establish Context
            SlideFolder slideFolderToExpect = 
                SlideFolderInstanceFactory.CreateValidTransientSlideFolder();

            slideFolderRepository.Expect(r => r.Get(1))
                .Return(slideFolderToExpect);

            // Act
            SlideFolder slideFolderRetrieved = 
                slideFolderManagementService.Get(1);

            // Assert
            slideFolderRetrieved.ShouldNotBeNull();
            slideFolderRetrieved.ShouldEqual(slideFolderToExpect);
        }

        [Test]
        public void CanGetAllSlideFolders() {
            // Establish Context
            IList<SlideFolder> slideFoldersToExpect = new List<SlideFolder>();

            SlideFolder slideFolder = 
                SlideFolderInstanceFactory.CreateValidTransientSlideFolder();

            slideFoldersToExpect.Add(slideFolder);

            slideFolderRepository.Expect(r => r.GetAll())
                .Return(slideFoldersToExpect);

            // Act
            IList<SlideFolder> slideFoldersRetrieved =
                slideFolderManagementService.GetAll();

            // Assert
            slideFoldersRetrieved.ShouldNotBeNull();
            slideFoldersRetrieved.Count.ShouldEqual(1);
            slideFoldersRetrieved[0].ShouldNotBeNull();
            slideFoldersRetrieved[0].ShouldEqual(slideFolder);
        }

        [Test]
        public void CanGetSlideFolderSummaries() {
            // Establish Context
            IList<SlideFolderDto> slideFolderSummariesToExpect = new List<SlideFolderDto>();

            SlideFolderDto slideFolderDto = new SlideFolderDto();
            slideFolderSummariesToExpect.Add(slideFolderDto);

            slideFolderRepository.Expect(r => r.GetSlideFolderSummaries())
                .Return(slideFolderSummariesToExpect);

            // Act
            IList<SlideFolderDto> slideFolderSummariesRetrieved =
                slideFolderManagementService.GetSlideFolderSummaries();

            // Assert
            slideFolderSummariesRetrieved.ShouldNotBeNull();
            slideFolderSummariesRetrieved.Count.ShouldEqual(1);
            slideFolderSummariesRetrieved[0].ShouldNotBeNull();
            slideFolderSummariesRetrieved[0].ShouldEqual(slideFolderDto);
        }

        [Test]
        public void CanCreateFormViewModel() {
            // Establish Context
            SlideFolderFormViewModel viewModelToExpect = new SlideFolderFormViewModel();

            // Act
            SlideFolderFormViewModel viewModelRetrieved =
                slideFolderManagementService.CreateFormViewModel();

            // Assert
            viewModelRetrieved.ShouldNotBeNull();
            viewModelRetrieved.SlideFolder.ShouldBeNull();
        }

        [Test]
        public void CanCreateFormViewModelForSlideFolder() {
            // Establish Context
            SlideFolderFormViewModel viewModelToExpect = new SlideFolderFormViewModel();

            SlideFolder slideFolder = 
                SlideFolderInstanceFactory.CreateValidTransientSlideFolder();

            slideFolderRepository.Expect(r => r.Get(1))
                .Return(slideFolder);

            // Act
            SlideFolderFormViewModel viewModelRetrieved =
                slideFolderManagementService.CreateFormViewModelFor(1);

            // Assert
            viewModelRetrieved.ShouldNotBeNull();
            viewModelRetrieved.SlideFolder.ShouldNotBeNull();
            viewModelRetrieved.SlideFolder.ShouldEqual(slideFolder);
        }

        [Test]
        public void CanSaveOrUpdateValidSlideFolder() {
            // Establish Context
            SlideFolder validSlideFolder = 
                SlideFolderInstanceFactory.CreateValidTransientSlideFolder();

            // Act
            ActionConfirmation confirmation =
                slideFolderManagementService.SaveOrUpdate(validSlideFolder);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldNotBeNull();
            confirmation.Value.ShouldEqual(validSlideFolder);
        }

        [Test]
        public void CannotSaveOrUpdateInvalidSlideFolder() {
            // Establish Context
            SlideFolder invalidSlideFolder = new SlideFolder();

            // Act
            ActionConfirmation confirmation =
                slideFolderManagementService.SaveOrUpdate(invalidSlideFolder);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CanUpdateWithValidSlideFolderFromForm() {
            // Establish Context
            SlideFolder validSlideFolderFromForm = 
                SlideFolderInstanceFactory.CreateValidTransientSlideFolder();
            
            // Intentionally empty to ensure successful transfer of values
            SlideFolder slideFolderFromDb = new SlideFolder();

            slideFolderRepository.Expect(r => r.Get(1))
                .Return(slideFolderFromDb);

            // Act
            ActionConfirmation confirmation =
                slideFolderManagementService.UpdateWith(validSlideFolderFromForm, 1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldNotBeNull();
            confirmation.Value.ShouldEqual(slideFolderFromDb);
            confirmation.Value.ShouldEqual(validSlideFolderFromForm);
        }

        [Test]
        public void CannotUpdateWithInvalidSlideFolderFromForm() {
            // Establish Context
            SlideFolder invalidSlideFolderFromForm = new SlideFolder();

            // Intentionally empty to ensure successful transfer of values
            SlideFolder slideFolderFromDb = new SlideFolder();

            slideFolderRepository.Expect(r => r.Get(1))
                .Return(slideFolderFromDb);

            // Act
            ActionConfirmation confirmation =
                slideFolderManagementService.UpdateWith(invalidSlideFolderFromForm, 1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CanDeleteSlideFolder() {
            // Establish Context
            SlideFolder slideFolderToDelete = new SlideFolder();

            slideFolderRepository.Expect(r => r.Get(1))
                .Return(slideFolderToDelete);

            // Act
            ActionConfirmation confirmation =
                slideFolderManagementService.Delete(1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CannotDeleteNonexistentSlideFolder() {
            // Establish Context
            slideFolderRepository.Expect(r => r.Get(1))
                .Return(null);

            // Act
            ActionConfirmation confirmation =
                slideFolderManagementService.Delete(1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        private ISlideFolderRepository slideFolderRepository;
        private ISlideFolderManagementService slideFolderManagementService;
    }
}
