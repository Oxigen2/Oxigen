using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using OxigenIIAdvertising.ServerConnectAttempt;
using Setup.UserManagementServicesLive;

namespace Setup
{
  public partial class PCFormNewUser : SetupForm
  {
    private SimpleErrorWrapper _wrapper = null;

    public PCFormNewUser()
    {
      InitializeComponent();
    }

    private void PCFormNewUser_Load(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(AppDataSingleton.Instance.NewPCName))
        txtPCName.Text = Environment.MachineName;
      else
        txtPCName.Text = AppDataSingleton.Instance.NewPCName;
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      string pcName = txtPCName.Text.Trim();

      if (string.IsNullOrEmpty(pcName))
      {
        MessageBox.Show("Please enter a name for your PC", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        return;
      }

      AppDataSingleton.Instance.NewPCName = pcName;
      AppDataSingleton.Instance.User.UserGUID = System.Guid.NewGuid().ToString().ToUpper() + "_" + SetupHelper.GetRandomLetter();;
      AppDataSingleton.Instance.User.MachineGUID = System.Guid.NewGuid().ToString().ToUpper() + "_" + SetupHelper.GetRandomLetter();;
            
      if (!AppDataSingleton.Instance.FileDetectedSubscriptionsFound)
        SetupHelper.OpenForm<InstallationPathsForm>(this);
      else
        SetupHelper.OpenForm<MergeChannelsForm>(this);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      SetupHelper.ExitNoChanges();
    }
  }
}
