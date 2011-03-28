using MvcContrib.TestHelper;
using NUnit.Framework;
using Rhino.Mocks;
using SharpArch.Testing.NUnit;
using System.Collections.Generic;
using System.Web.Mvc;
using Oxigen.Core;
using Oxigen.Core.QueryDtos;
using Oxigen.ApplicationServices;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Web.Controllers;
using Tests.Oxigen.Core;
 

namespace Tests.Oxigen.Web.Controllers
{
    [TestFixture]
    public class SlidesControllerTests
    {
        [SetUp]
        public void SetUp() {
            ServiceLocatorInitializer.Init();

            slideManagementService =
                MockRepository.GenerateMock<ISlideManagementService>();
            slidesController = 
                new SlidesController(slideManagementService);
        }

        [Test]
        public void CanListSlides() {
            // Establish Context
            IList<SlideDto> slideSummariesToExpect = new List<SlideDto>();

            SlideDto slideDto = new SlideDto();
            slideSummariesToExpect.Add(slideDto);

            slideManagementService.Expect(r => r.GetSlideSummaries())
                .Return(slideSummariesToExpect);

            // Act
            ViewResult result = slidesController.Index().AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as IList<SlideDto>).ShouldNotBeNull();
            (result.ViewData.Model as IList<SlideDto>).Count.ShouldEqual(1);
        }

        [Test]
        public void CanShowSlide() {
            // Establish Context
            Slide slide = 
                SlideInstanceFactory.CreateValidTransientSlide();

            slideManagementService.Expect(r => r.Get(1))
                .Return(slide);

            // Act
            ViewResult result = slidesController.Show(1).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as Slide).ShouldNotBeNull();
            (result.ViewData.Model as Slide).ShouldEqual(slide);
        }

        [Test]
        public void CanInitCreate() {
            // Establish Context
            SlideFormViewModel viewModel = new SlideFormViewModel();

            slideManagementService.Expect(r => r.CreateFormViewModel())
                .Return(viewModel);

            // Act
            ViewResult result = slidesController.Create().AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as SlideFormViewModel).ShouldNotBeNull();
            (result.ViewData.Model as SlideFormViewModel).Slide.ShouldBeNull();
        }

        [Test]
        public void CanCreateValidSlideFromForm() {
            // Establish Context
            Slide slideFromForm = new Slide();

            slideManagementService.Expect(r => r.SaveOrUpdate(slideFromForm))
                .Return(ActionConfirmation.CreateSuccessConfirmation("saved"));

            // Act
            RedirectToRouteResult redirectResult =
                slidesController.Create(slideFromForm)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            slidesController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
				.ShouldEqual("saved");
        }

        [Test]
        public void CannotCreateInvalidSlideFromForm() {
            // Establish Context
            Slide slideFromForm = new Slide();
            SlideFormViewModel viewModelToExpect = new SlideFormViewModel();

            slideManagementService.Expect(r => r.SaveOrUpdate(slideFromForm))
                .Return(ActionConfirmation.CreateFailureConfirmation("not saved"));
            slideManagementService.Expect(r => r.CreateFormViewModelFor(slideFromForm))
                .Return(viewModelToExpect);

            // Act
            ViewResult result =
                slidesController.Create(slideFromForm).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as SlideFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanInitEdit() {
            // Establish Context
            SlideFormViewModel viewModel = new SlideFormViewModel();

            slideManagementService.Expect(r => r.CreateFormViewModelFor(1))
                .Return(viewModel);

            // Act
            ViewResult result = slidesController.Edit(1).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as SlideFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanUpdateValidSlideFromForm() {
            // Establish Context
            Slide slideFromForm = new Slide();

            slideManagementService.Expect(r => r.UpdateWith(slideFromForm, 0))
                .Return(ActionConfirmation.CreateSuccessConfirmation("updated"));

            // Act
            RedirectToRouteResult redirectResult =
                slidesController.Edit(slideFromForm)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            slidesController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
                .ShouldEqual("updated");
        }

        [Test]
        public void CannotUpdateInvalidSlideFromForm() {
            // Establish Context
            Slide slideFromForm = new Slide();
            SlideFormViewModel viewModelToExpect = new SlideFormViewModel();

            slideManagementService.Expect(r => r.UpdateWith(slideFromForm, 0))
                .Return(ActionConfirmation.CreateFailureConfirmation("not updated"));
            slideManagementService.Expect(r => r.CreateFormViewModelFor(slideFromForm))
                .Return(viewModelToExpect);

            // Act
            ViewResult result =
                slidesController.Edit(slideFromForm).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as SlideFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanDeleteSlide() {
            // Establish Context
            slideManagementService.Expect(r => r.Delete(1))
                .Return(ActionConfirmation.CreateSuccessConfirmation("deleted"));
            
            // Act
            RedirectToRouteResult redirectResult =
                slidesController.Delete(1)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            slidesController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
                .ShouldEqual("deleted");
        }

        private ISlideManagementService slideManagementService;
        private SlidesController slidesController;
    }
}
