using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.PlaylistLogic;
using OxigenIIAdvertising.AssetScheduling;
using OxigenIIAdvertising.Singletons;
using OxigenIIAdvertising.ScreenSaver.Properties;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using OxigenIIAdvertising.NoAssetsAnimator;
using OxigenIIAdvertising.UserSettings;
using AxWMPLib;
using AxShockwaveFlashObjects;
using AxQTOControlLib;
using OxigenIIAdvertising.OxigenIIStopwatch;
using OxigenIIAdvertising.LoggerInfo;
using System.Globalization;

namespace OxigenIIAdvertising.ScreenSaver
{
  public partial class ScreenSaver : Form, IScreenSaver
  {
    // power event messages
    const int WM_POWERBROADCAST = 0x0218;

    const int PBT_APMSUSPEND = 0x0004;
    const int PBT_APMRESUMESUSPEND = 0x0007;
    const int PBT_APMSTANDBY = 0x0005;
    const int PBT_APMRESUMESTANDBY = 0x0008;

    [DllImport("winmm.dll")]
    public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

    [DllImport("winmm.dll")]
    public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

    public uint uintCurrentVol = 0;

    //start off originalLocation with an X and Y of int.MaxValue, because
    //it is impossible for the cursor to be at that position. That way, we
    //know if this variable has been set yet.
    private Point _originalLocation = new Point(int.MaxValue, int.MaxValue);

    private int _screenNo;
    private bool _bPrimaryMonitor = false;

    private LoggerInfo.Logger _logger = null;

    // used by UI thread and display thread
    private Stopwatch _stopwatch = null;
    private LogSingletonAccessor _logSingletonAccessor = null;
    private volatile ChannelAssetAssociation _currentChannelAssetAssociationOnDisplay = null;
    private int _flashVolume = 1;
    private int _videoVolume = 50;
    private bool _bMuteFlash = false;
    private bool _bMuteVideo = false;
    private const int _fadeOutToBlack = 200;
    private const int _fadeInToSlide = 200;
    private const int _fadeInToDesktop = 150;
    private const int _fadeOutToDesktop = 50;
    private const int _fadeSleep = 100;
    private string _tempDecryptPath = null;
    private string _assetPath = null;

    private volatile bool _bFadeToDesktop = false;
    private volatile bool _bAllFormsShowing = true;
    private volatile bool _bFirstRun = true;

    // used by worker thread
    private AssetScheduler _assetScheduler = null;
    private PlaylistAssetPicker _playlistAssetPicker = null;
    private List<string> _assetDeleteList = null;
    private float _protectedContentTime = -1F;
    private float _advertDisplayThreshold = -1F;
    private float _displayMessageAssetDisplayLength = -1F;
    private int _requestTimeout = -1;
    private bool _bErrorOnSetup = false;
    private string _appToRun = null;
    private string _displayMessage = null;

    // used by both
    private volatile DisplayToggle _displayToggle;
    private volatile bool _bRunLoadThread = true;
    private WebBrowser _webBrowserA = null;
    private WebBrowser _webBrowserB = null;
    private PictureBox _pictureBoxA = null;
    private PictureBox _pictureBoxB = null;
    private ChannelAssetAssociation _ChannelAssetAssociationA = null;
    private ChannelAssetAssociation _ChannelAssetAssociationB = null;
    private NoAssetsAnimatorPlayer _noAssetsAnimatorPlayer = null;
    private AxShockwaveFlash _flashPlayerA;
    private AxShockwaveFlash _flashPlayerB;
    private AxWindowsMediaPlayer _videoPlayerA;
    private AxWindowsMediaPlayer _videoPlayerB;
    private AxQTControl _quickTimePlayerA;
    private AxQTControl _quickTimePlayerB;
    private float _totalDisplayTime = 0;
    private float _totalAdvertDisplayTime = 0;
    private float _assetDisplayLength = 0;
    private bool _bLogImpressions = true;
    private FaderForm _faderForm = null;
    private DDFormFader _ddFormFader = null;
    private bool _bInsufficientMemoryForLargeFiles = false;
    private float _defaultDisplayLength = -1F;

    private volatile object _lockPlaylistObj = null;
    private volatile object _lockingObject = new object();
    
    private Thread _displayAssetThread = null;
    private Thread _loadAssetThread = null;

    /// <summary>
    /// Sets the playlist object from which the Screensaver plays its slides.
    /// Places a lock on _lockingObject before Setting the playlist to make thread safe.
    /// </summary>
    public void SetPlaylistAssetPickerPlaylist(Playlist playlist)
    {
      if (_playlistAssetPicker != null)
      {
        lock (_lockPlaylistObj)
          _playlistAssetPicker.Playlist = playlist;
      }
    }

