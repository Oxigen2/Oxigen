using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIUserDataMarshallrService
{
  internal class FileRepositoryHelper
  {
    private static readonly string _assetPath = System.Configuration.ConfigurationManager.AppSettings["assetPath"];

    internal static string GetAssetFullPath(string assetFileName)
    {
      return _assetPath +
             assetFileName.Substring(assetFileName.LastIndexOf("_") + 1, 1)
             + "\\" + assetFileName;
    }
  }
}
