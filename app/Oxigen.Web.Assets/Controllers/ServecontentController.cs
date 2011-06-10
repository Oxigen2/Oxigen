using System.Web.Mvc;
using log4net;

namespace Oxigen.Web.Assets
{
  [HandleError]
  public class ServecontentController : Controller
  {
    private static readonly ILog _logger = LogManager.GetLogger("FileLogger");

    [HttpGet]
    public ActionResult Slide(string filename)
    {
      string path = FileRepositoryHelper.GetAssetFullPath(filename);

      if (!System.IO.File.Exists(path))
      {
        _logger.Debug(path + " does not exist.");
        return HttpNotFound("File not found");
      }

      _logger.Debug("Retrieving " + path);

      return new FilePathResult(path, "bad/type");
    }

    [HttpGet]
    public ActionResult Advert(string filename)
    {
      return Slide(filename);
    }
  }
}