using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OxigenIIAdvertising.BLClients;

namespace OxigenIIPresentation
{
  public partial class ForgottenPassword : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Next_Click(object sender, EventArgs e)
    {
      BLClient client = null;

      try
      {
        client = new BLClient();

        client.SendPasswordReminder(txtEmail.Text);
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      Response.Redirect("PasswordSent.aspx");
    }
  }
}
