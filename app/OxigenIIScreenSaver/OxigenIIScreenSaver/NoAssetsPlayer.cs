using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.NoAssetsAnimator;

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
            throw new NotImplementedException();
        }

        public void Stop() {
            throw new NotImplementedException();
        }

        public void ReleaseAssetForDesktop() {
            throw new NotImplementedException();
        }

        public void ReleaseAssetForTransition() {
            throw new NotImplementedException();
        }

        public bool IsReadyToPlay {
            get { throw new NotImplementedException(); }
        }

        public Control Control {
            get { throw new NotImplementedException(); }
        }

        public void Load(string message)
        {
            _control.Message = message;
        }
    }
}
