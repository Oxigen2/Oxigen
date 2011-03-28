using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using OxigenIIAdvertising.UserDataMarshallerServiceClient;
using InterCommunicationStructures;
using ServiceErrorReporting;
using System.IO;

namespace OxigenIIAdvertising.ContentExchanger
{
  public partial class SafeModeForm : Form
  {
    private bool _bError = false;
    private bool _bFinished = false;

    const int MF_BYPOSITION = 0x400;

    [DllImport("User32")]
    private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

    [DllImport("User32")]
    private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("User32")]
    private static extern int GetMenuItemCount(IntPtr hWnd);

    public SafeModeForm()
    {
      InitializeComponent();
    }

    private void SafeModeForm_Load(object sender, EventArgs e)
    {
      IntPtr hMenu = GetSystemMenu(this.Handle, false);
      int menuItemCount = GetMenuItemCount(hMenu);
      RemoveMenu(hMenu, menuItemCount - 1, MF_BYPOSITION);      
    }

    private void SafeModeForm_Shown(object sender, EventArgs e)
    {
      backgroundWorker.RunWorkerAsync();
    }

    private void Worker_DoWork(object sender, DoWorkEventArgs e)
    {
      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      System.Threading.Thread.CurrentThread.CurrentCulture = ci;
      System.Threading.Thread.CurrentThread.CurrentUICulture = ci; 

      UserDataMarshallerStreamerClient client = null;
      StreamErrorWrapper sw = null;

      try
      {
        client = new UserDataMarshallerStreamerClient();

        client.Endpoint.Address = new System.ServiceModel.EndpointAddress("https://relay-getconfig-1.oxigen.net/UserDataMarshaller.svc/file");

        backgroundWorker.ReportProgress(10);

        AppDataFileParameterMessage appDataFileParameterMessage = new AppDataFileParameterMessage();
        appDataFileParameterMessage.DataFileType = DataFileType.GeneralConfiguration;
        appDataFileParameterMessage.SystemPassPhrase = "password";

        sw = client.GetAppDataFiles(appDataFileParameterMessage);

        backgroundWorker.ReportProgress(80);

        if (sw.ErrorStatus != ErrorStatus.Success)
        {
          MessageBox.Show(OxigenIIAdvertising.ContentExchanger.Properties.Resources.CannotDownloadGeneralDataSafeMode + sw.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          sw.ReturnStream.Dispose();
          _bError = true;
          return;
        }
        else
          SaveStreamAndDispose(sw.ReturnStream, System.Configuration.ConfigurationSettings.AppSettings["AppDataPath"] + "\\SettingsData\\ss_general_data.dat");
      }
      catch
      {
        MessageBox.Show(OxigenIIAdvertising.ContentExchanger.Properties.Resources.SMGeneralError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        sw.ReturnStream.Dispose();
        _bError = true;
        return;
      }
    }

    private void SaveStreamAndDispose(Stream stream, string filePath)
    {
      FileStream fileStream = null;
      byte[] downloadedData = null;

      try
      {
        downloadedData = StreamToByteArray(stream);
      }
      catch
      {
        MessageBox.Show(OxigenIIAdvertising.ContentExchanger.Properties.Resources.CannotConvertStreamToByteArray, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        _bError = true;
        return;
      }
      finally
      {
        if (stream != null)
          stream.Dispose();
      }

      backgroundWorker.ReportProgress(90);

      try
      {
        File.WriteAllBytes(filePath, downloadedData);
      }
      catch
      {
        MessageBox.Show(OxigenIIAdvertising.ContentExchanger.Properties.Resources.CannotSaveStream, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        _bError = true;
        return;
      }
      finally
      {
        if (fileStream != null)
          fileStream.Dispose();
      }

      backgroundWorker.ReportProgress(100);
    }

    private byte[] StreamToByteArray(Stream stream)
    {
      MemoryStream ms = new MemoryStream();

      byte[] buffer = new byte[1000];

      int bytesRead = 0;

      do
      {
        bytesRead = stream.Read(buffer, 0, buffer.Length);

        ms.Write(buffer, 0, bytesRead);
      }
      while (bytesRead > 0);

      byte[] downloadedDataBuffer = ms.ToArray();

      ms.Close();
      ms.Dispose();

      return downloadedDataBuffer;
    }

    private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      progressBar.Value = e.ProgressPercentage;
    }

    private void Worker_Completed(object sender, RunWorkerCompletedEventArgs e)
    {
      _bFinished = true;

      if (!_bError)
        MessageBox.Show(OxigenIIAdvertising.ContentExchanger.Properties.Resources.SMCompleted, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

      Application.Exit();
    }

    private void SafeModeForm_Closing(object sender, FormClosingEventArgs e)
    {
      if (_bFinished)
        Application.Exit();
      else
        e.Cancel = true;
    }
  }
}
