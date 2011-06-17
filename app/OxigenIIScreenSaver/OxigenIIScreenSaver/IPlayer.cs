using System.IO;
using System.Windows.Forms;
using OxigenIIAdvertising.AppData;

namespace OxigenIIAdvertising.ScreenSaver
{
    public interface IPlayer
    {
        void Init();
        void Play(bool primaryMonitor);
        void Stop();
        void ReleaseAssetForDesktop();
        void ReleaseAssetForTransition();
        bool IsReadyToPlay { get; }
        Control Control { get; }
    }

    public interface IFileLoader
    {
        void Load(string filepath);
    }

    public interface IStreamLoader
    {
        void Load(Stream stream);
    }

    public interface IURLLoader
    {
        void Load(string url);
    }

    public interface INoAssetsLoader
    {
        void Load(string message);
    }
    
}
    
