using System;
using System.IO;
using AxQTOControlLib;
using OxigenIIAdvertising.LoggerInfo;

namespace OxigenIIAdvertising.ScreenSaver.Players
{
  public class QuicktimePlayer : IPlayer, IFileLoader
  {
      private AxQTControl _control;
      private Logger _logger;
      private float _videoVolume;
      private bool _bMuteVideo;

      public QuicktimePlayer(bool bMuteVideo, float videoVolume, Logger logger)
      {
          _control = new AxQTControl();
          _videoVolume = videoVolume;
          _bMuteVideo = bMuteVideo;
          _logger = logger;
      }

      public IPlayer DeepCopy()
      {
          return new QuicktimePlayer(_bMuteVideo, _videoVolume, _logger);
      }

      public void Play(bool primaryMonitor)
      {
          if (!primaryMonitor) {
              _control.Movie.AudioMute = true;
              _logger.WriteTimestampedMessage("successfully muted quicktime.");
          }
          else {
              _control.Movie.AudioVolume = (float)_videoVolume / 100F;

              _logger.WriteTimestampedMessage("successfully set the the quicktime volume.");

              _control.Movie.AudioMute = _bMuteVideo;

              _logger.WriteTimestampedMessage("successfully set mute/no mute of quicktime sound.");
          }

          _logger.WriteTimestampedMessage("Starting to play Quicktime");

          _control.Movie.Play(1);

          _logger.WriteTimestampedMessage("Started Quicktime");
      }

      public void Stop()
      {
          if (_control.Movie != null)
              _control.Movie.Stop();
      }

      public System.Windows.Forms.Control Control
      {
          get { return _control; }
      }


      public void Load(string filePath)
      {
          _control.URL = filePath;
      }


      public void ReleaseAssetForDesktop() {
          string fileToDelete = _control.URL;
          _control.URL = "";
          _logger.WriteTimestampedMessage("successfully unloaded quicktime");

          if (fileToDelete != "") File.Delete(fileToDelete);
      }

      public void ReleaseAssetForTransition()
      {
          ReleaseAssetForDesktop();
      }

      public bool IsReadyToPlay {
          get { throw new NotImplementedException(); }
      }

      public void Init()
      {
          throw new NotImplementedException();
      }

      public virtual void Dispose() {
          _control.Dispose();
      }

      public void CompleteSetup()
      {
          _control.Sizing = QTOControlLib.QTSizingModeEnum.qtMovieFitsControlMaintainAspectRatio;
          _control.MovieControllerVisible = false;
      }
  }
}
