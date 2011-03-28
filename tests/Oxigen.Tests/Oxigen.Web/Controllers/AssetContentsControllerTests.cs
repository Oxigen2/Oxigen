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
    public class AssetContentsControllerTests
    {
        [SetUp]
        public void SetUp() {
            ServiceLocatorInitializer.Init();

            assetContentManagementService =
                MockRepository.GenerateMock<IAssetContentManagementService>();
            assetContentsController = 
                new AssetContentsController(assetContentManagementService);
        }

        [Test]
        public void CanListAssetContents() {
            // Establish Context
            IList<AssetContentDto> assetContentSummariesToExpect = new List<AssetContentDto>();

            AssetContentDto assetContentDto = new AssetContentDto();
            assetContentSummariesToExpect.Add(assetContentDto);

            assetContentManagementService.Expect(r => r.GetAssetContentSummaries())
                .Return(assetContentSummariesToExpect);

            // Act
            ViewResult result = assetContentsController.Index().AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as IList<AssetContentDto>).ShouldNotBeNull();
            (result.ViewData.Model as IList<AssetContentDto>).Count.ShouldEqual(1);
        }

        [Test]
        public void CanShowAssetContent() {
            // Establish Context
            AssetContent assetContent = 
                AssetContentInstanceFactory.CreateValidTransientAssetContent();

            assetContentManagementService.Expect(r => r.Get(1))
                .Return(assetContent);

            // Act
            ViewResult result = assetContentsController.Show(1).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as AssetContent).ShouldNotBeNull();
            (result.ViewData.Model as AssetContent).ShouldEqual(assetContent);
        }

        [Test]
        public void CanInitCreate() {
            // Establish Context
            AssetContentFormViewModel viewModel = new AssetContentFormViewModel();

            assetContentManagementService.Expect(r => r.CreateFormViewModel())
                .Return(viewModel);

            // Act
            ViewResult result = assetContentsController.Create().AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as AssetContentFormViewModel).ShouldNotBeNull();
            (result.ViewData.Model as AssetContentFormViewModel).AssetContent.ShouldBeNull();
        }

        [Test]
        public void CanCreateValidAssetContentFromForm() {
            // Establish Context
            AssetContent assetContentFromForm = new AssetContent();

            assetContentManagementService.Expect(r => r.SaveOrUpdate(assetContentFromForm))
                .Return(ActionConfirmation.CreateSuccessConfirmation("saved"));

            // Act
            RedirectToRouteResult redirectResult =
                assetContentsController.Create(assetContentFromForm)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            assetContentsController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
				.ShouldEqual("saved");
        }

        [Test]
        public void CannotCreateInvalidAssetContentFromForm() {
            // Establish Context
            AssetContent assetContentFromForm = new AssetContent();
            AssetContentFormViewModel viewModelToExpect = new AssetContentFormViewModel();

            assetContentManagementService.Expect(r => r.SaveOrUpdate(assetContentFromForm))
                .Return(ActionConfirmation.CreateFailureConfirmation("not saved"));
            assetContentManagementService.Expect(r => r.CreateFormViewModelFor(assetContentFromForm))
                .Return(viewModelToExpect);

            // Act
            ViewResult result =
                assetContentsController.Create(assetContentFromForm).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as AssetContentFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanInitEdit() {
            // Establish Context
            AssetContentFormViewModel viewModel = new AssetContentFormViewModel();

            assetContentManagementService.Expect(r => r.CreateFormViewModelFor(1))
                .Return(viewModel);

            // Act
            ViewResult result = assetContentsController.Edit(1).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as AssetContentFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanUpdateValidAssetContentFromForm() {
            // Establish Context
            AssetContent assetContentFromForm = new AssetContent();

            assetContentManagementService.Expect(r => r.UpdateWith(assetContentFromForm, 0))
                .Return(ActionConfirmation.CreateSuccessConfirmation("updated"));

            // Act
            RedirectToRouteResult redirectResult =
                assetContentsController.Edit(assetContentFromForm)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            assetContentsController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
                .ShouldEqual("updated");
        }

        [Test]
        public void CannotUpdateInvalidAssetContentFromForm() {
            // Establish Context
            AssetContent assetContentFromForm = new AssetContent();
            AssetContentFormViewModel viewModelToExpect = new AssetContentFormViewModel();

            assetContentManagementService.Expect(r => r.UpdateWith(assetContentFromForm, 0))
                .Return(ActionConfirmation.CreateFailureConfirmation("not updated"));
            assetContentManagementService.Expect(r => r.CreateFormViewModelFor(assetContentFromForm))
                .Return(viewModelToExpect);

            // Act
            ViewResult result =
                assetContentsController.Edit(assetContentFromForm).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as AssetContentFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanDeleteAssetContent() {
            // Establish Context
            assetContentManagementService.Expect(r => r.Delete(1))
                .Return(ActionConfirmation.CreateSuccessConfirmation("deleted"));
            
            // Act
            RedirectToRouteResult redirectResult =
                assetContentsController.Delete(1)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            assetContentsController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
                .ShouldEqual("deleted");
        }

        private IAssetContentManagementService assetContentManagementService;
        private AssetContentsController assetContentsController;
    }
}
