using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OxigenIIAdvertising.BLClients;
using OxigenIIAdvertising.SOAStructures;
using System.IO;
using System.Text;

namespace OxigenIIPresentation
{
  public partial class DownloadStream : System.Web.UI.Page
  {
    Channel _channel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (Request.Params["channelID"] == null)
        Response.Redirect("~AllChannels.aspx");

      int userID = -1;
      int channelID;

      if (!int.TryParse(Request.Params["channelID"], out channelID))
        Response.Redirect("~AllChannels.aspx");

      if (Session["User"] != null)
        userID = ((User)Session["User"]).UserID;

      BLClient client = null;

      try
      {
        client = new BLClient();

        _channel = client.GetChannelToDownload(userID, channelID);
      }
      finally
      {
        client.Dispose();
      }

      StreamName.Text = _channel.ChannelName;
      URL.Text = System.Configuration.ConfigurationSettings.AppSettings["streamDetailsURL"] + channelID;
    }

    protected void DownloadButton_Click(object sender, EventArgs e)
    {
      // create Custom dir
      string tempInstallersPath = System.Configuration.ConfigurationSettings.AppSettings["tempInstallersPath"];
      string GUID = System.Guid.NewGuid().ToString();
      string tempInstallersPathTemp = tempInstallersPath + GUID + "\\";

      Directory.CreateDirectory(tempInstallersPathTemp);

      // Create custom Setup.ini file
      string installerSubscriptions = GetInstallerSubscriptions(_channel);

      File.WriteAllText(tempInstallersPathTemp + "Setup.ini", installerSubscriptions);
      File.Copy(tempInstallersPath + "Setup.exe", tempInstallersPathTemp + "Setup.exe");
      File.Copy(tempInstallersPath + "Oxigen.msi", tempInstallersPathTemp + "Oxigen.msi");

      string convertedName = Helper.CreateSelfExtractor(_channel.ChannelName, tempInstallersPathTemp);

      Response.Redirect("DownloadInstaller.aspx?dir=" + GUID + "&convertedName=" + convertedName);
    }

    private string GetInstallerSubscriptions(Channel _channel)
    {
      // Create custom Setup.ini file
      StringBuilder sb = new StringBuilder();

      sb.Append(_channel.ChannelID);
      sb.Append(",,");
      sb.Append(_channel.ChannelGUID);
      sb.Append(",,");
      sb.Append(_channel.ChannelName);
      sb.Append(",,");
      sb.Append(10);
      sb.AppendLine();

      return sb.ToString();
    }
  }
}
