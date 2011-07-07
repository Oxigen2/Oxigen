using System;
using WMPLib;

namespace Oxigen.DurationDetectors
{
    public class WindowsMediaFormatsFileDurationDetector : IFileDurationDetector
    {
        private WindowsMediaPlayerClass _wmp;
 
        public WindowsMediaFormatsFileDurationDetector()
        {
            _wmp = new WindowsMediaPlayerClass();
        }

        public double GetDurationInSeconds(string path)
        {
            IWMPMedia mediaInfo = _wmp.newMedia(path);
            
            return Math.Round(mediaInfo.duration, 2);
        }
    }
}