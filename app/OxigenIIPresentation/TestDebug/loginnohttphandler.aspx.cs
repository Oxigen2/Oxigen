using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OxigenIIAdvertising.SOAStructures;

namespace OxigenIIPresentation.TestDebug
{
  public partial class loginnohttphandler : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      Session.Add("User", new User(1, "michali.konstantinidis@obs-group.co.uk", "a", "Michali", "Konstantinidis", 100, 100));
    }
  }
}
