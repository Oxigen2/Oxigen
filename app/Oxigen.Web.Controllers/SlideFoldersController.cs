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
    public class SlideFoldersController : Controller
    {
        public SlideFoldersController(ISlideFolderManagementService slideFolderManagementService) {
            Check.Require(slideFolderManagementService != null, "slideFolderManagementService may not be null");

            this.slideFolderManagementService = slideFolderManagementService;
        }

        [Transaction]
        public ActionResult Index() {
            IList<SlideFolderDto> slideFolders = 
                slideFolderManagementService.GetSlideFolderSummaries();
            return View(slideFolders);
        }

        [Transaction]
        public ActionResult ListByProducer(int id)
        {
            IList<SlideFolderDto> slideFolders =
                slideFolderManagementService.GetByProducer(id);
            return Json(slideFolders, JsonRequestBehavior.AllowGet);
        }

        [Transaction]
        public ActionResult Show(int id) {
            SlideFolder slideFolder = slideFolderManagementService.Get(id);
            return View(slideFolder);
        }

        [Transaction]
        public ActionResult Create() {
            SlideFolderFormViewModel viewModel = 
                slideFolderManagementService.CreateFormViewModel();
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(SlideFolder slideFolder) {
            if (ViewData.ModelState.IsValid) {
                ActionConfirmation saveOrUpdateConfirmation = 
                    slideFolderManagementService.SaveOrUpdate(slideFolder);

                if (saveOrUpdateConfirmation.WasSuccessful) {
                    TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                        saveOrUpdateConfirmation.Message;
                    return RedirectToAction("Index");
                }
            } else {
                slideFolder = null;
            }

            SlideFolderFormViewModel viewModel = 
                slideFolderManagementService.CreateFormViewModelFor(slideFolder);
            return View(viewModel);
        }

        [Transaction]
        public ActionResult Edit(int id) {
            SlideFolderFormViewModel viewModel = 
                slideFolderManagementService.CreateFormViewModelFor(id);
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(SlideFolder slideFolder) {
            if (ViewData.ModelState.IsValid) {
                ActionConfirmation updateConfirmation = 
                    slideFolderManagementService.UpdateWith(slideFolder, slideFolder.Id);

                if (updateConfirmation.WasSuccessful) {
                    TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                        updateConfirmation.Message;
                    return RedirectToAction("Index");
                }
            }

            SlideFolderFormViewModel viewModel = 
                slideFolderManagementService.CreateFormViewModelFor(slideFolder);
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id) {
            ActionConfirmation deleteConfirmation = slideFolderManagementService.Delete(id);
            TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                deleteConfirmation.Message;
            return RedirectToAction("Index");
        }

        private readonly ISlideFolderManagementService slideFolderManagementService;
    }
}
