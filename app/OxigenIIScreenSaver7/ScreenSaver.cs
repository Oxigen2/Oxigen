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
using System.Diagnostics;
using OxigenIIAdvertising.ScreenSaver7.Properties;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using OxigenIIAdvertising.NoAssetsAnimator;
using OxigenIIAdvertising.UserSettings;
using AxShockwaveFlashObjects;
using AxWMPLib;
using OxigenIIAdvertising.ScreenSaver;

namespace OxigenIIAdvertising.ScreenSaver7
{
  public partial class ScreenSaver : Form
  {
    [DllImport("user32.dll")]
    private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

    //start off originalLocation with an X and Y of int.MaxValue, because
    //it is impossible for the cursor to be at that position. That way, we
    //know if this variable has been set yet.
    private Point _originalLocation = new Point(int.MaxValue, int.MaxValue);

    private int _screenNo;
    private bool _bPreviewMode = false;

    private LoggerInfo.Logger _logger = null;

    // used by UI thread and timer only
    private System.Timers.Timer _pulseTimer = null;
    private Stopwatch _stopwatch = null;
    private LogSingletonAccessor _logSingletonAccessor = null;
    private IntPtr _previewHandle;
    private PlaylistAsset _currentPlaylistAssetOnDisplay = null;

    // used by worker thread
    private Playlist _playlist = null;
    private AssetScheduler _assetScheduler = null;
    private PlaylistAssetPicker _playlistAssetPicker = null;
    private int _protectedContentTime = -1;
    private float _advertDisplayThreshold = -1;
    private int _noAssetDisplayLength = -1;
    private int _requestTimeout = -1;

    // used by both
    private volatile DisplayToggle _displayToggle;
    private volatile bool _bKeepRunning = true;
    private WebBrowser _webBrowserA = null;
    private WebBrowser _webBrowserB = null;
    private PictureBox _pictureBoxA = null;
    private PictureBox _pictureBoxB = null;
    private PlaylistAsset _assetA = null;
    private PlaylistAsset _assetB = null;
    private NoAssetsAnimatorPlayer _noAssetsAnimatorPlayer = null;
    private AxShockwaveFlash _flashPlayerA;
    private AxShockwaveFlash _flashPlayerB;
    private AxWindowsMediaPlayer _videoPlayerA;
    private AxWindowsMediaPlayer _videoPlayerB;
    private int _totalDisplayTime = 0;
    private int _totalAdvertDisplayTime = 0;
    private int _assetDisplayLength = 0;
    private User _user = null;

    private volatile object _lockingObject = new object();

    private Thread _loadAssetThread = null;

    /// <summary>
    /// Default constructor that runs the screensaver in normal mode
    /// </summary>
    /// <param name="screenNo">index number of screen to run screensaver on</param>
    /// <param name="playlist">The playlist from which to draw assets</param>
    /// <param name="advertDisplayThreshold"></param>
    /// <param name="protectedContentTime"></param>
    public ScreenSaver(int screenNo, Playlist playlist, float advertDisplayThreshold, int protectedContentTime, int noAssetDisplayLength, int requestTimeout, User user)
    {
      InitializeComponent();

      //Cursor.Hide();
      //TopMost = true;

      _screenNo = screenNo;

      _user = user;

      _logger = new OxigenIIAdvertising.LoggerInfo.Logger("ScreenSaver", _user.FilePaths["DebugFilePath"] + _screenNo + ".txt", OxigenIIAdvertising.LoggerInfo.LoggingMode.Debug);

      InitializeCustom(playlist, advertDisplayThreshold, protectedContentTime, noAssetDisplayLength, requestTimeout);
    }

