using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace OxigenIIPresentation
{
  public partial class DownloadInstaller : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (Request.Params["dir"] == null && Request.Params["convertedPCName"] == null)
        return;

      string tempInstallersPath = System.Configuration.ConfigurationSettings.AppSettings["tempInstallersPath"];
      string tempInstallersPathTemp = tempInstallersPath + Request.Params["dir"] + "\\";
      string filename = Request.Params["convertedName"] + ".exe";
      string filePath = tempInstallersPathTemp + filename;

      FileInfo fi = new FileInfo(filePath);

      if (!fi.Exists)
        Response.Redirect("Download.aspx");

      try
      {
        Response.Clear();
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("content-disposition", "filename=" + filename);
        Response.AddHeader("Content-Length", fi.Length.ToString());
        Response.TransmitFile(filePath);
        Response.Flush();
      }
      catch
      {
        Response.Redirect("Download.aspx");
      }

      // the installer has been server, now delete temp dir in a different try/catch block.
      try
      {
        Directory.Delete(tempInstallersPathTemp, true);
      }
      catch
      {
        // ignore
      }
    }
  }
}
