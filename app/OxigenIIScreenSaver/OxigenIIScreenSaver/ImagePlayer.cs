using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OxigenIIAdvertising.AppData;

namespace OxigenIIAdvertising.ScreenSaver
{
  public class ImagePlayer : IPlayer, IStreamLoader
  {
      private PictureBox _control;

      public ImagePlayer()
      {
          _control = new PictureBox();
          _control.SizeMode = PictureBoxSizeMode.Zoom;
      }

      public void Play(bool primaryMonitor)
      {
          // does not apply to images
      }

      public void Stop()
      {
          // does not apply to images
      }



      public Control Control
      {
          get { return _control; }
      }


      public void ReleaseAssetForTransition()
      {
          // promptly dispose of existing image
          if (_control.Image != null) {
              Image disposableImage = _control.Image;
              disposableImage.Dispose();
              disposableImage = null;
              _control.Image = null;
          }
      }

      public bool IsReadyToPlay
      {
          get { throw new NotImplementedException(); }
      }


      public void ReleaseAssetForDesktop() {
          // not applicable
      }


      public void Load(Stream stream)
      {
        // Stream must be kept open for the lifetime of the image
        // use a temp image, then clone it, to make independent of the stream, then close the stream
        Image tempImage = Image.FromStream(stream);

        ((PictureBox)_control).Image = new Bitmap(tempImage);
        tempImage.Dispose();
      }
  }
}
