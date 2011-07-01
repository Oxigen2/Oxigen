using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Cache;
using System.IO;
using OxigenIIAdvertising.XMLSerializer;
using OxigenIIAdvertising.Exceptions;
using OxigenIIAdvertising.LoggerInfo;
using OxigenIIAdvertising.PlaylistLogic;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.UserSettings;
using System.Configuration;
using InterCommunicationStructures;
using OxigenIIAdvertising.FileRights;
using System.Diagnostics;

namespace OxigenIIAdvertising.ContentExchanger
{
    public class Exchanger
    {
        private string _userGUID = "";
        private string _machineGUID = "";
        private string _systemPassPhrase = "";

        private string _appDataPath = "";
        private string _playlistPath = "";
        private string _generalDataPath = "";
        private string _advertDataPath = "";
        private string _demographicDataPath = "";
        private string _userChannelSubscriptionsPath = "";
        private string _assetPath = "";
        private string _channelDataPath = "";
        private string _userSettingsPath = "";
        private string _debugFilePath = "";
        private string _cdnSubdomain = "";
        private string _mDataSubdomain = "";
        private string _password = "";

        private int _daysToKeepAssetFiles = -1;
        private long _assetFolderSize = -1;
        private float _defaultDisplayDuration = -1F;

        Logger _logger = null;

        private bool _bGlobalsSet = false;
        private System.ComponentModel.BackgroundWorker _worker = null;
        private bool _verboseMode = false;


        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
           out ulong lpFreeBytesAvailable,
           out ulong lpTotalNumberOfBytes,
           out ulong lpTotalNumberOfFreeBytes);

        public Exchanger(System.ComponentModel.BackgroundWorker worker)
        {
            _worker = worker;
            _verboseMode = true;
            _bGlobalsSet = SetGlobals();
        }

        public Exchanger()
        {
            _bGlobalsSet = SetGlobals();
        }

