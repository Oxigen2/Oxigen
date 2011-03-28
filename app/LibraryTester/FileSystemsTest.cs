using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OxigenIIAdvertising.FileRights;

namespace LibraryTester
{
  public partial class FileSystemsTest : Form
  {
    public FileSystemsTest()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      folderBrowserDialog1.ShowDialog();

      if (folderBrowserDialog1.SelectedPath == "")
        return;

      bool b = false;

      try
      {
        b = FileDirectoryRightsChecker.AreFilesReadableWritable(folderBrowserDialog1.SelectedPath);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }

      label1.Text = b.ToString();
    }
  }
}
