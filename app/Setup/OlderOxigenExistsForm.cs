using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Setup
{
  public partial class OlderOxigenExistsForm : SetupForm
  {
    public OlderOxigenExistsForm()
    {
      InitializeComponent();
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      SetupHelper.OpenForm<UninstallOldOxigenWaitForm>(this);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void Form_Shown(object sender, EventArgs e)
    {
      IClientLogger logger = new ClientLogger();
      logger.Log("2.1-OlderOxigenExists");
    }
  }
}