    /// <summary>
    /// This constructor is the handle to the select screen saver dialog preview window. It is used when in preview mode (/p)
    /// </summary>
    /// <param name="previewHandle">handle of the parent preview window</param>
    /// <param name="playlist">screen saver playlist object</param>
    /// <param name="assetScheduler">asset scheduler to determine temporal scheduling of assets</param>
    /// <param name="advertDisplayThreshold">threshold to determine whether to display an asset or an advert as the next asset</param>
    public ScreenSaver(IntPtr previewHandle, Playlist playlist, float advertDisplayThreshold, int protectedContentTime, int noAssetDisplayLength, int requestTimeout, User user)
    {
      InitializeComponent();

      _previewHandle = previewHandle;

      _user = user;

      _logger = new OxigenIIAdvertising.LoggerInfo.Logger("ScreenSaver", _user.FilePaths["DebugFilePath"], OxigenIIAdvertising.LoggerInfo.LoggingMode.Debug);

      _bPreviewMode = true;

      InitializeCustom(playlist, advertDisplayThreshold, protectedContentTime, noAssetDisplayLength, requestTimeout);
    }

    private void InitializeCustom(Playlist playlist, float advertDisplayThreshold, int protectedContentTime,
      int noAssetDisplayLength, int requestTimeout)
    {
      _logSingletonAccessor = new LogSingletonAccessor();

      _advertDisplayThreshold = advertDisplayThreshold;
      _protectedContentTime = protectedContentTime;
      _noAssetDisplayLength = noAssetDisplayLength;
      _requestTimeout = requestTimeout;

      _playlist = playlist;

      _assetScheduler = new AssetScheduler();

      _playlistAssetPicker = new PlaylistAssetPicker(_playlist, _assetScheduler, _noAssetDisplayLength, _user.FilePaths["AssetPath"], Settings.Default.Password, _requestTimeout, _bPreviewMode);
      _stopwatch = new Stopwatch();
      _pulseTimer = new System.Timers.Timer();

      _webBrowserA = new WebBrowser();
      _webBrowserB = new WebBrowser();

      _pictureBoxA = new PictureBox();
      _pictureBoxB = new PictureBox();

      _flashPlayerA = new AxShockwaveFlash();
      _flashPlayerB = new AxShockwaveFlash();

      _videoPlayerA = new AxWindowsMediaPlayer();
      _videoPlayerB = new AxWindowsMediaPlayer();

      _noAssetsAnimatorPlayer = new NoAssetsAnimatorPlayer(_bPreviewMode);

      _pictureBoxA.SizeMode = PictureBoxSizeMode.CenterImage;
      _pictureBoxB.SizeMode = PictureBoxSizeMode.CenterImage;

      // suppress page script errors
      _webBrowserA.ScriptErrorsSuppressed = true;
      _webBrowserB.ScriptErrorsSuppressed = true;

      Controls.Add(_noAssetsAnimatorPlayer);
      Controls.Add(_pictureBoxA);
      Controls.Add(_pictureBoxB);
      Controls.Add(_webBrowserA);
      Controls.Add(_webBrowserB);
      Controls.Add(_flashPlayerA);
      Controls.Add(_flashPlayerB);
      Controls.Add(_videoPlayerA);
      Controls.Add(_videoPlayerB);

      Control.CheckForIllegalCrossThreadCalls = false;

      // set z-index of all containers/players to the same value
      Controls.SetChildIndex(_pictureBoxA, 1);
      Controls.SetChildIndex(_pictureBoxB, 1);
      Controls.SetChildIndex(_webBrowserA, 1);
      Controls.SetChildIndex(_webBrowserB, 1);
      Controls.SetChildIndex(_flashPlayerA, 1);
      Controls.SetChildIndex(_flashPlayerB, 1);
      Controls.SetChildIndex(_noAssetsAnimatorPlayer, 1);

      _pictureBoxA.Dock = DockStyle.Fill;
      _pictureBoxB.Dock = DockStyle.Fill;
      _webBrowserA.Dock = DockStyle.Fill;
      _webBrowserB.Dock = DockStyle.Fill;
      _flashPlayerA.Dock = DockStyle.Fill;
      _flashPlayerB.Dock = DockStyle.Fill;
      _videoPlayerA.Dock = DockStyle.Fill;
      _videoPlayerB.Dock = DockStyle.Fill;
      _noAssetsAnimatorPlayer.Dock = DockStyle.Fill;

      _displayToggle = DisplayToggle.A;

      _loadAssetThread = new Thread(new ThreadStart(DecideSelectAndLoadAsset));
      _loadAssetThread.Start();

      _pulseTimer.Interval = 100;
      _pulseTimer.AutoReset = false;
      _pulseTimer.Elapsed += new System.Timers.ElapsedEventHandler(ProcessPulseTimer);
      _pulseTimer.Start();
    }

