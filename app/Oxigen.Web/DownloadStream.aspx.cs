using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oxigen.Core.Installer;
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

      if (Request.QueryString["a"] != "v")
      {
          var installerSetup = new InstallerSetup();
          installerSetup.Add(channelID, _channel.ChannelGUID, _channel.ChannelName, 10);
          Response.RedirectPermanent(Url.For(installerSetup), true);        
          return;
      }

      StreamName.Text = _channel.ChannelName;
      URL.Text = System.Configuration.ConfigurationSettings.AppSettings["streamDetailsURL"] + channelID;
    }

    protected void DownloadButton_Click(object sender, EventArgs e)
    {
        var installerSetup = new InstallerSetup();
        installerSetup.Add((int)_channel.ChannelID, _channel.ChannelGUID, _channel.ChannelName, 10);
        Response.RedirectPermanent(Url.For(installerSetup), true);
    }
  }
}
