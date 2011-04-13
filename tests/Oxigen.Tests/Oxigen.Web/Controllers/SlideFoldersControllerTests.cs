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
    public class SlideFoldersControllerTests
    {
        [SetUp]
        public void SetUp() {
            ServiceLocatorInitializer.Init();

            slideFolderManagementService =
                MockRepository.GenerateMock<ISlideFolderManagementService>();
            slideFoldersController = 
                new SlideFoldersController(slideFolderManagementService);
        }

        [Test]
        public void CanListSlideFolders() {
            // Establish Context
            IList<SlideFolderDto> slideFolderSummariesToExpect = new List<SlideFolderDto>();

            SlideFolderDto slideFolderDto = new SlideFolderDto();
            slideFolderSummariesToExpect.Add(slideFolderDto);

            slideFolderManagementService.Expect(r => r.GetSlideFolderSummaries())
                .Return(slideFolderSummariesToExpect);

            // Act
            ViewResult result = slideFoldersController.Index().AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as IList<SlideFolderDto>).ShouldNotBeNull();
            (result.ViewData.Model as IList<SlideFolderDto>).Count.ShouldEqual(1);
        }

        [Test]
        public void CanShowSlideFolder() {
            // Establish Context
            SlideFolder slideFolder = 
                SlideFolderInstanceFactory.CreateValidTransientSlideFolder();

            slideFolderManagementService.Expect(r => r.Get(1))
                .Return(slideFolder);

            // Act
            ViewResult result = slideFoldersController.Show(1).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as SlideFolder).ShouldNotBeNull();
            (result.ViewData.Model as SlideFolder).ShouldEqual(slideFolder);
        }

        [Test]
        public void CanInitCreate() {
            // Establish Context
            SlideFolderFormViewModel viewModel = new SlideFolderFormViewModel();

            slideFolderManagementService.Expect(r => r.CreateFormViewModel())
                .Return(viewModel);

            // Act
            ViewResult result = slideFoldersController.Create().AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as SlideFolderFormViewModel).ShouldNotBeNull();
            (result.ViewData.Model as SlideFolderFormViewModel).SlideFolder.ShouldBeNull();
        }

        [Test]
        public void CanCreateValidSlideFolderFromForm() {
            // Establish Context
            SlideFolder slideFolderFromForm = new SlideFolder();

            slideFolderManagementService.Expect(r => r.SaveOrUpdate(slideFolderFromForm))
                .Return(ActionConfirmation.CreateSuccessConfirmation("saved"));

            // Act
            RedirectToRouteResult redirectResult =
                slideFoldersController.Create(slideFolderFromForm)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            slideFoldersController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
				.ShouldEqual("saved");
        }

        [Test]
        public void CannotCreateInvalidSlideFolderFromForm() {
            // Establish Context
            SlideFolder slideFolderFromForm = new SlideFolder();
            SlideFolderFormViewModel viewModelToExpect = new SlideFolderFormViewModel();

            slideFolderManagementService.Expect(r => r.SaveOrUpdate(slideFolderFromForm))
                .Return(ActionConfirmation.CreateFailureConfirmation("not saved"));
            slideFolderManagementService.Expect(r => r.CreateFormViewModelFor(slideFolderFromForm))
                .Return(viewModelToExpect);

            // Act
            ViewResult result =
                slideFoldersController.Create(slideFolderFromForm).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as SlideFolderFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanInitEdit() {
            // Establish Context
            SlideFolderFormViewModel viewModel = new SlideFolderFormViewModel();

            slideFolderManagementService.Expect(r => r.CreateFormViewModelFor(1))
                .Return(viewModel);

            // Act
            ViewResult result = slideFoldersController.Edit(1).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as SlideFolderFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanUpdateValidSlideFolderFromForm() {
            // Establish Context
            SlideFolder slideFolderFromForm = new SlideFolder();

            slideFolderManagementService.Expect(r => r.UpdateWith(slideFolderFromForm, 0))
                .Return(ActionConfirmation.CreateSuccessConfirmation("updated"));

            // Act
            RedirectToRouteResult redirectResult =
                slideFoldersController.Edit(slideFolderFromForm)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            slideFoldersController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
                .ShouldEqual("updated");
        }

        [Test]
        public void CannotUpdateInvalidSlideFolderFromForm() {
            // Establish Context
            SlideFolder slideFolderFromForm = new SlideFolder();
            SlideFolderFormViewModel viewModelToExpect = new SlideFolderFormViewModel();

            slideFolderManagementService.Expect(r => r.UpdateWith(slideFolderFromForm, 0))
                .Return(ActionConfirmation.CreateFailureConfirmation("not updated"));
            slideFolderManagementService.Expect(r => r.CreateFormViewModelFor(slideFolderFromForm))
                .Return(viewModelToExpect);

            // Act
            ViewResult result =
                slideFoldersController.Edit(slideFolderFromForm).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as SlideFolderFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanDeleteSlideFolder() {
            // Establish Context
            slideFolderManagementService.Expect(r => r.Delete(1))
                .Return(ActionConfirmation.CreateSuccessConfirmation("deleted"));
            
            // Act
            RedirectToRouteResult redirectResult =
                slideFoldersController.Delete(1)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            slideFoldersController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
                .ShouldEqual("deleted");
        }

        private ISlideFolderManagementService slideFolderManagementService;
        private SlideFoldersController slideFoldersController;
    }
}