    /// <summary>
    /// Default constructor that runs the screensaver in normal mode
    /// </summary>
    /// <param name="screenNo">index number of screen to run screensaver on</param>
    /// <param name="playlist">The playlist from which to draw assets</param>
    /// <param name="advertDisplayThreshold">threshold to determine whether to display an asset or an advert as the next asset</param>
    /// <param name="bErrorOnSetup">When error has occurred during setting up, screen saver will not run in normal mode</param>
    public ScreenSaver(int screenNo, Playlist playlist, float advertDisplayThreshold,
      float protectedContentTime, float displayMessageAssetDisplayLength, int requestTimeout, 
      bool bMuteFlash, bool bMuteVideo, int flashVolume, int videoVolume,
      bool bErrorOnSetup, string displayMessage, string appToRun,
      string tempDecryptPath, string assetPath, bool bInsufficientMemoryForLargeFiles,
      object lockPlaylistObj, float defaultDisplayLength, Logger logger)
    {
      _logger = logger;

      InitializeComponent();

      _logger.WriteTimestampedMessage("successfully InitializeComponent.");

      Cursor.Hide();

      _logger.WriteTimestampedMessage("successfully hidden the cursor.");

      this.ShowInTaskbar = false;

      _logger.WriteTimestampedMessage("successfully removed from taskbar.");

      _screenNo = screenNo;

      _logSingletonAccessor = new LogSingletonAccessor();

      _logger.WriteTimestampedMessage("successfully created a LogSingletonAccessor object.");

      _lockPlaylistObj = lockPlaylistObj;
      _advertDisplayThreshold = advertDisplayThreshold;
      _protectedContentTime = protectedContentTime;
      _displayMessageAssetDisplayLength = displayMessageAssetDisplayLength == -1F ? 15F : displayMessageAssetDisplayLength;
      _requestTimeout = requestTimeout;
      _bErrorOnSetup = bErrorOnSetup;
      _bInsufficientMemoryForLargeFiles = bInsufficientMemoryForLargeFiles;
      _displayMessage = displayMessage;
      _appToRun = appToRun;
      _tempDecryptPath = tempDecryptPath;
      _assetPath = assetPath;
      _defaultDisplayLength = defaultDisplayLength;
      _assetDeleteList = new List<string>();

      _bPrimaryMonitor = IsPrimaryMonitor();

      if (!_bErrorOnSetup)
      {
        _bMuteFlash = bMuteFlash;
        _bMuteVideo = bMuteVideo;

        _flashVolume = flashVolume;
        _videoVolume = videoVolume;
      }

      if (_bErrorOnSetup)
        _assetPath = "";

      _logger.WriteTimestampedMessage("successfully allocated global variables.");

      _assetScheduler = new AssetScheduler();

      _logger.WriteTimestampedMessage("successfully created an AssetScheduler object.");

      lock (_lockPlaylistObj)
        _playlistAssetPicker = new PlaylistAssetPicker(playlist, _assetScheduler, _displayMessageAssetDisplayLength, assetPath, "password",
        _requestTimeout, _bErrorOnSetup, _screenNo, _logger, _bInsufficientMemoryForLargeFiles);

      _logger.WriteTimestampedMessage("successfully created a PlaylistAssetPicker object.");

      _stopwatch = new Stopwatch();

      _logger.WriteTimestampedMessage("successfully created a Stopwatch object.");

      _logger.WriteTimestampedMessage("successfully created one noassetsanimator player object.");

      _pictureBoxA.SizeMode = PictureBoxSizeMode.Zoom;
      _pictureBoxB.SizeMode = PictureBoxSizeMode.Zoom;

      _logger.WriteTimestampedMessage("successfully set PictureBox.Sizemode.");

      // suppress page script errors
      _webBrowserA.ScriptErrorsSuppressed = true;
      _webBrowserB.ScriptErrorsSuppressed = true;

      _logger.WriteTimestampedMessage("successfully suppressed web browser errors.");

      Controls.Add(_noAssetsAnimatorPlayer);
      Controls.Add(_pictureBoxA);
      Controls.Add(_pictureBoxB);
      Controls.Add(_webBrowserA);
      Controls.Add(_webBrowserB);
      Controls.Add(_flashPlayerA);
      Controls.Add(_flashPlayerB);
      Controls.Add(_videoPlayerA);
      Controls.Add(_videoPlayerB);
      Controls.Add(_quickTimePlayerA);
      Controls.Add(_quickTimePlayerB);

      _logger.WriteTimestampedMessage("successfully added all the controls to Screensaver Form.");

      Control.CheckForIllegalCrossThreadCalls = false;

      // set z-index of all containers/players to the same value
      Controls.SetChildIndex(_pictureBoxA, 2);
      Controls.SetChildIndex(_pictureBoxB, 3);
      Controls.SetChildIndex(_webBrowserA, 4);
      Controls.SetChildIndex(_webBrowserB, 5);
      Controls.SetChildIndex(_flashPlayerA, 6);
      Controls.SetChildIndex(_flashPlayerB, 7);
      Controls.SetChildIndex(_videoPlayerA, 8);
      Controls.SetChildIndex(_videoPlayerB, 9);
      Controls.SetChildIndex(_quickTimePlayerA, 10);
      Controls.SetChildIndex(_quickTimePlayerB, 11);
      Controls.SetChildIndex(_noAssetsAnimatorPlayer, 12);

      _logger.WriteTimestampedMessage("successfully set the z-indices of the players.");

      _faderForm = new FaderForm();

      _logger.WriteTimestampedMessage("successfully created a fader form.");

