using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.PlaylistLogic;
using OxigenIIAdvertising.AssetScheduling;
using OxigenIIAdvertising.ScreenSaver.Players;
using OxigenIIAdvertising.ScreenSaver.Properties;
using System.Threading;
using System.IO;
using OxigenIIAdvertising.OxigenIIStopwatch;
using OxigenIIAdvertising.LoggerInfo;
using System.Globalization;
using OxigenIIAdvertising.ScreenSaver.Players;

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
        private const int _fadeInToDesktop = 150;
        private const int _fadeSleep = 100;
        private string _tempDecryptPath = null;
        private string _assetPath = null;

        private volatile bool _runScreenSaver = true;
        private volatile bool _bFirstRun = true;

        // used by worker thread
        private AssetScheduler _assetScheduler = null;
        private PlaylistAssetPicker _playlistAssetPicker = null;
        private float _protectedContentTime = -1F;
        private float _advertDisplayThreshold = -1F;
        private float _displayMessageAssetDisplayLength = -1F;
        private int _requestTimeout = -1;
        private bool _bErrorOnSetup = false;
        private string _appToRun = null;
        private string _displayMessage = null;

        // used by both
        private float _totalDisplayTime = 0;
        private float _totalAdvertDisplayTime = 0;
        private float _assetDisplayLength = 0;
        private bool _bLogImpressions = true;
        private FaderForm _faderForm = null;
        private DDFormFader _ddFormFader = null;
        private bool _bInsufficientMemoryForLargeFiles = false;
        private float _defaultDisplayLength = -1F;

        private PlayerContainer _players;
        private int _count = 0;
        private Slide _currentSlide;
        private Slide _previousSlide;

        /// <summary>
        /// Sets the playlist object from which the Screensaver plays its slides.
        /// Places a lock on _lockingObject before Setting the playlist to make thread safe.
        /// </summary>
        public void SetPlaylistAssetPickerPlaylist(Playlist playlist)
        {
            if (_playlistAssetPicker != null)
                _playlistAssetPicker.Playlist = playlist;
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
          string tempDecryptPath, string assetPath, bool bInsufficientMemoryForLargeFiles, float defaultDisplayLength, Logger logger)
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

            _playlistAssetPicker = new PlaylistAssetPicker(playlist, _assetScheduler, _displayMessageAssetDisplayLength, assetPath, "password",
              _requestTimeout, _bErrorOnSetup, _screenNo, _logger, _bInsufficientMemoryForLargeFiles);

            _logger.WriteTimestampedMessage("successfully created a PlaylistAssetPicker object.");

            _stopwatch = new Stopwatch();

            _logger.WriteTimestampedMessage("successfully created a Stopwatch object.");

            _players = new PlayerContainer();

            if (PlayerContainer.QuickTimeRightVersionExists())
                _players.Add(PlayerType.VideoQT, new QuicktimePlayer(_bMuteVideo, _videoVolume, _logger));

            if (PlayerContainer.WindowsMediaPlayerRightVersionExists())
                _players.Add(PlayerType.VideoNonQT, new WindowsMediaPlayer(_bMuteVideo, _videoVolume, _logger));

            // these players always exist
            _players.Add(PlayerType.Flash, new FlashPlayer(bMuteFlash, _logger));
            _players.Add(PlayerType.Image, new ImagePlayer());
            _players.Add(PlayerType.WebSite, new WebsitePlayer(_logger));
            _players.Add(PlayerType.NoAssetsAnimator, new NoAssetsPlayer());

            foreach (IPlayer player in _players.AllPlayers())
                Controls.Add(player.Control);

            //Control.CheckForIllegalCrossThreadCalls = false;

            // set z-index of all containers/players to the same value
            int index = 2;
            foreach (IPlayer player in _players.AllPlayers())
            {
                _logger.WriteMessage(player.GetType() + " index set to " + index);
                Controls.SetChildIndex(player.Control, index);
                index++;
            }

            _currentSlide = new Slide(_players.APlayers);
            _previousSlide = new Slide(_players.BPlayers);

            _logger.WriteTimestampedMessage("successfully set the z-indices of the players.");

            _faderForm = new FaderForm();

            _logger.WriteTimestampedMessage("successfully created a fader form.");

            _ddFormFader = new DDFormFader(_faderForm.Handle);

            _logger.WriteTimestampedMessage("successfully created the object to fade the fader form.");

            _faderForm.StartPosition = FormStartPosition.Manual;

            _logger.WriteTimestampedMessage("successfully set the start position of the fader form to manual.");

            AddOwnedForm(_faderForm);

            _logger.WriteTimestampedMessage("successfully added the fader form as a form owned by the Screensaver Form.");


        }



        private bool HasCurrentSlideFinishedPlaying()
        {
            return _stopwatch.ElapsedTotalMilliseconds > ((_assetDisplayLength * 1000) - 200);
        }

        private void LogImpressionForCurrent()
        {
            // is this the first run of the pulse? If yes, there is nothing to log
            if (_currentSlide.ChannelAssetAssociation != null)
            {
                // Add logs of the asset previously impressed (i.e. if a is to be impressed next,
                // log previously impressed asset B, else log previously impressed A)
                ChannelAssetAssociation channelAssetAssociation = _currentSlide.ChannelAssetAssociation;
                AddImpressionLog(channelAssetAssociation);
                _logger.WriteTimestampedMessage("successfully added impression log for " +
                                                channelAssetAssociation.PlaylistAsset.AssetID +
                                                " in channel: " + channelAssetAssociation.ChannelID);
            }
        }

        public void FadeToDesktop()
        {
            _runScreenSaver = false;

            FadeToBlack();
            AddImpressionLog(_currentChannelAssetAssociationOnDisplay);
            foreach (IPlayer player in _players.AllPlayers())
            {
                player.Stop();
                player.Control.Visible = false;
            }

            _logger.WriteTimestampedMessage("successfully hid players.");

            _ddFormFader.clearTransparentLayeredWindow();

            WinAPI.AnimateWindow(this.Handle, 1, WinAPI.AW_HIDE | WinAPI.AW_BLEND);
            WinAPI.AnimateWindow(_faderForm.Handle, _fadeInToDesktop, WinAPI.AW_HIDE | WinAPI.AW_BLEND);
            this.Activate();

            _logger.WriteTimestampedMessage("successfully faded from black to desktop.");
        }

        public void ReleaseForms()
        {
            SafelyReleaseAssetForDesktop(_previousSlide);
            SafelyReleaseAssetForDesktop(_currentSlide);
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



        private string DecryptToTemp(ChannelAssetAssociation ca)
        {
            string assetTempPath = _tempDecryptPath + Guid.NewGuid() + Path.GetExtension(ca.PlaylistAsset.AssetFilename);

            MemoryStream ms = ca.PlaylistAsset.DecryptAssetFile(_assetPath + ca.PlaylistAsset.GetAssetFilenameGUIDSuffix() + "\\" + ca.PlaylistAsset.AssetFilename, "password");

            FileStream fs = new FileStream(assetTempPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);

            ms.WriteTo(fs);

            ms.Dispose();
            fs.Dispose();

            return assetTempPath;
        }

        // Selects the next asset from the playlist, if temporal scheduling allows it and current date is within specified date bounds.
        // Decides if the next asset must be an advert or a content.
        // If the next asset is an advert and there is no advert available at this time, it attempts to select a content.
        // If no asset was found, a "no assets" asset is created.
        private ChannelAssetAssociation SelectAsset()
        {
            ChannelAssetAssociation channelAssetAssociation = null;

            if (!_bErrorOnSetup)
            {
                channelAssetAssociation = _playlistAssetPicker.SelectAsset(_totalDisplayTime, _totalAdvertDisplayTime, _protectedContentTime, _advertDisplayThreshold, Resources.NoAssets, "app://ContentExchanger");

                if (channelAssetAssociation.PlaylistAsset.DisplayLength == -1F)
                    channelAssetAssociation.PlaylistAsset.DisplayLength = _defaultDisplayLength;

                if (!_players.Exists(channelAssetAssociation.PlaylistAsset.PlayerType))
                {
                    _logger.WriteMessage(channelAssetAssociation.PlaylistAsset.PlayerType + " does not exist.");
                    PlayerNotExistsResponse response = PlayerContainer.PlayerNotExistResponses[channelAssetAssociation.PlaylistAsset.PlayerType];
                    channelAssetAssociation = new ChannelAssetAssociation(0, new ContentPlaylistAsset(_displayMessageAssetDisplayLength, response.Message, response.Link));
                }
                else
                    _logger.WriteMessage(channelAssetAssociation.PlaylistAsset.PlayerType + " exists.");

                return channelAssetAssociation;
            }

            channelAssetAssociation = _playlistAssetPicker.SelectAsset(_totalDisplayTime, _totalAdvertDisplayTime,
                                                                           _protectedContentTime, _advertDisplayThreshold,
                                                                           _displayMessage, _appToRun);

            return channelAssetAssociation;
        }

        private void FlipAssetPlayer()
        {
            _logger.WriteMessage("FlipAssetPlayer 1 : primary monitor? " + _bPrimaryMonitor);
            // keep start date and time of the display for logging

            var tempSlide = _currentSlide;
            _currentSlide = _previousSlide;
            _previousSlide = tempSlide;

            _previousSlide.NewContentRequired();
            _currentSlide.Displaying();

            _logSingletonAccessor.AssetImpressionStartDateTime = DateTime.Now;
            _assetDisplayLength = _currentSlide.ChannelAssetAssociation.PlaylistAsset.DisplayLength;
            Transit(_currentSlide.ChannelAssetAssociation, _previousSlide.ChannelAssetAssociation, _currentSlide.Players, _previousSlide.Players);
            _logger.WriteTimestampedMessage("successfully flipped asset player.");
        }

        private void Transit(ChannelAssetAssociation channelAssetAssociationAssetToShow,
          ChannelAssetAssociation channelAssetAssociationAssetToHide,
          Dictionary<PlayerType, IPlayer> playersToShow,
            Dictionary<PlayerType, IPlayer> playersToHide)
        {
            // transition to black between assets
            if (!_bFirstRun)
            {
                FadeToBlack();
            }
            else
            {
                _bFirstRun = false;
                _logger.WriteTimestampedMessage("first run of the flipping function.");
            }

            // stop previous players
            foreach (IPlayer playerb in playersToHide.Values)
            {
                playerb.Stop();
            }

            IPlayer player = playersToShow[channelAssetAssociationAssetToShow.PlaylistAsset.PlayerType];

            Controls.SetChildIndex(player.Control, 0);
            _logger.WriteMessage("Player " + channelAssetAssociationAssetToShow.PlaylistAsset.PlayerType + ", channelAssetAssociationAssetToShow.PlaylistAsset " + channelAssetAssociationAssetToShow.PlaylistAsset.AssetID + " index changed to 0.");
            player.Play(_bPrimaryMonitor);

            // Some players need to be refreshed so their display changes immediately
            player.Control.Refresh();

            // release previously played streamed asset from player
            // and delete temp file
            if (channelAssetAssociationAssetToHide != null)
                SafelyReleaseAssetForTransition(channelAssetAssociationAssetToHide, playersToHide);

            _currentChannelAssetAssociationOnDisplay = channelAssetAssociationAssetToShow;

            _logger.WriteTimestampedMessage("successfully kept reference to asset that was just flipped on.");

            // restart stopwatch after revealing appropriate control
            _stopwatch.Reset();
            _stopwatch.Start();
            
            Thread.Sleep(_fadeSleep);

            //_faderForm.Show();

            for (int i = 204; i >= 0; i -= 51)
            {
                _ddFormFader.updateOpacity((byte)i, false);
                Thread.Sleep(40);
            }

            _logger.WriteTimestampedMessage("successfully faded from black to asset.");

        }

        private void FadeToBlack()
        {
            for (int i = 51; i <= 255; i += 51)
            {
                _ddFormFader.updateOpacity((byte)i, false);
                Thread.Sleep(40);
            }

            _logger.WriteTimestampedMessage("successfully faded the form from asset to black.");
        }

        private void SafelyReleaseAssetForTransition(ChannelAssetAssociation channelAssetAssociationAssetToHide,
          Dictionary<PlayerType, IPlayer> players)
        {
            var player = players[channelAssetAssociationAssetToHide.PlaylistAsset.PlayerType];

            try
            {
                player.ReleaseAssetForTransition();
                _logger.WriteTimestampedMessage("successfully deleted previous asset.");
            } //TODO: do we need this here??
            catch (Exception ex)
            {
                _logger.WriteError(ex);
            }
        }

        private void SafelyReleaseAssetForDesktop(Slide slide)
        {

            try
            {
                if (slide.ChannelAssetAssociation != null)
                    slide.Players[slide.ChannelAssetAssociation.PlaylistAsset.PlayerType].ReleaseAssetForDesktop();
                _logger.WriteTimestampedMessage("successfully deleted previous asset.");
            } //TODO: do we need this here??
            catch (Exception ex)
            {
                _logger.WriteError(ex);
            }
        }

        public bool SelectAndLoadThreadRunning
        {
            get { return _previousSlide.State == SlideState.Creating; }
        }


        private void SelectAndLoadAsset(object state)
        {
            _previousSlide.Creating();
            //_count++;
            //if (_count > 2) Thread.Sleep(10000);

            try
            {
                ChannelAssetAssociation channelAssetAssociation = SelectAsset();
                _logger.WriteTimestampedMessage("successfully selected asset: " +
                                                channelAssetAssociation.PlaylistAsset.AssetID + " from channel " +
                                                channelAssetAssociation.ChannelID);

                _previousSlide.ChannelAssetAssociation = channelAssetAssociation;
                LoadAsset(channelAssetAssociation,_previousSlide.Players[channelAssetAssociation.PlaylistAsset.PlayerType]);
                _previousSlide.ReadyForDisplay();
            }
            catch (Exception ex)
            {
                _logger.WriteError(ex + " - Primary monitor : " + _bPrimaryMonitor);
                _previousSlide.NewContentRequired();
            }
   
        }

        private void LoadAsset(ChannelAssetAssociation channelAssetAssociation, IPlayer player)
        {

            PlaylistAsset playlistAsset = channelAssetAssociation.PlaylistAsset;

            AddPlayTimes(playlistAsset);

            _logger.WriteMessage("PlayerType: " + playlistAsset.PlayerType);


            if (player is IStreamLoader)
            {
                _logger.WriteMessage("Decrypting");
                IStreamLoader streamLoader = (IStreamLoader)player;
                using (
                    MemoryStream stream =
                        channelAssetAssociation.PlaylistAsset.DecryptAssetFile(
                            _assetPath + channelAssetAssociation.PlaylistAsset.GetAssetFilenameGUIDSuffix() + "\\" +
                            channelAssetAssociation.PlaylistAsset.AssetFilename, "password"))
                {
                    _logger.WriteMessage("Loading");
                    streamLoader.Load(stream);
                }
            }
            else if (player is IURLLoader)
            {
                IURLLoader urlLoader = (IURLLoader)player;
                urlLoader.Load(channelAssetAssociation.PlaylistAsset.AssetWebSite);
            }
            else if (player is IFileLoader)
            {
                _logger.WriteMessage("Decrypting");
                string decryptedFilePath = DecryptToTemp(channelAssetAssociation);
                _logger.WriteMessage("Decrypting Finished");
                IFileLoader fileLoader = (IFileLoader)player;
                fileLoader.Load(decryptedFilePath);
                _logger.WriteMessage("Finished loading");
            }
            else if (player is INoAssetsLoader)
            {
                ((INoAssetsLoader)player).Load(((ContentPlaylistAsset)channelAssetAssociation.PlaylistAsset).Message);
            }
        }

        void ScreenSaver_HandleCreated(object sender, EventArgs e)
        {
            _logger.WriteTimestampedMessage("ScreenSaver_HandleCreated");
            // set the form and control bounds on Load, with the updated form bounds
            this.Bounds = Screen.AllScreens[_screenNo].Bounds;
            _faderForm.Bounds = this.Bounds;
        }

        private void ScreenSaver_Load(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle();

            // set the form and control bounds on Load, with the updated form bounds
            this.Bounds = Screen.AllScreens[_screenNo].Bounds;

            _logger.WriteTimestampedMessage("successfully set the size and position of the Screensaver to the monitor dimensions.");

            rect = new Rectangle(0, 0, this.Bounds.Width, this.Bounds.Height);
            foreach (var player in _players.AllPlayers())
                player.Control.Bounds = rect;

            _logger.WriteTimestampedMessage("successfully set the size and position of the players to the Screensaver dimensions.");

            _faderForm.Bounds = this.Bounds;

            _logger.WriteTimestampedMessage("successfully set the size and position of the fader form to the Screensaver dimensions.");

            foreach (IPlayer player in _players.AllPlayers())
                player.CompleteSetup();

            // TODO: maintain QuickTime aspect ratio for non primary monitors

            _logger.WriteTimestampedMessage("fader form set to Layered Window to be used by the between-slides fader.");
            WinAPI.AnimateWindow(_faderForm.Handle, _fadeOutToBlack, WinAPI.AW_ACTIVATE | WinAPI.AW_BLEND);
            _logger.WriteTimestampedMessage("fader form faded from desktop to black.");
            _ddFormFader.setTransparentLayeredWindow();

            _faderForm.Show();
            workerTimer.Interval = 100;
            workerTimer.Start();
            _logger.WriteTimestampedMessage("successfully started the Display thread.");
        }

        private bool IsPrimaryMonitor()
        {
            return Screen.AllScreens[_screenNo].Primary;
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

        private void WorkerTimerTick(object sender, EventArgs e)
        {
            //workerTimer.Enabled = false;
            try
            {
                if (_runScreenSaver && _previousSlide.State != SlideState.Creating)
                {
                    switch (_previousSlide.State)
                    {
                        case SlideState.NewContentRequired:
                            var callBack = new WaitCallback(SelectAndLoadAsset);
                            ThreadPool.QueueUserWorkItem(callBack);
                            break;
                        case SlideState.ReadyForDisplay:
                            if (HasCurrentSlideFinishedPlaying())
                            {
                                LogImpressionForCurrent();
                                FlipAssetPlayer();
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.WriteError(ex);
 
            }
            //workerTimer.Enabled = true;
        }
    }

}
