using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.ServiceProcess;
using Microsoft.Win32;
using OxigenIIAdvertising.LoggerInfo;
using System.Reflection;
using System.Configuration;
using System.Globalization;
using System.Runtime.Remoting;
using System.Diagnostics;
using OxigenIIAdvertising.SSG.Properties;
using System.Runtime.InteropServices;
using System.Drawing;
using OxigenIIAdvertising.XMLSerializer;
using OxigenIIAdvertising.UserSettings;

namespace OxigenIIAdvertising.SSG
{
  static class Program
  {
    static NotifyIcon _appIcon;
    static System.Timers.Timer _timer;

    static string _serviceName = "";
    static string _debugFilePath = "";
    static string _contentExchangerPath = "";
    static string _softwareUpdaterPath = "";
    static string _screenSaverConfigPath = "";
    static string _userSettingsPath = "";
    static string _screenSaverPath = "";
    static string _decryptionPassword = "";
    static bool _bCanUpdate = false;
    
    static EventLog _eventLog = null;    

    [STAThread]
    static void Main()
    {
      // Make sure no other trays are running
      Process process = Process.GetCurrentProcess();
      string processName = process.ProcessName;

      Process[] processes = Process.GetProcessesByName(processName);

      if (processes.Length > 1)
        return;

      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      System.Threading.Thread.CurrentThread.CurrentCulture = ci;
      System.Threading.Thread.CurrentThread.CurrentUICulture = ci; 

      SetGlobals();

      Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

      Application.EnableVisualStyles();

      IntializeIcon();

      SystemEvents.SessionEnded += new SessionEndedEventHandler(SystemEvent_SessionEnded);

      _timer = new System.Timers.Timer(10000);
      _timer.Elapsed += new System.Timers.ElapsedEventHandler(ServiceChecker);
      _timer.Enabled = true;

      Application.Run();      
    }

    static void Application_ApplicationExit(object sender, EventArgs e)
    {
      if (_timer != null)
        _timer.Dispose();

      if (_eventLog != null)
        _eventLog.Dispose();
    }

    private static void SetGlobals()
    {
      _serviceName = Properties.Settings.Default.ServiceName;
      _debugFilePath = ConfigurationSettings.AppSettings["AppDataPath"] + "SettingsData\\OxigenDebug.txt";
      _userSettingsPath = ConfigurationSettings.AppSettings["AppDataPath"] + "SettingsData\\UserSettings.dat";
      _contentExchangerPath = ConfigurationSettings.AppSettings["BinariesPath"] + "OxigenCE.exe";
      _softwareUpdaterPath = ConfigurationSettings.AppSettings["BinariesPath"] + "OxigenSU.exe";
      _screenSaverConfigPath = ConfigurationSettings.AppSettings["BinariesPath"] + "ScreenSaverConfig.exe";
      _screenSaverPath = ConfigurationSettings.AppSettings["WindowsSystemPath"] + "Oxigen.scr";
      _decryptionPassword = "password";

      _eventLog = new EventLog();
      _eventLog.Log = String.Empty;
      _eventLog.Source = "Oxigen Tray";

      _bCanUpdate = GetCanUpdate();
    }

    private static bool GetCanUpdate()
    {
      User user = null;

      // try 5 times to deserialize user settings.
      for (int i = 0; i < 5; i++)
      {
        try
        {
          user = (User)Serializer.Deserialize(typeof(User), _userSettingsPath, _decryptionPassword);
          return user.CanUpdate;
        }
        catch
        {
          // ignore and wait before trying again
          System.Threading.Thread.Sleep(300);
        }
      }

      // don't allow updates, as protection.
      return false;
    }

    private static void ServiceChecker(object source, System.Timers.ElapsedEventArgs e)
    {
      if (IsProcessRunning("OxigenService"))
      {
        _appIcon.Icon = Properties.Resources.ServiceStart;
        _appIcon.Text = "Oxigen Service is running";
      }
      else
      {
        _appIcon.Icon = Properties.Resources.ServiceStop;
        _appIcon.Text = "Oxigen Service is not running";
      }
    }

    internal static bool IsProcessRunning(string processName)
    {
      Process[] processes = Process.GetProcessesByName(processName);

      if (processes.Length > 0)
        return true;

      return false;
    }

