using System;
using System.Windows.Forms;
using System.Threading;
using Setup.UserManagementServicesLive;

namespace Setup
{
  public partial class RegistrationForm1 : SetupForm
  {
    private object _lockObj = new object();
    string _emailAddress = null;
    SimpleErrorWrapper _wrapper = null;

    public RegistrationForm1()
    {
      InitializeComponent();

      txtEmailAddress.Text = AppDataSingleton.Instance.EmailAddress;
      txtPassword1.Text = AppDataSingleton.Instance.Password;
      txtPassword2.Text = AppDataSingleton.Instance.Password;
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      if (txtPassword1.Text != txtPassword2.Text)
      {
        MessageBox.Show("Password fields do not match.", "Message");
        return;
      }

      ((Button)sender).Enabled = false;

      lock (_lockObj)
        _emailAddress = txtEmailAddress.Text;

      Thread thread = new Thread(new ThreadStart(CheckEmailExists));
      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      thread.CurrentCulture = ci;
      thread.CurrentUICulture = ci; 
      thread.Start();

      while (!thread.IsAlive) ;

      SetupHelper.ShowCommunicationAnimatingText(lblProgress, "Checking email address", thread);

      lock (_lockObj)
      {
        if (_wrapper.ErrorStatus == ErrorStatus1.Failure)
        {
          MessageBox.Show(_wrapper.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          ((Button)sender).Enabled = true;
          return;
        }

        switch (_wrapper.ErrorCode)
        {
          case "E":
            MessageBox.Show("This email address is already registered with Oxigen.  Please either click 'Back' and select 'Yes' I am an existing Oxigen user or enter a different email address.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            ((Button)sender).Enabled = true;
            return;
          default:
            AppDataSingleton.Instance.EmailAddress = txtEmailAddress.Text;
            AppDataSingleton.Instance.Password = txtPassword1.Text;

            SetupHelper.OpenForm<RegistrationForm2>(this);
            break;
        }       
      }
    }

    private void CheckEmailExists()
    {
      UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer client = null;

      lock (_lockObj)
      {
        try
        {
          string url = SetupHelper.GetResponsiveServer(ServerType.MasterGetConfig, "masterConfig", "UserManagementServices.svc");

          if (string.IsNullOrEmpty(url))
          {
              AppDataSingleton.Instance.SetupLogger.WriteError("Checking if e-mail address exists: Couldn't get a responsive URL");
            _wrapper = SetupHelper.GetGenericErrorConnectingWrapper();
            return;
          }

          client = new UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer();

          client.Url = url;

          _wrapper = client.CheckEmailAddressExists(_emailAddress, "password");
        }
        catch (System.Net.WebException ex)
        {
            AppDataSingleton.Instance.SetupLogger.WriteError(ex);
          _wrapper = SetupHelper.GetGenericErrorConnectingWrapper();
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

    private void textBox_TextChanged(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(txtEmailAddress.Text.Trim()) &&
        !string.IsNullOrEmpty(txtPassword1.Text.Trim()) &&
        !string.IsNullOrEmpty(txtPassword2.Text.Trim()))
      {
        btnNext.Enabled = true;
      }

      if (string.IsNullOrEmpty(txtEmailAddress.Text.Trim()) ||
        string.IsNullOrEmpty(txtPassword1.Text.Trim()) ||
        string.IsNullOrEmpty(txtPassword2.Text.Trim()))
      {
        btnNext.Enabled = false;
      }
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      SetupHelper.OpenForm<ExistingUserPromptForm>(this);
    }

    private void Form_Shown(object sender, EventArgs e)
    {
      IClientLogger logger = new ClientLogger();
      logger.Log("5.5-Registration1");
    }
  }
}
