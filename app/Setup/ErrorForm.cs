using System;
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


      try
      {
        string macAddress = SetupHelper.GetMACAddress();

        using (var client = new UserDataManagementClient())
        {
            client.Url = "https://master-getconfig-a-1.oxigen.net/UserManagementServices.svc";

            client.SendErrorReport(macAddress, _exception);
        }


      }
      catch
      {
        // ignore       
      }
      
      Application.Exit();
    }
  }
}
