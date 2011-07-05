using System;

namespace Oxigen.DurationDetectors
{
    public class DurationDetectorFactory
    {
        public IFileDurationDetector CreateDurationDetector(string extension)
        {
            switch(extension)
            {
                case ".avi":
                    goto case ".wmv";
                case ".wmv":
                    return new WindowsMediaFormatsFileDetector();
                    break;
                case ".mov":
                    return new QuicktimeFileDurationDetector();
                default:
                    throw new ArgumentException("Extension " + extension + " is not valid.");
            }
        }
    }
}