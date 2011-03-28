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
    public class PublishersControllerTests
    {
        [SetUp]
        public void SetUp() {
            ServiceLocatorInitializer.Init();

            publisherManagementService =
                MockRepository.GenerateMock<IPublisherManagementService>();
            publishersController = 
                new PublishersController(publisherManagementService);
        }

        [Test]
        public void CanListPublishers() {
            // Establish Context
            IList<PublisherDto> publisherSummariesToExpect = new List<PublisherDto>();

            PublisherDto publisherDto = new PublisherDto();
            publisherSummariesToExpect.Add(publisherDto);

            publisherManagementService.Expect(r => r.GetPublisherSummaries())
                .Return(publisherSummariesToExpect);

            // Act
            ViewResult result = publishersController.Index().AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as IList<PublisherDto>).ShouldNotBeNull();
            (result.ViewData.Model as IList<PublisherDto>).Count.ShouldEqual(1);
        }

        [Test]
        public void CanShowPublisher() {
            // Establish Context
            Publisher publisher = 
                PublisherInstanceFactory.CreateValidTransientPublisher();

            publisherManagementService.Expect(r => r.Get(1))
                .Return(publisher);

            // Act
            ViewResult result = publishersController.Show(1).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as Publisher).ShouldNotBeNull();
            (result.ViewData.Model as Publisher).ShouldEqual(publisher);
        }

        [Test]
        public void CanInitCreate() {
            // Establish Context
            PublisherFormViewModel viewModel = new PublisherFormViewModel();

            publisherManagementService.Expect(r => r.CreateFormViewModel())
                .Return(viewModel);

            // Act
            ViewResult result = publishersController.Create().AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as PublisherFormViewModel).ShouldNotBeNull();
            (result.ViewData.Model as PublisherFormViewModel).Publisher.ShouldBeNull();
        }

        [Test]
        public void CanCreateValidPublisherFromForm() {
            // Establish Context
            Publisher publisherFromForm = new Publisher();

            publisherManagementService.Expect(r => r.SaveOrUpdate(publisherFromForm))
                .Return(ActionConfirmation.CreateSuccessConfirmation("saved"));

            // Act
            RedirectToRouteResult redirectResult =
                publishersController.Create(publisherFromForm)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            publishersController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
				.ShouldEqual("saved");
        }

        [Test]
        public void CannotCreateInvalidPublisherFromForm() {
            // Establish Context
            Publisher publisherFromForm = new Publisher();
            PublisherFormViewModel viewModelToExpect = new PublisherFormViewModel();

            publisherManagementService.Expect(r => r.SaveOrUpdate(publisherFromForm))
                .Return(ActionConfirmation.CreateFailureConfirmation("not saved"));
            publisherManagementService.Expect(r => r.CreateFormViewModelFor(publisherFromForm))
                .Return(viewModelToExpect);

            // Act
            ViewResult result =
                publishersController.Create(publisherFromForm).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as PublisherFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanInitEdit() {
            // Establish Context
            PublisherFormViewModel viewModel = new PublisherFormViewModel();

            publisherManagementService.Expect(r => r.CreateFormViewModelFor(1))
                .Return(viewModel);

            // Act
            ViewResult result = publishersController.Edit(1).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as PublisherFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanUpdateValidPublisherFromForm() {
            // Establish Context
            Publisher publisherFromForm = new Publisher();

            publisherManagementService.Expect(r => r.UpdateWith(publisherFromForm, 0))
                .Return(ActionConfirmation.CreateSuccessConfirmation("updated"));

            // Act
            RedirectToRouteResult redirectResult =
                publishersController.Edit(publisherFromForm)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            publishersController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
                .ShouldEqual("updated");
        }

        [Test]
        public void CannotUpdateInvalidPublisherFromForm() {
            // Establish Context
            Publisher publisherFromForm = new Publisher();
            PublisherFormViewModel viewModelToExpect = new PublisherFormViewModel();

            publisherManagementService.Expect(r => r.UpdateWith(publisherFromForm, 0))
                .Return(ActionConfirmation.CreateFailureConfirmation("not updated"));
            publisherManagementService.Expect(r => r.CreateFormViewModelFor(publisherFromForm))
                .Return(viewModelToExpect);

            // Act
            ViewResult result =
                publishersController.Edit(publisherFromForm).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as PublisherFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanDeletePublisher() {
            // Establish Context
            publisherManagementService.Expect(r => r.Delete(1))
                .Return(ActionConfirmation.CreateSuccessConfirmation("deleted"));
            
            // Act
            RedirectToRouteResult redirectResult =
                publishersController.Delete(1)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            publishersController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
                .ShouldEqual("deleted");
        }

        private IPublisherManagementService publisherManagementService;
        private PublishersController publishersController;
    }
}
