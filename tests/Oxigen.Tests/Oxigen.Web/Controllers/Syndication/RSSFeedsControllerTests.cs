using MvcContrib.TestHelper;
using NUnit.Framework;
using Rhino.Mocks;
using SharpArch.Testing.NUnit;
using System.Collections.Generic;
using System.Web.Mvc;
using Oxigen.Core.Syndication;
using Oxigen.Core.QueryDtos.Syndication;
using Oxigen.ApplicationServices.Syndication;
using Oxigen.ApplicationServices.ViewModels.Syndication;
using Oxigen.Web.Controllers.Syndication;
using Tests.Oxigen.Core.Syndication;
using Oxigen.Core;
using Oxigen.Web.Controllers; 

namespace Tests.Oxigen.Web.Controllers.Syndication
{
    [TestFixture]
    public class RSSFeedsControllerTests
    {
        [SetUp]
        public void SetUp() {
            ServiceLocatorInitializer.Init();

            rSSFeedManagementService =
                MockRepository.GenerateMock<IRSSFeedManagementService>();
            rSSFeedsController = 
                new RSSFeedsController(rSSFeedManagementService);
        }

        [Test]
        public void CanListRSSFeeds() {
            // Establish Context
            IList<RSSFeedDto> rSSFeedSummariesToExpect = new List<RSSFeedDto>();

            RSSFeedDto rSSFeedDto = new RSSFeedDto();
            rSSFeedSummariesToExpect.Add(rSSFeedDto);

            rSSFeedManagementService.Expect(r => r.GetRSSFeedSummaries())
                .Return(rSSFeedSummariesToExpect);

            // Act
            ViewResult result = rSSFeedsController.Index().AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as IList<RSSFeedDto>).ShouldNotBeNull();
            (result.ViewData.Model as IList<RSSFeedDto>).Count.ShouldEqual(1);
        }

        [Test]
        public void CanShowRSSFeed() {
            // Establish Context
            RSSFeed rSSFeed = 
                RSSFeedInstanceFactory.CreateValidTransientRSSFeed();

            rSSFeedManagementService.Expect(r => r.Get(1))
                .Return(rSSFeed);

            // Act
            ViewResult result = rSSFeedsController.Show(1).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as RSSFeed).ShouldNotBeNull();
            (result.ViewData.Model as RSSFeed).ShouldEqual(rSSFeed);
        }

        [Test]
        public void CanInitCreate() {
            // Establish Context
            RSSFeedFormViewModel viewModel = new RSSFeedFormViewModel();

            rSSFeedManagementService.Expect(r => r.CreateFormViewModel())
                .Return(viewModel);

            // Act
            ViewResult result = rSSFeedsController.Create().AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as RSSFeedFormViewModel).ShouldNotBeNull();
            (result.ViewData.Model as RSSFeedFormViewModel).RSSFeed.ShouldBeNull();
        }

        [Test]
        public void CanCreateValidRSSFeedFromForm() {
            // Establish Context
            RSSFeed rSSFeedFromForm = new RSSFeed();

            rSSFeedManagementService.Expect(r => r.SaveOrUpdate(rSSFeedFromForm))
                .Return(ActionConfirmation.CreateSuccessConfirmation("saved"));

            // Act
            RedirectToRouteResult redirectResult =
                rSSFeedsController.Create(rSSFeedFromForm)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            rSSFeedsController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
				.ShouldEqual("saved");
        }

        [Test]
        public void CannotCreateInvalidRSSFeedFromForm() {
            // Establish Context
            RSSFeed rSSFeedFromForm = new RSSFeed();
            RSSFeedFormViewModel viewModelToExpect = new RSSFeedFormViewModel();

            rSSFeedManagementService.Expect(r => r.SaveOrUpdate(rSSFeedFromForm))
                .Return(ActionConfirmation.CreateFailureConfirmation("not saved"));
            rSSFeedManagementService.Expect(r => r.CreateFormViewModelFor(rSSFeedFromForm))
                .Return(viewModelToExpect);

            // Act
            ViewResult result =
                rSSFeedsController.Create(rSSFeedFromForm).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as RSSFeedFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanInitEdit() {
            // Establish Context
            RSSFeedFormViewModel viewModel = new RSSFeedFormViewModel();

            rSSFeedManagementService.Expect(r => r.CreateFormViewModelFor(1))
                .Return(viewModel);

            // Act
            ViewResult result = rSSFeedsController.Edit(1).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as RSSFeedFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanUpdateValidRSSFeedFromForm() {
            // Establish Context
            RSSFeed rSSFeedFromForm = new RSSFeed();

            rSSFeedManagementService.Expect(r => r.UpdateWith(rSSFeedFromForm, 0))
                .Return(ActionConfirmation.CreateSuccessConfirmation("updated"));

            // Act
            RedirectToRouteResult redirectResult =
                rSSFeedsController.Edit(rSSFeedFromForm)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            rSSFeedsController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
                .ShouldEqual("updated");
        }

        [Test]
        public void CannotUpdateInvalidRSSFeedFromForm() {
            // Establish Context
            RSSFeed rSSFeedFromForm = new RSSFeed();
            RSSFeedFormViewModel viewModelToExpect = new RSSFeedFormViewModel();

            rSSFeedManagementService.Expect(r => r.UpdateWith(rSSFeedFromForm, 0))
                .Return(ActionConfirmation.CreateFailureConfirmation("not updated"));
            rSSFeedManagementService.Expect(r => r.CreateFormViewModelFor(rSSFeedFromForm))
                .Return(viewModelToExpect);

            // Act
            ViewResult result =
                rSSFeedsController.Edit(rSSFeedFromForm).AssertViewRendered();

            // Assert
            result.ViewData.Model.ShouldNotBeNull();
            (result.ViewData.Model as RSSFeedFormViewModel).ShouldNotBeNull();
        }

        [Test]
        public void CanDeleteRSSFeed() {
            // Establish Context
            rSSFeedManagementService.Expect(r => r.Delete(1))
                .Return(ActionConfirmation.CreateSuccessConfirmation("deleted"));
            
            // Act
            RedirectToRouteResult redirectResult =
                rSSFeedsController.Delete(1)
                .AssertActionRedirect().ToAction("Index");

            // Assert
            rSSFeedsController.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()].ToString()
                .ShouldEqual("deleted");
        }

        private IRSSFeedManagementService rSSFeedManagementService;
        private RSSFeedsController rSSFeedsController;
    }
}
