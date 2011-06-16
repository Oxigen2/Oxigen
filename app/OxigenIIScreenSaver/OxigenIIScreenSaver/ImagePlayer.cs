using System.IO;
using System.Windows.Forms;
using OxigenIIAdvertising.AppData;

namespace OxigenIIAdvertising.ScreenSaver
{
  public class ImagePlayer : IPlayer
  {
      private Control _control;

      public ImagePlayer()
      {
          _control = new PictureBox();
      }

      public void EnableSound(bool enableSound)
      {
          // does not apply to images
      }

      public void Play()
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


      public void Load(ChannelAssetAssociation channelAssetAssociation)
      {
          //using (MemoryStream imageStream = channelAssetAssociation.PlaylistAsset.DecryptAssetFile())
          // if (_control is PictureBox)
          //    ((PictureBox)_control).Image = 
      }
  }
}
