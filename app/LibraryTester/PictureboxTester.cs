using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.XMLSerializer;
using System.IO;
using OxigenIIAdvertising.EncryptionDecryption;
using System.Threading;

namespace LibraryTester
{
  public partial class PictureboxTester : Form
  {
    Playlist playList;
    bool bKeepRunning = false;
    string[] images;
    Random rnd; 
    int noImages;
    int randomIndex;

    public PictureboxTester()
    {
      InitializeComponent();
      images = Directory.GetFiles(@"C:\OxigenData\Assets");
      rnd = new Random();
      noImages = images.Length;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      bKeepRunning = true;

      button1.Click += new EventHandler(button1_Click2);
      button1.Click -= new EventHandler(button1_Click);

      Thread flipImageThread = new Thread(new ThreadStart(FlipImage));
      flipImageThread.Start();
    }

    private void FlipImage()
    {
      while (bKeepRunning)
      {
        if (pictureBox1.Image != null)
        {
          Image img = pictureBox1.Image;
          img.Dispose();
          img = null;
          pictureBox1.Image = null;
        }

        randomIndex = rnd.Next(noImages);
                
        try
        {
          pictureBox1.Image = Image.FromStream(DecryptAssetFile(images[randomIndex], "password"));
        }
        catch
        {
        }

        pictureBox1.Refresh();

        Thread.Sleep(200);
      }
    }

    private MemoryStream DecryptAssetFile(string assetPath, string decryptionPassword)
    {
      byte[] encryptedBuffer = File.ReadAllBytes(assetPath);

      byte[] decryptedbuffer = Cryptography.Decrypt(encryptedBuffer, decryptionPassword);

      return new MemoryStream(decryptedbuffer);
    }

    private void button1_Click2(object sender, EventArgs e)
    {
      button1.Click += new EventHandler(button1_Click);
      button1.Click -= new EventHandler(button1_Click2);

      bKeepRunning = false;
    }
  }
}
