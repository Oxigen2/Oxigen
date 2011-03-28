using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Setup
{
  public partial class SubscriptionsUpdatedForm : OxigenForm
  {
    public SubscriptionsUpdatedForm()
    {
      InitializeComponent();
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
      Process processCE = null;

      ProcessStartInfo startInfoCE = new ProcessStartInfo((string)GenericRegistryAccess.GetRegistryValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Oxigen", "ProgramPath") + "bin\\OxigenCE.exe", "/v");

      try
      {
        processCE = Process.Start(startInfoCE);
      }
      catch
      {
        // ignore
      }

      Application.Exit();
    }
  }
}
