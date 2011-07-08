using System;
using Setup.ClientLoggers;

namespace Setup
{
  public partial class InstallConfirm : SetupForm
  {
    public InstallConfirm()
    {
      InitializeComponent();
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      SetupHelper.OpenForm<InstallationPathsForm>(this);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      SetupHelper.ExitConfirmNoChanges();
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      SetupHelper.OpenForm<InstallationProgressForm>(this);
    }

    private void Form_Shown(object sender, EventArgs e)
    {
      ClientLogger logger = new PersistentClientLogger();
      logger.Log("7-InstallConfirm");
    }
  }
}
