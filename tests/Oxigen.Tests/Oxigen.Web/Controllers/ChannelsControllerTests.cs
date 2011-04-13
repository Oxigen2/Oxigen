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
    public class ChannelsControllerTests
    {
        [SetUp]
        public void SetUp() {
            ServiceLocatorInitializer.Init();

            channelManagementService =
                MockRepository.GenerateMock<IChannelManagementService>();
            channelsController = 
                new ChannelsController(channelManagementService);
        }

        [Test]
        public void CanListChannels() {
            // Establish Context
            IList<ChannelDto> channelSummariesToExpect = new List<ChannelDto>();

            ChannelDto channelsDto = new ChannelDto();
            channelSummariesToExpect.Add(channelsDto);

            channelManagementService.Expect(r => r.GetChannelSummaries())
                .Return(channelSummariesToExpect);

            // Act
            ViewResult result = channelsController.Index().AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as IList<ChannelDto>).ShouldNotBeNull();
            (result.ViewData.Model as IList<ChannelDto>).Count.ShouldEqual(1);
        }

        [Test]
        public void CanShowChannels() {
            // Establish Context
            Channel channel = 
                ChannelInstanceFactory.CreateValidTransientChannel();

            channelManagementService.Expect(r => r.Get(1))
                .Return(channel);

            // Act
            ViewResult result = channelsController.Show(1).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as Channel).ShouldNotBeNull();
            (result.ViewData.Model as Channel).ShouldEqual(channel);
        }

        [Test]
        public void CanInitCreate() {
            // Establish Context
            ChannelFormViewModel viewModel = new ChannelFormViewModel();

            channelManagementService.Expect(r => r.CreateFormViewModel())
                .Return(viewModel);

            // Act
            ViewResult result = channelsController.Create().AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as ChannelFormViewModel).ShouldNotBeNull();
            (result.ViewData.Model as ChannelFormViewModel).Channel.ShouldBeNull();
        }

        [Test]
        public void CanCreateValidChannelFromForm() {
            // Establish Context
            Channel channelFromForm = new Channel();

            channelManagementService.Expect(r => r.SaveOrUpdate(channelFromForm))
                .Return(ActionConfirmation.CreateSuccessConfirmation("saved"));

            // Act
            RedirectToRouteResult redirectResult =
                channelsController.Create(channelFromForm)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            channelsController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
				.ShouldEqual("saved");
        }

        [Test]
        public void CannotCreateInvalidChannelFromForm() {
            // Establish Context
            Channel channelFromForm = new Channel();
            ChannelFormViewModel viewModelToExpect = new ChannelFormViewModel();

            channelManagementService.Expect(r => r.SaveOrUpdate(channelFromForm))
                .Return(ActionConfirmation.CreateFailureConfirmation("not saved"));
            channelManagementService.Expect(r => r.CreateFormViewModelFor(channelFromForm))
                .Return(viewModelToExpect);

            // Act
            ViewResult result =
                channelsController.Create(channelFromForm).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as ChannelFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanInitEdit() {
            // Establish Context
            ChannelFormViewModel viewModel = new ChannelFormViewModel();

            channelManagementService.Expect(r => r.CreateFormViewModelFor(1))
                .Return(viewModel);

            // Act
            ViewResult result = channelsController.Edit(1).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as ChannelFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanUpdateValidChannelFromForm() {
            // Establish Context
            Channel channelFromForm = new Channel();

            channelManagementService.Expect(r => r.UpdateWith(channelFromForm, 0))
                .Return(ActionConfirmation.CreateSuccessConfirmation("updated"));

            // Act
            RedirectToRouteResult redirectResult =
                channelsController.Edit(channelFromForm)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            channelsController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
                .ShouldEqual("updated");
        }

        [Test]
        public void CannotUpdateInvalidChannelFromForm() {
            // Establish Context
            Channel channelFromForm = new Channel();
            ChannelFormViewModel viewModelToExpect = new ChannelFormViewModel();

            channelManagementService.Expect(r => r.UpdateWith(channelFromForm, 0))
                .Return(ActionConfirmation.CreateFailureConfirmation("not updated"));
            channelManagementService.Expect(r => r.CreateFormViewModelFor(channelFromForm))
                .Return(viewModelToExpect);

            // Act
            ViewResult result =
                channelsController.Edit(channelFromForm).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as ChannelFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanDeleteChannel() {
            // Establish Context
            channelManagementService.Expect(r => r.Delete(1))
                .Return(ActionConfirmation.CreateSuccessConfirmation("deleted"));
            
            // Act
            RedirectToRouteResult redirectResult =
                channelsController.Delete(1)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            channelsController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
                .ShouldEqual("deleted");
        }

        private IChannelManagementService channelManagementService;
        private ChannelsController channelsController;
    }
}
