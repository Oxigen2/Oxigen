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

namespace TextDecrypter
{
  public partial class EncryptedTextViewer : Form
  {
    public EncryptedTextViewer()
    {
      InitializeComponent();
    }

    private string originalText;
    
    private void DecryptShow(string fileName)
    {
      FileStream fs = null;
      string existingLogs = "";

      try
      {
        existingLogs = ReadExistingLogsString(ref fs, fileName, "password");
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
     string path1, string cryptPassword)
    {
      MemoryStream memoryStream = null;

      try
      {
         memoryStream = Locker.ReadDecryptFile(ref fileStream, path1, cryptPassword, false, true); 
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

      originalText = textBox1.Text;
    }

    private void btnEncrypt_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(textBox1.Text.Trim()))
      {
        MessageBox.Show("Please enter the XML to encrypt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }

      Save();
    }

    private void Save()
    {
      FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\EncryptedText.dat", FileMode.Create);

      try
      {
        Locker.WriteEncryptString(ref fs, textBox1.Text, "password");
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }
      finally
      {
        Locker.ClearFileStream(ref fs);
      }

      MessageBox.Show("Encryption done. New file is on desktop.");
    }

    private bool requestClose = true;

    private void Closing(object sender, FormClosingEventArgs e)
    {
      if (requestClose && !string.IsNullOrEmpty(textBox1.Text) && originalText != textBox1.Text)
      {
        DialogResult dr = MessageBox.Show("Save changes?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

        switch (dr)
        {
          case DialogResult.Yes:
            Save();
            requestClose = false;
            Application.Exit();
            return;
          case DialogResult.No:
            requestClose = false;
            Application.Exit();
            return;
          default:
            e.Cancel = true;
            break;
        }         
      }
    }
  }
}
