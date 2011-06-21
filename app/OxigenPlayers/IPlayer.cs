using System;
using System.IO;
using System.Windows.Forms;

namespace OxigenPlayers
{
    public interface IPlayer : IDisposable
    {
        void Init(); // TODO: implement in classes or remove
        void Play(bool primaryMonitor);
        void Stop();
        void ReleaseAssetForDesktop();
        void ReleaseAssetForTransition();
        bool IsReadyToPlay { get; } // TODO: maybe remove
        void SetupComplete();
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
    
