using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Setup.UserManagementServicesLive;

namespace Setup
{
  public partial class PcFormExistingUser : SetupForm
  {
    private object _lockObj = new object();
    SimpleErrorWrapper _wrapper = null;

    public PcFormExistingUser()
    {
      InitializeComponent();
    }

    private void PCListForm_Load(object sender, EventArgs e)
    {
      Setup.UserManagementServicesLive.PcInfo[] pcs = null;  

      if (string.IsNullOrEmpty(AppDataSingleton.Instance.NewPCName))
        txtPCName.Text = Environment.MachineName;
      else
        txtPCName.Text = AppDataSingleton.Instance.NewPCName;
    }

    private void CheckPCForRepairAndRedirect()
    {
      string registryPCGUID = SetupHelper.GetPcGUIDFromRegistry();
      SimpleErrorWrapper wrapper = null;

      if (registryPCGUID != null)
      {
        // check if pc GUID exists on master server and belongs to that User GUID
        bool bGUIDMatch = false;

        wrapper = GetGUIDMatch(AppDataSingleton.Instance.User.UserGUID, registryPCGUID, ref bGUIDMatch);

        if (wrapper.ErrorStatus == ErrorStatus1.Failure)
        {
          MessageBox.Show(wrapper.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          SetupHelper.OpenForm<CredentialsForm>(this);
          return;
        }

        if (wrapper.ErrorCode == "M")
        {
          AppDataSingleton.Instance.User.MachineGUID = registryPCGUID;
          GetPCSubscriptionsByMachineGUID(registryPCGUID);

          if (_wrapper.ErrorStatus == ErrorStatus1.Failure)
          {
            MessageBox.Show(wrapper.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            SetupHelper.OpenForm<CredentialsForm>(this);
            return;
          }

          // if installer has come with subscriptions, present the subscription form.
          if (AppDataSingleton.Instance.FileDetectedSubscriptionsFound)
          {
            SetupHelper.OpenForm<MergeChannelsForm>(this);

            return;
          }

          // PC GUID exists on PC and on master server and belongs to that User GUID          
          SetupHelper.OpenForm<InstallationPathsForm>(this);
         
          return;
        }
      }

      // there is a GUID mismatch or GUID is missing, check PC's MAC address against database
      string macAddress = SetupHelper.GetMACAddress();
      
      // check against database and create new GUID
      string newPcGUID;
      bool bMatch;

      wrapper = CompareMACAddresses(macAddress, AppDataSingleton.Instance.User.UserGUID, 
        AppDataSingleton.Instance.User.SoftwareMajorVersionNumber, AppDataSingleton.Instance.User.SoftwareMinorVersionNumber,
        out newPcGUID, out bMatch);

      if (wrapper.ErrorStatus == ErrorStatus1.Failure)
      {
        MessageBox.Show(wrapper.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        SetupHelper.OpenForm<CredentialsForm>(this);
        return;
      }

      AppDataSingleton.Instance.User.MachineGUID = newPcGUID;

      // if MAC Addresses have matched, get PC subscriptions, otherwise a new PC Profile has
      // already been created on the server and the newPcGUID will be stored on the PC that
      // runs the installer.
      if (bMatch)
      {
        GetPCSubscriptionsByMachineGUID(newPcGUID);
      
        if (_wrapper.ErrorStatus == ErrorStatus1.Failure)
        {
          MessageBox.Show(wrapper.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          SetupHelper.OpenForm<CredentialsForm>(this);
          return;
        }
      }

      // if installer has come with subscriptions, present the subscription form.
      if (AppDataSingleton.Instance.FileDetectedSubscriptionsFound)
      {
        SetupHelper.OpenForm<MergeChannelsForm>(this);

        return;
      }
      else
      {
        if (AppDataSingleton.Instance.FileDetectedSubscriptionsFound)
          AppDataSingleton.Instance.ChannelSubscriptionsToUpload.SubscriptionSet = SetupHelper.GetChannelSubscriptionsNetFromLocal(AppDataSingleton.Instance.FileDetectedChannelSubscriptionsLocal.SubscriptionSet);

        if (AppDataSingleton.Instance.DownloadedChannelSubscriptionsExist)
          AppDataSingleton.Instance.ChannelSubscriptionsToUpload.SubscriptionSet = SetupHelper.GetChannelSubscriptionsNetFromLocal(AppDataSingleton.Instance.DownloadedChannelSubscriptionsLocal.SubscriptionSet);      
      }

      SetupHelper.OpenForm<InstallationPathsForm>(this);
    }

    private SimpleErrorWrapper GetGUIDMatch(string userGUID, string registryPCGUID, ref bool bGUIDMatch)
    {
      SimpleErrorWrapper wrapper = null;
      UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer client = null;

      try
      {
        string url = SetupHelper.GetResponsiveServer(ServerType.MasterGetConfig, "masterConfig", "UserManagementServices.svc");

        if (string.IsNullOrEmpty(url))
        {
            AppDataSingleton.Instance.SetupLogger.WriteTimestampedMessage("Matching PC: Couldn't find a responsive URL.");
            return SetupHelper.GetGenericErrorConnectingWrapper();
        }

        client = new UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer();

        client.Url = url;

        wrapper = client.GetMatchedMachineGUID(userGUID, registryPCGUID, "password");
      }
      catch (System.Net.WebException ex)
      {
        AppDataSingleton.Instance.SetupLogger.WriteError(ex);
        return SetupHelper.GetGenericErrorConnectingWrapper();
      }

      return wrapper;
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
      AppDataSingleton.Instance.NewPC = true;    

      AppDataSingleton.Instance.User.MachineGUID = System.Guid.NewGuid().ToString().ToUpper() + "_" + SetupHelper.GetRandomLetter();
            
      if (AppDataSingleton.Instance.FileDetectedSubscriptionsFound)
      {
        AppDataSingleton.Instance.ChannelSubscriptionsToUpload.SubscriptionSet = SetupHelper.GetChannelSubscriptionsNetFromLocal(AppDataSingleton.Instance.FileDetectedChannelSubscriptionsLocal.SubscriptionSet);

        SetupHelper.OpenForm<MergeChannelsForm>(this);
        return;
      }      

      SetupHelper.OpenForm<UpdateExistingUserDetailsForm>(this);
    }

    private void GetPCSubscriptionsByMachineGUID(string newPcGUID)
    {
      UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer client = null;
      Setup.UserManagementServicesLive.ChannelSubscriptions subscriptionsNet = null;

      lock (_lockObj)
      {
        try
        {
          string url = SetupHelper.GetResponsiveServer(ServerType.MasterGetConfig, "masterConfig", "UserManagementServices.svc");

          if (string.IsNullOrEmpty(url))
          {
              AppDataSingleton.Instance.SetupLogger.WriteTimestampedMessage("Getting subscriptions: Couldn't get a responsive URL.");
            _wrapper = SetupHelper.GetGenericErrorConnectingWrapper();
            return;
          }

          client = new UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer();

          client.Url = url;

          _wrapper = client.GetPCSubscriptionsByMachineGUID(AppDataSingleton.Instance.User.UserGUID,
            newPcGUID,
            "password",
            out subscriptionsNet);
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

        if (subscriptionsNet.SubscriptionSet.Length > 0)
          AppDataSingleton.Instance.DownloadedChannelSubscriptionsLocal.SubscriptionSet = SetupHelper.GetChannelSubscriptionsLocalFromNet(subscriptionsNet.SubscriptionSet);
      }
    }

    private SimpleErrorWrapper CompareMACAddresses(string macAddress, string userGUID, 
      int softwareMajorVersionNumber, int softwareMinorVersionNumber, out string newPcGUID, out bool bMatch)
    {
      newPcGUID = null;
      bMatch = false;
      bool specified = true;      
      SimpleErrorWrapper wrapper = null;
      BasicHttpBinding_IUserManagementServicesNonStreamer client = null;

      try
      {
        string url = SetupHelper.GetResponsiveServer(ServerType.MasterGetConfig, "masterConfig", "UserManagementServices.svc");

        if (string.IsNullOrEmpty(url))
        {
            AppDataSingleton.Instance.SetupLogger.WriteMessage("Comparing MAC Addresses: Couldn't get a responsive URL.");
            return SetupHelper.GetGenericErrorConnectingWrapper();
        }
          client = new UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer();

        client.Url = url;

        wrapper = client.CompareMACAddresses(macAddress, userGUID,
          softwareMajorVersionNumber,
          true,
          softwareMinorVersionNumber,
          true, 
          "password",
          out newPcGUID, out bMatch, out specified);
      }
      catch (System.Net.WebException ex)
      {
          AppDataSingleton.Instance.SetupLogger.WriteError(ex);
        return SetupHelper.GetGenericErrorConnectingWrapper();
      }

      return wrapper;
    }

    private SimpleErrorWrapper GetPCList(out PcInfo[] pcs)
    {
      pcs = null;
      SimpleErrorWrapper wrapper = null;

      UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer client = null;

      try
      {
        string url = SetupHelper.GetResponsiveServer(ServerType.MasterGetConfig, "masterConfig", "UserManagementServices.svc");

        if (string.IsNullOrEmpty(url))
        {
            AppDataSingleton.Instance.SetupLogger.WriteTimestampedMessage("PCList Form: could not find a responsive URL");
          return SetupHelper.GetGenericErrorConnectingWrapper();
        }

        client = new UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer();

        client.Url = url;

        wrapper = client.GetPcListForInstallerEmail(AppDataSingleton.Instance.EmailAddress, "password", out pcs);
      }
      catch (System.Net.WebException ex)
      {
          AppDataSingleton.Instance.SetupLogger.WriteError(ex);
        return SetupHelper.GetGenericErrorConnectingWrapper();
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

      return wrapper;
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

      SetupHelper.OpenForm<CredentialsForm>(this);
    }

    private void ddPCs_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (((ComboBox)sender).SelectedIndex == 0)
      {
        lblPCName.Enabled = true;
        txtPCName.Enabled = true;
      }
      else
      {
        lblPCName.Enabled = false;
        txtPCName.Enabled = false;
      }
    }
  }
}