        /// <summary>
        /// Updates application data and asset files from the relay server, generates the user's playlist
        /// and cleans up any old files
        /// </summary>
        internal ExchangeStatus Execute()
        {
            CELog log = null;
            ExchangeStatus status = new ExchangeStatus();
            bool bContentDownloaded = false;

            if (!_bGlobalsSet)
            {
                _logger.WriteTimestampedMessage("Error Setting Globals. Exiting Content Exchanger");
                status.ExitWithError = true;
                status.ContentDownloaded = false;

                log = new CELog(DateTime.Now, status.ContentDownloaded, status.ExitWithError);
                log.SaveLog();

                return status;
            }

            status.LowDiskSpace = IsDiskSpaceLow();

            if (status.LowDiskSpace)
            {
                _logger.WriteTimestampedMessage("Low Disk Space. Exiting Content Exchanger");
                status.ExitWithError = true;
                status.ContentDownloaded = false;

                log = new CELog(DateTime.Now, status.ContentDownloaded, status.ExitWithError);
                log.SaveLog();

                return status;
            }

            if (!DirStructureOK())
            {
                _logger.WriteTimestampedMessage("Error checking disk structure. Exiting Content Exchanger");
                status.ExitWithError = true;
                status.ContentDownloaded = false;

                log = new CELog(DateTime.Now, status.ContentDownloaded, status.ExitWithError);
                log.SaveLog();

                return status;
            }

            if (!FileAccessRights())
            {
                _logger.WriteTimestampedMessage("Insufficient file access rights. Exiting Content Exchanger");
                status.ExitWithError = true;
                status.ContentDownloaded = false;

                log = new CELog(DateTime.Now, status.ContentDownloaded, status.ExitWithError);
                log.SaveLog();

                return status;
            }

            try
            {
                // update config files
                bool bCancelled = false;

                using (WebClient client = new WebClient())
                {
                    if (client.Proxy != null)
                        client.Proxy.Credentials = CredentialCache.DefaultCredentials;

                    UpdateConfigFiles(client, ref bCancelled);

                    if (bCancelled)
                    {
                        status.ExitWithError = false;

                        log = new CELog(DateTime.Now, status.ContentDownloaded, status.ExitWithError);
                        log.SaveLog();

                        return status;
                    }

                    // raed the downloaded channel subscriptions
                    ChannelSubscriptions channelSubscriptions = GetChannelSubscriptions();

                    if (channelSubscriptions == null)
                    {
                        _logger.WriteTimestampedMessage("No channel subscripitions found. Exiting Content Exchanger");

                        ReportProgress(100, 100);
                        status.ExitWithError = false;
                        status.ContentDownloaded = false;

                        log = new CELog(DateTime.Now, status.ContentDownloaded, status.ExitWithError);
                        log.SaveLog();

                        return status;
                    }

                    channelSubscriptions = FilterChannelSubscriptionsByLock(channelSubscriptions);

                    // get the channel data files
                    GetChannelDataFiles(channelSubscriptions, ref bCancelled, client);

                    if (bCancelled)
                    {
                        status.ExitWithError = false;

                        log = new CELog(DateTime.Now, status.ContentDownloaded, status.ExitWithError);
                        log.SaveLog();

                        return status;
                    }

                    // Generate Playlist      
                    Playlist playlist = null;

                    ReportProgress(0, 20, "Creating the local playlist");

                    try
                    {
                        playlist = PlaylistMaker.CreatePlaylist(_channelDataPath,
                                                                _advertDataPath,
                                                                _demographicDataPath,
                                                                _playlistPath,
                                                                _password,
                                                                _defaultDisplayDuration,
                                                                channelSubscriptions,
                                                                _logger);
                    }
                    catch (Exception ex)
                    {
                        _logger.WriteError(ex);
                        _logger.WriteTimestampedMessage("Exiting Content Exchanger");
                        ReportProgress(100, 100);
                        status.ExitWithError = true;
                        status.ContentDownloaded = false;

                        log = new CELog(DateTime.Now, status.ContentDownloaded, status.ExitWithError);
                        log.SaveLog();

                        return status;
                    }

                    ReportProgress(100, 30);

                    // clean up unused files
                    CleanUpUnusedFiles(channelSubscriptions, playlist);

                    if (_worker != null && _worker.CancellationPending)
                    {
                        status.ExitWithError = false;

                        log = new CELog(DateTime.Now, status.ContentDownloaded, status.ExitWithError);
                        log.SaveLog();

                        return status;
                    }

                    bool bLowAssetSpace = false;

                    // Get content and advert files
                    bool bAssetFilesSuccessful = GetAssetFiles(playlist, ref bLowAssetSpace, ref bContentDownloaded,
                                                               ref bCancelled, client);

                    if (bCancelled)
                    {
                        status.ContentDownloaded = bContentDownloaded;
                        status.ExitWithError = false;

                        log = new CELog(DateTime.Now, status.ContentDownloaded, status.ExitWithError);
                        log.SaveLog();

                        return status;
                    }

                    status.ContentDownloaded = bContentDownloaded;
                    status.ExitWithError = !bAssetFilesSuccessful;
                    status.LowAssetSpace = bLowAssetSpace;
                }
            }
            catch (Exception ex)
            {
                _logger.WriteError(ex);
                status.ExitWithError = true;
            }

            _logger.WriteTimestampedMessage("Exiting Content Exchanger");

            ReportProgress(100, 100);

            log = new CELog(DateTime.Now, status.ContentDownloaded, status.ExitWithError);
            log.SaveLog();

            return status;
        }

