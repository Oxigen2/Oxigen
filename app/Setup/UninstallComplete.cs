using System;
using System.Windows.Forms;

namespace Setup
{
  public partial class UninstallComplete : OxigenForm
  {
    public UninstallComplete()
    {
      InitializeComponent();
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void UninstallComplete_Closed(object sender, FormClosedEventArgs e)
    {
      Application.Exit();
    }

    private void Form_Shown(object sender, EventArgs e)
    {
      try
      {
        GenericRegistryAccess.DeleteRegistryKey(RegistryBranch.HKLM_LOCAL_MACHINE__SOFTWARE_OxigenRef);
      }
      catch
      {
        // suppress all errors
      }
    }
  }
}
