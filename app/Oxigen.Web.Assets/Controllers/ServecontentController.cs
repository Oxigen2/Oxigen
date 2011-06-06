using System;
using System.Web.Mvc;


namespace Oxigen.Web.Assets
{
  [HandleError]
  public class ServecontentController : Controller
  {
    [HttpGet]
    public ActionResult Slide(string filename)
    {
      string path = FileRepositoryHelper.GetAssetFullPath(filename);

      if (!System.IO.File.Exists(path))
        return HttpNotFound("File not found");

      return new FilePathResult(path, "bad/type");
    }

    [HttpGet]
    public ActionResult Advert(string filename)
    {
      return Slide(filename);
    }
  }
}