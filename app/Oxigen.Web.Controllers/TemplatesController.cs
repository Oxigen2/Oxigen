using System.Web;
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
    public class TemplatesController : Controller
    {
        public TemplatesController(ITemplateManagementService templateManagementService) {
            Check.Require(templateManagementService != null, "templateManagementService may not be null");

            this.templateManagementService = templateManagementService;
        }

        [Transaction]
        public ActionResult Index()
        {
            IList<TemplateDto> templates = 
                templateManagementService.GetTemplateSummaries();
            return View(templates);
        }
        [Transaction]
        public ActionResult ListByProducer(int id)
        {
            IList<TemplateDto> slideFolders =
                templateManagementService.GetByPublisher(id);
            return Json(slideFolders, JsonRequestBehavior.AllowGet);
        }
        [Transaction]
        public ActionResult Show(int id) {
            Template template = templateManagementService.Get(id);
            return View(template);
        }

        [Transaction]
        public ActionResult Create() {
            TemplateFormViewModel viewModel = 
                templateManagementService.CreateFormViewModel();
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Template template, HttpPostedFileBase file)
        {
            if (file != null)
            {
            //if (ViewData.ModelState.IsValid) {
                byte[] fileByteArray = new byte[file.InputStream.Length];
                file.InputStream.Read(fileByteArray, 0, (int) file.InputStream.Length);    
                ActionConfirmation saveOrUpdateConfirmation =
                    templateManagementService.Create(template, file.FileName, fileByteArray);

                if (saveOrUpdateConfirmation.WasSuccessful) {
                    TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                        saveOrUpdateConfirmation.Message;
                    return RedirectToAction("Index");
                }
            } else {
                ViewData.ModelState.AddModelError("File", "Must provide file");
                template = null;
            }

            TemplateFormViewModel viewModel = 
                templateManagementService.CreateFormViewModelFor(template);
            return View(viewModel);
        }

        [Transaction]
        public ActionResult Edit(int id) {
            TemplateFormViewModel viewModel = 
                templateManagementService.CreateFormViewModelFor(id);
            return View(viewModel);
        }

        [Transaction]
        public ActionResult ServeFile(int id)
        {
            Template template = templateManagementService.Get(id);

            return File(System.Configuration.ConfigurationSettings.AppSettings["templatePath"] + template.Filename,
                        "application/x-shockwave-flash", template.Name);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Template template, HttpPostedFileBase file)
        {
         //   if (ViewData.ModelState.IsValid) {
            byte[] fileByteArray = null;
            string fileName = null;

            if (file != null) {
                fileByteArray = new byte[file.InputStream.Length];
                file.InputStream.Read(fileByteArray, 0, (int) file.InputStream.Length);
                fileName = file.FileName;
            }

            ActionConfirmation updateConfirmation =
                templateManagementService.UpdateWith(template, template.Id, fileName, fileByteArray);

            if (updateConfirmation.WasSuccessful)
            {
                TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] =
                    updateConfirmation.Message;
                return RedirectToAction("Index");
            }

            TemplateFormViewModel viewModel = 
                templateManagementService.CreateFormViewModelFor(template);
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id) {
            ActionConfirmation deleteConfirmation = templateManagementService.Delete(id);
            TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
                deleteConfirmation.Message;
            return RedirectToAction("Index");
        }

        private readonly ITemplateManagementService templateManagementService;
    }
}
