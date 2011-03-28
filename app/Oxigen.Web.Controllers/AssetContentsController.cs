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
    public class AssetContentsController : Controller
    {
        public AssetContentsController(IAssetContentManagementService assetContentManagementService) {
            Check.Require(assetContentManagementService != null, "assetContentManagementService may not be null");

            this.assetContentManagementService = assetContentManagementService;
        }

        [Transaction]
        public ActionResult Index() {
            IList<AssetContentDto> assetContents = 
                assetContentManagementService.GetAssetContentSummaries();
            return View(assetContents);
        }

        [Transaction]
        public ActionResult Show(int id) {
            AssetContent assetContent = assetContentManagementService.Get(id);
            return View(assetContent);
        }

        [Transaction]
        public ActionResult Create() {
            AssetContentFormViewModel viewModel = 
                assetContentManagementService.CreateFormViewModel();
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(AssetContent assetContent) {
            if (ViewData.ModelState.IsValid) {
                ActionConfirmation saveOrUpdateConfirmation = 
                    assetContentManagementService.SaveOrUpdate(assetContent);

                if (saveOrUpdateConfirmation.WasSuccessful) {
                    TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                        saveOrUpdateConfirmation.Message;
                    return RedirectToAction("Index");
                }
            } else {
                assetContent = null;
            }

            AssetContentFormViewModel viewModel = 
                assetContentManagementService.CreateFormViewModelFor(assetContent);
            return View(viewModel);
        }

        [Transaction]
        public ActionResult Edit(int id) {
            AssetContentFormViewModel viewModel = 
                assetContentManagementService.CreateFormViewModelFor(id);
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(AssetContent assetContent) {
            if (ViewData.ModelState.IsValid) {
                ActionConfirmation updateConfirmation = 
                    assetContentManagementService.UpdateWith(assetContent, assetContent.Id);

                if (updateConfirmation.WasSuccessful) {
                    TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                        updateConfirmation.Message;
                    return RedirectToAction("Index");
                }
            }

            AssetContentFormViewModel viewModel = 
                assetContentManagementService.CreateFormViewModelFor(assetContent);
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id) {
            ActionConfirmation deleteConfirmation = assetContentManagementService.Delete(id);
            TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                deleteConfirmation.Message;
            return RedirectToAction("Index");
        }

        private readonly IAssetContentManagementService assetContentManagementService;
    }
}
