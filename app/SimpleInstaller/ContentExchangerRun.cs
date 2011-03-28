using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace SimpleInstaller
{
  public partial class ContentExchangerRun : Form
  {
    private System.Timers.Timer _timer = new System.Timers.Timer();

    private bool _bChannelDataDownloading = false;
    private bool _bAdvertListDownloaded = false;
    private bool _bAssetsDownloading = false;
    private bool _bDemographicDataDownloaded = false;
    private bool _bChannelSubscriptionsDownloaded = false;
    private bool _bPlayListCreated = false;

    private bool _bToNextForm = false;

    public ContentExchangerRun()
    {
      InitializeComponent();

      Control.CheckForIllegalCrossThreadCalls = false;

      pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
      pictureBox.Image = Resources.ProgressAnimation;

      this.Activate();

      // start content exchanger
      try
      {
        System.Diagnostics.Process.Start(InstallerGlobalData.Instance.ContentExchangerPath);

        // wait until content exchanger starts
        while (!ContentExchangerRunning()) ;
      }
      catch
      {
        MessageBox.Show("Data could not be downloaded from the server. Installer will now close and a new download will be attempted later.", "Error");

        this.Close();
        Application.Exit();
      }

      progressInfo.Text = "Downloading...";

      _timer.Interval = 200;
      _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
      _timer.Start();
    }

    void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      _bAdvertListDownloaded = File.Exists(InstallerGlobalData.Instance.SettingsDataPath + "\\ss_adcond_data.dat");

      _bDemographicDataDownloaded = File.Exists(InstallerGlobalData.Instance.SettingsDataPath + "\\ss_demo_data.dat");

      _bChannelDataDownloading = DirectoryNotEmpty(InstallerGlobalData.Instance.DataPath + "\\ChannelData");

      _bChannelSubscriptionsDownloaded = File.Exists(InstallerGlobalData.Instance.SettingsDataPath + "\\ss_channel_subscription_data.dat");

      _bPlayListCreated = File.Exists(InstallerGlobalData.Instance.SettingsDataPath + "\\ss_play_list.dat");
     
      _bAssetsDownloading = DirectoryNotEmpty(InstallerGlobalData.Instance.DataPath + "\\Assets");

      if (_bAdvertListDownloaded)
        progressInfo.Text = "Advert data downloaded.";

      if (_bDemographicDataDownloaded)
        progressInfo.Text = "Demographic data downloaded.";

      if (_bChannelSubscriptionsDownloaded)
        progressInfo.Text = "Channel Subscriptions downloaded.";

      if (_bChannelDataDownloading)
        progressInfo.Text = "Channel Data Downloading...";

      if (_bPlayListCreated)
        progressInfo.Text = "Playlist created.";

      if (_bAssetsDownloading)
        progressInfo.Text = "Playable content downloading...";

      if (!ContentExchangerRunning())
      {
        _timer.Stop();

        pictureBox.Visible = false;

        info.Text = "Download finished!";
        progressInfo.Text = "You can now close this window.";

        btnClose.Enabled = true;
      }
    }

    private bool DirectoryNotEmpty(string path)
    {
      string[] files = Directory.GetFiles(path);

      return files.Length > 0;
    }

    private bool ContentExchangerRunning()
    {
      Process[] runningProcesses = Process.GetProcesses();

      foreach (Process clsProcess in runningProcesses)
      {
        if (clsProcess.ProcessName.Contains("ContentExchanger"))
          return true;
      }

      return false;
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      _bToNextForm = true;

      this.Close();

      (new InstallationEnd()).Show();

      this.Dispose();
    }

    private void Form_Closing(object sender, FormClosingEventArgs e)
    {
      if (!_bToNextForm)
        Application.Exit();
    }
  }
}
