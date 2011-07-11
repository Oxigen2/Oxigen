using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Setup.ClientLoggers;

namespace Setup
{
    public partial class PCNameStreamSubscriptionsForm : SetupForm
    {
        public PCNameStreamSubscriptionsForm()
        {
            InitializeComponent();

            streams.CheckOnClick = true;

            if (!File.Exists("Setup.ini")) return;

            Setup.DuplicateLibrary.ChannelSubscriptions nonInstallerAccompanyingSubscriptions = null;

            List<Setup.DuplicateLibrary.ChannelSubscription> channelSubscriptions =
                new List<Setup.DuplicateLibrary.ChannelSubscription>();

            foreach (
                Setup.DuplicateLibrary.ChannelSubscription subscription in
                    AppDataSingleton.Instance.FileDetectedChannelSubscriptionsLocal.SubscriptionSet)
                channelSubscriptions.Add(subscription);

            foreach (Setup.DuplicateLibrary.ChannelSubscription cs in channelSubscriptions)
                streams.Items.Add(cs, true);

            streams.DisplayMember = "ChannelName";
        }

        private bool Contains(List<Setup.DuplicateLibrary.ChannelSubscription> channelSubscriptions, Setup.DuplicateLibrary.ChannelSubscription subscription)
        {
            foreach (Setup.DuplicateLibrary.ChannelSubscription cs in channelSubscriptions)
            {
                if (cs.ChannelGUID == subscription.ChannelGUID)
                    return true;
            }

            return false;
        }

        private void PCNameStreamSubscriptionsForm_Load(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(AppDataSingleton.Instance.PCName))
                txtPCName.Text = Environment.MachineName;
            else
                txtPCName.Text = AppDataSingleton.Instance.PCName;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (File.Exists("Setup.ini") && streams.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one stream", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            // store selected subscriptions
            if (File.Exists("Setup.ini"))
            {
                List<Setup.DuplicateLibrary.ChannelSubscription> selectedSubscriptions =
                    new List<Setup.DuplicateLibrary.ChannelSubscription>();

                foreach (object obj in streams.CheckedItems)
                    selectedSubscriptions.Add((Setup.DuplicateLibrary.ChannelSubscription) obj);

                UserManagementServicesLive.ChannelSubscriptions subscriptionsToUpload =
                    new Setup.UserManagementServicesLive.ChannelSubscriptions();
                UserManagementServicesLive.ChannelSubscription[] subscriptionArray =
                    SetupHelper.GetChannelSubscriptionsNetFromLocal(selectedSubscriptions.ToArray());

                subscriptionsToUpload.SubscriptionSet = subscriptionArray;

                AppDataSingleton.Instance.ChannelSubscriptionsToUpload = subscriptionsToUpload;
            }
            
            string pcName = txtPCName.Text.Trim();

            if (string.IsNullOrEmpty(pcName))
            {
                MessageBox.Show("Please enter a name for your PC", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            AppDataSingleton.Instance.PCName = pcName;
            AppDataSingleton.Instance.User.MachineGUID = Guid.NewGuid().ToString().ToUpper() + "_" + SetupHelper.GetRandomLetter();

            SetupHelper.OpenForm<InstallationPathsForm>(this);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            SetupHelper.OpenForm<UserDetailsForm>(this);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetupHelper.ExitConfirmNoChanges();
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            ClientLogger logger = new PersistentClientLogger();
            logger.Log("6-PCNamePCSubscriptions");
        }
    }
}
