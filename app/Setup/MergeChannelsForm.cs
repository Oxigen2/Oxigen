using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Setup.ClientLoggers;
using Setup.UserManagementServicesLive;

namespace Setup
{
  public partial class MergeChannelsForm : SetupForm
  {
    private object _lockObj = new object();
    SimpleErrorWrapper _wrapper = null;
    string _userGUID = null;
    string _pcGUID = null;
    public volatile bool _bThreadStarted = false;

    public MergeChannelsForm()
    {
      InitializeComponent();

      streams.CheckOnClick = true;
      string dataPath = (string)GenericRegistryAccess.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Oxigen", "DataSettingsPath") + "data\\";
      
      // subscriptions either downloaded from server or existing on user's machine with existing Oxigen II.
      // (that is, subscriptions not in Setup.ini)
      Setup.DuplicateLibrary.ChannelSubscriptions nonInstallerAccompanyingSubscriptions = null;

      if (AppDataSingleton.Instance.MergeStreamsInstallation)
      {
        string existingSubscriptionsPath = dataPath + "SettingsData\\ss_channel_subscription_data.dat";

        if (File.Exists(existingSubscriptionsPath))
          nonInstallerAccompanyingSubscriptions = (Setup.DuplicateLibrary.ChannelSubscriptions)Serializer.Deserialize(typeof(Setup.DuplicateLibrary.ChannelSubscriptions), existingSubscriptionsPath, "password");
      }

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

    private void btnBack_Click(object sender, EventArgs e)
    {
        SetupHelper.OpenForm<OxigenExistsForm>(this);
        return;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      if (streams.CheckedItems.Count == 0)
      {
        MessageBox.Show("Please select at least one stream", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        return;
      }

      if (AppDataSingleton.Instance.MergeStreamsInstallation)
      {
        _pcGUID = SetupHelper.GetPcGUIDFromRegistry();

        if (_pcGUID == null)
        {
          MessageBox.Show("Cannot find your PC profile. Please run Setup on Repair mode.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          Application.Exit();
          return;
        }

        _userGUID = SetupHelper.GetUserGUIDFromRegistry();

        if (_userGUID == null)
        {
          MessageBox.Show("Cannot find your User profile. Please run Setup on Repair mode.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          Application.Exit();
          return;
        }
      }
      else
      {
        _userGUID = AppDataSingleton.Instance.User.UserGUID;
        _pcGUID = AppDataSingleton.Instance.User.MachineGUID;
      }

      // store selected subscriptions
      List<Setup.DuplicateLibrary.ChannelSubscription> selectedSubscriptions = new List<Setup.DuplicateLibrary.ChannelSubscription>();

      foreach (object obj in streams.CheckedItems)
        selectedSubscriptions.Add((Setup.DuplicateLibrary.ChannelSubscription)obj);

      Setup.UserManagementServicesLive.ChannelSubscriptions subscriptionsToUpload = new Setup.UserManagementServicesLive.ChannelSubscriptions();

      Setup.UserManagementServicesLive.ChannelSubscription[] subscriptionArray = SetupHelper.GetChannelSubscriptionsNetFromLocal(selectedSubscriptions.ToArray());

      subscriptionsToUpload.SubscriptionSet = subscriptionArray;
      
      AppDataSingleton.Instance.ChannelSubscriptionsToUpload = subscriptionsToUpload;
      
      if (AppDataSingleton.Instance.MergeStreamsInstallation)
      {
        ((Button)sender).Enabled = false;

        Thread thread = new Thread(new ThreadStart(UpdateUserSubscriptions));
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
        thread.CurrentCulture = ci;
        thread.CurrentUICulture = ci; 
        thread.Start();

        while (!_bThreadStarted) ;

        SetupHelper.ShowCommunicationAnimatingText(lblProgress, "Updating your subscriptions", thread);

        lock (_lockObj)
        {
          if (_wrapper.ErrorStatus == ErrorStatus1.Failure)
          {
            MessageBox.Show(_wrapper.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ((Button)sender).Enabled = true;

            return;
          }
        }
      }

      if (!AppDataSingleton.Instance.MergeStreamsInstallation && AppDataSingleton.Instance.ExistingUser)
      {
        SetupHelper.OpenForm<UserDetailsForm>(this);

        return;
      }

      if (!AppDataSingleton.Instance.MergeStreamsInstallation && !AppDataSingleton.Instance.ExistingUser)
      {
        SetupHelper.OpenForm<InstallationPathsForm>(this);

        return;
      }

      SetupHelper.OpenForm<SubscriptionsUpdatedForm>(this);
    }

    private void UpdateUserSubscriptions()
    {
      _bThreadStarted = true;

      lock (_lockObj)
      {
        try
        {
            using (var client = new UserDataManagementClient())
            {
                _wrapper = client.EditSubscriptions(_userGUID, _pcGUID,
                                                    AppDataSingleton.Instance.ChannelSubscriptionsToUpload, "password");
            }
        }
        catch (System.Net.WebException ex)
        {
            AppDataSingleton.Instance.SetupLogger.WriteError(ex);
          _wrapper = SetupHelper.GetGenericErrorConnectingWrapper();
        }
      }
    }

    private void Form_Shown(object sender, EventArgs e)
    {
      ClientLogger logger = new PersistentClientLogger();
      logger.Log("5.4-MergeChannels");
    }

    private void MergeChannelsForm_Load(object sender, EventArgs e)
    {

    }
  }
}