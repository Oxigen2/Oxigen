using System.Windows.Forms;

namespace OxigenIIAdvertising.ScreenSaver
{
    public interface IPlayer
    {
        void EnableSound(bool enableSound);
        void Play();
        void Stop();
        void Load(string filePath);
        Control Control { get; }
    }
}
