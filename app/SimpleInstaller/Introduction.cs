using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimpleInstaller
{
  public partial class Introduction : Form
  {
    bool _bToNextForm = false;

    public Introduction()
    {
      InitializeComponent();
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      _bToNextForm = true;

      this.Close();

      (new InstallationPathPrompt()).Show();

      this.Dispose();
    }

    private void Form_Closing(object sender, FormClosingEventArgs e)
    {
      if (!_bToNextForm)
        Application.Exit();
    }
  }
}
