using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.ScreenSaver.Players;

namespace OxigenIIAdvertising.ScreenSaver
{
    internal class Slide
    {
        private readonly Dictionary<PlayerType, IPlayer> _players;
        private volatile SlideState _slideState = SlideState.NewContentRequired;

        public Slide(Dictionary<PlayerType, IPlayer> players)
        {
            _players = players;
        }

        public SlideState State { get { return _slideState; } }

        public ChannelAssetAssociation ChannelAssetAssociation { get; set; }

        public Dictionary<PlayerType, IPlayer> Players
        {
            get { return _players; }
        }

        public void ReadyForDisplay()
        {
            _slideState = SlideState.ReadyForDisplay;
        }

        public void NewContentRequired()
        {
            _slideState = SlideState.NewContentRequired;
        }

        public void Displaying()
        {
            _slideState = SlideState.Displaying;
        }

        public void Creating()
        {
            _slideState = SlideState.Creating;
        }
    }

    internal enum SlideState
    {
        NewContentRequired,
        Creating,
        ReadyForDisplay,
        Displaying
    }
}
