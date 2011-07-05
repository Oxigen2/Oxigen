using WMPLib;

namespace Oxigen.DurationDetectors
{
    public class WindowsMediaFormatsFileDetector : IFileDurationDetector
    {
        private WindowsMediaPlayerClass _wmp;
 
        public WindowsMediaFormatsFileDetector()
        {
            _wmp = new WindowsMediaPlayerClass();
        }

        public double GetDurationInSeconds(string path)
        {
            IWMPMedia mediaInfo = _wmp.newMedia(path);
            
            return mediaInfo.duration;
        }
    }
}