      _ddFormFader = new DDFormFader(_faderForm.Handle);

      _logger.WriteTimestampedMessage("successfully created the object to fade the fader form.");

      _faderForm.StartPosition = FormStartPosition.Manual;

      _logger.WriteTimestampedMessage("successfully set the start position of the fader form to manual.");

      _logger.WriteTimestampedMessage("successfully hid the fader form from taskbar.");

      AddOwnedForm(_faderForm);

      _logger.WriteTimestampedMessage("successfully added the fader form as a form owned by the Screensaver Form.");

      _displayToggle = DisplayToggle.A;

      _logger.WriteTimestampedMessage("successfully set the DisplayToggle to A.");

      // At this point, CurrVol gets assigned the volume
     // waveOutGetVolume(IntPtr.Zero, out uintCurrentVol);
      uintCurrentVol = 4294967295;

      _logger.WriteMessage("uintCurrentVol is " + uintCurrentVol);
    }

    private void DisplayAsset()
    {
      while (true)
      {
        lock (_lockingObject)
        {
          if (_stopwatch.ElapsedTotalMilliseconds > ((_assetDisplayLength * 1000) - 200) || _bFadeToDesktop)
          {
            if (_displayToggle == DisplayToggle.B && _ChannelAssetAssociationA != null)
              _logger.WriteTimestampedMessage("Time to change " + _ChannelAssetAssociationA.PlaylistAsset.AssetFilename + " (Asset A)");

            if (_displayToggle == DisplayToggle.A && _ChannelAssetAssociationB != null)
              _logger.WriteTimestampedMessage("Time to change " + _ChannelAssetAssociationB.PlaylistAsset.AssetFilename + " (Asset B)");

            // is this the first run of the pulse? If yes, there is nothing to log
            if (_logSingletonAccessor.AssetImpressionStartDateTime.Year != 1)
            {
              // Add logs of the asset previously impressed (i.e. if a is to be impressed next,
              // log previously impressed asset B, else log previously impressed A)
              ChannelAssetAssociation channelAssetAssociation = _displayToggle == DisplayToggle.A ? _ChannelAssetAssociationB : _ChannelAssetAssociationA;

              AddImpressionLog(channelAssetAssociation);

              _logger.WriteTimestampedMessage("successfully added impression log for " + channelAssetAssociation.PlaylistAsset.AssetID + " in channel: " + channelAssetAssociation.ChannelID);
            }

            FlipAssetPlayer();

            _logger.WriteTimestampedMessage("successfully flipped asset player.");

            if (!_bFadeToDesktop)
            {
              _displayToggle = _displayToggle == DisplayToggle.A ? DisplayToggle.B : DisplayToggle.A;

              _logger.WriteTimestampedMessage("successfully flipped the display toggle to " + _displayToggle);

            }
            else
            {
              _logger.WriteTimestampedMessage("final run of the display asset thread.");
              return;
            }
          }
        }

        Thread.Sleep(100);
      }
    }

    private void AddPlayTimes(PlaylistAsset playlistAsset)
    {
      _totalDisplayTime += playlistAsset.DisplayLength;

      if (playlistAsset is AdvertPlaylistAsset)
        _totalAdvertDisplayTime += playlistAsset.DisplayLength;
    }

    private void AddImpressionLog(ChannelAssetAssociation channelAssetAssociation)
    {
      // reminder: LogEntriesRawSingleton is thread safe. No need to lock again.      
      if (_bLogImpressions || _bErrorOnSetup)
        _logSingletonAccessor.AddImpressionLog(channelAssetAssociation);
    }

    /// <summary>
    /// Gets the click through URL of the asset currently displayed
    /// </summary>
    /// <returns>a string with the URL to launch</returns>
    internal string GetClickThroughURL()
    {
      return _currentChannelAssetAssociationOnDisplay.PlaylistAsset.ClickDestination;
    }

    /// <summary>
    /// Adds a click log of the asset currently displayed
    /// </summary>
    internal void AddClickLog()
    {
      _logSingletonAccessor.AddClickLog(_currentChannelAssetAssociationOnDisplay);
    }

    /// <summary>
    /// Stops load/display/log work and adds an impression log for the last asset impressed,
    /// since it won't have run its full circle.
    /// </summary>
    internal void StopWork()
    {
      lock (_lockingObject)
        _bRunLoadThread = false;

      AddImpressionLog(_currentChannelAssetAssociationOnDisplay);
    }

    private string DecryptToTemp(ChannelAssetAssociation ca, string displayToggleString)
    {
      string assetTempPath = _tempDecryptPath + Path.GetFileNameWithoutExtension(ca.PlaylistAsset.AssetFilename) + displayToggleString + _screenNo + Path.GetExtension(ca.PlaylistAsset.AssetFilename);

      MemoryStream ms = ca.PlaylistAsset.DecryptAssetFile(_assetPath + ca.PlaylistAsset.GetAssetFilenameGUIDSuffix() + "\\" + ca.PlaylistAsset.AssetFilename, "password");

      FileStream fs = new FileStream(assetTempPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);

      ms.WriteTo(fs);

      ms.Dispose();
      fs.Dispose();

      return assetTempPath;
    }

