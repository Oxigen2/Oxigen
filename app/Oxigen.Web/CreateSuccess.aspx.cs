using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OxigenIIAdvertising.BLClients;
using OxigenIIAdvertising.SOAStructures;
using System.Text;
using System.IO;

namespace OxigenIIPresentation
{
  public partial class CreateSuccess : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      BLClient client = null;

      try
      {
        client = new BLClient();

        Streams.DataSource = client.GetChannelsByUserID(((User)Session["User"]).UserID);
      }
      finally
      {
        client.Dispose();
      }

      Streams.DataBind();
    }

    protected void Streams_DataBound(object sender, RepeaterItemEventArgs e)
    {
      Channel channel = (Channel)e.Item.DataItem;

      if (channel == null)
        return;

      Label StreamName = (Label)e.Item.FindControl("StreamName");
      Label URL = (Label)e.Item.FindControl("URL");
      LinkButton DownloadButton = (LinkButton)e.Item.FindControl("DownloadButton");

      DownloadButton.CommandArgument = channel.ChannelID.ToString();

      StreamName.Text = channel.ChannelName;
      URL.Text = System.Configuration.ConfigurationSettings.AppSettings["streamDetailsURL"] + channel.ChannelID;
    }

    protected void DownloadButton_Command(object sender, CommandEventArgs e)
    {
      int channelID = int.Parse((string)e.CommandArgument);

      Channel channel = GetChannelFromList(channelID);

      // create Custom dir
      string tempInstallersPath = System.Configuration.ConfigurationSettings.AppSettings["tempInstallersPath"];
      string GUID = System.Guid.NewGuid().ToString();
      string tempInstallersPathTemp = tempInstallersPath + GUID + "\\";

      Directory.CreateDirectory(tempInstallersPathTemp);

      // Create custom Setup.ini file
      string installerSubscriptions = GetInstallerSubscriptions(channel);

      File.WriteAllText(tempInstallersPathTemp + "Setup.ini", installerSubscriptions);
      File.Copy(tempInstallersPath + "Setup.exe", tempInstallersPathTemp + "Setup.exe");
      File.Copy(tempInstallersPath + "Oxigen.msi", tempInstallersPathTemp + "Oxigen.msi");

      string convertedName = Helper.CreateSelfExtractor(channel.ChannelName, tempInstallersPathTemp);

      Response.Redirect("DownloadInstaller.aspx?dir=" + GUID + "&convertedName=" + convertedName);
    }

    private string GetInstallerSubscriptions(Channel channel)
    {
      // Create custom Setup.ini file
      StringBuilder sb = new StringBuilder();

      sb.Append(channel.ChannelID);
      sb.Append(",,");
      sb.Append(channel.ChannelGUID);
      sb.Append(",,");
      sb.Append(channel.ChannelName);
      sb.Append(",,");
      sb.Append(10);
      sb.AppendLine();

      return sb.ToString();
    }

    private Channel GetChannelFromList(int channelID)
    {
      List<Channel> channelList = (List<Channel>)Streams.DataSource;

      foreach (Channel channel in channelList)
      {
        if (channel.ChannelID == channelID)
          return channel;
      }

      return null;
    }    
  }
}
