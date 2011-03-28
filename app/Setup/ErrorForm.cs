using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Setup
{
  public partial class ErrorForm : Form
  {
    private string _exception;

    public ErrorForm(string exception)
    {
      InitializeComponent();
      _exception = exception;
    }

    private void btnNoSend_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void btnSend_Click(object sender, EventArgs e)
    {
      FormCollection openForms = Application.OpenForms;

      foreach (Form openForm in openForms)
        openForm.Hide();

      UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer client = null;

      try
      {
        string macAddress = SetupHelper.GetMACAddress();

        client = new Setup.UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer();

        client.Url = "https://master-getconfig-a-1.oxigen.net/UserManagementServices.svc";

        client.SendErrorReport(macAddress, _exception);
      }
      catch
      {
        // ignore       
      }
      finally
      {
        if (client != null)
        {
          try
          {
            client.Dispose();
          }
          catch
          {
            client.Abort();
          }
        }
      }

      Application.Exit();
    }
  }
}
