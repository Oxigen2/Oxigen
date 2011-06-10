using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Setup.Properties;
using System.IO;
using System.Security.Principal;

namespace Setup
{
  public partial class TermsAndConditionsForm : SetupForm
  {
    public TermsAndConditionsForm()
    {
      InitializeComponent();

      System.Text.ASCIIEncoding  encoding = new System.Text.ASCIIEncoding();
      byte [] buffer = encoding.GetBytes(Resources.OxigenTermsOfService);

      MemoryStream stream = new MemoryStream(buffer);

      laBox.LoadFile(stream, RichTextBoxStreamType.RichText);

      stream.Dispose();
    }

    private void rbAgree_CheckedChanged(object sender, EventArgs e)
    {
      if (((RadioButton)sender).Checked)
        btnNext.Enabled = true;
    }

    private void rbNotAgree_CheckedChanged(object sender, EventArgs e)
    {
      if (((RadioButton)sender).Checked)
        btnNext.Enabled = false;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      SetupHelper.ExitNoChanges();
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      SetupHelper.OpenForm<WelcomeForm>(this);
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      if (!SetupHelper.HasAdminRights())
      {
        MessageBox.Show("You do not have the necessary access level to perform administration task on this PC. Installation cannot continue.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        AppDataSingleton.Instance.ExitPromptSuppressed = true;
        Application.Exit();
        return;
      }

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
      IClientLogger logger = new ClientLogger();
      logger.Log("2-TermsAndConditions");
    }
  }
}
