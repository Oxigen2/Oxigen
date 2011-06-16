using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.AppData;

namespace OxigenIIAdvertising.ScreenSaver
{
    public class Players
    {
        public Players()
        {
            APlayers = new Dictionary<PlayerType, IPlayer>();
            AddPlayers(APlayers);
            BPlayers = new Dictionary<PlayerType, IPlayer>();
            AddPlayers(BPlayers);
            NoAssetsPlayer = new NoAssetsPlayer();

        }

        private void AddPlayers(Dictionary<PlayerType, IPlayer> players)
        {
            players.Add(PlayerType.Flash, new FlashPlayer());
            players.Add(PlayerType.Image, new ImagePlayer());
            players.Add(PlayerType.VideoQT, new QTPlayer());
            players.Add(PlayerType.VideoNonQT, new WMPlayer());
            players.Add(PlayerType.WebSite, new WebSitePlayer());

        }

//        public NoAssetsPlayer NoAssestsPlayer;
        public Dictionary<PlayerType, IPlayer> APlayers;
        public Dictionary<PlayerType, IPlayer> BPlayers;
    }
}
