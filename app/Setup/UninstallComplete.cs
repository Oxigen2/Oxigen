using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Setup
{
  public partial class UninstallComplete : OxigenForm
  {
    public UninstallComplete()
    {
      InitializeComponent();
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void UninstallComplete_Closed(object sender, FormClosedEventArgs e)
    {
      Application.Exit();
    }
  }
}
