using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Setup.ClientLoggers;
using Setup.UserManagementServicesLive;
using System.Threading;

namespace Setup
{
  public partial class InstallationProgressForm : OxigenForm
  {
    const int MF_BYPOSITION = 0x400;

    [DllImport("User32")]
    private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

    [DllImport("User32")]
    private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("User32")]
    private static extern int GetMenuItemCount(IntPtr hWnd);

    private SimpleErrorWrapper _wrapper = null;
    private volatile bool _bThreadStarted = false;
    private object _lockObj = new object();

    public InstallationProgressForm()
    {
      InitializeComponent();

      progressBar.Maximum = 100;
    }

    private void InstallationProgressForm_Load(object sender, EventArgs e)
    {
      IntPtr hMenu = GetSystemMenu(this.Handle, false);
      int menuItemCount = GetMenuItemCount(hMenu);
      RemoveMenu(hMenu, menuItemCount - 1, MF_BYPOSITION);      
    }

    private void InstallationProgress_Shown(object sender, EventArgs e)
    {
      Application.DoEvents();

      ClientLogger logger = new PersistentClientLogger();
      logger.Log("8-InstallationProgress");

      SetupHelper.ShowMessage(lblProgress, "Installing binaries...");

      progressBar.Value = 3;
      
      // do uninstall steps if this is a repair
      if (AppDataSingleton.Instance.Repair)
      {
        SetupHelper.ShowMessage(lblProgress, "Repairing installation...");

        try
        {
          SetupHelper.KillProcess("OxigenService");
        }
        catch
        {
          MessageBox.Show("Oxigen Service could not be stopped. Oxigen Service must be stopped before uninstalling the software. Uninstall will now exit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          Application.Exit();
          return;
        }
                
        SetupHelper.RemoveAllFiles(AppDataSingleton.Instance.BinariesPath + "bin", SetupHelper.GetSystemDirectory(), AppDataSingleton.Instance.DataPath);
        SetupHelper.UninstallMSI(this);
      }

      if (!CheckMSI())
      {
        Application.Exit();
        return;
      }
      
      SetupHelper.InstallMSI(this);

      progressBar.Value = 80;

      SetupHelper.ShowMessage(lblProgress, "Saving settings...");

      try
      {
        SetupHelper.DoPostMSIInstallSteps();

        progressBar.Value = 85;

        SetupHelper.CopySetup();

        SetupHelper.ShowMessage(lblProgress, "Updating your details...");

        SendDetails();

        AppDataSingleton.Instance.SetupLogger.WriteMessage("InstallationProgress_Shown 1");
        SetupHelper.SetRegistryForModifyUninstall();
        AppDataSingleton.Instance.SetupLogger.WriteMessage("InstallationProgress_Shown 2");
        progressBar.Value = 95;
      }
      catch (Exception ex)
      {
          if (ex is System.Net.WebException)
          {
              AppDataSingleton.Instance.SetupLogger.WriteError(ex);

              MessageBox.Show(
                  "Unable to communicate with Oxigen servers. Please check your internet connection or try again later.",
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
          else
              MessageBox.Show("An error has occurred. Please contact Oxigen stating the following message:\r\n" + ex.Message + "\r\n\r\nOxigen now will rollback the changes it has made to your system and exit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        
        progressBar.Value = 80;
        SetupHelper.ShowMessage(lblProgress, "Removing binaries...");

        SetupHelper.UninstallMSI(this);

        progressBar.Value = 40;

        SetupHelper.ShowMessage(lblProgress, "Cleaning up...");

        SetupHelper.RemoveAllFiles(AppDataSingleton.Instance.BinariesPath + "bin", SetupHelper.GetSystemDirectory(), AppDataSingleton.Instance.DataPath);

        progressBar.Value = 0;

        Application.Exit();
        return;
      }

      SetupHelper.ShowMessage(lblProgress, "Starting Oxigen Service...");

      // Start Screensaver Guardian
      try
      {
        System.Diagnostics.Process.Start(AppDataSingleton.Instance.BinariesPath + "bin\\OxigenService.exe");
      }
      catch
      {
        // ignore starting of service.
      }

      progressBar.Value = 100;

      SetupHelper.OpenForm<InstallComplete>(this);
    }

    private bool CheckMSI()
    {
      if (!File.Exists(Directory.GetCurrentDirectory() + "\\Oxigen.msi"))
      {
        AppDataSingleton.Instance.SetupLogger.WriteError(Directory.GetCurrentDirectory() + "\\Oxigen.msi not found.");
        if (MessageBox.Show("Oxigen.msi is not found in the same directory as Setup. You need Oxigen.msi that came with this Setup file to install the Oxigen files. Please make sure Oxigen.msi is in the same folder as Setup. Pressing Cancel will exit installation. Your system will not be modified.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand) == DialogResult.Retry)
          return CheckMSI();
        else
          return false;
      }

      return true;
    }

    private void SendDetails()
    {
      Thread t = null;

      if (AppDataSingleton.Instance.ExistingUser)
        t = new Thread(new ThreadStart(UpdateUserDetails));
      else
        t = new Thread(new ThreadStart(RegisterUserDetails));

      t.Start();

      // wait until thread starts before continuing
      while (!_bThreadStarted) ;

      // now wait until it finishes
      while (t.IsAlive) ;

      if (_wrapper.ErrorStatus != ErrorStatus1.Success)
        throw new RemoteServiceException(_wrapper.Message);
    }

    // new user communication
    private void RegisterUserDetails()
    {
      _bThreadStarted = true;

      string macAddress = SetupHelper.GetMACAddress();

      lock (_lockObj)
      {
        try
        {
          AppDataSingleton.Instance.SetupLogger.WriteMessage("RegisterUserDetails 2");

             AppDataSingleton.Instance.SetupLogger.WriteMessage("RegisterUserDetails 3");
            using (var client = new UserDataManagementClient())
          {

              AppDataSingleton.Instance.SetupLogger.WriteMessage("RegisterUserDetails 4");

              _wrapper = client.RegisterNewUser(AppDataSingleton.Instance.EmailAddress,
                AppDataSingleton.Instance.Password,
                AppDataSingleton.Instance.FirstName,
                AppDataSingleton.Instance.LastName,
                AppDataSingleton.Instance.Gender,
                AppDataSingleton.Instance.DOB,
                true,
                AppDataSingleton.Instance.TownCity,
                AppDataSingleton.Instance.State,
                AppDataSingleton.Instance.Country,
                AppDataSingleton.Instance.OccupationSector,
                AppDataSingleton.Instance.EmploymentLevel,
                AppDataSingleton.Instance.AnnualHouseholdIncome,
                AppDataSingleton.Instance.User.UserGUID,
                AppDataSingleton.Instance.User.MachineGUID,
                AppDataSingleton.Instance.User.SoftwareMajorVersionNumber,
                true,
                AppDataSingleton.Instance.User.SoftwareMinorVersionNumber,
                true,
                macAddress,
                AppDataSingleton.Instance.PCName,
                AppDataSingleton.Instance.ChannelSubscriptionsToUpload,
                "password");          
            }




          AppDataSingleton.Instance.SetupLogger.WriteMessage("RegisterUserDetails 5");
        }
        catch (System.Net.WebException ex)
        {
            AppDataSingleton.Instance.SetupLogger.WriteError(ex);
          _wrapper = SetupHelper.GetGenericErrorConnectingWrapper();
        }
      }
    }

    // existing user communication
    private void UpdateUserDetails()
    {
      _bThreadStarted = true;

      lock (_lockObj)
      {
        try
        {

          using (var client = new UserDataManagementClient())
          {
              _wrapper = client.UpdateUserAccount(AppDataSingleton.Instance.EmailAddress,
                                                  AppDataSingleton.Instance.Password,
                                                  AppDataSingleton.Instance.FirstName,
                                                  AppDataSingleton.Instance.LastName,
                                                  AppDataSingleton.Instance.Gender,
                                                  AppDataSingleton.Instance.DOB,
                                                  true,
                                                  AppDataSingleton.Instance.TownCity,
                                                  AppDataSingleton.Instance.State,
                                                  AppDataSingleton.Instance.Country,
                                                  AppDataSingleton.Instance.OccupationSector,
                                                  AppDataSingleton.Instance.EmploymentLevel,
                                                  AppDataSingleton.Instance.AnnualHouseholdIncome,
                                                  AppDataSingleton.Instance.ChannelSubscriptionsToUpload,
                                                  AppDataSingleton.Instance.GeneralData.SoftwareMajorVersionNumber,
                                                  true,
                                                  AppDataSingleton.Instance.GeneralData.SoftwareMinorVersionNumber,
                                                  true,
                                                  AppDataSingleton.Instance.User.MachineGUID,
                                                  AppDataSingleton.Instance.PCName,
                                                  SetupHelper.GetMACAddress(),
                                                  "password");
          }
        }
        catch (System.Net.WebException ex)
        {
          AppDataSingleton.Instance.SetupLogger.WriteError(ex);
          _wrapper = SetupHelper.GetGenericErrorConnectingWrapper();
        }
      }
    }    
  }
}
