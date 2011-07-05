using System.IO;

namespace Oxigen.DurationDetectors
{
    public class QuicktimeFileDurationDetector : IFileDurationDetector
    {
        private const int MVHD_HEADER_NAME_LENGTH = 4;
        private int _numBytesRead;
        private int _numBytesToRead;
        private byte[] _bytes;

        public double GetDurationInSeconds(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                _numBytesToRead = (int)fs.Length;
                 _bytes = new byte[_numBytesToRead];

                while (_numBytesToRead > 0)
                {
                    int n = fs.Read(_bytes, _numBytesRead, _numBytesToRead);

                    if (n == 0)
                        break;

                    _numBytesRead += n;
                    _numBytesToRead -= n;

                    if (_numBytesRead <= MVHD_HEADER_NAME_LENGTH) continue;
                    if (!MvhdAtomFound()) continue;


                }
            }
        }

        private bool MvhdAtomFound()
        {
            for (int i = 4; i < _numBytesRead; i++)
            {
                if (_bytes[i - 3] == 0x6C && _bytes[i - 2] == 0x6D && _bytes[i - 1] == 0x76 && _bytes[i] == 0x68)
                    return true;
            }

            return false;
        }
    }
}
