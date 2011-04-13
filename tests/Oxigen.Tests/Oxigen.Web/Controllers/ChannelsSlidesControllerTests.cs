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
    public class ChannelsSlidesControllerTests
    {
        [SetUp]
        public void SetUp() {
            ServiceLocatorInitializer.Init();

            channelsSlideManagementService =
                MockRepository.GenerateMock<IChannelsSlideManagementService>();
            channelsSlidesController = 
                new ChannelsSlidesController(channelsSlideManagementService);
        }

        [Test]
        public void CanListChannelsSlides() {
            // Establish Context
            IList<ChannelsSlideDto> channelsSlideSummariesToExpect = new List<ChannelsSlideDto>();

            ChannelsSlideDto channelsSlideDto = new ChannelsSlideDto();
            channelsSlideSummariesToExpect.Add(channelsSlideDto);

            channelsSlideManagementService.Expect(r => r.GetChannelsSlideSummaries())
                .Return(channelsSlideSummariesToExpect);

            // Act
            ViewResult result = channelsSlidesController.Index().AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as IList<ChannelsSlideDto>).ShouldNotBeNull();
            (result.ViewData.Model as IList<ChannelsSlideDto>).Count.ShouldEqual(1);
        }

        [Test]
        public void CanShowChannelsSlide() {
            // Establish Context
            ChannelsSlide channelsSlide = 
                ChannelsSlideInstanceFactory.CreateValidTransientChannelsSlide();

            channelsSlideManagementService.Expect(r => r.Get(1))
                .Return(channelsSlide);

            // Act
            ViewResult result = channelsSlidesController.Show(1).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as ChannelsSlide).ShouldNotBeNull();
            (result.ViewData.Model as ChannelsSlide).ShouldEqual(channelsSlide);
        }

        [Test]
        public void CanInitCreate() {
            // Establish Context
            ChannelsSlideFormViewModel viewModel = new ChannelsSlideFormViewModel();

            channelsSlideManagementService.Expect(r => r.CreateFormViewModel())
                .Return(viewModel);

            // Act
            ViewResult result = channelsSlidesController.Create().AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as ChannelsSlideFormViewModel).ShouldNotBeNull();
            (result.ViewData.Model as ChannelsSlideFormViewModel).ChannelsSlide.ShouldBeNull();
        }

        [Test]
        public void CanCreateValidChannelsSlideFromForm() {
            // Establish Context
            ChannelsSlide channelsSlideFromForm = new ChannelsSlide();

            channelsSlideManagementService.Expect(r => r.SaveOrUpdate(channelsSlideFromForm))
                .Return(ActionConfirmation.CreateSuccessConfirmation("saved"));

            // Act
            RedirectToRouteResult redirectResult =
                channelsSlidesController.Create(channelsSlideFromForm)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            channelsSlidesController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
				.ShouldEqual("saved");
        }

        [Test]
        public void CannotCreateInvalidChannelsSlideFromForm() {
            // Establish Context
            ChannelsSlide channelsSlideFromForm = new ChannelsSlide();
            ChannelsSlideFormViewModel viewModelToExpect = new ChannelsSlideFormViewModel();

            channelsSlideManagementService.Expect(r => r.SaveOrUpdate(channelsSlideFromForm))
                .Return(ActionConfirmation.CreateFailureConfirmation("not saved"));
            channelsSlideManagementService.Expect(r => r.CreateFormViewModelFor(channelsSlideFromForm))
                .Return(viewModelToExpect);

            // Act
            ViewResult result =
                channelsSlidesController.Create(channelsSlideFromForm).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as ChannelsSlideFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanInitEdit() {
            // Establish Context
            ChannelsSlideFormViewModel viewModel = new ChannelsSlideFormViewModel();

            channelsSlideManagementService.Expect(r => r.CreateFormViewModelFor(1))
                .Return(viewModel);

            // Act
            ViewResult result = channelsSlidesController.Edit(1).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as ChannelsSlideFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanUpdateValidChannelsSlideFromForm() {
            // Establish Context
            ChannelsSlide channelsSlideFromForm = new ChannelsSlide();

            channelsSlideManagementService.Expect(r => r.UpdateWith(channelsSlideFromForm, 0))
                .Return(ActionConfirmation.CreateSuccessConfirmation("updated"));

            // Act
            RedirectToRouteResult redirectResult =
                channelsSlidesController.Edit(channelsSlideFromForm)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            channelsSlidesController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
                .ShouldEqual("updated");
        }

        [Test]
        public void CannotUpdateInvalidChannelsSlideFromForm() {
            // Establish Context
            ChannelsSlide channelsSlideFromForm = new ChannelsSlide();
            ChannelsSlideFormViewModel viewModelToExpect = new ChannelsSlideFormViewModel();

            channelsSlideManagementService.Expect(r => r.UpdateWith(channelsSlideFromForm, 0))
                .Return(ActionConfirmation.CreateFailureConfirmation("not updated"));
            channelsSlideManagementService.Expect(r => r.CreateFormViewModelFor(channelsSlideFromForm))
                .Return(viewModelToExpect);

            // Act
            ViewResult result =
                channelsSlidesController.Edit(channelsSlideFromForm).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as ChannelsSlideFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanDeleteChannelsSlide() {
            // Establish Context
            channelsSlideManagementService.Expect(r => r.Delete(1))
                .Return(ActionConfirmation.CreateSuccessConfirmation("deleted"));
            
            // Act
            RedirectToRouteResult redirectResult =
                channelsSlidesController.Delete(1)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            channelsSlidesController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
                .ShouldEqual("deleted");
        }

        private IChannelsSlideManagementService channelsSlideManagementService;
        private ChannelsSlidesController channelsSlidesController;
    }
}
