using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LibraryTester
{
  public partial class ChecksumCalculator : Form
  {
    public ChecksumCalculator()
    {
      InitializeComponent();
    }

    private void btnChecksumWrite_Click(object sender, EventArgs e)
    {
      folderBrowserDialog.ShowDialog();

      string selectedPath = folderBrowserDialog.SelectedPath;

      if (selectedPath == "")
        return;

      string[] files = Directory.GetFiles(selectedPath, "*", SearchOption.AllDirectories);

      foreach (string file in files)
      {
        string checksum = OxigenIIAdvertising.FileChecksumCalculator.ChecksumCalculator.GetChecksum(file);

        File.WriteAllText(Path.GetDirectoryName(file) + "\\" + Path.GetFileNameWithoutExtension(file) + ".chk", checksum);
      }
    }
  }
}
