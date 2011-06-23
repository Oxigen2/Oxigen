using System;
using System.Web.Mvc;
using log4net;

namespace Oxigen.Web.Assets
{
  [HandleError]
  public class ServecontentController : Controller
  {
    private static readonly ILog _logger = LogManager.GetLogger("FileLogger");

    [HttpGet, FileCache(MaxAge = 31536000)] //one year
    public ActionResult Slide(string filename)
    {
      string path = FileRepositoryHelper.GetAssetFullPath(filename);
      return new FilePathResult(path, "bad/type");
    }

      [FileCache(MaxAge = 1800)] //30 min
    public ActionResult Channel(string filename) {
        string path = FileRepositoryHelper.GetChannelDataFullPath(filename);
        return new FilePathResult(path, "bad/type");
    }
      [HttpGet, FileCache(MaxAge = 31536000)] //one year
    public ActionResult Advert(string filename)
    {
      return Slide(filename);
    }
      [FileCache(MaxAge = 10800)] //3 hours
    public ActionResult Data(string filename)
    {
        return new FilePathResult(FileRepositoryHelper.GetDataFullPath(filename), "bad/type");
    }

  }
}