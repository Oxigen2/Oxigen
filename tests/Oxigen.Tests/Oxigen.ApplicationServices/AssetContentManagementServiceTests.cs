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
    public class AssetContentManagementServiceTests
    {
        [SetUp]
        public void SetUp() {
            ServiceLocatorInitializer.Init();

            assetContentRepository = 
                MockRepository.GenerateMock<IAssetContentRepository>();
            assetContentRepository.Stub(r => r.DbContext)
                .Return(MockRepository.GenerateMock<IDbContext>());
            
            assetContentManagementService =
                new AssetContentManagementService(assetContentRepository);
        }

        [Test]
        public void CanGetAssetContent() {
            // Establish Context
            AssetContent assetContentToExpect = 
                AssetContentInstanceFactory.CreateValidTransientAssetContent();

            assetContentRepository.Expect(r => r.Get(1))
                .Return(assetContentToExpect);

            // Act
            AssetContent assetContentRetrieved = 
                assetContentManagementService.Get(1);

            // Assert
            assetContentRetrieved.ShouldNotBeNull();
            assetContentRetrieved.ShouldEqual(assetContentToExpect);
        }

        [Test]
        public void CanGetAllAssetContents() {
            // Establish Context
            IList<AssetContent> assetContentsToExpect = new List<AssetContent>();

            AssetContent assetContent = 
                AssetContentInstanceFactory.CreateValidTransientAssetContent();

            assetContentsToExpect.Add(assetContent);

            assetContentRepository.Expect(r => r.GetAll())
                .Return(assetContentsToExpect);

            // Act
            IList<AssetContent> assetContentsRetrieved =
                assetContentManagementService.GetAll();

            // Assert
            assetContentsRetrieved.ShouldNotBeNull();
            assetContentsRetrieved.Count.ShouldEqual(1);
            assetContentsRetrieved[0].ShouldNotBeNull();
            assetContentsRetrieved[0].ShouldEqual(assetContent);
        }

        [Test]
        public void CanGetAssetContentSummaries() {
            // Establish Context
            IList<AssetContentDto> assetContentSummariesToExpect = new List<AssetContentDto>();

            AssetContentDto assetContentDto = new AssetContentDto();
            assetContentSummariesToExpect.Add(assetContentDto);

            assetContentRepository.Expect(r => r.GetAssetContentSummaries())
                .Return(assetContentSummariesToExpect);

            // Act
            IList<AssetContentDto> assetContentSummariesRetrieved =
                assetContentManagementService.GetAssetContentSummaries();

            // Assert
            assetContentSummariesRetrieved.ShouldNotBeNull();
            assetContentSummariesRetrieved.Count.ShouldEqual(1);
            assetContentSummariesRetrieved[0].ShouldNotBeNull();
            assetContentSummariesRetrieved[0].ShouldEqual(assetContentDto);
        }

        [Test]
        public void CanCreateFormViewModel() {
            // Establish Context
            AssetContentFormViewModel viewModelToExpect = new AssetContentFormViewModel();

            // Act
            AssetContentFormViewModel viewModelRetrieved =
                assetContentManagementService.CreateFormViewModel();

            // Assert
            viewModelRetrieved.ShouldNotBeNull();
            viewModelRetrieved.AssetContent.ShouldBeNull();
        }

        [Test]
        public void CanCreateFormViewModelForAssetContent() {
            // Establish Context
            AssetContentFormViewModel viewModelToExpect = new AssetContentFormViewModel();

            AssetContent assetContent = 
                AssetContentInstanceFactory.CreateValidTransientAssetContent();

            assetContentRepository.Expect(r => r.Get(1))
                .Return(assetContent);

            // Act
            AssetContentFormViewModel viewModelRetrieved =
                assetContentManagementService.CreateFormViewModelFor(1);

            // Assert
            viewModelRetrieved.ShouldNotBeNull();
            viewModelRetrieved.AssetContent.ShouldNotBeNull();
            viewModelRetrieved.AssetContent.ShouldEqual(assetContent);
        }

        [Test]
        public void CanSaveOrUpdateValidAssetContent() {
            // Establish Context
            AssetContent validAssetContent = 
                AssetContentInstanceFactory.CreateValidTransientAssetContent();

            // Act
            ActionConfirmation confirmation =
                assetContentManagementService.SaveOrUpdate(validAssetContent);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldNotBeNull();
            confirmation.Value.ShouldEqual(validAssetContent);
        }

        [Test]
        public void CannotSaveOrUpdateInvalidAssetContent() {
            // Establish Context
            AssetContent invalidAssetContent = new AssetContent();

            // Act
            ActionConfirmation confirmation =
                assetContentManagementService.SaveOrUpdate(invalidAssetContent);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CanUpdateWithValidAssetContentFromForm() {
            // Establish Context
            AssetContent validAssetContentFromForm = 
                AssetContentInstanceFactory.CreateValidTransientAssetContent();
            
            // Intentionally empty to ensure successful transfer of values
            AssetContent assetContentFromDb = new AssetContent();

            assetContentRepository.Expect(r => r.Get(1))
                .Return(assetContentFromDb);

            // Act
            ActionConfirmation confirmation =
                assetContentManagementService.UpdateWith(validAssetContentFromForm, 1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldNotBeNull();
            confirmation.Value.ShouldEqual(assetContentFromDb);
            confirmation.Value.ShouldEqual(validAssetContentFromForm);
        }

        [Test]
        public void CannotUpdateWithInvalidAssetContentFromForm() {
            // Establish Context
            AssetContent invalidAssetContentFromForm = new AssetContent();

            // Intentionally empty to ensure successful transfer of values
            AssetContent assetContentFromDb = new AssetContent();

            assetContentRepository.Expect(r => r.Get(1))
                .Return(assetContentFromDb);

            // Act
            ActionConfirmation confirmation =
                assetContentManagementService.UpdateWith(invalidAssetContentFromForm, 1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CanDeleteAssetContent() {
            // Establish Context
            AssetContent assetContentToDelete = new AssetContent();

            assetContentRepository.Expect(r => r.Get(1))
                .Return(assetContentToDelete);

            // Act
            ActionConfirmation confirmation =
                assetContentManagementService.Delete(1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CannotDeleteNonexistentAssetContent() {
            // Establish Context
            assetContentRepository.Expect(r => r.Get(1))
                .Return(null);

            // Act
            ActionConfirmation confirmation =
                assetContentManagementService.Delete(1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        private IAssetContentRepository assetContentRepository;
        private IAssetContentManagementService assetContentManagementService;
    }
}
