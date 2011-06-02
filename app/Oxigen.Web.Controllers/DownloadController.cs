using System.Web.Mvc;

namespace Oxigen.Web.Controllers
{
    [HandleError]
    public class DownloadController : Controller
    {
        public ActionResult Installer(string channels) {
            return View();
        }
    }
}