    private void LoadWebsite(ChannelAssetAssociation ca, WebBrowser webBrowser)
    {
      webBrowser.Navigate(ca.PlaylistAsset.AssetWebSite);

      _logger.WriteTimestampedMessage("successfully told web browser to load asset.");

      while (webBrowser.ReadyState != WebBrowserReadyState.Complete) ;

      _logger.WriteTimestampedMessage("successfully loaded website.");
    }

    private void LoadFlash(ChannelAssetAssociation ca, AxShockwaveFlash flashPlayer, string displayToggleString)
    {
      flashPlayer.Movie = DecryptToTemp(ca, displayToggleString);

      _logger.WriteTimestampedMessage("successfully loaded flash video.");

      flashPlayer.Stop();
      flashPlayer.Rewind();

      _logger.WriteTimestampedMessage("successfully stopped and rewound flash video.");
    }

    private void LoadVideoNonQT(ChannelAssetAssociation ca, AxWindowsMediaPlayer videoPlayer, string displayToggleString)
    {
      videoPlayer.URL = DecryptToTemp(ca, displayToggleString);

      _logger.WriteTimestampedMessage("successfully loaded media player video.");

      videoPlayer.Ctlcontrols.stop();

      _logger.WriteTimestampedMessage("successfully stopped media player video.");
    }

    private void LoadVideoQT(ChannelAssetAssociation ca, AxQTControl quickTimePlayer, string displayToggleString)
    {
      quickTimePlayer.URL = DecryptToTemp(ca, displayToggleString);

      _logger.WriteTimestampedMessage("successfully loaded quicktime video.");
    }

    private void LoadNoAssetsAsset(ChannelAssetAssociation channelAssetAssociation, NoAssetsAnimatorPlayer noAssetsAnimatorPlayer)
    {
      noAssetsAnimatorPlayer.Message = ((ContentPlaylistAsset)channelAssetAssociation.PlaylistAsset).Message;

      _logger.WriteTimestampedMessage("successfully set the \"No Assets\" message.");
    }

    private void ClearImage(PictureBox pictureBox)
    {
      // promptly dispose of existing image
      if (pictureBox.Image != null)
      {
        Image disposableImage = pictureBox.Image;
        disposableImage.Dispose();
        disposableImage = null;
        pictureBox.Image = null;
      }
    }

    private void LoadImage(ChannelAssetAssociation ca, PictureBox pictureBox)
    {
      ClearImage(pictureBox);

      _logger.WriteTimestampedMessage("successfully cleared old image from picturebox.");

      MemoryStream memoryStream = ca.PlaylistAsset.DecryptAssetFile(_assetPath + ca.PlaylistAsset.GetAssetFilenameGUIDSuffix() + "\\" + ca.PlaylistAsset.AssetFilename, "password");

      _logger.WriteTimestampedMessage("successfully decrypted image to memory.");

      // Stream must be kept open for the lifetime of the image
      // use a temp image, then clone it, to make independent of the stream, then close the stream
      Image tempImage = Image.FromStream(memoryStream);

      _logger.WriteTimestampedMessage("successfully created temp image from memory data.");

      pictureBox.Image = new Bitmap(tempImage);
      _logger.WriteTimestampedMessage("successfully created an image from temp image and resized it to the screen.");
      
      tempImage.Dispose();
      tempImage = null;

      _logger.WriteTimestampedMessage("successfully disposed of temp image.");

      memoryStream.Close();
      memoryStream.Dispose();
      memoryStream = null;

      _logger.WriteTimestampedMessage("successfully disposed of temp decrypted data.");
    }

    // Selects the next asset from the playlist, if temporal scheduling allows it and current date is within specified date bounds.
    // Decides if the next asset must be an advert or a content.
    // If the next asset is an advert and there is no advert available at this time, it attempts to select a content.
    // If no asset was found, a "no assets" asset is created.
    private ChannelAssetAssociation SelectAsset()
    {
      if (!_bErrorOnSetup)
      {
        lock (_lockPlaylistObj)
        {
          ChannelAssetAssociation caa = _playlistAssetPicker.SelectAsset(_totalDisplayTime, _totalAdvertDisplayTime, _protectedContentTime, _advertDisplayThreshold, Resources.NoAssets, "app://ContentExchanger");

          if (caa.PlaylistAsset.DisplayLength == -1F)
            caa.PlaylistAsset.DisplayLength = _defaultDisplayLength;

          return caa;
        }
      }

      lock (_lockPlaylistObj)
        return _playlistAssetPicker.SelectAsset(_totalDisplayTime, _totalAdvertDisplayTime, _protectedContentTime, _advertDisplayThreshold, _displayMessage, _appToRun);
    }

