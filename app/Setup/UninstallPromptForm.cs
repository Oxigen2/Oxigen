using System;
using System.Windows.Forms;

namespace Setup
{
  public partial class UninstallPromptForm : SetupForm
  {
    public UninstallPromptForm()
    {
      InitializeComponent();
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      if (!SetupHelper.HasAdminRights())
      {
        MessageBox.Show("You do not have the necessary access level to perform administration tasks on this PC. Installation cannot continue.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        AppDataSingleton.Instance.ExitPromptSuppressed = true;
        Application.Exit();
        return;
      }

      SetupHelper.OpenForm<UninstallProgressForm>(this);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      SetupHelper.ExitNoChanges();
    }
  }
}