    private void ProcessPulseTimer(object sender, EventArgs e)
    {
      lock (_lockingObject)
      {
        // has the stop command for the Load Thread been sent by Program.cs
        if (!_bKeepRunning)
          return;

        if (_stopwatch.IsRunning && _stopwatch.Elapsed.TotalSeconds <= _assetDisplayLength)
        {
          _pulseTimer.Start();
          return;
        }

        // is this the first run of the pulse? If yes, there is nothing to log
        if (_logSingletonAccessor.AssetImpressionStartDateTime.Year != 1)
        {
          // Add logs of the asset previously impressed (i.e. if a is to be impressed next,
          // log previously impressed asset B, else log previously impressed A)
          PlaylistAsset playlistAsset = _displayToggle == DisplayToggle.A ? _assetB : _assetA;

          AddPlayTimes(playlistAsset);
          AddImpressionLog(playlistAsset);
        }

        _stopwatch.Reset();
        _stopwatch.Start();

        DisplayAsset();

        _displayToggle = _displayToggle == DisplayToggle.A ? DisplayToggle.B : DisplayToggle.A;

        _pulseTimer.Start();
      }
    }

    private void AddPlayTimes(PlaylistAsset playlistAsset)
    {
      _totalDisplayTime += playlistAsset.DisplayLength;

      if (playlistAsset is AdvertPlaylistAsset)
        _totalAdvertDisplayTime += playlistAsset.DisplayLength;
    }

    private void AddImpressionLog(PlaylistAsset playlistAsset)
    {
      // reminder: LogEntriesRawSingleton is thread safe. No need to lock again.
      _logSingletonAccessor.AddImpressionLog(playlistAsset);
    }

    /// <summary>
    /// Gets the click through URL of the asset currently displayed
    /// </summary>
    /// <returns>a string with the URL to launch</returns>
    internal string GetClickThroughURL()
    {
      return _currentPlaylistAssetOnDisplay.ClickDestination;
    }

    /// <summary>
    /// Adds a click log of the asset currently displayed
    /// </summary>
    internal void AddClickLog()
    {
      _logSingletonAccessor.AddClickLog(_currentPlaylistAssetOnDisplay);
    }

    /// <summary>
    /// Stops load/display/log work and adds an impression log for the last asset impressed,
    /// since it won't have run its full circle.
    /// </summary>
    internal void StopWork()
    {
      _bKeepRunning = false;

      _pulseTimer.Stop();

      AddImpressionLog(_currentPlaylistAssetOnDisplay);
    }

    private void LoadWebsite(PlaylistAsset playlistAsset, WebBrowser webBrowser)
    {
      webBrowser.Navigate(playlistAsset.AssetWebSite);
      while (webBrowser.ReadyState != WebBrowserReadyState.Complete) ;
    }

    private void LoadFlash(PlaylistAsset playlistAsset, AxShockwaveFlash flashPlayer)
    {
      DecryptToTemp(playlistAsset);

      // has the termination command been sent. check after timely DecryptToTemp() work
      if (!_bKeepRunning)
        return;

      flashPlayer.Movie = Settings.Default.TempDecryptPath + playlistAsset.AssetFilename;

      flashPlayer.Stop();
      flashPlayer.Rewind();

      if (_bPreviewMode)
        flashPlayer.ScaleMode = 1; // no border
      else
        flashPlayer.ScaleMode = 0; // no scale
    }

    private void LoadVideo(PlaylistAsset playlistAsset, AxWindowsMediaPlayer videoPlayer)
    {
      DecryptToTemp(playlistAsset);

      // has the termination command been sent. check after timely DecryptToTemp() work
      if (!_bKeepRunning)
        return;

      videoPlayer.URL = Settings.Default.TempDecryptPath + playlistAsset.AssetFilename;
    }

