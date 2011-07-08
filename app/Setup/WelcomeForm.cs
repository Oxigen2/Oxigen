using System;
using System.Windows.Forms;
using Setup.ClientLoggers;

namespace Setup
{
  public partial class WelcomeForm : SetupForm
  {
    public WelcomeForm()
    {
      InitializeComponent();
    }

    private void btnReject_Click(object sender, EventArgs e)
    {
        AppDataSingleton.Instance.ExitPromptSuppressed = true;
        Application.Exit();
    }

    private void btnAccept_Click(object sender, EventArgs e)
    {
        if (SetupHelper.OlderOxigenExists())
        {
            SetupHelper.OpenForm<OlderOxigenExistsForm>(this);
            return;
        }

        if (SetupHelper.OxigenExists())
        {
            SetupHelper.OpenForm<OxigenExistsForm>(this);
            return;
        }

        SetupHelper.OpenForm<PrerequisitesForm>(this);
    }

    private void Form_Shown(object sender, EventArgs e)
    {
      ClientLogger logger = new PersistentClientLogger();
      logger.Log("1-Welcome");
    }

    private void TCs_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
    {
        System.Diagnostics.Process.Start("http://new.oxigen.net/Terms.aspx");
    }
  }
}
