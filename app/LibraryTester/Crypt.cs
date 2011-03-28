using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OxigenIIAdvertising.FileLocker;
using OxigenIIAdvertising.LogWriter;
using System.IO;
using OxigenIIAdvertising.LoggerInfo;

namespace LibraryTester
{
  public partial class Crypt : Form
  {   
    public Crypt()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      HashSet<string> logs = new HashSet<string>();

      logs.Add("1000|20091116|15:00:01");
      logs.Add("1001|20091117|18:11:12");
      logs.Add("1002|20091118|12:54:32");

      WriteRawLogs(@"C:\OxigenData\SettingsData\ss_ad_c_1.dat",
        @"C:\OxigenData\SettingsData\ss_ad_c_2.dat",
        "password",
        logs);
    }

    private void WriteRawLogs(string path1, string path2, string cryptPassword, HashSet<string> rawLogEntries)
    {
      FileStream fileStream = null;

      // read existing logs and initialize string builder with them
      StringBuilder sb = new StringBuilder(ReadExistingLogs(ref fileStream, path1, path2, cryptPassword));

      foreach (string rawLogEntry in rawLogEntries)
      {
        sb.Append(Environment.NewLine);
        sb.Append(rawLogEntry);
      }

      Locker.WriteEncryptString(ref fileStream, sb.ToString(), cryptPassword);
    }

    private void btnDecrypt_Click(object sender, EventArgs e)
    {
      FileStream fileStream = null;

      string existingLogs = ReadExistingLogs(ref fileStream, @"C:\OxigenData\SettingsData\ss_ad_c_1.dat",
        @"C:\OxigenData\SettingsData\ss_ad_c_2.dat", "password");
      Locker.ClearFileStream(ref fileStream);

      MessageBox.Show(existingLogs);
    }

    /// <summary>
    /// Opens and locks a log file and returns a string with the decrypted logs if they exist.
    /// If they dont exist, an empty string is returned.
    /// </summary>
    /// <param name="fileStream">FileStream to read and lock a log file</param>
    /// <param name="path1">Path of first file to try</param>
    /// <param name="path2">Path of second file to try if first file is locked</param>
    /// <param name="cryptPassword">password to use for encryption/decryption</param>
    /// <returns>a string with the decrypted logs or an empty string if logs don't exist</returns>
    private string ReadExistingLogs(ref FileStream fileStream,
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

      string existingRawLogs = "";

      // if memory stream is not null (i.e. log files already exist)
      if (memoryStream != null)
      {
        StreamReader sr = new StreamReader(memoryStream);

        try
        {
          existingRawLogs = sr.ReadToEnd();
        }
        catch (Exception ex)
        {
          Locker.ClearFileStream(ref fileStream);

          throw ex;
        }
        finally
        {
          sr.Close();
          sr.Dispose();
          memoryStream.Close();
          memoryStream.Dispose();
        }
      }

      return existingRawLogs;
    }
  }
}
