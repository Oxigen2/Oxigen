using System.Configuration;

namespace Oxigen.Web.Assets
{
  public static class FileRepositoryHelper
  {
    private static readonly string _assetPath = ConfigurationManager.AppSettings["assetPath"];

    public static string GetAssetFullPath(string assetFileName)
    {
      return _assetPath +
             assetFileName.Substring(assetFileName.LastIndexOf("_") + 1, 1)
             + "\\" + assetFileName;
    }
  }
}
