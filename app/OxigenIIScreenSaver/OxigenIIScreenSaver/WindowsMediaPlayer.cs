using System;
using System.IO;
using AxWMPLib;
using OxigenIIAdvertising.LoggerInfo;

namespace OxigenIIAdvertising.ScreenSaver
{
  public class WindowsMediaPlayer : IPlayer, IFileLoader
  {
      private AxWindowsMediaPlayer _control;
      private bool _muteSound;
      private int _videoVolume;
      private LoggerInfo.Logger _logger;

      public WindowsMediaPlayer(bool muteSound, int videoVolume, Logger logger)
      {
          _control = new AxWindowsMediaPlayer();
          _muteSound = muteSound;
          _logger = logger;
          _videoVolume = videoVolume;
      }

      public void Play(bool primaryMonitor)
      {
          if (!primaryMonitor) {
              _control.settings.mute = true;
          }
          else {
              _control.settings.volume = _videoVolume;
              _control.settings.mute = _muteSound;
          }

          _control.Ctlcontrols.play();

          if (!primaryMonitor) {
              // TODO: set aspect ratio correctly for non primary monitors
              _control.settings.mute = true;
          }
          else {
              _control.stretchToFit = true;
          }
      }

      public void Stop()
      {
          throw new NotImplementedException();
      }

      public System.Windows.Forms.Control Control
      {
          get { throw new NotImplementedException(); }
      }


      public void Load(string filePath)
      {
          _control.URL = filePath;
          _control.Ctlcontrols.stop();
      }

      public void ReleaseAssetForDesktop() {
          string fileToDelete = _control.URL;
          _control.URL = "";
          _logger.WriteTimestampedMessage("successfully unloaded previous windows media");

          if (fileToDelete != "") File.Delete(fileToDelete);
      }

      public void ReleaseAssetForTransition()
      {
          ReleaseAssetForDesktop();
      }

      public bool IsReadyToPlay {
          get { throw new NotImplementedException(); }
      }
  }
}