    private void FlipAssetPlayer()
    {
      // keep start date and time of the display for logging
      _logSingletonAccessor.AssetImpressionStartDateTime = DateTime.Now;

      if (_displayToggle == DisplayToggle.A)
      {
        _assetDisplayLength = _ChannelAssetAssociationA.PlaylistAsset.DisplayLength;

        Transit(_ChannelAssetAssociationA, _ChannelAssetAssociationB, _pictureBoxA, _pictureBoxB, _webBrowserA, _webBrowserB, _flashPlayerA, _flashPlayerB, _videoPlayerA, _videoPlayerB, _quickTimePlayerA, _quickTimePlayerB);
      }
      else
      {
        _assetDisplayLength = _ChannelAssetAssociationB.PlaylistAsset.DisplayLength;

        Transit(_ChannelAssetAssociationB, _ChannelAssetAssociationA, _pictureBoxB, _pictureBoxA, _webBrowserB, _webBrowserA, _flashPlayerB, _flashPlayerA, _videoPlayerB, _videoPlayerA, _quickTimePlayerB, _quickTimePlayerA);
      }
    }

    private void Transit(ChannelAssetAssociation channelAssetAssociationAssetToShow,
      ChannelAssetAssociation channelAssetAssociationAssetToHide,
      PictureBox pictureBoxToShow, PictureBox pictureBoxToHide,
      WebBrowser webBrowserToShow, WebBrowser webBrowserToHide,
      AxShockwaveFlash flashPlayerToShow, AxShockwaveFlash flashPlayerToHide,
      AxWindowsMediaPlayer videoPlayerToShow, AxWindowsMediaPlayer videoPlayerToHide,
      AxQTControl quickTimePlayerToShow, AxQTControl quickTimePlayerToHide)
    {

      // transition to black between assets
      if (!_bFirstRun)
      {
        _faderForm.Show();

        for (int i = 51; i <= 255; i += 51)
        {
          DateTime startFadeTimeStamp = DateTime.Now;

          _ddFormFader.updateOpacity((byte)i, false);

          while ((DateTime.Now - startFadeTimeStamp).Milliseconds < 40) ;
        }

        _logger.WriteTimestampedMessage("successfully faded the form from asset to black.");
      }
      else
      {
        _bFirstRun = false;
        _logger.WriteTimestampedMessage("first run of the flipping function.");
      }      

      // stop previous players
      flashPlayerToHide.Stop();
      videoPlayerToHide.Ctlcontrols.stop();
      if (quickTimePlayerToHide.Movie != null)
        quickTimePlayerToHide.Movie.Stop();

      if (!_bFadeToDesktop)
      {
        switch (channelAssetAssociationAssetToShow.PlaylistAsset.PlayerType)
        {
          case PlayerType.Image:
            Controls.SetChildIndex(pictureBoxToShow, 0);
            _logger.WriteTimestampedMessage("successfully flipped to image.");
            break;
          case PlayerType.WebSite:
            Controls.SetChildIndex(webBrowserToShow, 0);
            _logger.WriteTimestampedMessage("successfully flipped to website.");
            break;
          case PlayerType.NoAssetsAnimator:
            Controls.SetChildIndex(_noAssetsAnimatorPlayer, 0);
            _logger.WriteTimestampedMessage("successfully flipped to no assets animator.");
            _noAssetsAnimatorPlayer.Play();
            _logger.WriteTimestampedMessage("successfully played no assets animator.");
            break;
          case PlayerType.Flash:

            if (!_bPrimaryMonitor)
            {
              // Set the same volume for both the left and the right channels
              uint NewVolumeAllChannels = (((uint)0 & 0x0000ffff) | ((uint)0 << 16));

              // Set the volume
              waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
            }
            else
            {
              if (_bMuteFlash)
              {
                _logger.WriteMessage("flash is set to mute");

                // Set the same volume for both the left and the right channels
                uint NewVolumeAllChannels = (((uint)0 & 0x0000ffff) | ((uint)0 << 16));

                _logger.WriteMessage("NewVolumeAllChannels = " + NewVolumeAllChannels);

                // Set the volume
                waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
              }
              else
              {
                _logger.WriteMessage("flash is not set to mute");

                // Set the same volume for both the left and the right channels
                uint NewVolumeAllChannels = (((uint)uintCurrentVol & 0x0000ffff) | ((uint)uintCurrentVol << 16));

                _logger.WriteMessage("NewVolumeAllChannels = " + NewVolumeAllChannels);

                // Set the volume
                waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
              }
            }

            Controls.SetChildIndex(flashPlayerToShow, 0);
            _logger.WriteTimestampedMessage("successfully flipped to flash.");
            flashPlayerToShow.Play();
            _logger.WriteTimestampedMessage("successfully played flash.");
            break;
          case PlayerType.VideoNonQT:
            Controls.SetChildIndex(videoPlayerToShow, 0);
            _logger.WriteTimestampedMessage("successfully flipped to quicktime.");

            if (!_bPrimaryMonitor)
            {
              videoPlayerToShow.settings.mute = true;
              _logger.WriteTimestampedMessage("successfully muted windows media.");
            }
            else
            {
              videoPlayerToShow.settings.volume = _videoVolume;

              _logger.WriteTimestampedMessage("successfully set windows media volume.");

              videoPlayerToShow.settings.mute = _bMuteVideo;

              _logger.WriteTimestampedMessage("successfully set mute/no mute of windows media sound.");
            }

            videoPlayerToShow.Ctlcontrols.play();

            _logger.WriteTimestampedMessage("successfully played quicktime.");

            if (!_bPrimaryMonitor)
            {
              // TODO: set aspect ratio correctly for non primary monitors
              videoPlayerToShow.settings.mute = true;

              _logger.WriteTimestampedMessage("successfully muted windows media.");
            }
            else
            {
              videoPlayerToShow.stretchToFit = true;

              _logger.WriteTimestampedMessage("successfully stretched windows media clip to fit player.");
            }

            break;
          case PlayerType.VideoQT:
            Controls.SetChildIndex(quickTimePlayerToShow, 0);
            _logger.WriteTimestampedMessage("successfully flipped to quicktime.");

            if (!_bPrimaryMonitor)
            {
              quickTimePlayerToShow.Movie.AudioMute = true;
              _logger.WriteTimestampedMessage("successfully muted quicktime.");
            }
            else
            {
              quickTimePlayerToShow.Movie.AudioVolume = (float)_videoVolume / 100F;

              _logger.WriteTimestampedMessage("successfully set the the quicktime volume.");

              quickTimePlayerToShow.Movie.AudioMute = _bMuteVideo;

              _logger.WriteTimestampedMessage("successfully set mute/no mute of quicktime sound.");
            }

            quickTimePlayerToShow.Movie.Play(1);

            _logger.WriteTimestampedMessage("successfully played quicktime.");

            break;
        }
      }

      if (_bFadeToDesktop)
      {
        videoPlayerToHide.Visible = false;
        videoPlayerToShow.Visible = false;
        quickTimePlayerToHide.Visible = false;
        quickTimePlayerToShow.Visible = false;
        flashPlayerToHide.Visible = false;
        flashPlayerToShow.Visible = false;
        pictureBoxToHide.Visible = false;
        pictureBoxToShow.Visible = false;
        webBrowserToShow.Visible = false;
        webBrowserToHide.Visible = false;
        _noAssetsAnimatorPlayer.Visible = false;

        _logger.WriteTimestampedMessage("successfully hid players.");

        _ddFormFader.clearTransparentLayeredWindow();

        WinAPI.AnimateWindow(this.Handle, 1, WinAPI.AW_HIDE | WinAPI.AW_BLEND);
        WinAPI.AnimateWindow(_faderForm.Handle, _fadeInToDesktop, WinAPI.AW_HIDE | WinAPI.AW_BLEND);
        this.Activate();

        _logger.WriteTimestampedMessage("successfully faded from black to desktop.");

        _bAllFormsShowing = false;        
      }

      // release previously played streamed asset from player
      // and delete temp file
      if (channelAssetAssociationAssetToHide != null)
        ReleaseDeleteDoneAsset(channelAssetAssociationAssetToHide, flashPlayerToHide, videoPlayerToHide,
          quickTimePlayerToHide, pictureBoxToHide, webBrowserToHide);

      if (_bFadeToDesktop)
        ReleaseDeleteCurrentAsset(channelAssetAssociationAssetToShow, flashPlayerToShow, videoPlayerToShow, quickTimePlayerToShow);

      if (!_bFadeToDesktop)
      {
        _currentChannelAssetAssociationOnDisplay = channelAssetAssociationAssetToShow;

        _logger.WriteTimestampedMessage("successfully kept reference to asset that was just flipped on.");

        // restart stopwatch after revealing appropriate control
        _stopwatch.Reset();
        _stopwatch.Start();
      }

      if (!_bFadeToDesktop)
      {
        Thread.Sleep(_fadeSleep);

        _faderForm.Show();

        for (int i = 204; i >= 0; i -= 51)
        {
          DateTime startFadeTimeStamp = DateTime.Now;

          _ddFormFader.updateOpacity((byte)i, false);

          while ((DateTime.Now - startFadeTimeStamp).Milliseconds < 40) ;
        }

        _logger.WriteTimestampedMessage("successfully faded from black to asset.");        
      }
    }
    
