using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OxigenIIAdvertising.FileLocker;
using System.IO;
using OxigenIIAdvertising.EncryptionDecryption;
using System.Collections;
using OxigenIIAdvertising.LogWriter;
using OxigenIIAdvertising.ScreenSaver;
using OxigenIIAdvertising.AppData;
using System.Threading;
using OxigenIIAdvertising.LogStats;
using OxigenIIAdvertising.XMLSerializer;
using OxigenIIAdvertising.LoggerInfo;
using OxigenIIAdvertising;

namespace LibraryTester
{
  public partial class Appender : Form
  {
    public Appender()
    {
      InitializeComponent();     
    }

    private void PopulateSingleton()
    {
      //LogSingletonAccessor lsa = new LogSingletonAccessor();

      //lsa.AssetImpressionStartDateTime = DateTime.Now;

      //Thread.Sleep(1000);

      //// advert click logs
      //PlaylistAsset pla = new AdvertPlaylistAsset
      //{
      //  AssetID = 1000,
      //  DisplayLength = 5
      //};

      //lsa.AddClickLog(pla);

      //pla = new AdvertPlaylistAsset
      //{
      //  AssetID = 1001,
      //  DisplayLength = 10
      //};

      //lsa.AddClickLog(pla);

      //// content click logs
      //pla = new ContentPlaylistAsset
      //{
      //  AssetID = 1002,
      //  DisplayLength = 15
      //};

      //lsa.AddClickLog(pla);

      //pla = new ContentPlaylistAsset
      //{
      //  AssetID = 1003,
      //  DisplayLength = 20
      //};

      //lsa.AddClickLog(pla);

      //// advert impression logs
      //pla = new AdvertPlaylistAsset
      //{
      //  AssetID = 1004,
      //  DisplayLength = 25
      //};

      //lsa.AddImpressionLog(pla);

      //pla = new AdvertPlaylistAsset
      //{
      //  AssetID = 1005,
      //  DisplayLength = 30
      //};

      //lsa.AddImpressionLog(pla);

      //// content impression Logs
      //pla = new ContentPlaylistAsset
      //{
      //  AssetID = 1006,
      //  DisplayLength = 35
      //};

      //lsa.AddImpressionLog(pla);

      //pla = new ContentPlaylistAsset
      //{
      //  AssetID = 1007,
      //  DisplayLength = 40
      //};

      //lsa.AddImpressionLog(pla);
    }

    private void btnAppend_Click(object sender, EventArgs e)
    {
      PopulateSingleton();

      Logger logger = new Logger("vjhveruio huiruierg", @"C:\OxigenData\SettingsData\OxigenDebug.txt");

      try
      {
        LogFileWriter.WriteLogs(@"C:\OxigenData\SettingsData\ss_ad_c_1.dat",
          @"C:\OxigenData\SettingsData\ss_ad_c_2.dat",
          @"C:\OxigenData\SettingsData\ss_ad_s_1.dat",
          @"C:\OxigenData\SettingsData\ss_ad_s_2.dat",
          @"C:\OxigenData\SettingsData\ss_co_c_1.dat",
          @"C:\OxigenData\SettingsData\ss_co_c_2.dat",
          @"C:\OxigenData\SettingsData\ss_co_s_1.dat",
          @"C:\OxigenData\SettingsData\ss_co_s_2.dat",
          @"C:\OxigenData\SettingsData\ss_usg_1.dat",
          @"C:\OxigenData\SettingsData\ss_usg_2.dat",
          "machineGUIDGoesHere", "userGUIDGoesHere", 123, "password", true, false, logger);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
        return;
      }

      MessageBox.Show("Logs Appended and Encrypted");
    }
    
    private void btnLoad_Click(object sender, EventArgs e)
    {
      openFileDialog.ShowDialog();
      
      if (openFileDialog.FileNames.Length < 0)
        return;

      if (openFileDialog.FileNames.Length > 1)
      {
        MessageBox.Show("Only one file is allowed");
        return;
      }

      DecryptShow(openFileDialog.FileName);
    }

    private void DecryptShow(string fileName)
    {
      FileStream fs = null;
      string existingLogs = "";

      try
      {
        existingLogs = ReadExistingLogsString(ref fs, fileName, fileName, "password");
      }
      catch (Exception eX)
      {
        textBox1.Text = eX.ToString();
        return;
      }
      finally
      {
        Locker.ClearFileStream(ref fs);
      }

      textBox1.Text = existingLogs;
    }

