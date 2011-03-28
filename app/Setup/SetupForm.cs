using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Setup
{
  public partial class SetupForm : OxigenForm
  {
    public SetupForm()
    {
      InitializeComponent();
    }

    private void SetupForm_Closing(object sender, FormClosingEventArgs e)
    {
      if (AppDataSingleton.Instance.OneFormClosed)
        return;

      if (AppDataSingleton.Instance.ExitPromptSuppressed)
      {
        Application.Exit();
        return;
      }

      if (AppDataSingleton.Instance.OldOxigenSystemModified)
      {
        Application.Exit();
        return;
      }

      if (MessageBox.Show("Are you sure you want to exit Setup?\r\nYour system has not been modified. Please click OK to exit.", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
      {
        AppDataSingleton.Instance.OneFormClosed = true;

        Application.Exit();
      }
      else
        e.Cancel = true;
    }
  }
}