    private void ReleaseDeleteDoneAsset(ChannelAssetAssociation channelAssetAssociationAssetToHide,
      AxShockwaveFlash flashPlayerToHide, AxWindowsMediaPlayer videoPlayerToHide, AxQTControl quickTimePlayerToHide,
      PictureBox pictureBoxToHide, WebBrowser webBrowserToHide)
    {
      string fileToDelete = "";

      switch (channelAssetAssociationAssetToHide.PlaylistAsset.PlayerType)
      {
        case PlayerType.Flash:
          fileToDelete = flashPlayerToHide.Movie;
          flashPlayerToHide.Movie = "";
          _logger.WriteTimestampedMessage("successfully unloaded previous flash");
          break;

        case PlayerType.VideoNonQT:
          fileToDelete = videoPlayerToHide.URL;
          videoPlayerToHide.URL = "";
          _logger.WriteTimestampedMessage("successfully unloaded previous windows media");
          break;

        case PlayerType.VideoQT:
          fileToDelete = quickTimePlayerToHide.URL;
          quickTimePlayerToHide.URL = "";
          _logger.WriteTimestampedMessage("successfully unloaded quicktime");
          break;

        case PlayerType.Image:
          ClearImage(pictureBoxToHide);
          _logger.WriteTimestampedMessage("successfully unloaded previous image");
          break;

        case PlayerType.WebSite:
          webBrowserToHide.DocumentText = "<html><body style='background-color:Black'></body></html>";
          _logger.WriteTimestampedMessage("successfully unloaded previous web site");
          break;
      }

      if (fileToDelete != "")
      {
        try
        {
          File.Delete(fileToDelete);
          _logger.WriteTimestampedMessage("successfully deleted previous asset.");
        }
        catch { }
      }
    }

