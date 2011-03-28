using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.ServiceProcess;

namespace SimpleInstaller
{
  public partial class InstallationPathPrompt : Form
  {
    string _systemDrive = Environment.GetEnvironmentVariable("SystemDrive") + "\\";
    string _winDir = Environment.GetEnvironmentVariable("WinDir") + "\\";
    string _programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\";
    string _systemFolder = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\";
    string _documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";
    string _oxigenIIBinarySubfolder = "Oxigen\\bin\\";
    string _oxigenIIDataRootFolder = "OxigenData\\";
    string _serviceName = Settings.Default.ServiceName;

    string _selectedDataPath = "";
    string _selectedSettingsDataPath = "";
    string _selectedBinariesPath = "";

    bool _bToNextForm = false;

    public InstallationPathPrompt()
    {
      InitializeComponent();

      binariesPathTextBox.Text = _programFiles + _oxigenIIBinarySubfolder;
      dataPathTextBox.Text = _documentsFolder + _oxigenIIDataRootFolder;
    }
    
    private void btnInstall_Click(object sender, EventArgs e)
    {
      _selectedDataPath = dataPathTextBox.Text;
      _selectedSettingsDataPath = _selectedDataPath + "SettingsData\\";
      _selectedBinariesPath = binariesPathTextBox.Text;

      StopSSGService();

      try
      {
        Installer.Install(_selectedDataPath, _selectedSettingsDataPath, _selectedBinariesPath,
          _systemFolder, _winDir, _programFiles, _serviceName);

      }
      catch (IOException)
      {
        MessageBox.Show("Oxigen is currently in use and some of the files cannot be copied. If your Oxigen Screen Saver is on preview mode, please close the preview window and try again.", "Error");

        this.Close();

        Application.Exit();

        return;
      }

      this.Activate();

      InstallerGlobalData.Instance.ContentExchangerPath = _selectedBinariesPath + "OxigenCE.exe";
      InstallerGlobalData.Instance.DataPath = _selectedDataPath;
      InstallerGlobalData.Instance.SettingsDataPath = _selectedSettingsDataPath;

      DeleteOldData();

      _bToNextForm = true;

      (new InstallationEnd()).Show();
            
      this.Close();

      this.Dispose();
    }

    private void DeleteOldData()
    {
      if (Directory.Exists(_selectedDataPath + "Assets"))
      {
        string[] assets = Directory.GetFiles(_selectedDataPath + "Assets");

        foreach (string assetFile in assets)
          File.Delete(assetFile);
      }

      if (Directory.Exists(_selectedDataPath + "ChannelData"))
      {
        string[] channelData = Directory.GetFiles(_selectedDataPath + "ChannelData");

        foreach (string channelFile in channelData)
          File.Delete(channelFile);
      }

      if (Directory.Exists(_selectedSettingsDataPath))
      {
        string [] settingsDataFiles = Directory.GetFiles(_selectedSettingsDataPath);

        foreach (string file in settingsDataFiles)
        {
          if (!file.EndsWith("ss_general_data.dat") && !file.EndsWith("UserSettings.dat"))
            File.Delete(file);
        }
      }
    }

    private void StopSSGService()
    {
      // stop service
      System.Diagnostics.Process.Start("cmd", @"/c ""net stop ""Oxigen Service"" """);     

      // wait until service is stopped
      ServiceController sc = new ServiceController(_serviceName);

      ServiceControllerStatus scs;

      while (true)
      {
        sc.Refresh();

        try
        {
          scs = sc.Status;
        }
        catch // service does not exist
        {
          return;
        }

        if (scs == ServiceControllerStatus.Stopped)
          return;
      }
    }

    private void dataPathButton_Click(object sender, EventArgs e)
    {
      dataBrowseDialog.ShowDialog();
      dataPathTextBox.Text = dataBrowseDialog.SelectedPath + _oxigenIIDataRootFolder;
    }

    private void binariesPathButton_Click(object sender, EventArgs e)
    {
      binariesBrowseDialog.ShowDialog();
      binariesPathTextBox.Text = dataBrowseDialog.SelectedPath + _oxigenIIBinarySubfolder;
    }
  }
}
