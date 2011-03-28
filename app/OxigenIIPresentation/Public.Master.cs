using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

namespace OxigenIIPresentation
{
    public partial class Public : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // Find the OxigenIIPresentation.dll version number (defined in AssemblyInfo.cs)
            Assembly assembly = Assembly.Load("OxigenIIPresentation");
            string version = assembly.GetName().Version.ToString();
            versionLabel.Text = version;

        }
    }
}