    private void ReleaseDeleteCurrentAsset(ChannelAssetAssociation channelAssetAssociationAssetToShow, AxShockwaveFlash flashPlayerToShow, AxWindowsMediaPlayer videoPlayerToShow, AxQTControl quickTimePlayerToShow)
    {
      string fileToDelete = "";

      switch (channelAssetAssociationAssetToShow.PlaylistAsset.PlayerType)
      {
        case PlayerType.Flash:
          fileToDelete = flashPlayerToShow.Movie;
          flashPlayerToShow.Movie = "";
          _logger.WriteTimestampedMessage("successfully unloaded current flash.");
          break;

        case PlayerType.VideoNonQT:
          fileToDelete = videoPlayerToShow.URL;
          videoPlayerToShow.URL = "";
          _logger.WriteTimestampedMessage("successfully unloaded current windows media.");
          break;

        case PlayerType.VideoQT:
          fileToDelete = quickTimePlayerToShow.URL;
          quickTimePlayerToShow.URL = "";
          _logger.WriteTimestampedMessage("successfully unloaded current quicktime.");
          break;
      }

      if (fileToDelete != "")
      {
        try
        {
          File.Delete(fileToDelete);
          _logger.WriteTimestampedMessage("successfully deleted current asset.");
        }
        catch { }
      }
    }

    // Decides which asset slot and player to select and load the next asset to
    private void DecideSelectAndLoadAsset()
    {
      DisplayToggle displayToggle = _displayToggle == DisplayToggle.A ? DisplayToggle.B : DisplayToggle.A;

      while (_bRunLoadThread)
      {
        lock (_lockingObject)
        {
          try
          {
            if (displayToggle != _displayToggle)
            {
              if (_displayToggle == DisplayToggle.A)
                SelectAndLoadAsset(ref _ChannelAssetAssociationA, _pictureBoxA, _webBrowserA, _flashPlayerA, _videoPlayerA, _noAssetsAnimatorPlayer, _quickTimePlayerA, _displayToggle.ToString());
              else
                SelectAndLoadAsset(ref _ChannelAssetAssociationB, _pictureBoxB, _webBrowserB, _flashPlayerB, _videoPlayerB, _noAssetsAnimatorPlayer, _quickTimePlayerB, _displayToggle.ToString());

              displayToggle = _displayToggle;
            }
          }
          catch (Exception ex)
          {
            _logger.WriteError(ex);
          }
        }

        Thread.Sleep(100);
      }
    }

    private void SelectAndLoadAsset(ref ChannelAssetAssociation channelAssetAssociation, PictureBox pictureBox,
      WebBrowser webBrowser, AxShockwaveFlash flashPlayer, AxWindowsMediaPlayer videoPlayer,
      NoAssetsAnimatorPlayer noAssetsAnimatorPlayer, AxQTControl quickTimePlayer,
      string displayToggleString)
    {
      channelAssetAssociation = SelectAsset();

      PlaylistAsset playlistAsset = channelAssetAssociation.PlaylistAsset;

      _logger.WriteTimestampedMessage("successfully selected asset: " + playlistAsset.AssetID + " from channel " + channelAssetAssociation.ChannelID);

      AddPlayTimes(playlistAsset);

      _logger.WriteTimestampedMessage("successfully added asset's duration to play times.");

      switch (playlistAsset.PlayerType)
      {
        case PlayerType.Image:
          LoadImage(channelAssetAssociation, pictureBox);
          break;
        case PlayerType.WebSite:
          LoadWebsite(channelAssetAssociation, webBrowser);
          break;
        case PlayerType.NoAssetsAnimator:
          LoadNoAssetsAsset(channelAssetAssociation, noAssetsAnimatorPlayer);
          break;
        case PlayerType.Flash:
          LoadFlash(channelAssetAssociation, flashPlayer, displayToggleString);
          break;
        case PlayerType.VideoNonQT:
          LoadVideoNonQT(channelAssetAssociation, videoPlayer, displayToggleString);
          break;
        case PlayerType.VideoQT:
          LoadVideoQT(channelAssetAssociation, quickTimePlayer, displayToggleString);
          break;
      }
    }

    void ScreenSaver_HandleCreated(object sender, EventArgs e)
    {
      // set the form and control bounds on Load, with the updated form bounds
      this.Bounds = Screen.AllScreens[_screenNo].Bounds;

      _logger.WriteTimestampedMessage("successfully set the size and position of the Screensaver.");

      _faderForm.Bounds = this.Bounds;

      _logger.WriteTimestampedMessage("successfully set the size and position of the fader form.");      
    }

