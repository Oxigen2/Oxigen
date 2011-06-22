using System;
using System.IO;
using System.Runtime.InteropServices;
using AxShockwaveFlashObjects;
using OxigenIIAdvertising.LoggerInfo;


namespace OxigenIIAdvertising.ScreenSaver.Players
{
  public class FlashPlayer : IPlayer, IFileLoader
  {
      private bool _muteSound;
      private uint _uintCurrentVol = 4294967295;
      private AxShockwaveFlash _control;
      private LoggerInfo.Logger _logger;

      [DllImport("winmm.dll")]
      private static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

      [DllImport("winmm.dll")]
      private static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

      public FlashPlayer(bool muteSound, Logger logger)
      {
          _muteSound = muteSound;
          _logger = logger;
          _control = new AxShockwaveFlash();
      }

      public IPlayer DeepCopy()
      {
          return new FlashPlayer(_muteSound, _logger);
      }

      public void Play(bool primaryMonitor)
      {
          if (!primaryMonitor) {
              // Set the same volume for both the left and the right channels
              uint NewVolumeAllChannels = (((uint)0 & 0x0000ffff) | ((uint)0 << 16));

              // Set the volume
              waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
          }
          else {
              if (_muteSound) {
                  // Set the same volume for both the left and the right channels
                  uint NewVolumeAllChannels = (((uint)0 & 0x0000ffff) | ((uint)0 << 16));

                  // Set the volume
                  waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
              }
              else {
                  // Set the same volume for both the left and the right channels
                  uint NewVolumeAllChannels = (((uint)_uintCurrentVol & 0x0000ffff) | ((uint)_uintCurrentVol << 16));

             // Set the volume
                  waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
              }
          }

          _control.Play();
      }

      public void Stop()
      {
          _control.Stop();
      }

      public void ReleaseAssetForDesktop()
      {
          string fileToDelete = _control.Movie;
          _control.Movie = "";
          _logger.WriteTimestampedMessage("successfully unloaded previous flash");
          if (fileToDelete != "") File.Delete(fileToDelete);
      }

      public void ReleaseAssetForTransition()
      {
          ReleaseAssetForDesktop();
      }

      public bool IsReadyToPlay
      {
          get { throw new NotImplementedException(); }
      }

      public System.Windows.Forms.Control Control
      {
          get { return _control; }
      }

      public void Load(string filepath)
      {
          _control.Movie = filepath;
          _control.Stop();
          _control.Rewind();
      }

      public void Init()
      {
          throw new NotImplementedException();
      }

      public void Dispose() {
          _control.Dispose();
      }


      public void CompleteSetup()
      {
          // does not apply
      }
  }
}
