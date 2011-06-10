using System;
using System.Windows.Forms;
using System.Threading;
using Setup.UserManagementServicesLive;

namespace Setup
{
  public partial class CredentialsForm : SetupForm
  {
    private object _lockObj = new object();
    private SimpleErrorWrapper _wrapper = null;

    public CredentialsForm()
    {
      InitializeComponent();

      txtEmailAddress.Text = AppDataSingleton.Instance.EmailAddress;
      txtPassword.Text = AppDataSingleton.Instance.Password;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      SetupHelper.ExitNoChanges();
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      if (AppDataSingleton.Instance.Repair)
      {
        SetupHelper.OpenForm<PrerequisitesForm>(this);
        return;
      }

      SetupHelper.OpenForm<ExistingUserPromptForm>(this);
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      ((Button)sender).Enabled = false;

      if (AppDataSingleton.Instance.EmailAddress != txtEmailAddress.Text)
        AppDataSingleton.Instance.ExistingUserDetailsDataRetrieved = false;

      AppDataSingleton.Instance.EmailAddress = txtEmailAddress.Text;
      AppDataSingleton.Instance.Password = txtPassword.Text;

      Thread thread = new Thread(new ThreadStart(CheckUserDetails));
      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      thread.CurrentCulture = ci;
      thread.CurrentUICulture = ci; 
      thread.Start();

      while (!thread.IsAlive) ;

      SetupHelper.ShowCommunicationAnimatingText(lblProgress, "Checking your details", thread);

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
          case "CONN":
            MessageBox.Show("Unable to communicate with Oxigen servers. Please check your internet connection or try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ((Button)sender).Enabled = true;
            return;
          case "EEPN":
            CredentialsRemindForm form = new CredentialsRemindForm();
            form.ShowDialog();
            ((Button)sender).Enabled = true;
            return;
          case "ENPN":
            MessageBox.Show("This email address is not registered with Oxigen. If you would like to create an account for this address, please click Back and select that you are not a registered user.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            ((Button)sender).Enabled = true;
            return;
          case "OK":
            // user data retrieved. check if Oxigen is already installed and if so, use that PC's GUID.
            SetupHelper.OpenForm<PcFormExistingUser>(this);
            break;
        }
      }
    }

    private void CheckUserDetails()
    {
      UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer client = null;
      string userGUID = null;
      
      lock (_lockObj)
      {
        try
        {
          string url = SetupHelper.GetResponsiveServer(ServerType.MasterGetConfig, "masterConfig", "UserManagementServices.svc");

          if (string.IsNullOrEmpty(url))
          {
              AppDataSingleton.Instance.SetupLogger.WriteTimestampedMessage("Checking user details - Couldn't find a responsive url.");
            _wrapper = SetupHelper.GetGenericErrorConnectingWrapper();
            return;
          }

          client = new UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer();

          client.Url = url;

          _wrapper = client.GetUserExistsByUserCredentials(txtEmailAddress.Text,
            txtPassword.Text,
            "password",
            out userGUID);
        }
        catch (System.Net.WebException ex)
        {
           AppDataSingleton.Instance.SetupLogger.WriteError(ex);

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

        if (_wrapper.ErrorStatus == Setup.UserManagementServicesLive.ErrorStatus1.Success)
          AppDataSingleton.Instance.User.UserGUID = userGUID;
      }
    }

    private void textBox_TextChanged(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(txtEmailAddress.Text.Trim()) || string.IsNullOrEmpty(txtPassword.Text.Trim()))
        btnNext.Enabled = false;

      if (!string.IsNullOrEmpty(txtEmailAddress.Text.Trim()) && !string.IsNullOrEmpty(txtPassword.Text.Trim()))
        btnNext.Enabled = true;
    }

    private void Form_Shown(object sender, EventArgs e)
    {
      IClientLogger logger = new ClientLogger();
      logger.Log("5.1-Credentials");
    }
  }
}
