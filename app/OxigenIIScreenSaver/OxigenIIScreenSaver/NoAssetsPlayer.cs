﻿using System;
using System.Windows.Forms;
using OxigenIIAdvertising.NoAssetsAnimator;
using OxigenPlayers;

namespace OxigenIIAdvertising.ScreenSaver
{
    public class NoAssetsPlayer : IPlayer, INoAssetsLoader
    {
        private NoAssetsAnimatorPlayer _control;

        public NoAssetsPlayer()
        {
            _control = new NoAssetsAnimatorPlayer();
        }

        public void Play(bool primaryMonitor) {
            _control.Play();
        }

        public void Stop() {
            // does not apply
        }

        public void ReleaseAssetForDesktop() {
            // does not apply
        }

        public void ReleaseAssetForTransition() {
            // does not apply
        }

        public bool IsReadyToPlay {
            get { throw new NotImplementedException(); }
        }

        public Control Control {
            get { return _control; }
        }

        public void Load(string message)
        {
            _control.Message = message;
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public virtual void Dispose()
        {
             _control.Dispose();
        }

        public void SetupComplete()
        {
            // does not apply
        }
    }
}
