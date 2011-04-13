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
    public class ChannelsController : Controller
    {
        public ChannelsController(IChannelManagementService channelManagementService) {
            Check.Require(channelManagementService != null, "channelManagementService may not be null");

            this.channelManagementService = channelManagementService;
        }

        [Transaction]
        public ActionResult Index() {
            IList<ChannelDto> channel = 
                channelManagementService.GetChannelSummaries();
            return View(channel);
        }

        [Transaction]
        public ActionResult ListByProducer(int id)
        {
            IList<ChannelDto> channelDtos =
                channelManagementService.GetByPublisher(id);
            return Json(channelDtos, JsonRequestBehavior.AllowGet);
        }

        [Transaction]
        public ActionResult Show(int id) {
            Channel channel = channelManagementService.Get(id);
            return View(channel);
        }

        [Transaction]
        public ActionResult Create() {
            ChannelFormViewModel viewModel = 
                channelManagementService.CreateFormViewModel();
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Channel channel) {
            if (ViewData.ModelState.IsValid) {
                ActionConfirmation saveOrUpdateConfirmation = 
                    channelManagementService.SaveOrUpdate(channel);

                if (saveOrUpdateConfirmation.WasSuccessful) {
                    TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                        saveOrUpdateConfirmation.Message;
                    return RedirectToAction("Index");
                }
            } else {
                channel = null;
            }

            ChannelFormViewModel viewModel = 
                channelManagementService.CreateFormViewModelFor(channel);
            return View(viewModel);
        }

        [Transaction]
        public ActionResult Edit(int id) {
            ChannelFormViewModel viewModel = 
                channelManagementService.CreateFormViewModelFor(id);
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Channel channel) {
            if (ViewData.ModelState.IsValid) {
                ActionConfirmation updateConfirmation = 
                    channelManagementService.UpdateWith(channel, channel.Id);

                if (updateConfirmation.WasSuccessful) {
                    TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                        updateConfirmation.Message;
                    return RedirectToAction("Index");
                }
            }

            ChannelFormViewModel viewModel = 
                channelManagementService.CreateFormViewModelFor(channel);
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id) {
            ActionConfirmation deleteConfirmation = channelManagementService.Delete(id);
            TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                deleteConfirmation.Message;
            return RedirectToAction("Index");
        }

        private readonly IChannelManagementService channelManagementService;
    }
}
