using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Setup.ClientLoggers;
using Setup.UserManagementServicesLive;
using System.Net;

namespace Setup
{
  public partial class ExistingUserPromptForm : SetupForm
  {
    public ExistingUserPromptForm()
    {
      InitializeComponent();
    }

    private void ExistingUserPromptForm_Load(object sender, EventArgs e)
    {
      
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      if (rbYes.Checked)
      {
        AppDataSingleton.Instance.ExistingUser = true;
        SetupHelper.OpenForm<CredentialsForm>(this);
      }
      else
      {
        AppDataSingleton.Instance.ExistingUser = false;
        SetupHelper.OpenForm<RegistrationForm1>(this);
      }
    }

    private void rbYes_CheckedChanged(object sender, EventArgs e)
    {
      btnNext.Enabled = true;
    }

    private void rbNo_CheckedChanged(object sender, EventArgs e)
    {
      btnNext.Enabled = true;
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      SetupHelper.OpenForm<PrerequisitesForm>(this);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      SetupHelper.ExitNoChanges();
    }

    private void Form_Shown(object sender, EventArgs e)
    {
      ClientLogger logger = new PersistentClientLogger();
      logger.Log("4-ExistingUserPrompt");
    }
  }
}
