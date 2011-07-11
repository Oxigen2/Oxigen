using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Setup
{
    public partial class PCNameStreamSubscriptionsForm : SetupForm
    {
        public PCNameStreamSubscriptionsForm()
        {
            InitializeComponent();

            streams.CheckOnClick = true;
            string dataPath = (string)GenericRegistryAccess.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Oxigen", "DataSettingsPath") + "data\\";

            // subscriptions either downloaded from server or existing on user's machine with existing Oxigen II.
            // (that is, subscriptions not in Setup.ini)
            Setup.DuplicateLibrary.ChannelSubscriptions nonInstallerAccompanyingSubscriptions = null;

            List<Setup.DuplicateLibrary.ChannelSubscription> channelSubscriptions = new List<Setup.DuplicateLibrary.ChannelSubscription>();

            foreach (Setup.DuplicateLibrary.ChannelSubscription subscription in AppDataSingleton.Instance.FileDetectedChannelSubscriptionsLocal.SubscriptionSet)
                channelSubscriptions.Add(subscription);

            // merge Subscriptions from existing user's ss_channel_subscription_data 
            // with those from Setup.ini with subscription. For existing users only.
            if (AppDataSingleton.Instance.MergeStreamsInstallation)
            {
                if (nonInstallerAccompanyingSubscriptions != null && nonInstallerAccompanyingSubscriptions.SubscriptionSet != null)
                {
                    foreach (Setup.DuplicateLibrary.ChannelSubscription subscription in nonInstallerAccompanyingSubscriptions.SubscriptionSet)
                    {
                        if (!Contains(channelSubscriptions, subscription))
                            channelSubscriptions.Add(subscription);
                    }
                }
            }

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
            if (streams.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one stream", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            // store selected subscriptions
            List<Setup.DuplicateLibrary.ChannelSubscription> selectedSubscriptions = new List<Setup.DuplicateLibrary.ChannelSubscription>();

            foreach (object obj in streams.CheckedItems)
                selectedSubscriptions.Add((Setup.DuplicateLibrary.ChannelSubscription)obj);

            UserManagementServicesLive.ChannelSubscriptions subscriptionsToUpload = new Setup.UserManagementServicesLive.ChannelSubscriptions();
            UserManagementServicesLive.ChannelSubscription[] subscriptionArray = SetupHelper.GetChannelSubscriptionsNetFromLocal(selectedSubscriptions.ToArray());

            subscriptionsToUpload.SubscriptionSet = subscriptionArray;

            AppDataSingleton.Instance.ChannelSubscriptionsToUpload = subscriptionsToUpload;

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
    }
}