    private static void IntializeIcon()
    {
      _appIcon = new NotifyIcon();
      _appIcon.Icon = Properties.Resources.ServiceStart;
      _appIcon.Visible = true;
      ContextMenu contextMenu = new ContextMenu();

      MenuItem menuItem = new MenuItem("Launch Oxigen");
      menuItem.DefaultItem = true;
      menuItem.Name = "LaunchOxigen";
      menuItem.Click += new EventHandler(ScreenSaver_Click);
      contextMenu.MenuItems.Add(menuItem);

      menuItem = new MenuItem("Oxigen Settings");
      menuItem.Name = "OxigenSettings";
      menuItem.Click += new EventHandler(Config_Click);
      contextMenu.MenuItems.Add(menuItem);

      menuItem = new MenuItem("Update Content");
      menuItem.Name = "UpdateContent";
      menuItem.Click += new EventHandler(Update_Click);
      UserAccountControl.AddShieldToMenuItem(menuItem);
      contextMenu.MenuItems.Add(menuItem);

      if (_bCanUpdate)
      {
        menuItem = new MenuItem("Update Software");
        menuItem.Name = "UpdateSoftware";
        menuItem.Click += new EventHandler(UpdateSoftware_Click);
        contextMenu.MenuItems.Add(menuItem);
      }

      menuItem = new MenuItem(OxigenIIAdvertising.SSG.Properties.Resources.HomePageName + " / Help");
      menuItem.Name = "HomePage";
      menuItem.Click += new EventHandler(HomePage_Click);
      contextMenu.MenuItems.Add(menuItem);

      menuItem = new MenuItem("-");
      menuItem.Name = "Separator1";
      contextMenu.MenuItems.Add(menuItem);

      menuItem = new MenuItem("Exit");
      menuItem.Name = "ExitApp";
      menuItem.Click += new EventHandler(Exit_Click);
      contextMenu.MenuItems.Add(menuItem);

      _appIcon.ContextMenu = contextMenu;
      _appIcon.Text = "Oxigen";
      _appIcon.DoubleClick += new EventHandler(Config_Click);
    }
      
    private static void Config_Click(object sender, EventArgs e)
    {
      try
      {
        System.Diagnostics.Process.Start(_screenSaverConfigPath);
      }
      catch
      {
        MessageBox.Show(Resources.GenericErrorMessage, Resources.GenericErrorHeader);
      }
    }

    private static void Update_Click(object sender, EventArgs e)
    {
      if (IsProcessRunning("OxigenCE"))
      {
        MessageBox.Show("Your Content Exchanger is already running. Please wait 5 minutes and try again.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        return;
      }

      try
      {
        System.Diagnostics.Process.Start(_contentExchangerPath, "/v");
      }
      catch
      {
        MessageBox.Show(Resources.GenericErrorMessage, Resources.GenericErrorHeader);
      }
    }

    private static void ScreenSaver_Click(object sender, EventArgs e)
    {
      // Make sure this works on all versions of windows!
      try
      {
        System.Diagnostics.Process.Start(_screenSaverPath);
      }
      catch
      {
        MessageBox.Show(Resources.GenericErrorMessage, Resources.GenericErrorHeader);
      }
    }

    private static void HomePage_Click(object sender, EventArgs e)
    {
      try
      {
        System.Diagnostics.Process.Start(OxigenIIAdvertising.SSG.Properties.Resources.HomePage);
      }
      catch
      {
        MessageBox.Show(Resources.HomePageErrorMessage, Resources.GenericErrorHeader);
      }
    }

    private static void UpdateSoftware_Click(object sender, EventArgs e)
    {
      // Make sure this works on all versions of windows!
      try
      {
        System.Diagnostics.Process.Start(_softwareUpdaterPath, "/v");
      }
      catch
      {
        MessageBox.Show(Resources.GenericErrorMessage, Resources.GenericErrorHeader);
      }
    }

    private static void Exit_Click(object sender, EventArgs e)
    {
      _appIcon.Dispose();

      Application.Exit();
    }

    private static void SystemEvent_SessionEnded(object sender, SessionEndedEventArgs e)
    {
      _appIcon.Dispose();

      Application.Exit();
    }
  }
}
