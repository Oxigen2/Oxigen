using System.Web.Mvc;
using log4net;

namespace Oxigen.Web.Assets
{
  [HandleError]
  public class ServecontentController : Controller
  {
    private static readonly ILog _logger = LogManager.GetLogger("FileLogger");

    [HttpGet, FileCache]
    public ActionResult Slide(string filename)
    {
      string path = FileRepositoryHelper.GetAssetFullPath(filename);

      if (!System.IO.File.Exists(path))
      {
        _logger.Debug(path + " does not exist.");
        return HttpNotFound("File not found");
      }

      _logger.Debug("Retrieving " + path);
      Response.AddFileDependency(path);
      Response.Cache.SetLastModifiedFromFileDependencies();
      return new FilePathResult(path, "bad/type");
    }

    public ActionResult Channel(string filename) {
        string path = FileRepositoryHelper.GetAssetFullPath(filename);

        if (!System.IO.File.Exists(path)) {
            _logger.Debug(path + " does not exist.");
            return HttpNotFound("File not found");
        }

        _logger.Debug("Retrieving " + path);
        Response.AddFileDependency(path);
        Response.Cache.SetLastModifiedFromFileDependencies();
        return new FilePathResult(path, "bad/type");
    }

    [HttpGet, FileCache]
    public ActionResult Advert(string filename)
    {
      return Slide(filename);
    }
  }
}