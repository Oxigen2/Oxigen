using System.Web.Mvc;
using log4net;

namespace Oxigen.Web.Assets
{
  [HandleError, FileCache]
  public class ServecontentController : Controller
  {
    private static readonly ILog _logger = LogManager.GetLogger("FileLogger");

    [HttpGet]
    public ActionResult Slide(string filename)
    {
      string path = FileRepositoryHelper.GetAssetFullPath(filename);
      return new FilePathResult(path, "bad/type");
    }

    public ActionResult Channel(string filename) {
        string path = FileRepositoryHelper.GetChannelDataFullPath(filename);
        return new FilePathResult(path, "bad/type");
    }

    public ActionResult Advert(string filename)
    {
      return Slide(filename);
    }

    public ActionResult Data(string filename)
    {
        return new FilePathResult(FileRepositoryHelper.GetDataFullPath(filename), "bad/type");
    }

  }
}