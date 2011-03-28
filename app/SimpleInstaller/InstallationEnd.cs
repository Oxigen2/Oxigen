using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimpleInstaller
{
  public partial class InstallationEnd : Form
  {
    public InstallationEnd()
    {
      InitializeComponent();
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
      this.Close();

      Application.Exit();
    }
  }
}
