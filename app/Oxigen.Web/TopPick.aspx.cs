using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OxigenIIAdvertising.SOAStructures;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation
{
  public partial class TopPick : System.Web.UI.Page
  {
    protected int _counter = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
      int categoryID = -1;
      List<Channel> channels = null;
      BLClient client = null;
      string categoryName ;

      if (Request.Params["categoryID"] == null)
        Response.Redirect("~/Home.aspx");
      
      if (!int.TryParse(Request.Params["categoryID"], out categoryID))
        Response.Redirect("~/Home.aspx");


      if (!IsPostBack)
      {
        try
        {
          client = new BLClient();

          channels = client.GetTop5MostPopular(categoryID);

          categoryName = client.GetCategoryName(categoryID);
        }
        finally
        {
          client.Dispose();
        }

        CategoryName1.Text = categoryName;
        CategoryName2.Text = categoryName;

        Channels.DataSource = channels;
        Channels.DataBind();
      }
    }

    public void Channels_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      Channel channel = (Channel)e.Item.DataItem;

      if (channel == null)
        return;

      Literal ChannelNumber = (Literal)e.Item.FindControl("ChannelNumber");
      Literal ChannelName1 = (Literal)e.Item.FindControl("ChannelName1");
      Literal ChannelName2 = (Literal)e.Item.FindControl("ChannelName2");
      Literal PublisherName = (Literal)e.Item.FindControl("PublisherName");
      Literal NoFollowers = (Literal)e.Item.FindControl("NoFollowers");
      Literal AddDate = (Literal)e.Item.FindControl("AddDate");
      Literal NoContent = (Literal)e.Item.FindControl("NoContent");
      Literal ShortDescription = (Literal)e.Item.FindControl("ShortDescription");
      Literal LongDescription = (Literal)e.Item.FindControl("LongDescription");
      Image Thumbnail = (Image)e.Item.FindControl("Thumbnail");

      ChannelNumber.Text = _counter.ToString();
      ChannelName1.Text = channel.ChannelName;
      ChannelName2.Text = channel.ChannelName;
      PublisherName.Text = channel.PublisherDisplayName;
      NoFollowers.Text = channel.NoFollowers.ToString();
      AddDate.Text = channel.AddDate.ToShortDateString();
      NoContent.Text = channel.NoContents.ToString();
      ShortDescription.Text = channel.ChannelDescription;
      LongDescription.Text = channel.ChannelLongDescription == String.Empty ? "<p>No description available</p>" : "<p>" + Helper.FirstWords(channel.ChannelLongDescription, 30).Replace("\r\n", "</p><p>") + "...<a href=\"ChannelDetails.aspx?a=v&channelID=" + channel.ChannelID + "\">More</a></p>";
      Thumbnail.AlternateText = channel.ChannelName;
      Thumbnail.ImageUrl = System.Configuration.ConfigurationSettings.AppSettings["thumbnailChannelRelativePath"] + channel.ImagePath;

      _counter++;
    }
  }
}
