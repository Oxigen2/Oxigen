using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OxigenIIAdvertising.EncryptionDecryption;

namespace LibraryTester
{
  public partial class EncryptBatch : Form
  {
    public EncryptBatch()
    {
      InitializeComponent();
    }

    private void EncryptBatch_Load(object sender, EventArgs e)
    {
      openFileDialog1.Multiselect = true;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      openFileDialog1.ShowDialog();

      if (openFileDialog1.FileNames.Length == 0)
        return;

      string[] files = openFileDialog1.FileNames;

      foreach (string file in files)        
        try
        {
          Encrypt(file);
        }
        catch { }

      MessageBox.Show("Files Encrypted");
    }

    private void Encrypt(string file)
    {
      if (file.EndsWith(".db"))
        return;

      byte[] buffer = File.ReadAllBytes(file);

      byte[] encryptedbuffer = Cryptography.Encrypt(buffer, "password");

      File.WriteAllBytes(Path.GetDirectoryName(file) + "\\" + Path.GetFileNameWithoutExtension(file) + "-enc" + Path.GetExtension(file), encryptedbuffer);
    }

    // gets the sub directory the file is in
    private string GetFilesCurrentDirectory(string fullPath)
    {
      string pathWithoutFilename = Path.GetDirectoryName(fullPath);

      int length = pathWithoutFilename.Length;

      int offset = pathWithoutFilename.LastIndexOf("\\") + 1;

      string fileParentDirectory = pathWithoutFilename.Substring(offset, length - offset);

      GC.Collect();

      return fileParentDirectory;
    }
  }
}
