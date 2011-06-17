using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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

        private void AddPlayers(Dictionary<PlayerType, IPlayer> players)
        {
            players.Add(PlayerType.Flash, new FlashPlayer());
            players.Add(PlayerType.Image, new ImagePlayer());
            players.Add(PlayerType.VideoQT, new QuicktimePlayer());
            players.Add(PlayerType.VideoNonQT, new WindowsMediaPlayer());
            players.Add(PlayerType.WebSite, new WebsitePlayer());
            players.Add(PlayerType.NoAssetsAnimator, new NoAssetsPlayer());

        }

        public Dictionary<PlayerType, IPlayer> APlayers;
        public Dictionary<PlayerType, IPlayer> BPlayers;
    }

}
