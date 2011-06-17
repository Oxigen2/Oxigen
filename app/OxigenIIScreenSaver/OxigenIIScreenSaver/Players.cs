using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OxigenIIAdvertising.AppData;

namespace OxigenIIAdvertising.ScreenSaver
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
            {
                yield return player;
            }

            foreach (IPlayer player in BPlayers.Values)
            {
                yield return player;
            }

            yield break;
        }

        public void Add(PlayerType playerType, IPlayer player)
        {
            APlayers.Add(playerType, player);
            BPlayers.Add(playerType, player);
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
            Dictionary<PlayerType, PlayerNotExistsResponse> playerNotExistResponses =
                new Dictionary<PlayerType, PlayerNotExistsResponse>();

            playerNotExistResponses.Add(PlayerType.Flash, new PlayerNotExistsResponse("You do not have ActiveX flash installed. Press space bar to go to the Flash download site.",
                                                                                                      "http://get.adobe.com/flashplayer"));

            playerNotExistResponses.Add(PlayerType.VideoNonQT, new PlayerNotExistsResponse("You do not have Windows Media Player 10 or over installed. Press space bar to go to the Windows Media Player download site.",
                                                                                          "http://windows.microsoft.com/en-US/windows/products/windows-media-player"));

            playerNotExistResponses.Add(PlayerType.VideoQT, new PlayerNotExistsResponse("You do not have QuickTime 7 or over installed. Press space bar to go to the Quicktime download site.",
                                                                                          "http://www.apple.com/quicktime/download/"));

            return playerNotExistResponses;
        }

        internal static bool FlashActiveXExists()
        {
            return GenericRegistryAccess.RegistryKeyExists(@"HKEY_LOCAL_MACHINE\SOFTWARE\Macromedia\FlashPlayerActiveX") ||
              GenericRegistryAccess.RegistryKeyExists(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Macromedia\FlashPlayerActiveX");
        }

        internal static bool QuickTimeRightVersionExists()
        {
            return FileRightVersionExists(@"HKEY_LOCAL_MACHINE\SOFTWARE\Apple Computer, Inc.\QuickTime", "InstallDir", "QuickTimePlayer.exe", 7)
            || FileRightVersionExists(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Apple Computer, Inc.\QuickTime", "InstallDir", "QuickTimePlayer.exe", 7);
        }

        internal static bool WindowsMediaPlayerRightVersionExists()
        {
            return FileRightVersionExists(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MediaPlayer", "Installation Directory", "wmplayer.exe", 10);
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
