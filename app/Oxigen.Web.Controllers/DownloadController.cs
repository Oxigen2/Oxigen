using System.Web.Mvc;
using Oxigen.Core.Installer;

namespace Oxigen.Web.Controllers
{
    [HandleError]
    public class DownloadController : Controller
    {
        public ActionResult Installer(SetupFile subscription)
        {
            return Content(subscription.GetSetupText());
        }
    }
}
