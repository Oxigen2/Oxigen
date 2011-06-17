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
        public Players()
        {
            APlayers = new Dictionary<PlayerType, IPlayer>();
            BPlayers = new Dictionary<PlayerType, IPlayer>();
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
    }

}
