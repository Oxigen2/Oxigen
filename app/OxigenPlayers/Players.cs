using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Win32;
using OxigenIIAdvertising.AppData;
using RegistryAccess;

namespace OxigenPlayers
{
    public class Players : IDisposable
    {
        private static readonly Dictionary<PlayerType, PlayerNotExistsResponse> _playerNotExistResponses = GetPlayerNotExistsResponses();
        
        public Players()
        {
            APlayers = new Dictionary<PlayerType, IPlayer>();
            BPlayers = new Dictionary<PlayerType, IPlayer>();
        }

        public static Dictionary<PlayerType, PlayerNotExistsResponse> PlayerNotExistResponses
        {
            get { return _playerNotExistResponses; }
        }

        public IEnumerable<IPlayer> AllPlayers()
        {
            foreach (IPlayer player in APlayers.Values)
                yield return player;

            foreach (IPlayer player in BPlayers.Values)
                yield return player;

            yield break;
        }

        public void Add(PlayerType playerType, IPlayer player)
        {
            APlayers.Add(playerType, player);
            BPlayers.Add(playerType, player);
        }

        public bool Exists(PlayerType playerType)
        {
          return APlayers.ContainsKey(playerType); // could be interrogating BPlayer instead; both dictionaries 
        }

        public Dictionary<PlayerType, IPlayer> APlayers;
        public Dictionary<PlayerType, IPlayer> BPlayers;

        public void Dispose() {
            foreach (IPlayer player in AllPlayers())
            {
                player.Dispose();
            }
        }

        private static Dictionary<PlayerType, PlayerNotExistsResponse> GetPlayerNotExistsResponses()
        {
            Dictionary<PlayerType, PlayerNotExistsResponse> playerNotExistResponses = new Dictionary<PlayerType, PlayerNotExistsResponse>();

            playerNotExistResponses.Add(PlayerType.Flash, new PlayerNotExistsResponse("You do not have ActiveX flash installed. Press space bar to go to the Flash download site.",
                                                                                                      "http://get.adobe.com/flashplayer"));

            playerNotExistResponses.Add(PlayerType.VideoNonQT, new PlayerNotExistsResponse("You do not have Windows Media Player 10 or over installed. Press space bar to go to the Windows Media Player download site.",
                                                                                          "http://windows.microsoft.com/en-US/windows/products/windows-media-player"));

            playerNotExistResponses.Add(PlayerType.VideoQT, new PlayerNotExistsResponse("You do not have QuickTime 7 or over installed. Press space bar to go to the Quicktime download site.",
                                                                                          "http://www.apple.com/quicktime/download/"));

            return playerNotExistResponses;
        }

        public static bool QuickTimeRightVersionExists()
        {
            return FileRightVersionExists(@"HKEY_LOCAL_MACHINE\SOFTWARE\Apple Computer, Inc.\QuickTime", "InstallDir", "QuickTimePlayer.exe", 7)
            || FileRightVersionExists(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Apple Computer, Inc.\QuickTime", "InstallDir", "QuickTimePlayer.exe", 7);
        }

        public static bool WindowsMediaPlayerRightVersionExists()
        {
            return FileRightVersionExists(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MediaPlayer", "Installation Directory", "wmplayer.exe", 10);
        }

        private static bool FileRightVersionExists(string registryKey, string registryValue, string executableToCheck, int minimumVersion)
        {
            // get the installation path of the executable
            RegistryKey installationPathKey = GenericRegistryAccess.GetRegistryKeyReadOnly(registryKey);

            if (installationPathKey == null)
                return false;

            object installationPathValue = installationPathKey.GetValue(registryValue);

            if (installationPathValue == null)
                return false;

            string executableFullPath = installationPathValue.ToString() + "\\" + executableToCheck;

            int version;
            string stringVersion;

            try
            {
                FileVersionInfo fv = FileVersionInfo.GetVersionInfo(executableFullPath);

                stringVersion = fv.FileVersion.Split(new char[] { '.' })[0];
            }
            catch
            {
                return false;
            }

            if (!int.TryParse(stringVersion, out version))
                return false;

            if (version >= minimumVersion)
                return true;

            return false;
        }
    }

    public struct PlayerNotExistsResponse
    {
        private string _message;
        private string _link;

        public PlayerNotExistsResponse(string message, string link)
        {
            _link = link;
            _message = message;
        }

        public string Message
        {
            get { return _message; }
        }

        public string Link
        {
            get { return _link; }
        }
    }
}
