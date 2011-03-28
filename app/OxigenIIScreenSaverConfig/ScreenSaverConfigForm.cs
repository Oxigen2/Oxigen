using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OxigenIIScreenSaverConfig.Properties;
using OxigenIIAdvertising.XMLSerializer;
using OxigenIIAdvertising.UserSettings;
using OxigenIIAdvertising.LoggerInfo;
using System.Configuration;
using System.Runtime.InteropServices;
using System.IO;

namespace OxigenIIScreenSaverConfig
{
  public partial class ScreenSaverConfigForm : Form
  {
    [DllImport("msi.dll", SetLastError = true)]
    static extern int MsiOpenProduct(string szProduct, out IntPtr hProduct);

    [DllImport("msi.dll", SetLastError = true)]
    static extern int MsiSetProperty(IntPtr hInstall, string szName, string szValue);

    [DllImport("msi.dll", SetLastError = true)]
    static extern int MsiCloseHandle(IntPtr hAny);

    string _debugFilePath = "";
    string _userSettingsPath = "";
    string _appDataPath = "";

    long _freeSpaceBytes = -1;

    Logger _logger = null;
    System.Globalization.CultureInfo _systemCultureInfo;
    User _user;

    public ScreenSaverConfigForm(System.Globalization.CultureInfo systemCultureInfo)
    {
      InitializeComponent();

      _systemCultureInfo = systemCultureInfo;

      _appDataPath = ConfigurationSettings.AppSettings["AppDataPath"];
      _debugFilePath = _appDataPath + "SettingsData\\OxigenDebug.txt";
      _userSettingsPath = _appDataPath + "SettingsData\\UserSettings.dat";

      CELog log = new CELog();
      log.LoadLog();

      System.Globalization.CultureInfo englishCultureInfo = new System.Globalization.CultureInfo("en-GB");

      if (log.HasLog)
      {
        System.Threading.Thread.CurrentThread.CurrentCulture = _systemCultureInfo;
        txtLastRunDate.Text = log.LastRun.ToString();
        txtLastRunStatus.Text = log.LastStatus;

        // if content never downloaded or log file deleted, a new DateTime() will always return a date of 1/1/0001
        if (log.LastDownloadedContent.Year != 1)
          txtContentLastDownloaded.Text = log.LastDownloadedContent.ToString();

        System.Threading.Thread.CurrentThread.CurrentCulture = englishCultureInfo;
      }

      try
      {
        _user = GetUserSettings();
      }
      catch
      {
        MessageBox.Show(Resources.ErrorLoadingUserSettings, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

        Application.Exit();
        return;
      }

      soundEnabled.Checked = !_user.MuteVideo;
     
      txtDefaultDisplayDuration.Text = _user.DefaultDisplayDuration.ToString();

      long freeBytes = -1;
      int freeMB = -1;
      float freeGB = -1;

      GetFreeSpace(ref freeBytes, ref freeMB, ref freeGB);

      // Check free space
      // If user space > free space - 20MB
      //    Show on textbox: free space - 20MB
      // Else show user's free space
      if (_user.AssetFolderSize > freeBytes)
        txtSpace.Text = freeMB.ToString();
      else
        txtSpace.Text = (_user.AssetFolderSize / (1024L * 1024L)).ToString();

      System.Threading.Thread.CurrentThread.CurrentCulture = _systemCultureInfo;
      maxSpaceMessage.Text = "You can use up to " + freeMB + " MB (" + freeGB + " GB)";
      System.Threading.Thread.CurrentThread.CurrentCulture = englishCultureInfo;
    }

    private void GetFreeSpace(ref long freeBytes, ref int freeMB, ref float freeGB)
    {
      DriveInfo di = new DriveInfo(_appDataPath.Substring(0, 1));

      // user can use the available free space of the allocated drive to store Oxigen assets
      // minus 20 MB.
      freeBytes = di.AvailableFreeSpace - 20971520L;
      double availableFreeSpace = (double)freeBytes;

      freeMB = (int)(availableFreeSpace / 1048576D);
      freeGB = (int)(availableFreeSpace / 1073741824D);

      _freeSpaceBytes = freeBytes;
    }

    private User GetUserSettings()
    {
      User user = null;

      try
      {
        user = (User)Serializer.Deserialize(typeof(User), _userSettingsPath, "password");
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);

        MessageBox.Show(Resources.ErrorLoadingUserSettings, "Error");
        Application.Exit(); 
      }

      return user;
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      if (btnApply.Enabled)
      {
        if (SaveSettings())
          Application.Exit();
      }
      else
      {
        Application.Exit();
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      if (SaveSettings())
        btnApply.Enabled = false;
    }

    private bool SaveSettings()
    {
      // no need to try/catch int parsing as textboxes are masked
      int defaultDisplayDuration = int.Parse(txtDefaultDisplayDuration.Text);
      long space = long.Parse(txtSpace.Text) * 1024L * 1024L; // convert to bytes

      if (defaultDisplayDuration < 5 || defaultDisplayDuration > 20)
      {
        MessageBox.Show("Default display duration must be between 5 and 20 seconds.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        return false;
      }

      if (space < 104857600)
      {
        MessageBox.Show("Oxigen needs at least 100 MB to store slides.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        return false;
      }

      if (space > _freeSpaceBytes)
      {
        MessageBox.Show("Please select a value within 100 MB and the free space of your drive.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        return false;
      }

      _user.AssetFolderSize = space;
      _user.DefaultDisplayDuration = defaultDisplayDuration;
      _user.MuteVideo = !soundEnabled.Checked;
      _user.MuteFlash = !soundEnabled.Checked;

      try
      {
        Serializer.Serialize(_user, _userSettingsPath, "password");
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);

        MessageBox.Show(Resources.ErrorSavingUserSettings, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Application.Exit();
      }

      return true;
    }

    private void Sound_CheckedChanged(object sender, EventArgs e)
    {
      btnApply.Enabled = true;
    }

    private void DisplayDuration_TextChanged(object sender, EventArgs e)
    {
      btnApply.Enabled = true;
    }

    private void Space_TextChanged(object sender, EventArgs e)
    {
      btnApply.Enabled = true;
    }
  }
}