    private void DecryptToTemp(PlaylistAsset playlistAsset)
    {
      if (File.Exists(Settings.Default.TempDecryptPath + playlistAsset.AssetFilename))
        return;

      MemoryStream ms = playlistAsset.DecryptAssetFile(_user.FilePaths["AssetPath"] + playlistAsset.AssetFilename, Settings.Default.Password);

      FileStream fs = new FileStream(Settings.Default.TempDecryptPath + playlistAsset.AssetFilename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);

      ms.WriteTo(fs);

      ms.Dispose();
      fs.Dispose();
    }

    private void LoadNoAssetsAsset(PlaylistAsset playlistAsset, NoAssetsAnimatorPlayer noAssetsAnimatorPlayer)
    {
      noAssetsAnimatorPlayer.Message = ((ContentPlaylistAsset)playlistAsset).Message;
    }

    private void LoadImage(PlaylistAsset playlistAsset, PictureBox pictureBox)
    {
      // has the termination command been sent
      if (!_bKeepRunning)
        return;

      // promptly dispose of existing image
      if (pictureBox.Image != null)
      {
        Image disposableImage = pictureBox.Image;
        disposableImage.Dispose();
        disposableImage = null;
        pictureBox.Image = null;
      }

      MemoryStream memoryStream = playlistAsset.DecryptAssetFile(_user.FilePaths["AssetPath"] + playlistAsset.AssetFilename, Settings.Default.Password);

      // Stream must be kept open for the lifetime of the image
      // use a temp image, then clone it, to make independent of the stream, then close the stream
      Image tempImage = Image.FromStream(memoryStream);

      // if preview mode, make an image the size of the picturebox, otherwise make an image the size of the original one's.
      if (_bPreviewMode)
        pictureBox.Image = new Bitmap(tempImage, pictureBox.Size);
      else
        pictureBox.Image = new Bitmap(tempImage);

      tempImage.Dispose();
      tempImage = null;

      memoryStream.Close();
      memoryStream.Dispose();
      memoryStream = null;
    }

    // Selects the next asset from the playlist, if temporal scheduling allows it and current date is within specified date bounds.
    // Decides if the next asset must be an advert or a content.
    // If the next asset is an advert and there is no advert available at this time, it attempts to select a content.
    // If no asset was found, a "no assets" asset is created.
    private PlaylistAsset SelectAsset()
    {
      return _playlistAssetPicker.SelectAsset(_totalDisplayTime, _totalAdvertDisplayTime, _protectedContentTime, _advertDisplayThreshold, Resources.NoAssets);
    }

    private void DisplayAsset()
    {
      // keep start date and time of the display for logging
      _logSingletonAccessor.AssetImpressionStartDateTime = DateTime.Now;

      if (_displayToggle == DisplayToggle.A)
      {
        _assetDisplayLength = _assetA.DisplayLength;

        _currentPlaylistAssetOnDisplay = _assetA;

        HideAndRevealContainers(_assetA, _pictureBoxA, _pictureBoxB, _webBrowserA, _webBrowserB, _flashPlayerA, _flashPlayerB, _videoPlayerA, _videoPlayerB);

        _logger.WriteMessage(DateTime.Now.ToLongTimeString() + " Displaying asset A - " + _assetA.PlayerType.ToString() + " ID: " + _assetA.AssetID);
      }
      else
      {
        _assetDisplayLength = _assetB.DisplayLength;

        _currentPlaylistAssetOnDisplay = _assetB;

        HideAndRevealContainers(_assetB, _pictureBoxB, _pictureBoxA, _webBrowserB, _webBrowserA, _flashPlayerB, _flashPlayerA, _videoPlayerB, _videoPlayerA);

        _logger.WriteMessage(DateTime.Now.ToLongTimeString() + " Displaying asset B - " + _assetB.PlayerType.ToString() + " ID: " + _assetB.AssetID);
      }
    }

