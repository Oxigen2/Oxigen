using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Setup
{
  public partial class RepairConfirm : SetupForm
  {
    public RepairConfirm()
    {
      InitializeComponent();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      SetupHelper.OpenForm<OxigenExistsForm>(this);
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      SetupHelper.OpenForm<InstallationProgressForm>(this);
    }
  }
}
