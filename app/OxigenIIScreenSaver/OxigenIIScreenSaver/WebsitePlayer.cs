using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.LoggerInfo;

namespace OxigenIIAdvertising.ScreenSaver
{
    public class WebsitePlayer : IPlayer, IURLLoader
    {
        private WebBrowser _control;
        private LoggerInfo.Logger _logger;

        public WebsitePlayer(Logger logger)
        {
            _control = new WebBrowser();
            _control.ScriptErrorsSuppressed = true;
            _logger = logger;
        }

        public void Play(bool primaryMonitor)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void ReleaseAssetForDesktop()
        {
            // not applicable
        }

        public bool IsReadyToPlay
        {
            get { throw new NotImplementedException(); }
        }

        public void Load(string url)
        {
            _control.Navigate(url);
            while (_control.ReadyState != WebBrowserReadyState.Complete) ;
        }

        public System.Windows.Forms.Control Control
        {
            get { throw new NotImplementedException(); }
        }


        public void ReleaseAssetForTransition() {
            _control.DocumentText = "<html><body style='background-color:Black'></body></html>";
        }
    }
}
