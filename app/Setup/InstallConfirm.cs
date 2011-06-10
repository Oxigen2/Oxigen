using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
      SetupHelper.ExitNoChanges();
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      SetupHelper.OpenForm<InstallationProgressForm>(this);
    }

    private void Form_Shown(object sender, EventArgs e)
    {
      IClientLogger logger = new ClientLogger();
      logger.Log("7-InstallConfirm");
    }
  }
}
