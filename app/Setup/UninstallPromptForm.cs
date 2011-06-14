using System;

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
      SetupHelper.OpenForm<UninstallProgressForm>(this);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      SetupHelper.ExitNoChanges();
    }
  }
}
