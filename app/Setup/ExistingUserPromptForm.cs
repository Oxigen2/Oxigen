using System;
using System.Threading;
using System.Windows.Forms;
using Setup.ClientLoggers;
using Setup.UserManagementServicesLive;

namespace Setup
{
    public partial class ExistingUserPromptForm : SetupForm
    {
        private object _lockObj = new object();
        private SimpleErrorWrapper _wrapper = null;

        public ExistingUserPromptForm()
        {
            InitializeComponent();
        }

        private void ExistingUserPromptForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AppDataSingleton.Instance.EmailAddress))
            {
                rbNo.Checked = true;
                return;
            }

            if (AppDataSingleton.Instance.ExistingUser)
            {
                rbYes.Checked = true;
                txtEmailAddressExisting.Text = AppDataSingleton.Instance.EmailAddress;
                txtPasswordExisting.Text = AppDataSingleton.Instance.Password;
            }
            else
            {
                rbNo.Checked = true;
                txtEmailAddressNew.Text = AppDataSingleton.Instance.EmailAddress;
                txtPassword1New.Text = AppDataSingleton.Instance.Password;
                txtPassword2New.Text = AppDataSingleton.Instance.Password;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (rbYes.Checked)
            {
                if (string.IsNullOrEmpty(txtEmailAddressExisting.Text.Trim()))
                {
                    MessageBox.Show("Please enter your email address.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (string.IsNullOrEmpty(txtPasswordExisting.Text.Trim()))
                {
                    MessageBox.Show("Please enter your password.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                AppDataSingleton.Instance.ExistingUser = true;

                ((Button)sender).Enabled = false;

                if (AppDataSingleton.Instance.EmailAddress != txtEmailAddressExisting.Text)
                    AppDataSingleton.Instance.ExistingUserDetailsDataRetrieved = false;

                AppDataSingleton.Instance.EmailAddress = txtEmailAddressExisting.Text;
                AppDataSingleton.Instance.Password = txtPasswordExisting.Text;

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
                            SetupHelper.OpenForm<UserDetailsForm>(this);
                            break;
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtEmailAddressNew.Text.Trim()))
                {
                    MessageBox.Show("Please enter your email address.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (string.IsNullOrEmpty(txtPassword1New.Text.Trim()))
                {
                    MessageBox.Show("Please enter a password.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (txtPassword1New.Text.Trim() != txtPassword2New.Text.Trim())
                {
                    MessageBox.Show("Password fields do not match.", "Message");
                    return;
                }

                ((Button)sender).Enabled = false;

                Thread thread = new Thread(new ThreadStart(CheckIfEmailForNewUserAlreadyExists));
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
                            AppDataSingleton.Instance.EmailAddress = txtEmailAddressNew.Text.Trim();
                            AppDataSingleton.Instance.Password = txtPassword1New.Text.Trim();
                            AppDataSingleton.Instance.User.UserGUID = System.Guid.NewGuid().ToString().ToUpper() + "_" + SetupHelper.GetRandomLetter(); ;
                            SetupHelper.OpenForm<UserDetailsForm>(this);
                            break;
                    }
                }
            }
        }

        private void CheckIfEmailForNewUserAlreadyExists()
        {
            lock (_lockObj)
            {
                try
                {

                    using (var client = new UserDataManagementClient())
                    {
                        _wrapper = client.CheckEmailAddressExists(txtEmailAddressNew.Text.Trim(), "password");
                    }
                }
                catch (System.Net.WebException ex)
                {
                    AppDataSingleton.Instance.SetupLogger.WriteError(ex);
                    _wrapper = SetupHelper.GetGenericErrorConnectingWrapper();
                }

            }
        }

        private void CheckUserDetails()
        {
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

                    using (var client = new UserDataManagementClient())
                    {
                        client.Url = url;

                        _wrapper = client.GetUserExistsByUserCredentials(txtEmailAddressExisting.Text.Trim(),
                          txtPasswordExisting.Text.Trim(),
                          "password",
                          out userGUID);
                    }

                }
                catch (System.Net.WebException ex)
                {
                    AppDataSingleton.Instance.SetupLogger.WriteError(ex);

                    _wrapper = SetupHelper.GetGenericErrorConnectingWrapper();
                    return;
                }


                if (_wrapper.ErrorStatus == Setup.UserManagementServicesLive.ErrorStatus1.Success)
                    AppDataSingleton.Instance.User.UserGUID = userGUID;
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmailAddressNew.Text.Trim()) || string.IsNullOrEmpty(txtPasswordExisting.Text.Trim()))
                btnNext.Enabled = false;

            if (!string.IsNullOrEmpty(txtEmailAddressNew.Text.Trim()) && !string.IsNullOrEmpty(txtPasswordExisting.Text.Trim()))
                btnNext.Enabled = true;
        }

        private void rbYes_CheckedChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = true;

            if (((RadioButton)sender).Checked)
            {
                newUser.Visible = false;
                existingUser.Visible = true;
            }
        }

        private void rbNo_CheckedChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = true;

            if (((RadioButton)sender).Checked)
            {
                newUser.Visible = true;
                existingUser.Visible = false;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (AppDataSingleton.Instance.PrerequisitesFullyMet)
                SetupHelper.OpenForm<WelcomeForm>(this);
            else
                SetupHelper.OpenForm<PrerequisitesForm>(this);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetupHelper.ExitConfirmNoChanges();
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            ClientLogger logger = new PersistentClientLogger();
            logger.Log("4-ExistingUserPrompt");
        }
    }
}
