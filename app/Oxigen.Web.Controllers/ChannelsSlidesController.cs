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
    public class ChannelsSlidesController : Controller
    {
        public ChannelsSlidesController(IChannelsSlideManagementService channelsSlideManagementService) {
            Check.Require(channelsSlideManagementService != null, "channelsSlideManagementService may not be null");

            this.channelsSlideManagementService = channelsSlideManagementService;
        }

        [Transaction]
        public ActionResult Index() {
            IList<ChannelsSlideDto> channelsSlides = 
                channelsSlideManagementService.GetChannelsSlideSummaries();
            return View(channelsSlides);
        }

        [Transaction]
        public ActionResult Show(int id) {
            ChannelsSlide channelsSlide = channelsSlideManagementService.Get(id);
            return View(channelsSlide);
        }

        [Transaction]
        public ActionResult Create() {
            ChannelsSlideFormViewModel viewModel = 
                channelsSlideManagementService.CreateFormViewModel();
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(ChannelsSlide channelsSlide) {
            if (ViewData.ModelState.IsValid) {
                ActionConfirmation saveOrUpdateConfirmation = 
                    channelsSlideManagementService.SaveOrUpdate(channelsSlide);

                if (saveOrUpdateConfirmation.WasSuccessful) {
                    TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                        saveOrUpdateConfirmation.Message;
                    return RedirectToAction("Index");
                }
            } else {
                channelsSlide = null;
            }

            ChannelsSlideFormViewModel viewModel = 
                channelsSlideManagementService.CreateFormViewModelFor(channelsSlide);
            return View(viewModel);
        }

        [Transaction]
        public ActionResult Edit(int id) {
            ChannelsSlideFormViewModel viewModel = 
                channelsSlideManagementService.CreateFormViewModelFor(id);
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(ChannelsSlide channelsSlide) {
            if (ViewData.ModelState.IsValid) {
                ActionConfirmation updateConfirmation = 
                    channelsSlideManagementService.UpdateWith(channelsSlide, channelsSlide.Id);

                if (updateConfirmation.WasSuccessful) {
                    TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                        updateConfirmation.Message;
                    return RedirectToAction("Index");
                }
            }

            ChannelsSlideFormViewModel viewModel = 
                channelsSlideManagementService.CreateFormViewModelFor(channelsSlide);
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id) {
            ActionConfirmation deleteConfirmation = channelsSlideManagementService.Delete(id);
            TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                deleteConfirmation.Message;
            return RedirectToAction("Index");
        }

        private readonly IChannelsSlideManagementService channelsSlideManagementService;
    }
}
