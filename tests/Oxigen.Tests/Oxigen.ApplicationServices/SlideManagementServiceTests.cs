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
    public class SlideManagementServiceTests
    {
        [SetUp]
        public void SetUp() {
            ServiceLocatorInitializer.Init();

            slideRepository = 
                MockRepository.GenerateMock<ISlideRepository>();
            slideRepository.Stub(r => r.DbContext)
                .Return(MockRepository.GenerateMock<IDbContext>());
            assetContentRepository =
                MockRepository.GenerateMock<IAssetContentRepository>();
            assetContentRepository.Stub(r => r.DbContext)
                .Return(MockRepository.GenerateMock<IDbContext>());
            
            slideManagementService =
                new SlideManagementService(slideRepository, assetContentRepository);
        }

        [Test]
        public void CanGetSlide() {
            // Establish Context
            Slide slideToExpect = 
                SlideInstanceFactory.CreateValidTransientSlide();

            slideRepository.Expect(r => r.Get(1))
                .Return(slideToExpect);

            // Act
            Slide slideRetrieved = 
                slideManagementService.Get(1);

            // Assert
            slideRetrieved.ShouldNotBeNull();
            slideRetrieved.ShouldEqual(slideToExpect);
        }

        [Test]
        public void CanGetAllSlides() {
            // Establish Context
            IList<Slide> slidesToExpect = new List<Slide>();

            Slide slide = 
                SlideInstanceFactory.CreateValidTransientSlide();

            slidesToExpect.Add(slide);

            slideRepository.Expect(r => r.GetAll())
                .Return(slidesToExpect);

            // Act
            IList<Slide> slidesRetrieved =
                slideManagementService.GetAll();

            // Assert
            slidesRetrieved.ShouldNotBeNull();
            slidesRetrieved.Count.ShouldEqual(1);
            slidesRetrieved[0].ShouldNotBeNull();
            slidesRetrieved[0].ShouldEqual(slide);
        }

        [Test]
        public void CanGetSlideSummaries() {
            // Establish Context
            IList<SlideDto> slideSummariesToExpect = new List<SlideDto>();

            SlideDto slideDto = new SlideDto();
            slideSummariesToExpect.Add(slideDto);

            slideRepository.Expect(r => r.GetSlideSummaries())
                .Return(slideSummariesToExpect);

            // Act
            IList<SlideDto> slideSummariesRetrieved =
                slideManagementService.GetSlideSummaries();

            // Assert
            slideSummariesRetrieved.ShouldNotBeNull();
            slideSummariesRetrieved.Count.ShouldEqual(1);
            slideSummariesRetrieved[0].ShouldNotBeNull();
            slideSummariesRetrieved[0].ShouldEqual(slideDto);
        }

        [Test]
        public void CanCreateFormViewModel() {
            // Establish Context
            SlideFormViewModel viewModelToExpect = new SlideFormViewModel();

            // Act
            SlideFormViewModel viewModelRetrieved =
                slideManagementService.CreateFormViewModel();

            // Assert
            viewModelRetrieved.ShouldNotBeNull();
            viewModelRetrieved.Slide.ShouldBeNull();
        }

        [Test]
        public void CanCreateFormViewModelForSlide() {
            // Establish Context
            SlideFormViewModel viewModelToExpect = new SlideFormViewModel();

            Slide slide = 
                SlideInstanceFactory.CreateValidTransientSlide();

            slideRepository.Expect(r => r.Get(1))
                .Return(slide);

            // Act
            SlideFormViewModel viewModelRetrieved =
                slideManagementService.CreateFormViewModelFor(1);

            // Assert
            viewModelRetrieved.ShouldNotBeNull();
            viewModelRetrieved.Slide.ShouldNotBeNull();
            viewModelRetrieved.Slide.ShouldEqual(slide);
        }

        [Test]
        public void CanSaveOrUpdateValidSlide() {
            // Establish Context
            Slide validSlide = 
                SlideInstanceFactory.CreateValidTransientSlide();

            // Act
            ActionConfirmation confirmation =
                slideManagementService.SaveOrUpdate(validSlide);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldNotBeNull();
            confirmation.Value.ShouldEqual(validSlide);
        }

        [Test]
        public void CannotSaveOrUpdateInvalidSlide() {
            // Establish Context
            Slide invalidSlide = new Slide();

            // Act
            ActionConfirmation confirmation =
                slideManagementService.SaveOrUpdate(invalidSlide);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CanUpdateWithValidSlideFromForm() {
            // Establish Context
            Slide validSlideFromForm = 
                SlideInstanceFactory.CreateValidTransientSlide();
            
            // Intentionally empty to ensure successful transfer of values
            Slide slideFromDb = new Slide();

            slideRepository.Expect(r => r.Get(1))
                .Return(slideFromDb);

            // Act
            ActionConfirmation confirmation =
                slideManagementService.UpdateWith(validSlideFromForm, 1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldNotBeNull();
            confirmation.Value.ShouldEqual(slideFromDb);
            confirmation.Value.ShouldEqual(validSlideFromForm);
        }

        [Test]
        public void CannotUpdateWithInvalidSlideFromForm() {
            // Establish Context
            Slide invalidSlideFromForm = new Slide();

            // Intentionally empty to ensure successful transfer of values
            Slide slideFromDb = new Slide();

            slideRepository.Expect(r => r.Get(1))
                .Return(slideFromDb);

            // Act
            ActionConfirmation confirmation =
                slideManagementService.UpdateWith(invalidSlideFromForm, 1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CanDeleteSlide() {
            // Establish Context
            Slide slideToDelete = new Slide();

            slideRepository.Expect(r => r.Get(1))
                .Return(slideToDelete);

            // Act
            ActionConfirmation confirmation =
                slideManagementService.Delete(1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeTrue();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CannotDeleteNonexistentSlide() {
            // Establish Context
            slideRepository.Expect(r => r.Get(1))
                .Return(null);

            // Act
            ActionConfirmation confirmation =
                slideManagementService.Delete(1);

            // Assert
            confirmation.ShouldNotBeNull();
            confirmation.WasSuccessful.ShouldBeFalse();
            confirmation.Value.ShouldBeNull();
        }

        [Test]
        public void CreateSlideFromTemplate() {
            // Establish Context
            int assetContentId = 1;
            var templateId = 3;
            var slidefolderId = 1;
            var userId = 1;
            
            // Act
            //ActionConfirmation confirmation =
                //slideManagementService.CreateFromTemplate(userId, slidefolderId, assetContentId, templateId, "Caption", "Credit");
            // Assert
            //confirmation.ShouldNotBeNull();
            //confirmation.WasSuccessful.ShouldBeFalse();
            //confirmation.Value.ShouldBeNull();
        }

        private ISlideRepository slideRepository;
        private ISlideManagementService slideManagementService;
        private IAssetContentRepository assetContentRepository;
    }
}