    private void ScreenSaver_Load(object sender, EventArgs e)
    {
      Rectangle rect = new Rectangle();

      // set the form and control bounds on Load, with the updated form bounds
      this.Bounds = Screen.AllScreens[_screenNo].Bounds;

      _logger.WriteTimestampedMessage("successfully set the size and position of the Screensaver to the monitor dimensions.");

      rect = new Rectangle(0, 0, this.Bounds.Width, this.Bounds.Height);
      
      _pictureBoxA.Bounds = rect;
      _pictureBoxB.Bounds = rect;
      _webBrowserA.Bounds = rect;
      _webBrowserB.Bounds = rect;
      _flashPlayerA.Bounds = rect;
      _flashPlayerB.Bounds = rect;
      _videoPlayerA.Bounds = rect;
      _videoPlayerB.Bounds = rect;
      _noAssetsAnimatorPlayer.Bounds = rect;
      _quickTimePlayerA.Bounds = rect;
      _quickTimePlayerB.Bounds = rect;

      _logger.WriteTimestampedMessage("successfully set the size and position of the players to the Screensaver dimensions.");

      _faderForm.Bounds = this.Bounds;

      _logger.WriteTimestampedMessage("successfully set the size and position of the fader form to the Screensaver dimensions.");
      
      // wrap controls around clips, keeping aspect ratio
      _quickTimePlayerA.Sizing = QTOControlLib.QTSizingModeEnum.qtMovieFitsControlMaintainAspectRatio;
      _quickTimePlayerB.Sizing = QTOControlLib.QTSizingModeEnum.qtMovieFitsControlMaintainAspectRatio;

      _logger.WriteTimestampedMessage("successfully set the aspect ratio of the two quicktime players.");

      // TODO: maintain QuickTime aspect ratio for non primary monitors

      // conceal scroll bars
      _webBrowserA.ScrollBarsEnabled = false;
      _webBrowserB.ScrollBarsEnabled = false;

      _logger.WriteTimestampedMessage("successfully disabled the scroll bars of the web browsers.");

      // conceal video player controls
      _videoPlayerA.uiMode = "none";
      _videoPlayerB.uiMode = "none";

      _logger.WriteTimestampedMessage("successfully disabled the play controls of the windows media players.");

      // conceal QuickTime player controls
      _quickTimePlayerA.MovieControllerVisible = false;
      _quickTimePlayerB.MovieControllerVisible = false;

      _logger.WriteTimestampedMessage("successfully disabled the play controls of the quicktime players.");

      _loadAssetThread = new Thread(new ThreadStart(DecideSelectAndLoadAsset));
      CultureInfo ci = new CultureInfo("en-GB");
      _loadAssetThread.CurrentCulture = ci;
      _loadAssetThread.CurrentUICulture = ci;
      _loadAssetThread.Start();

      _logger.WriteTimestampedMessage("successfully started the Load thread.");

      while (!_loadAssetThread.IsAlive) ;

      _logger.WriteTimestampedMessage("Load thread is now alive.");

      Thread.Sleep(5);

      WinAPI.AnimateWindow(_faderForm.Handle, _fadeOutToBlack, WinAPI.AW_ACTIVATE | WinAPI.AW_BLEND);

      _logger.WriteTimestampedMessage("fader form faded from desktop to black.");

      _ddFormFader.setTransparentLayeredWindow();

      _logger.WriteTimestampedMessage("fader form set to Layered Window to be used by the between-slides fader.");
      
      _displayAssetThread = new Thread(new ThreadStart(DisplayAsset));
      _displayAssetThread.CurrentCulture = ci;
      _displayAssetThread.CurrentUICulture = ci;
      _displayAssetThread.Start();

      _logger.WriteTimestampedMessage("successfully started the Display thread.");
    }

    private bool IsPrimaryMonitor()
    {
      return Screen.AllScreens[_screenNo].Primary;
    }

    private void ScreenSaver_Closed(object sender, FormClosedEventArgs e)
    {
      // no implementation
    }

    public void HideForms()
    {
      lock (_lockingObject)
        _bFadeToDesktop = true;
    }

    public bool FormsShowing()
    {
      lock (_lockingObject)
        return _bAllFormsShowing;
    }

    public bool AreScreensaverThreadsAlive()
    {
      return _displayAssetThread.IsAlive && _loadAssetThread.IsAlive;
    }

    /// <summary>
    /// This override traps suspension and stand-by messages and sets a boolean value accordingly.
    /// If the system goes on standby / hibernate, impression logs will not be written.
    /// If the system comes out of standby (somehow without user intervention, because that would end the screensaver),
    /// impression logs will resume
    /// </summary>
    /// <param name="m">Reference to windows message that is raised</param>
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    protected override void WndProc(ref Message m)
    {
      if (m.Msg == WM_POWERBROADCAST)
      {
        int reason = m.WParam.ToInt32();

        switch (reason)
        {
          case PBT_APMSUSPEND:
            _bLogImpressions = false;
            break;
          case PBT_APMSTANDBY:
            _bLogImpressions = false;
            break;
          case PBT_APMRESUMESUSPEND:
            _bLogImpressions = true;
            break;
          case PBT_APMRESUMESTANDBY:
            _bLogImpressions = true;
            break;
        }
      }

      base.WndProc(ref m);
    }
  }
}
