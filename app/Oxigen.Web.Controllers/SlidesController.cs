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
    public class SlidesController : Controller
    {
        public SlidesController(ISlideManagementService slideManagementService) {
            Check.Require(slideManagementService != null, "slideManagementService may not be null");

            this.slideManagementService = slideManagementService;
        }

        [Transaction]
        public ActionResult Index() {
            IList<SlideDto> slides = 
                slideManagementService.GetSlideSummaries();
            return View(slides);
        }

        [Transaction]
        public ActionResult Show(int id) {
            Slide slide = slideManagementService.Get(id);
            return View(slide);
        }

        [Transaction]
        public ActionResult Create() {
            SlideFormViewModel viewModel = 
                slideManagementService.CreateFormViewModel();
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Slide slide) {
            if (ViewData.ModelState.IsValid) {
                ActionConfirmation saveOrUpdateConfirmation = 
                    slideManagementService.SaveOrUpdate(slide);

                if (saveOrUpdateConfirmation.WasSuccessful) {
                    TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                        saveOrUpdateConfirmation.Message;   
                    return RedirectToAction("Index");
                }
            } else {
                slide = null;
            }

            SlideFormViewModel viewModel = 
                slideManagementService.CreateFormViewModelFor(slide);
            return View(viewModel);
        }

        [Transaction]
        public ActionResult Edit(int id) {
            SlideFormViewModel viewModel = 
                slideManagementService.CreateFormViewModelFor(id);
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Slide slide) {
            if (ViewData.ModelState.IsValid) {
                ActionConfirmation updateConfirmation = 
                    slideManagementService.UpdateWith(slide, slide.Id);

                if (updateConfirmation.WasSuccessful) {
                    TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                        updateConfirmation.Message;
                    return RedirectToAction("Index");
                }
            }

            SlideFormViewModel viewModel = 
                slideManagementService.CreateFormViewModelFor(slide);
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id) {
            ActionConfirmation deleteConfirmation = slideManagementService.Delete(id);
            TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                deleteConfirmation.Message;
            return RedirectToAction("Index");
        }

        private readonly ISlideManagementService slideManagementService;
    }
}