        internal static bool IsProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);

            if (processes.Length > 0)
                return true;

            return false;
        }

        private ChannelSubscriptions FilterChannelSubscriptionsByLock(ChannelSubscriptions channelSubscriptions)
        {
            ChannelSubscriptions newSubscriptions = new ChannelSubscriptions();

            if (channelSubscriptions == null || channelSubscriptions.SubscriptionSet.Count == 0)
                return newSubscriptions;

            foreach (ChannelSubscription subscription in channelSubscriptions.SubscriptionSet)
            {
                if (!subscription.Locked)
                    newSubscriptions.SubscriptionSet.Add(subscription);
            }

            return newSubscriptions;
        }

        private bool IsDiskSpaceLow()
        {
            ulong freeBytesAvail;
            ulong totalNumOfBytes;
            ulong totalNumOfFreeBytes;

            GetDiskFreeSpaceEx(Path.GetPathRoot(_assetPath), out freeBytesAvail, out totalNumOfBytes, out totalNumOfFreeBytes);

            // convert bytes to megabytes and check if they are enough
            if ((freeBytesAvail / 1048576L) < 100L)
                return true;

            return false;
        }

        private bool FileAccessRights()
        {
            // check permissions on all applicable directories for write access
            // if no write access return
            bool bReadableWritableAppDataPath = FileDirectoryRightsChecker.AreFilesReadableWritable(_appDataPath);
            bool bCanCreateDirectoriesPath = FileDirectoryRightsChecker.CanCreateDeleteDirectories(_appDataPath);

            if (!bReadableWritableAppDataPath || !bCanCreateDirectoriesPath)
                return false;

            return true;
        }

        private bool DirStructureOK()
        {
            return Directory.Exists(_assetPath) && Directory.Exists(_channelDataPath);
        }

        private void CleanUpUnusedFiles(ChannelSubscriptions channelSubscriptions, Playlist playlist)
        {
            _logger.WriteTimestampedMessage("Cleaning up unused files");

            ReportProgress(0, 30, "Cleaning up files...");

            // 1. channel data
            try
            {
                CleanUpUnsubscribedChannelFiles(channelSubscriptions);
            }
            catch (Exception ex)
            {
                _logger.WriteError(ex);
            }

            ReportProgress(50, 40);

            // 2. assets
            HashSet<string> playlistAssetsToKeep = GetAssetsToKeep(playlist);

            try
            {
                CleanUpAssetFolder(playlistAssetsToKeep);
            }
            catch (Exception ex)
            {
                _logger.WriteError(ex);
            }

            ReportProgress(100, 50);
        }

        /// <summary>
        /// Gets the files that this run of the Content Exchanger will retain.
        /// </summary>
        /// <param name="playlist">Playlist created on this run of the screen saver.</param>
        /// <returns>A HashSet with the paths of the files that will be kept.</returns>
        public HashSet<string> GetAssetsToKeep(Playlist playlist)
        {
            DateTime dateTimeStamp = DateTime.Now;
            TimeSpan daysToKeepAssetFilesTimeSpan = TimeSpan.FromDays(_daysToKeepAssetFiles);

            HashSet<string> playlistAssetsToKeep = new HashSet<string>();

            // get playlist files. These files will be kept
            foreach (ChannelBucket cb in playlist.ChannelBuckets)
            {
                foreach (PlaylistAsset pa in cb.ContentAssets)
                {
                    if (!playlistAssetsToKeep.Contains(pa.AssetFilename)) // a content asset may be contained in more than one channel bucket so don't enter it twice into the HashSet
                    {
                        _logger.WriteTimestampedMessage("Content will be kept: " + _assetPath + pa.GetAssetFilenameGUIDSuffix() + "\\" + pa.AssetFilename);
                        playlistAssetsToKeep.Add(_assetPath + pa.GetAssetFilenameGUIDSuffix() + "\\" + pa.AssetFilename);
                    }
                }
            }

            foreach (AdvertPlaylistAsset advertPlaylistAsset in playlist.AdvertBucket.AdvertAssets)
            {
                _logger.WriteTimestampedMessage("Advert will be kept: " + _assetPath + advertPlaylistAsset.GetAssetFilenameGUIDSuffix() + "\\" + advertPlaylistAsset.AssetFilename);
                playlistAssetsToKeep.Add(_assetPath + advertPlaylistAsset.GetAssetFilenameGUIDSuffix() + "\\" + advertPlaylistAsset.AssetFilename);
            }

            // from the existing files on disk keep those whose creation date is less or equal than x days ago.
            string[] filesOnDisk = Directory.GetFiles(_assetPath);

            foreach (string file in filesOnDisk)
            {
                FileInfo fi = new FileInfo(file);

                if (dateTimeStamp.Subtract(fi.CreationTime) <= daysToKeepAssetFilesTimeSpan)
                {
                    _logger.WriteTimestampedMessage("File will be kept due to its creation date being less than " + daysToKeepAssetFilesTimeSpan + " days old: " + file);
                    playlistAssetsToKeep.Add(file);
                }
            }

            return playlistAssetsToKeep;
        }

        private void CleanUpUnsubscribedChannelFiles(ChannelSubscriptions channelSubscriptions)
        {
            string[] allChannelDataFilenames = Directory.GetFiles(_channelDataPath, "*.dat");

            int noSubscriptions = channelSubscriptions.SubscriptionSet.Count;

            if (allChannelDataFilenames.Length == 0)
            {
                _logger.WriteTimestampedMessage("No channel data files.");
                return;
            }

            int subscriptionCount = 0;

            // create an array to contain the files that correspond to the updates subscribed channel list
            string[] channelSubscriptionFiles = new string[noSubscriptions];

            foreach (ChannelSubscription channelSubscription in channelSubscriptions.SubscriptionSet)
            {
                channelSubscriptionFiles[subscriptionCount] = _channelDataPath + channelSubscription.ChannelID.ToString() + "_channel.dat";

                subscriptionCount++;
            }

            int noAllChannelDataFileNames = allChannelDataFilenames.Length;

            if (noAllChannelDataFileNames > 0)
            {
                // search the two arrays and clean the files that no longer match the subscription list
                foreach (string existingFileName in allChannelDataFilenames)
                {
                    if (Array.IndexOf<string>(channelSubscriptionFiles, existingFileName) == -1)
                    {
                        _logger.WriteTimestampedMessage("Deleting " + existingFileName);
                        File.Delete(existingFileName);
                    }
                }
            }
        }

        // clean up asset folder
        private void CleanUpAssetFolder(HashSet<string> playlistAssetsToKeep)
        {
            string[] assetFilenames = Directory.GetFiles(_assetPath, "*", SearchOption.AllDirectories);

            int noAssetFilenames = assetFilenames.Length;

            if (noAssetFilenames > 0)
            {
                foreach (string existingFileName in assetFilenames)
                {
                    if (!playlistAssetsToKeep.Contains(existingFileName))
                    {
                        _logger.WriteTimestampedMessage("Deleting: " + existingFileName);
                        File.Delete(existingFileName);
                    }
                }
            }
        }

        void ReportProgress(string message)
        {
            ReportProgress(-1, -1, message);
        }

        void ReportProgress(float taskValue)
        {
            ReportProgress(taskValue, -1, String.Empty);
        }

        void ReportProgress(float taskValue, float overallValue)
        {
            ReportProgress(taskValue, overallValue, String.Empty);
        }

        void ReportProgress(float taskValue, float overallValue, string taskMessage)
        {
            if (_worker == null)
                return;

            _worker.ReportProgress(0, new ProcessStatus(taskValue, overallValue, taskMessage));
        }

        private void UpdateConfigFiles(WebClient client, ref bool bCancelled)
        {
            ReportProgress(1, 0, "Updating configuration files...");

            RequestCacheLevel cacheLevel = _verboseMode ? RequestCacheLevel.Revalidate : RequestCacheLevel.Default;

            IRemoteContentSaver[] savers = new IRemoteContentSaver[4];
            savers[0] = new RemoteFileSaver(_worker, client, _cdnSubdomain + "data/ss_general_data.dat",_generalDataPath, cacheLevel);
            savers[1] = new RemoteFileSaver(_worker, client, _cdnSubdomain + "data/ss_adcond_data.dat", _advertDataPath, cacheLevel);
            savers[2] = new RemoteDynamicContentSaver(_worker, client, _mDataSubdomain + "subscriberinfo/demographicdata/" + _userGUID,
                                           _demographicDataPath, _password, cacheLevel);
            savers[3] = new RemoteDynamicContentSaver(_worker, client, _mDataSubdomain + "subscriberinfo/subscriptions/" + _machineGUID,
                                           _userChannelSubscriptionsPath, _password, cacheLevel);

            int localStep = 25;
            int overallStep = 2;

            foreach (IRemoteContentSaver saver in savers)
            {
                saver.SaveFromRemote();
                ReportProgress(localStep, overallStep);

                if (saver.CancelRequested)
                {
                    bCancelled = true;
                    return;
                }

                localStep += 25;
                overallStep += 3;
            }
        }

        private ChannelSubscriptions GetChannelSubscriptions()
        {
            if (!File.Exists(_userChannelSubscriptionsPath))
            {
                _logger.WriteTimestampedMessage("Channel subscriptions don't exist");
                return null;
            }

            ChannelSubscriptions channelSubscriptions = null;

            try
            {
                channelSubscriptions = (ChannelSubscriptions)Serializer.Deserialize(typeof(ChannelSubscriptions), _userChannelSubscriptionsPath, _systemPassPhrase);
            }
            catch (Exception ex)
            {
                _logger.WriteError(ex);

                return null;
            }

            return channelSubscriptions;
        }

        private void GetChannelDataFiles(ChannelSubscriptions channelSubscriptions, ref bool bCancelled, WebClient client)
        {
            ReportProgress(0, 10, "Updating your subscriptions' playlist");
            _logger.WriteTimestampedMessage("Getting channel data files");

            int noSubscriptions = channelSubscriptions.SubscriptionSet.Count;
            float step = 100F / (float)noSubscriptions;
            float count = 0;

            RequestCacheLevel cacheLevel = _verboseMode ? RequestCacheLevel.Revalidate : RequestCacheLevel.Default;

            client.CachePolicy = new RequestCachePolicy(cacheLevel);

            // get channel data for each channel subscription
            foreach (ChannelSubscription channelSubscription in channelSubscriptions.SubscriptionSet)
            {
                if (_worker != null && _worker.CancellationPending)
                {
                    bCancelled = true;
                    return;
                }

                ReportProgress(count * step, 10, "Updating playlist for Stream: " + channelSubscription.ChannelName);

                // checksum of channel file
                string channelFileFullPath = _channelDataPath + channelSubscription.ChannelID + "_channel.dat";

                try
                {
                    _logger.WriteTimestampedMessage("Attempting to get Channel " + channelSubscription.ChannelID);

                    client.DownloadFile(_cdnSubdomain + "channel/" + channelSubscription.ChannelID + "_channel.dat", channelFileFullPath);
                }
                catch (Exception ex)
                {
                    // write error but do not return. continue with the other channels.
                    _logger.WriteError(ex);
                }
            }

            ReportProgress(100, 20);
        }

        /// <summary>
        /// Sets the global parameters
        /// </summary>
        private bool SetGlobals()
        {
            try
            {
                // hard-code the CDN subdomain as Content Exchanger is not allowed to make changes to app.config at runtime as it doesn't have the necessary permissions.
                _cdnSubdomain = "http://assets.oxigen.net/";
                _mDataSubdomain = "http://mdata.oxigen.net/";
                _appDataPath = ConfigurationSettings.AppSettings["AppDataPath"];

                _debugFilePath = ConfigurationSettings.AppSettings["AppDataPath"] + "SettingsData\\OxigenDebugCE.txt";
                _logger = new Logger("Content Exchanger", _debugFilePath, LoggingMode.Debug);

                _generalDataPath = _appDataPath + "SettingsData\\ss_general_data.dat";
                _userSettingsPath = _appDataPath + "SettingsData\\UserSettings.dat";
                _systemPassPhrase = "password";
                _password = "password";

                _playlistPath = _appDataPath + "SettingsData\\ss_play_list.dat";
                _advertDataPath = _appDataPath + "SettingsData\\ss_adcond_data.dat";
                _demographicDataPath = _appDataPath + "SettingsData\\ss_demo_data.dat";
                _userChannelSubscriptionsPath = _appDataPath + "SettingsData\\ss_channel_subscription_data.dat";
                _assetPath = _appDataPath + "Assets\\";
                _channelDataPath = _appDataPath + "ChannelData\\";
                
                GeneralData generalData = (GeneralData)Serializer.Deserialize(typeof(GeneralData), _generalDataPath, _password);

                _daysToKeepAssetFiles = int.Parse(generalData.Properties["daysToKeepAssetFiles"]);

                User user = (User)Serializer.Deserialize(typeof(User), _userSettingsPath, _password);

                _userGUID = user.UserGUID;
                _machineGUID = user.MachineGUID;
                _assetFolderSize = user.AssetFolderSize;
                _defaultDisplayDuration = user.DefaultDisplayDuration;
            }
            catch (Exception ex)
            {
                if (_logger != null)
                    _logger.WriteError(ex);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Downloads Advert and content files from the relay server
        /// </summary>
        /// <param name="playlist">Playlist objects with required asset objects</param>
        /// <returns>true if file retrieval is successful, false otherwise</returns>
        private bool GetAssetFiles(Playlist playlist, ref bool bLowAssetSpace, ref bool bContentDownloaded, ref bool bCancelled, WebClient client)
        {
            try
            {
                ReportProgress(0, 50, "Preparing to retrieve content...");

                // Get Advert assets
                LoopAndGetAssetFiles<AdvertPlaylistAsset>(playlist.AdvertBucket.AdvertAssets, AssetType.Advert, null, 
                    ref bLowAssetSpace, ref bContentDownloaded, ref bCancelled, client);

                if (bCancelled)
                    return true;

                ReportProgress(100, 55);

                if (bLowAssetSpace)
                    return true;

                int noChannelBuckets = playlist.ChannelBuckets.Count;
                float overallStep = 45F / (float)noChannelBuckets;
                int counter = 0;

                // Get Content assets
                foreach (ChannelBucket cb in playlist.ChannelBuckets)
                {
                    _logger.WriteTimestampedMessage("Attempting to get assets for channel " + cb.ChannelID);

                    LoopAndGetAssetFiles<ContentPlaylistAsset>(cb.ContentAssets, AssetType.Content, cb.ChannelName,
                      ref bLowAssetSpace, ref bContentDownloaded, ref bCancelled, client);

                    if (bCancelled)
                        return true;

                    counter++;

                    ReportProgress(100, 55 + (overallStep * counter));

                    if (bLowAssetSpace)
                        return true;
                }
            }
            catch (Exception ex)
            {
                _logger.WriteError(ex);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Loops through a hash set collection of asset information objects and downloads those assets 
        /// from the relay server 
        /// </summary>
        /// <param name="playlistAssets">The HashSet containing playlist asset information</param>
        /// <param name="assetType">Content or Advert</param>
        /// <param name="checksum">string object to hold checksum calculations</param>
        /// <exception cref="RemoteServerException">thrown when an error occurs on the relay server</exception>
        /// <exception cref="FileNotFoundException">The file cannot be found, such as when mode is FileMode.Truncate or FileMode.Open,
        /// and the file specified by path does not exist. The file must already exist in these modes.</exception>
        /// <exception cref="IOException">An I/O error occurs or the stream has been closed.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.
        /// For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
        private void LoopAndGetAssetFiles<T>(HashSet<T> playlistAssets, AssetType assetType, string channelName,
              ref bool bLowAssetSpace, ref bool bContentDownloaded, ref bool bCancelled, WebClient client) where T : PlaylistAsset
        {
            int noAssets = playlistAssets.Count;

            if (noAssets == 0)
                return;

            float step = 100F / (float)noAssets;
            int counter = 1;

            // check if saving of this slide will exceed allowed size in destination folder
            long directorySize = GetDirectorySize(_assetPath);

            client.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);

            foreach (PlaylistAsset pa in playlistAssets)
            {
                if (_worker != null && _worker.CancellationPending)
                {
                    bCancelled = true;
                    return;
                }

                ReportProgress("Stream " + channelName + ", Content " + counter + " of " + noAssets);

                string assetDir = _assetPath + pa.GetAssetFilenameGUIDSuffix().ToUpper() + "\\";
                string assetFullPath = assetDir + pa.AssetFilename;

                // If file exists, move on to the next one. No need to check if file has changed on the server
                // because that would be a new file under a different database ID.
                if (File.Exists(assetFullPath))
                {
                    _logger.WriteTimestampedMessage("File " + assetFullPath + " exists. Continuing to next file.");
                    continue;
                }

                _logger.WriteTimestampedMessage("File " + assetFullPath + " does not exist.");

                try
                {
                    if (!Directory.Exists(assetDir))
                        Directory.CreateDirectory(assetDir);

                    client.DownloadFile(_cdnSubdomain + "slide/" + pa.AssetFilename, assetFullPath);

                    var fileLength = new FileInfo(assetFullPath).Length;

                    if (fileLength == 0) File.Delete(assetFullPath);

                    directorySize += fileLength;

                    if (directorySize >= _assetFolderSize)
                    {
                        //TODO: delete the oldest files until the file total directory size is with limit
                        File.Delete(assetFullPath);
                        _logger.WriteMessage("downloadedData.Length + directorySize = " + (directorySize) +
                                             ", assetFolderSize - 104857600 = " + (_assetFolderSize - 104857600));
                        bLowAssetSpace = true;
                        return;
                    }

                    if (assetType == AssetType.Content)
                        bContentDownloaded = true;

                }
                catch (Exception ex)
                {
                    if (File.Exists(assetFullPath))
                        File.Delete(assetFullPath);

                    _logger.WriteError(ex);
                }

                if (!string.IsNullOrEmpty(channelName))
                    ReportProgress((float)counter * step);

                counter++;

                _logger.WriteMessage("Step: " + (((float)counter * step)));
            }

            ReportProgress(100);
        }

        public long GetDirectorySize(string path)
        {
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            long size = 0;

            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);

                size += info.Length;
            }

            return size;
        }
    }
}
