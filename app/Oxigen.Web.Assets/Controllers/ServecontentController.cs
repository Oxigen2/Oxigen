using System;
using System.Web.Mvc;
using log4net;

namespace Oxigen.Web.Assets
{
  [HandleError]
  public class ServecontentController : Controller
  {
    [OxigenFileCacheAttribute(MaxAge = 31536000)] //one year
    public ActionResult Slide(string filename)
    {
        string path = FileRepositoryHelper.GetAssetFullPath(filename);
        return new FilePathResult(path, System.Net.Mime.MediaTypeNames.Application.Octet);
    }

    [OxigenFileCacheAttribute(MaxAge = 30)] // 30 secs
    public ActionResult Channel(string filename) {
        string path = FileRepositoryHelper.GetChannelDataFullPath(filename);
        return new FilePathResult(path, System.Net.Mime.MediaTypeNames.Application.Octet);
    }
    [HttpGet, OxigenFileCacheAttribute(MaxAge = 31536000)] //one year
    public ActionResult Advert(string filename)
    {
      return Slide(filename);
    }
    [OxigenFileCacheAttribute(MaxAge = 10800)] //3 hours
    public ActionResult Data(string filename)
    {
        return new FilePathResult(FileRepositoryHelper.GetDataFullPath(filename), System.Net.Mime.MediaTypeNames.Application.Octet);
    }
  }
}