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
  public partial class ChannelDetails : System.Web.UI.Page
  {
    protected int _slideCount = 0;
    protected int _noSlides = -1;
    protected Channel _channel;

    protected void Page_Load(object sender, EventArgs e)
    {
      int userID = -1;
      int channelID;

      Helper.TryGetUserID(Session, out userID);

      if (Request.Params["channelID"] == null)
        Response.Redirect("~/AllChannels.aspx");

      if (!int.TryParse(Request.Params["channelID"], out channelID))
        Response.Redirect("~/AllChannels.aspx");

      BLClient client = null;

      try
      {
        client = new BLClient();

        _channel = client.GetChannelDetailsFull(userID, channelID);
      }
      finally
      {
        client.Dispose();
      }

      ChannelName1.Text = _channel.ChannelName;
      ChannelName2.Text = _channel.ChannelName;
      ChannelName3.Text = _channel.ChannelName;
      PublisherName.Text = _channel.PublisherDisplayName;
      NoContents.Text = _channel.NoContents.ToString();
      ShortDescription.Text = string.IsNullOrEmpty(_channel.ChannelDescription) ? "No description available" : _channel.ChannelDescription;
      LongDescription.Text = string.IsNullOrEmpty(_channel.ChannelLongDescription) ? "<p>No description available</p>" : "<p>" + Helper.FirstWords(_channel.ChannelLongDescription, 120).Replace("\r\n", "</p><p>") + "</p>";
      NoFollowers.Text = _channel.NoFollowers.ToString();
      AddDate.Text = _channel.AddDate.ToShortDateString();
      ContentLastAddedDate.Text = _channel.ContentLastAddedDate.ToShortDateString();
      PreviewLiteral.Visible = _channel.PrivacyStatus != ChannelPrivacyStatus.Locked;

      _noSlides = _channel.Slides.Count;

      addStreamLink.NavigateUrl = "~/Download.aspx?channelID=" + channelID + "_strm";

      ChannelSlides.DataSource = _channel.Slides;
      ChannelSlides.DataBind();
    }

    public void Download_Click(object sender, EventArgs e)
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

    public void ChannelSlides_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      SlideListSlide slide = (SlideListSlide)e.Item.DataItem;

      if (slide == null)
        return;

      Literal ChannelOpeningSlide = (Literal)e.Item.FindControl("ChannelOpeningSlide");
      Literal ChannelClosingSlide = (Literal)e.Item.FindControl("ChannelClosingSlide");
      Literal SlideName = (Literal)e.Item.FindControl("SlideName");
      Image Thumbnail = (Image)e.Item.FindControl("Thumbnail");
      SlideName.Text = _channel.PrivacyStatus != ChannelPrivacyStatus.Locked ? slide.SlideName : "Locked Stream";
      Thumbnail.CssClass = slide.SlideID.ToString();
      Thumbnail.ImageUrl = System.Configuration.ConfigurationSettings.AppSettings["thumbnailSlideRelativePath"] + (_channel.PrivacyStatus != ChannelPrivacyStatus.Locked ? slide.ImagePath : "locked_stream.jpg");
      Thumbnail.AlternateText = slide.SlideName;

      if (_slideCount % 12 == 0)
        ChannelOpeningSlide.Text = "<div class=\"ChannelSlide\">";

      if (_slideCount % 12 == 11)
        ChannelClosingSlide.Text = "</div>";

      if (_slideCount == _noSlides - 1)
        ChannelClosingSlide.Text = "</div>";

      _slideCount++;
    }
  }
}
