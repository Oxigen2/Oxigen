using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
      IClientLogger logger = new ClientLogger();
      logger.Log("1-Welcome");
    }
  }
}