    private void HideAndRevealContainers(PlaylistAsset playlistAsset, PictureBox pictureBoxToShow,
      PictureBox pictureBoxToHide, WebBrowser webBrowserToShow, WebBrowser webBrowserToHide,
      AxShockwaveFlash flashPlayerToShow, AxShockwaveFlash flashPlayerToHide,
      AxWindowsMediaPlayer videoPlayerToShow, AxWindowsMediaPlayer videoPlayerToHide)
    {
      switch (playlistAsset.PlayerType)
      {
        case PlayerType.Image:
          Controls.SetChildIndex(pictureBoxToShow, 0);
          Controls.SetChildIndex(pictureBoxToHide, 1);
          Controls.SetChildIndex(webBrowserToShow, 1);
          Controls.SetChildIndex(webBrowserToHide, 1);
          Controls.SetChildIndex(flashPlayerToShow, 1);
          Controls.SetChildIndex(flashPlayerToHide, 1);
          Controls.SetChildIndex(videoPlayerToShow, 1);
          Controls.SetChildIndex(videoPlayerToHide, 1);
          Controls.SetChildIndex(_noAssetsAnimatorPlayer, 1);

          // only stop, no need to mute
          flashPlayerToShow.Stop();
          flashPlayerToHide.Stop();

          flashPlayerToShow.Rewind();
          flashPlayerToHide.Rewind();

          videoPlayerToHide.Ctlcontrols.stop();
          videoPlayerToShow.Ctlcontrols.stop();
          break;
        case PlayerType.WebSite:
          Controls.SetChildIndex(webBrowserToShow, 0);
          Controls.SetChildIndex(pictureBoxToShow, 1);
          Controls.SetChildIndex(pictureBoxToHide, 1);
          Controls.SetChildIndex(webBrowserToHide, 1);
          Controls.SetChildIndex(flashPlayerToShow, 1);
          Controls.SetChildIndex(flashPlayerToHide, 1);
          Controls.SetChildIndex(videoPlayerToShow, 1);
          Controls.SetChildIndex(videoPlayerToHide, 1);
          Controls.SetChildIndex(_noAssetsAnimatorPlayer, 1);

          flashPlayerToShow.Stop();
          flashPlayerToHide.Stop();

          flashPlayerToShow.Rewind();
          flashPlayerToHide.Rewind();

          videoPlayerToHide.Ctlcontrols.stop();
          videoPlayerToShow.Ctlcontrols.stop();
          break;
        case PlayerType.NoAssetsAnimator:
          Controls.SetChildIndex(_noAssetsAnimatorPlayer, 0);
          Controls.SetChildIndex(pictureBoxToShow, 1);
          Controls.SetChildIndex(pictureBoxToHide, 1);
          Controls.SetChildIndex(webBrowserToShow, 1);
          Controls.SetChildIndex(webBrowserToHide, 1);
          Controls.SetChildIndex(flashPlayerToShow, 1);
          Controls.SetChildIndex(flashPlayerToHide, 1);
          Controls.SetChildIndex(videoPlayerToShow, 1);
          Controls.SetChildIndex(videoPlayerToHide, 1);

          flashPlayerToShow.Stop();
          flashPlayerToHide.Stop();

          flashPlayerToShow.Rewind();
          flashPlayerToHide.Rewind();

          videoPlayerToHide.Ctlcontrols.stop();
          videoPlayerToShow.Ctlcontrols.stop();
          break;
        case PlayerType.Flash:
          Controls.SetChildIndex(flashPlayerToShow, 0);
          Controls.SetChildIndex(pictureBoxToShow, 1);
          Controls.SetChildIndex(pictureBoxToHide, 1);
          Controls.SetChildIndex(webBrowserToShow, 1);
          Controls.SetChildIndex(webBrowserToHide, 1);
          Controls.SetChildIndex(flashPlayerToHide, 1);
          Controls.SetChildIndex(videoPlayerToShow, 1);
          Controls.SetChildIndex(videoPlayerToHide, 1);
          Controls.SetChildIndex(_noAssetsAnimatorPlayer, 1);

          flashPlayerToShow.Play();

          flashPlayerToHide.Stop();

          flashPlayerToHide.Rewind();

          videoPlayerToHide.Ctlcontrols.stop();
          videoPlayerToShow.Ctlcontrols.stop();
          break;
        case PlayerType.VideoNonQT:
          Controls.SetChildIndex(videoPlayerToShow, 0);
          Controls.SetChildIndex(pictureBoxToShow, 1);
          Controls.SetChildIndex(pictureBoxToHide, 1);
          Controls.SetChildIndex(webBrowserToShow, 1);
          Controls.SetChildIndex(webBrowserToHide, 1);
          Controls.SetChildIndex(flashPlayerToShow, 1);
          Controls.SetChildIndex(flashPlayerToHide, 1);
          Controls.SetChildIndex(videoPlayerToHide, 1);
          Controls.SetChildIndex(_noAssetsAnimatorPlayer, 1);

          videoPlayerToShow.Ctlcontrols.play();

          flashPlayerToShow.Stop();
          flashPlayerToHide.Stop();

          flashPlayerToShow.Rewind();
          flashPlayerToHide.Rewind();

          videoPlayerToHide.Ctlcontrols.stop();
          break;
      }
    }

