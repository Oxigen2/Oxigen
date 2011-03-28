using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OxigenIIAdvertising.SOAStructures;

namespace OxigenIIPresentation.TestDebug
{
  public partial class isloggedin : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (Session["User"] == null)
        Response.Write("Session object \"User\" is null");
      else
        Response.Write("User's ID: " + ((User)Session["User"]).UserID);
    }
  }
}
