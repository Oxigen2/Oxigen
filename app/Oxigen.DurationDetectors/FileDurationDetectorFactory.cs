using System;

namespace Oxigen.DurationDetectors
{
    public class FileDurationDetectorFactory
    {
        public IFileDurationDetector CreateDurationDetector(string extension)
        {
            switch(extension)
            {
                case ".avi":
                    goto case ".wmv";
                case ".wmv":
                    return new WindowsMediaFormatsFileDurationDetector();
                    break;
                case ".mp4":
                    goto case ".mov";
                case ".mov":
                    return new QuicktimeFileDurationDetector();
                default:
                    return new UserDefinedDurationDetector();
            }
        }
    }
}