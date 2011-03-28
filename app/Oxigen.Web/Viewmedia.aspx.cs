using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace OxigenIIPresentation
{
  public partial class Viewmedia : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      string previewType = Request.QueryString["previewType"];
      string mediaType = Request.QueryString["mediaType"];
      string fileName = Request.QueryString["fileName"];
      string subDir = Request.QueryString["subDir"];

      if (Request.QueryString["noPreview"] != null)
      {
        StreamFile(System.Configuration.ConfigurationSettings.AppSettings["noPreviewImagePath"]);

        return;
      }

      string path = GetPath(previewType, mediaType);

      string fullPath = path + subDir + "\\" + fileName;

      StreamFile(fullPath);
    }

    private void StreamFile(string fullPath)
    {
      FileStream fs = null;
      byte[] bufferBytes = null;

      try
      {
        fs = new FileStream(fullPath, FileMode.Open);

        long lFileLength = fs.Length;

        bufferBytes = new byte[(int)lFileLength];

        fs.Read(bufferBytes, 0, (int)lFileLength);
      }
      finally
      {
        if (fs != null)
          fs.Dispose();
      }

      Response.BinaryWrite(bufferBytes);
    }

    private string GetPath(string previewType, string mediaType)
    {
      // video & asset content
      if (previewType == "Video" && mediaType == "R")
        return System.Configuration.ConfigurationSettings.AppSettings["previewFramesAssetContentPath"];

      // video & slide or channel slide
      if (previewType == "Video")
        return System.Configuration.ConfigurationSettings.AppSettings["previewFramesSlidePath"];

      // non-video & asset content
      if (mediaType == "R")
        return System.Configuration.ConfigurationSettings.AppSettings["assetContentPath"];

      // non video & slide content
      return System.Configuration.ConfigurationSettings.AppSettings["slidePath"];
    }
  }
}
