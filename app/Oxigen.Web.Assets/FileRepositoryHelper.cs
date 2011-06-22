using System.Configuration;

namespace Oxigen.Web.Assets
{
  public static class FileRepositoryHelper
  {
      private static readonly string _assetPath = ConfigurationManager.AppSettings["assetPath"];
      private static readonly string _channelDataPath = ConfigurationManager.AppSettings["channelDataPath"];
      private static readonly string _appDataPath = ConfigurationManager.AppSettings["appDataPath"];

    public static string GetAssetFullPath(string assetFileName)
    {
      return _assetPath +
             assetFileName.Substring(assetFileName.LastIndexOf("_") + 1, 1)
             + "\\" + assetFileName;
    }
    public static string GetChannelDataFullPath(string channelDataFileName)
    {
        return _channelDataPath + "\\" + channelDataFileName;
    }
    public static string GetDataFullPath(string filename)
    {
        return _appDataPath + filename;
    }


  }
}
