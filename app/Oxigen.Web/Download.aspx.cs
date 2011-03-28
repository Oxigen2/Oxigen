using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OxigenIIAdvertising.BLClients;
using OxigenIIAdvertising.SOAStructures;

namespace OxigenIIPresentation
{
  public partial class Download : System.Web.UI.Page
  {
    private Random _random = new Random();

    protected void Page_Load(object sender, EventArgs e)
    {
      // parse to int and back to string;
      // this is to ensure that part of the channelID is an int
      if (Request.Params["channelID"] != null)
      {
        string channelIDFromDownload = Request.Params["channelID"];

        string[] channeIDComponents = channelIDFromDownload.Split(new char[] { '_' });

        if (channeIDComponents.Length != 2)
          return;

        int channelID;

        if (!int.TryParse(channeIDComponents[0], out channelID))
          return;

        // if the channelID part of the string <channelID>_strm is an int, assign it to a hidden field
        streamInfo.Value = channelIDFromDownload;
      }

      // Create a random code and store it in the Session object.
      this.Session["CaptchaImageText"] = GenerateRandomCode();
    }

    //
    // Returns a string of six random digits.
    //
    private string GenerateRandomCode()
    {
      string s = "";
      for (int i = 0; i < 6; i++)
        s = String.Concat(s, _random.Next(10).ToString());
      return s;
    }
  }
}
