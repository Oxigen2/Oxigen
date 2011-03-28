using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation
{
  public partial class ContactUs : System.Web.UI.Page
  {
    private Regex _emailRegex = new Regex(@"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", RegexOptions.Compiled);

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void OnClick_Next(object sender, EventArgs e)
    {
      if (!_emailRegex.IsMatch(txtEmail.Text))
      {
        rfvEmail.Visible = true;
        return;
      }
      else
        rfvEmail.Visible = false;

      BLClient client = null;

      try
      {
        client = new BLClient();
        client.SendContactEmail(txtName.Text, txtEmail.Text, ddlSubject.SelectedValue, txtMessage.Text);
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      Response.Redirect("ContactUsThanks.aspx");
    }
  }
}
