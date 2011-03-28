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
  public partial class DownloadSuccess : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!this.IsPostBack)
      {
        BLClient client = null;

        try
        {
          client = new BLClient();

          // logon conditions: if user is logged on, present their streams, else present the one stream they created
          // unregistered.
          if (Session["User"] == null && Session["PcProfileToken"] != null)
          {
            PCs.DataSource = client.GetSubscriptionsNotRegistered((string)Session["PcProfileToken"]);

            client.RemoveTemporaryPCProfilesNotRegistered((string)Session["PcProfileToken"]);

            Session.Remove("PcProfileToken");
          }
          else if (Session["User"] != null)
          {
            PCs.DataSource = client.GetSubscriptions(((User)Session["User"]).UserID);

            client.RemoveTemporaryPCProfiles(((User)Session["User"]).UserID);
          }
          else
          {
            client.Dispose();
            Response.Redirect("Download.aspx");
          }

          PCs.DataBind();
        }
        finally
        {
          if (client != null)
            client.Dispose();
        }
      }
    }

    protected void PCs_DataBound(object sender, RepeaterItemEventArgs e)
    {
      PC pc = (PC)e.Item.DataItem;

      if (pc == null)
        return;

      HiddenField PCID = (HiddenField)e.Item.FindControl("PCID");
      Label PcName = (Label)e.Item.FindControl("PcName");
      Repeater Streams = (Repeater)e.Item.FindControl("Streams");
      LinkButton DownloadButton = (LinkButton)e.Item.FindControl("DownloadButton");
      
      DownloadButton.CommandArgument = pc.PCID.ToString();

      PCID.Value = pc.PCID.ToString();
      PcName.Text = pc.Name;
      Streams.DataSource = pc.Channels;
      Streams.DataBind();
    }

    protected void Streams_DataBound(object sender, RepeaterItemEventArgs e)
    {
      ChannelListChannel channel = (ChannelListChannel)e.Item.DataItem;

      if (channel == null)
        return;

      HiddenField StreamID = (HiddenField)e.Item.FindControl("StreamID");
      HiddenField StreamGUID = (HiddenField)e.Item.FindControl("StreamGUID");
      Label StreamName = (Label)e.Item.FindControl("StreamName");
      Label Weighting = (Label)e.Item.FindControl("Weighting");

      StreamID.Value = channel.ChannelID.ToString();
      StreamGUID.Value = channel.ChannelGUID;
      StreamName.Text = channel.ChannelName;
      Weighting.Text = channel.ChannelWeightingUnnormalised.ToString();
    }

    protected void DownloadButton_Command(object sender, CommandEventArgs e)
    {
      int pcID = int.Parse((string)e.CommandArgument);      

      // create Custom dir
      string tempInstallersPath = System.Configuration.ConfigurationSettings.AppSettings["tempInstallersPath"];
      string GUID = System.Guid.NewGuid().ToString();
      string tempInstallersPathTemp = tempInstallersPath + GUID + "\\";

      Directory.CreateDirectory(tempInstallersPathTemp);

      // Create custom Setup.ini file
      string installerSubscriptions = null;
      string pcName = null;

      GetInstallerSubscriptions(pcID, ref installerSubscriptions, ref pcName);

      File.WriteAllText(tempInstallersPathTemp + "Setup.ini", installerSubscriptions);
      File.Copy(tempInstallersPath + "Setup.exe", tempInstallersPathTemp + "Setup.exe");
      File.Copy(tempInstallersPath + "Oxigen.msi", tempInstallersPathTemp + "Oxigen.msi");

      string convertedName = Helper.CreateSelfExtractor(pcName, tempInstallersPathTemp);

      Response.Redirect("DownloadInstaller.aspx?dir=" + GUID + "&convertedName=" + convertedName);
    }

    private void GetInstallerSubscriptions(int pcID, ref string subscriptions, ref string pcName)
    {
      StringBuilder sb = new StringBuilder();

      foreach (RepeaterItem ri in PCs.Items)
      {
        HiddenField PCID = (HiddenField)ri.FindControl("PCID");
        Label PcName = (Label)ri.FindControl("PcName");
        Repeater Streams = (Repeater)ri.FindControl("Streams");

        if (pcID == int.Parse(PCID.Value))
        {
          pcName = PcName.Text;

          foreach (RepeaterItem ri2 in Streams.Items)
          {
            HiddenField StreamID = (HiddenField)ri2.FindControl("StreamID");
            HiddenField StreamGUID = (HiddenField)ri2.FindControl("StreamGUID");
            Label StreamName = (Label)ri2.FindControl("StreamName");
            Label Weighting = (Label)ri2.FindControl("Weighting");

            sb.Append(StreamID.Value);
            sb.Append(",,");
            sb.Append(StreamGUID.Value);
            sb.Append(",,");
            sb.Append(StreamName.Text);
            sb.Append(",,");
            sb.Append(Weighting.Text);
            sb.AppendLine();
          }

          break;
        }
      }

      subscriptions = sb.ToString();
    }   
  }
}
