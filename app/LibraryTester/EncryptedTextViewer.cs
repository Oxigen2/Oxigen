using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OxigenIIAdvertising.FileLocker;

namespace LibraryTester
{
  public partial class EncryptedTextViewer : Form
  {
    public EncryptedTextViewer()
    {
      InitializeComponent();
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

    private void TextBox_DragEnter(object sender, DragEventArgs e)
    {
      if (!e.Data.GetDataPresent(DataFormats.FileDrop, false))
        return;

      e.Effect = DragDropEffects.All;
    }

    private void TextBox_DragDrop(object sender, DragEventArgs e)
    {
      string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

      if (files.Length > 1)
        textBox1.Text = "Only one file must be dropped";

      DecryptShow(files[0]);
    }
  }
}
