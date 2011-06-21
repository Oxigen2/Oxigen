using System;
using System.Windows.Forms;
using OxigenIIAdvertising.LoggerInfo;
using OxigenPlayers;

namespace OxigenIIAdvertising.ScreenSaver
{
    public class WebsitePlayer : IPlayer, IURLLoader
    {
        private WebBrowser _control;
        private Logger _logger;

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
            // not applicable
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

        public Control Control
        {
            get { return _control; }
        }


        public void ReleaseAssetForTransition() {
            _control.DocumentText = "<html><body style='background-color:Black'></body></html>";
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _control.Dispose();
        }


        public void SetupComplete()
        {
            _control.ScrollBarsEnabled = false;
        }
    }
}