    // Decides which asset slot and player to select and load the next asset to
    private void DecideSelectAndLoadAsset()
    {
      DisplayToggle displayToggle = _displayToggle == DisplayToggle.A ? DisplayToggle.B : DisplayToggle.A;

      try
      {
        while (_bKeepRunning)
        {
          lock (_lockingObject)
          {
            if (displayToggle != _displayToggle)
            {
              if (_displayToggle == DisplayToggle.A)
              {
                _logger.WriteMessage(DateTime.Now.ToLongTimeString() + " Begin loading A");
                SelectAndLoadAsset(ref _assetA, _pictureBoxA, _webBrowserA, _flashPlayerA, _videoPlayerA, _noAssetsAnimatorPlayer);
                _logger.WriteMessage(DateTime.Now.ToLongTimeString() + " End loading A - " + _assetA.PlayerType.ToString() + " ID: " + _assetA.AssetID);
              }
              else
              {
                _logger.WriteMessage(DateTime.Now.ToLongTimeString() + " Begin loading B");
                SelectAndLoadAsset(ref _assetB, _pictureBoxB, _webBrowserB, _flashPlayerB, _videoPlayerB, _noAssetsAnimatorPlayer);
                _logger.WriteMessage(DateTime.Now.ToLongTimeString() + " End loading B - " + _assetB.PlayerType.ToString() + " ID: " + _assetB.AssetID);
              }

              displayToggle = _displayToggle;
            }
          }
        }
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);
      }
    }

    private void SelectAndLoadAsset(ref PlaylistAsset playlistAsset, PictureBox pictureBox,
      WebBrowser webBrowser, AxShockwaveFlash flashPlayer, AxWindowsMediaPlayer videoPlayer, NoAssetsAnimatorPlayer noAssetsAnimatorPlayer)
    {
      playlistAsset = SelectAsset();

      switch (playlistAsset.PlayerType)
      {
        case PlayerType.Image:
          LoadImage(playlistAsset, pictureBox);
          break;
        case PlayerType.WebSite:
          LoadWebsite(playlistAsset, webBrowser);
          break;
        case PlayerType.NoAssetsAnimator:
          LoadNoAssetsAsset(playlistAsset, noAssetsAnimatorPlayer);
          break;
        case PlayerType.Flash:
          LoadFlash(playlistAsset, flashPlayer);
          break;
        case PlayerType.VideoNonQT:
          LoadVideo(playlistAsset, videoPlayer);
          break;
      }
    }

    private void ScreenSaver_Load(object sender, EventArgs e)
    {
      // set the form and control bounds on Load, with the updated form bounds
      if (_bPreviewMode)
      {
        //set the preview window as the parent of this window
        SetParent(this.Handle, _previewHandle);

        //make this a child window, so when the select screen saver dialog closes, this will also close
        SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

        //set our window's size to the size of our window's new parent
        Rectangle ParentRect;
        GetClientRect(_previewHandle, out ParentRect);
        this.Size = ParentRect.Size;

        //set our location at (0, 0)
        this.Location = new Point(0, 0);
      }
      else
      {
        // is this an 8-monitor debug mode
        // set the bounds again as the Load event makes the form and the controls lose their position
        this.Bounds = Screen.AllScreens[_screenNo].Bounds;
      }

      // conceal scroll bars
      _webBrowserA.ScrollBarsEnabled = false;
      _webBrowserB.ScrollBarsEnabled = false;
    }

    private void ScreenSaver_Closed(object sender, FormClosedEventArgs e)
    {
      // close the application from this end, if it was shown on preview mode.
      if (_bPreviewMode)
        Program.TerminateApplication(false);
    }
  }
}