using System;
using Setup.ClientLoggers;

namespace Setup
{
  public partial class WelcomeForm : SetupForm
  {
    public WelcomeForm()
    {
      InitializeComponent();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      SetupHelper.ExitNoChanges();
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      SetupHelper.OpenForm<TermsAndConditionsForm>(this);
    }

    private void Form_Shown(object sender, EventArgs e)
    {
      ClientLogger logger = new PersistentClientLogger();
      logger.Log("1-Welcome");
    }
  }
}
