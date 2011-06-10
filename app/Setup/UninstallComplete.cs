using System;
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
