using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OxigenIIAdvertising.XMLSerializer;
using OxigenIIAdvertising.EncryptionDecryption;

namespace LibraryTester
{
  public partial class Starter : Form
  {
    public Starter()
    {
      InitializeComponent();
    }

    private void btnGUIDCreator_Click(object sender, EventArgs e)
    {
      GUIDCreator gc = new GUIDCreator();
      gc.ShowDialog();
    }

    private void btnPlaylistTester_Click(object sender, EventArgs e)
    {
      PlayListTester pt = new PlayListTester();
      pt.ShowDialog();
    }

    private void btnSerializerDeserializer_Click(object sender, EventArgs e)
    {
      SerializerDeserializer sd = new SerializerDeserializer();
      sd.ShowDialog();
    }

    private void btnServerConnectCycle_Click(object sender, EventArgs e)
    {
      ServerCycleConnect scc = new ServerCycleConnect();
      scc.ShowDialog();
    }

    private void btnLinqAggregator_Click(object sender, EventArgs e)
    {
      LINQAggregator la = new LINQAggregator();
      la.ShowDialog();
    }

    private void btnComponentVersion_Click(object sender, EventArgs e)
    {
      ComponentVersionForm cv = new ComponentVersionForm();
      cv.ShowDialog();
    }

    private void btnEncryptSampleLogs_Click(object sender, EventArgs e)
    {
      EncryptFile(@"C:\OxigenData\Logs\ss_ad_c_1.dat");
      EncryptFile(@"C:\OxigenData\Logs\ss_ad_c_2.dat");
      EncryptFile(@"C:\OxigenData\Logs\ss_co_c_1.dat");
      EncryptFile(@"C:\OxigenData\Logs\ss_co_c_2.dat");
    }

    private void EncryptFile(string filepath)
    {
      byte[] uncencrypted = File.ReadAllBytes(filepath);

      byte[] encrypted = Cryptography.Encrypt(uncencrypted, "password");

      File.WriteAllBytes(filepath, encrypted);
    }

    private void btnFilePermitChecker_Click(object sender, EventArgs e)
    {
      FilePermitChecker fpc = new FilePermitChecker();
      fpc.ShowDialog();
    }

    private void btnEncryptBatch_Click(object sender, EventArgs e)
    {
      EncryptBatch eb = new EncryptBatch();
      eb.ShowDialog();
    }

    private void pictureBoxButton_Click(object sender, EventArgs e)
    {
      PictureboxTester pbt = new PictureboxTester();
      pbt.ShowDialog();
    }

    private void btnCrypt_Click(object sender, EventArgs e)
    {
      Crypt c = new Crypt();
      c.ShowDialog();
    }

    private void btnAppender_Click(object sender, EventArgs e)
    {
      Appender appender = new Appender();
      appender.ShowDialog();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      FileSystemsTest fst = new FileSystemsTest();
      fst.ShowDialog();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      EncryptedTextViewer ev = new EncryptedTextViewer();
      ev.ShowDialog();
    }

    private void tryWebService_Click(object sender, EventArgs e)
    {
      TryWebService t = new TryWebService();
      t.ShowDialog();
    }

    private void button3_Click(object sender, EventArgs e)
    {
      ChecksumCalculator cc = new ChecksumCalculator();
      cc.ShowDialog();
    }
  }
}