    private static Queue<string> ReadExistingLogs(ref FileStream fileStream,
      string path1, string path2, string cryptPassword)
    {
      MemoryStream memoryStream = null;

      try
      {
        try
        {
          memoryStream = Locker.ReadDecryptFile(ref fileStream, path1, cryptPassword, false, true);
        }
        catch (InvalidOperationException)
        {
          memoryStream = Locker.ReadDecryptFile(ref fileStream, path1, cryptPassword, false, true);
        }
      }
      catch (Exception ex)
      {
        Locker.ClearFileStream(ref fileStream);

        throw ex;
      }

      Queue<string> existingRawLogs = new Queue<string>();

      // if memory stream is not null (i.e. log files already exist)
      if (memoryStream != null)
      {
        StreamReader sr = new StreamReader(memoryStream);

        while (sr.Peek() >= 0)
        {
          try
          {
            existingRawLogs.Enqueue(sr.ReadLine());
          }
          catch (Exception ex)
          {
            Locker.ClearFileStream(ref fileStream);

            sr.Close();
            sr.Dispose();
            memoryStream.Close();
            memoryStream.Dispose();

            throw ex;
          }
        }

        sr.Close();
        sr.Dispose();
        memoryStream.Close();
        memoryStream.Dispose();
      }

      return existingRawLogs;
    }

    private static string ReadExistingLogsString(ref FileStream fileStream,
     string path1, string path2, string cryptPassword)
    {
      MemoryStream memoryStream = null;

      try
      {
        try
        {
          memoryStream = Locker.ReadDecryptFile(ref fileStream, path1, cryptPassword, false, true);
        }
        catch (InvalidOperationException)
        {
          memoryStream = Locker.ReadDecryptFile(ref fileStream, path1, cryptPassword, false, true);
        }
      }
      catch (Exception ex)
      {
        Locker.ClearFileStream(ref fileStream);

        throw ex;
      }

      string decryptedLogs = "";

      // if memory stream is not null (i.e. log files already exist)
      if (memoryStream != null)
      {
        StreamReader sr = new StreamReader(memoryStream);

        try
        {
          decryptedLogs = sr.ReadToEnd();
        }
        catch (Exception ex)
        {
          Locker.ClearFileStream(ref fileStream);

          sr.Close();
          sr.Dispose();
          memoryStream.Close();
          memoryStream.Dispose();

          throw ex;
        }

        sr.Close();
        sr.Dispose();
        memoryStream.Close();
        memoryStream.Dispose();
      }

      return decryptedLogs;
    }

    private void btnEncrypt_Click(object sender, EventArgs e)
    {
      openFileDialog.ShowDialog();

      if (openFileDialog.FileName == "")
        return;

      foreach (string fileName in openFileDialog.FileNames)
      {
        byte[] decryptedBuffer = File.ReadAllBytes(fileName);

        byte[] encryptedBuffer = Cryptography.Encrypt(decryptedBuffer, "password");

        File.WriteAllBytes(fileName, encryptedBuffer);
      }

      MessageBox.Show("Files Encrypted", "End");
    }

    private void btnEncryptUsage_Click(object sender, EventArgs e)
    {
      UsageCount uc = new UsageCount
      {
        MachineGUID = "machineGUIDGoesHere",
        UserGUID = "userGUIDGoesHere",
        NoClicks = 23,
        NoScreenSaverSessions = 32,
        TotalPlayTime = 47832
      };

      Serializer.Serialize(uc, @"C:\OxigenData\SettingsData\ss_usg_1.dat", "password");

      MessageBox.Show("UsageCount Serialized", "Message");
    }

    private void TextBox_DragDrop(object sender, DragEventArgs e)
    {
      string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

      if (files.Length > 1)
        textBox1.Text = "Only one file must be dropped";

      DecryptShow(files[0]);
    }

    private void TextBox_DragEnter(object sender, DragEventArgs e)
    {
      if (!e.Data.GetDataPresent(DataFormats.FileDrop, false))
        return;

      e.Effect = DragDropEffects.All;
    }
  }
}
