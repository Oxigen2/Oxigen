using System.Web.Mvc;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.DomainModel;
using System.Collections.Generic;
using SharpArch.Web.NHibernate;
using NHibernate.Validator.Engine;
using System.Text;
using SharpArch.Web.CommonValidator;
using SharpArch.Core;
using Oxigen.Core;
using Oxigen.ApplicationServices;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;
 

namespace Oxigen.Web.Controllers
{
    [HandleError]
    public class PublishersController : Controller
    {
        public PublishersController(IPublisherManagementService publisherManagementService) {
            Check.Require(publisherManagementService != null, "publisherManagementService may not be null");

            this.publisherManagementService = publisherManagementService;
        }

        [Transaction]
        public ActionResult Index() {
            IList<PublisherDto> publishers = 
                publisherManagementService.GetPublisherSummaries();
            return View(publishers);
        }

        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetPublishersByPartialName(string partialName) {
            IList<PublisherLookupDto> publishers = publisherManagementService.GetPublishersByPartialName(partialName);
            return Json(publishers);
        }

        [Transaction]
        public ActionResult Show(int id) {
            Publisher publisher = publisherManagementService.Get(id);
            return View(publisher);
        }

        [Transaction]
        public ActionResult Create() {
            PublisherFormViewModel viewModel = 
                publisherManagementService.CreateFormViewModel();
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Publisher publisher) {
            if (ViewData.ModelState.IsValid) {
                ActionConfirmation saveOrUpdateConfirmation = 
                    publisherManagementService.SaveOrUpdate(publisher);

                if (saveOrUpdateConfirmation.WasSuccessful) {
                    TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                        saveOrUpdateConfirmation.Message;
                    return RedirectToAction("Index");
                }
            } else {
                publisher = null;
            }

            PublisherFormViewModel viewModel = 
                publisherManagementService.CreateFormViewModelFor(publisher);
            return View(viewModel);
        }

        [Transaction]
        public ActionResult Edit(int id) {
            PublisherFormViewModel viewModel = 
                publisherManagementService.CreateFormViewModelFor(id);
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Publisher publisher) {
            if (ViewData.ModelState.IsValid) {
                ActionConfirmation updateConfirmation = 
                    publisherManagementService.UpdateWith(publisher, publisher.Id);

                if (updateConfirmation.WasSuccessful) {
                    TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                        updateConfirmation.Message;
                    return RedirectToAction("Index");
                }
            }

            PublisherFormViewModel viewModel = 
                publisherManagementService.CreateFormViewModelFor(publisher);
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id) {
            ActionConfirmation deleteConfirmation = publisherManagementService.Delete(id);
            TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                deleteConfirmation.Message;
            return RedirectToAction("Index");
        }

        private readonly IPublisherManagementService publisherManagementService;
    }
}
