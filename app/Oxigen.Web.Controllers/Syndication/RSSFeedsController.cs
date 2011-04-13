using System.Web.Mvc;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.DomainModel;
using System.Collections.Generic;
using SharpArch.Web.NHibernate;
using NHibernate.Validator.Engine;
using System.Text;
using SharpArch.Web.CommonValidator;
using SharpArch.Core;
using Oxigen.Core.Syndication;
using Oxigen.ApplicationServices.Syndication;
using Oxigen.ApplicationServices.ViewModels.Syndication;
using Oxigen.Core.QueryDtos.Syndication;
using Oxigen.Core.RepositoryInterfaces.Syndication;
using Oxigen.Core; 

namespace Oxigen.Web.Controllers.Syndication
{
    [HandleError]
    public class RSSFeedsController : Controller
    {
        public RSSFeedsController(IRSSFeedManagementService rSSFeedManagementService) {
            Check.Require(rSSFeedManagementService != null, "rSSFeedManagementService may not be null");

            this.rSSFeedManagementService = rSSFeedManagementService;
        }

        [Transaction]
        public ActionResult Index() {
            IList<RSSFeedDto> rSSFeeds = 
                rSSFeedManagementService.GetRSSFeedSummaries();
            return View(rSSFeeds);
        }

        [Transaction]
        public ActionResult Show(int id) {
            RSSFeed rSSFeed = rSSFeedManagementService.Get(id);
            return View(rSSFeed);
        }

        [Transaction]
        public ActionResult Create() {
            RSSFeedFormViewModel viewModel = 
                rSSFeedManagementService.CreateFormViewModel();
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(RSSFeed rSSFeed) {
            if (ViewData.ModelState.IsValid) {
                ActionConfirmation saveOrUpdateConfirmation = 
                    rSSFeedManagementService.SaveOrUpdate(rSSFeed);

                if (saveOrUpdateConfirmation.WasSuccessful) {
                    TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                        saveOrUpdateConfirmation.Message;
                    return RedirectToAction("Index");
                }
            } else {
                rSSFeed = null;
            }

            RSSFeedFormViewModel viewModel = 
                rSSFeedManagementService.CreateFormViewModelFor(rSSFeed);
            return View(viewModel);
        }

        [Transaction]
        public ActionResult Edit(int id) {
            RSSFeedFormViewModel viewModel = 
                rSSFeedManagementService.CreateFormViewModelFor(id);
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(RSSFeed rSSFeed) {
            if (ViewData.ModelState.IsValid) {
                ActionConfirmation updateConfirmation = 
                    rSSFeedManagementService.UpdateWith(rSSFeed, rSSFeed.Id);

                if (updateConfirmation.WasSuccessful) {
                    TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                        updateConfirmation.Message;
                    return RedirectToAction("Index");
                }
            }

            RSSFeedFormViewModel viewModel = 
                rSSFeedManagementService.CreateFormViewModelFor(rSSFeed);
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id) {
            ActionConfirmation deleteConfirmation = rSSFeedManagementService.Delete(id);
            TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                deleteConfirmation.Message;
            return RedirectToAction("Index");
        }

        private readonly IRSSFeedManagementService rSSFeedManagementService;
    }
}