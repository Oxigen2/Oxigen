using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Cache;
using OxigenIIAdvertising.EncryptionDecryption;

namespace OxigenIIAdvertising.ContentExchanger
{
    public interface IRemoteContentSaver
    {
        bool CancelRequested { get; }
        void SaveFromRemote();
    }

    public class RemoteFileSaver : IRemoteContentSaver
    {
        private readonly BackgroundWorker _worker;
        private readonly WebClient _client;
        private readonly string _url;
        private readonly string _localPath;
        private readonly RequestCacheLevel _cacheLevel;

        public RemoteFileSaver(BackgroundWorker worker, WebClient client, string url, string localPath, RequestCacheLevel cacheLevel)
        {
            _worker = worker;
            _client = client;
            _url = url;
            _localPath = localPath;
            _cacheLevel = cacheLevel;
        }

        public bool CancelRequested
        {
            get { return _worker != null && _worker.CancellationPending; }
        }

        public void SaveFromRemote()
        {
            _client.CachePolicy = new RequestCachePolicy(_cacheLevel);
            _client.DownloadFile(_url, _localPath);
        }
    }

    public class RemoteDynamicContentSaver : IRemoteContentSaver
    {
        private readonly BackgroundWorker _worker;
        private readonly WebClient _client;
        private readonly string _url;
        private readonly string _localPath;
        protected readonly string _encryptionPassword;
        private readonly RequestCacheLevel _cacheLevel;

        public RemoteDynamicContentSaver(BackgroundWorker worker, WebClient client, string url, string localPath, string encryptionPassword, RequestCacheLevel cacheLevel)
        {
            _worker = worker;
            _client = client;
            _url = url;
            _localPath = localPath;
            _encryptionPassword = encryptionPassword;
            _cacheLevel = cacheLevel;
        }

        public bool CancelRequested
        {
            get { return _worker != null && _worker.CancellationPending; }
        }

        public void SaveFromRemote()
        {
            _client.CachePolicy = new RequestCachePolicy(_cacheLevel);
            byte[] downloadedBytes = _client.DownloadData(_url);
            EncryptByteArrayAndSave(downloadedBytes, _localPath);
        }

        private void EncryptByteArrayAndSave(byte[] buffer, string outputPath)
        {
            byte[] encryptedData = Cryptography.Encrypt(buffer, _encryptionPassword);

            File.WriteAllBytes(outputPath, encryptedData);
        }
    }
}
