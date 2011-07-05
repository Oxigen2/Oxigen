using System;
using System.IO;
using System.Text;

namespace Oxigen.DurationDetectors
{
    public class QuicktimeFileDurationDetector : IFileDurationDetector
    {
        private const int MVHD_HEADER_NAME_LENGTH = 5;
        private const int USEFUL_BYTE_OFFSET = 12;
        private int _numBytesRead;
        private byte[] _bytes;
        private bool _mvhdAtomHeaderNamePassed;
        
        public double GetDurationInSeconds(string path)
        {
            byte[] timeScaleBytes = new byte[4];
            byte[] durationBasedOnTimeScaleBytes = new byte[4]; // In QT clips, time is specified in terms of the movie time scale
            int usefulByteCount = 0;
            int timeScaleBytesIndex = 0;
            int durationBasedOnTimeScaleBytesIndex = 0;

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                int fileStreamLength = (int)fs.Length;
                _bytes = new byte[fileStreamLength];
                
                while (_numBytesRead < fileStreamLength)
                {
                    int n = fs.Read(_bytes, _numBytesRead, 1);

                    if (n == 0)
                        break;

                    _numBytesRead += n;

                    if (!MvhdAtomHeaderNamePassed()) continue;

                    usefulByteCount++;

                    if (usefulByteCount > USEFUL_BYTE_OFFSET && usefulByteCount <= USEFUL_BYTE_OFFSET + 4)
                    {
                        timeScaleBytes[timeScaleBytesIndex] = _bytes[_numBytesRead - 1];
                        timeScaleBytesIndex++;
                    }

                    if (usefulByteCount > USEFUL_BYTE_OFFSET + 4 && usefulByteCount <= USEFUL_BYTE_OFFSET + 8)
                    {
                        durationBasedOnTimeScaleBytes[durationBasedOnTimeScaleBytesIndex] = _bytes[_numBytesRead - 1];
                        durationBasedOnTimeScaleBytesIndex++;
                    }

                    if (usefulByteCount > USEFUL_BYTE_OFFSET + 8)
                        break;
                }
            }

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timeScaleBytes);
                Array.Reverse(durationBasedOnTimeScaleBytes);
            }

            int timeScale = BitConverter.ToInt32(timeScaleBytes, 0);
            int durationBasedOnTimeScale = BitConverter.ToInt32(durationBasedOnTimeScaleBytes, 0);

            return (double)durationBasedOnTimeScale / timeScale;
        }

        private bool MvhdAtomHeaderNamePassed()
        {
            if (_numBytesRead < MVHD_HEADER_NAME_LENGTH)
                return false;

            if (_bytes[_numBytesRead - 2] == 0x64 && _bytes[_numBytesRead - 3] == 0x68 && _bytes[_numBytesRead - 4] == 0x76 && _bytes[_numBytesRead - 5] == 0x6D)
                _mvhdAtomHeaderNamePassed = true;

            return _mvhdAtomHeaderNamePassed;
        }
    }
}
