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
    public class TemplatesControllerTests
    {
        [SetUp]
        public void SetUp() {
            ServiceLocatorInitializer.Init();

            templateManagementService =
                MockRepository.GenerateMock<ITemplateManagementService>();
            templatesController = 
                new TemplatesController(templateManagementService);
        }

        [Test]
        public void CanListTemplates() {
            // Establish Context
            IList<TemplateDto> templateSummariesToExpect = new List<TemplateDto>();

            TemplateDto templateDto = new TemplateDto();
            templateSummariesToExpect.Add(templateDto);

            templateManagementService.Expect(r => r.GetTemplateSummaries())
                .Return(templateSummariesToExpect);

            // Act
            ViewResult result = templatesController.Index().AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as IList<TemplateDto>).ShouldNotBeNull();
            (result.ViewData.Model as IList<TemplateDto>).Count.ShouldEqual(1);
        }

        [Test]
        public void CanShowTemplate() {
            // Establish Context
            Template template = 
                TemplateInstanceFactory.CreateValidTransientTemplate();

            templateManagementService.Expect(r => r.Get(1))
                .Return(template);

            // Act
            ViewResult result = templatesController.Show(1).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as Template).ShouldNotBeNull();
            (result.ViewData.Model as Template).ShouldEqual(template);
        }

        [Test]
        public void CanInitCreate() {
            // Establish Context
            TemplateFormViewModel viewModel = new TemplateFormViewModel();

            templateManagementService.Expect(r => r.CreateFormViewModel())
                .Return(viewModel);

            // Act
            ViewResult result = templatesController.Create().AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as TemplateFormViewModel).ShouldNotBeNull();
            (result.ViewData.Model as TemplateFormViewModel).Template.ShouldBeNull();
        }

        [Test]
        public void CanCreateValidTemplateFromForm() {
            // Establish Context
            Template templateFromForm = new Template();

            templateManagementService.Expect(r => r.SaveOrUpdate(templateFromForm))
                .Return(ActionConfirmation.CreateSuccessConfirmation("saved"));

            // Act
            RedirectToRouteResult redirectResult =
                templatesController.Create(templateFromForm)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            templatesController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
				.ShouldEqual("saved");
        }

        [Test]
        public void CannotCreateInvalidTemplateFromForm() {
            // Establish Context
            Template templateFromForm = new Template();
            TemplateFormViewModel viewModelToExpect = new TemplateFormViewModel();

            templateManagementService.Expect(r => r.SaveOrUpdate(templateFromForm))
                .Return(ActionConfirmation.CreateFailureConfirmation("not saved"));
            templateManagementService.Expect(r => r.CreateFormViewModelFor(templateFromForm))
                .Return(viewModelToExpect);

            // Act
            ViewResult result =
                templatesController.Create(templateFromForm).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as TemplateFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanInitEdit() {
            // Establish Context
            TemplateFormViewModel viewModel = new TemplateFormViewModel();

            templateManagementService.Expect(r => r.CreateFormViewModelFor(1))
                .Return(viewModel);

            // Act
            ViewResult result = templatesController.Edit(1).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as TemplateFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanUpdateValidTemplateFromForm() {
            // Establish Context
            Template templateFromForm = new Template();

            templateManagementService.Expect(r => r.UpdateWith(templateFromForm, 0))
                .Return(ActionConfirmation.CreateSuccessConfirmation("updated"));

            // Act
            RedirectToRouteResult redirectResult =
                templatesController.Edit(templateFromForm)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            templatesController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
                .ShouldEqual("updated");
        }

        [Test]
        public void CannotUpdateInvalidTemplateFromForm() {
            // Establish Context
            Template templateFromForm = new Template();
            TemplateFormViewModel viewModelToExpect = new TemplateFormViewModel();

            templateManagementService.Expect(r => r.UpdateWith(templateFromForm, 0))
                .Return(ActionConfirmation.CreateFailureConfirmation("not updated"));
            templateManagementService.Expect(r => r.CreateFormViewModelFor(templateFromForm))
                .Return(viewModelToExpect);

            // Act
            ViewResult result =
                templatesController.Edit(templateFromForm).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as TemplateFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanDeleteTemplate() {
            // Establish Context
            templateManagementService.Expect(r => r.Delete(1))
                .Return(ActionConfirmation.CreateSuccessConfirmation("deleted"));
            
            // Act
            RedirectToRouteResult redirectResult =
                templatesController.Delete(1)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            templatesController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
                .ShouldEqual("deleted");
        }

        private ITemplateManagementService templateManagementService;
        private TemplatesController templatesController;
    }
}
