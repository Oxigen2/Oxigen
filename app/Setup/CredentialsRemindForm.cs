using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Setup.UserManagementServicesLive;
using OxigenIIAdvertising.ServerConnectAttempt;

namespace Setup
{
  public partial class CredentialsRemindForm : Form
  {
    private object _lockObj = new object();
    private SimpleErrorWrapper _wrapper = null;

    public CredentialsRemindForm()
    {
      InitializeComponent();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      this.Dispose();
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
      // send email
      Thread thread = new Thread(new ThreadStart(SendEmailReminder));
      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      thread.CurrentCulture = ci;
      thread.CurrentUICulture = ci; 
      thread.Start();

      while (!thread.IsAlive) ;

      SetupHelper.ShowCommunicationAnimatingText(lblProgress, "Contacting server", thread);

      lock (_lockObj)
      {
        if (_wrapper.ErrorStatus == ErrorStatus1.Failure)
        {
          MessageBox.Show(_wrapper.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          ((Button)sender).Enabled = true;
          return;
        }
      }

      MessageBox.Show("You should receive an e-mail with your password shortly.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

      this.Dispose();
    }

    private void SendEmailReminder()
    {
      UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer client = null;

      lock (_lockObj)
      {
        try
        {
          string url = SetupHelper.GetResponsiveServer(ServerType.MasterGetConfig, "masterConfig", "UserManagementServices.svc");

          if (string.IsNullOrEmpty(url))
          {
            _wrapper = SetupHelper.GetGenericErrorConnectingWrapper();
            return;
          }

          client = new UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer();

          client.Url = url;

          _wrapper = client.SendEmailReminder(AppDataSingleton.Instance.EmailAddress, "password");
        }
        catch (System.Net.WebException)
        {
          _wrapper = SetupHelper.GetGenericErrorConnectingWrapper();
          return;
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
      }
    }
  }
}
