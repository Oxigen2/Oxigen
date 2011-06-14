using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Setup
{
  public partial class OxigenExistsForm : SetupForm
  {
    public OxigenExistsForm()
    {
      InitializeComponent();

      if (!File.Exists("Setup.ini"))
      {
        rbAddNewStreams.Visible = false;
        lblInstructions.Text = "You uninstall the existing installation.";
      }
      else
        lblInstructions.Text = "Oxigen already exists on your computer. You can uninstall the existing installation or add the new Streams you have downloaded.";
    }

    public OxigenExistsForm(string param)
    {
      InitializeComponent();

      if (param == "/m" || !File.Exists("Setup.ini"))
      {
        rbAddNewStreams.Visible = false;
        lblInstructions.Text = "You can uninstall the existing installation.";
      }
      else
        lblInstructions.Text = "Oxigen already exists on your computer. You can uninstall the existing installation or add the new Streams you have downloaded.";
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      SetupHelper.ExitNoChanges();
    }

    private void btnNext_Click(object sender, EventArgs e)
    {     
      AppDataSingleton.Instance.MergeStreamsInstallation = false;
      AppDataSingleton.Instance.Repair = false;

      if (rbRepair.Checked)
      {
        AppDataSingleton.Instance.Repair = true;
        SetupHelper.OpenForm<RepairConfirm>(this);
      }

      if (rbUninstall.Checked)
        SetupHelper.OpenForm<UninstallPromptForm>(this);

      if (rbAddNewStreams.Checked)
      {
        AppDataSingleton.Instance.MergeStreamsInstallation = true;
        SetupHelper.OpenForm<MergeChannelsForm>(this);
      }
    }

    private void rbRepair_CheckedChanged(object sender, EventArgs e)
    {
      btnNext.Enabled = true;
    }

    private void rbUninstall_CheckedChanged(object sender, EventArgs e)
    {
      btnNext.Enabled = true;
    }

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
      btnNext.Enabled = true;
    }
  }
}
