using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.LogStats;
using System.IO;
using OxigenIIAdvertising.XMLSerializer;
using OxigenIIAdvertising.FileLocker;

namespace OxigenIIAdvertising.LogExchanger
{
  /// <summary>
  /// Provides methods to read from a usage count log file and lock it until all operations are complete or one is failed failed
  /// </summary>
  public class UsageCountLogReader
  {
    /// <summary>
    /// Track whether Dispose has been called.
    /// </summary> 
    private bool _bDisposed = false;

    private FileStream _fileStream = null;
    private MemoryStream _memoryStream = null;
    private string _usageCountPath1 = "";
    private string _usageCountPath2 = "";
    private string _decryptionPassword = "";

    /// <summary>
    /// Memory Stream holding the log file to be uploaded to the server
    /// </summary>
    public MemoryStream MemoryStream
    {
      get { return _memoryStream; }
      set { _memoryStream = value; }
    }

    /// <summary>
    /// Sets the file paths and the password to decrypt the usage count file.
    /// </summary>
    /// <param name="usageCountPath1">Path of the first file to try</param>
    /// <param name="usageCountPath2">Path of the second file to try</param>
    /// <param name="decryptionPassword">Password to use in decryption</param>
    public UsageCountLogReader(string usageCountPath1, string usageCountPath2, string decryptionPassword)
    {
      _usageCountPath1 = usageCountPath1;
      _usageCountPath2 = usageCountPath2;
      _decryptionPassword = decryptionPassword;
    }

    /// <summary>
    /// Locks, reads the usage count XML serialized file and decrypts it. If no data found, the lock is released
    /// </summary>
    /// <returns></returns>
    public bool Read()
    {
      try
      {
        try
        {
          _memoryStream = Locker.ReadDecryptFile(ref _fileStream, _usageCountPath1, _decryptionPassword, false);
        }
        catch (InvalidOperationException) // other exception will be thrown to the caller
        {
          _memoryStream = Locker.ReadDecryptFile(ref _fileStream, _usageCountPath2, _decryptionPassword, false);
        }
      }
      catch (Exception ex)
      {
        Locker.ClearFileStream(ref _fileStream);
        throw ex;
      }

      if (_memoryStream == null)
      {
        Locker.ClearFileStream(ref _fileStream);
        return false;
      }

      return true;
    }

    public void TruncateUsageCountFile()
    {
      if (_fileStream != null)
        _fileStream.SetLength(0);
    }

    /// <summary>
    /// Closes file stream and releases all resources used by the OxigenIIAdvertising.LogExchanger.UsageCountLogManipulator.
    /// Memory stream is not closed as it needs to be closed from the server if uploading operation succeeds.
    /// If uploading operation fails, use MemoryStream.Close() in error handling.
    /// </summary>
    public void Dispose()
    {
      Dispose(true);

      GC.SuppressFinalize(this);
    }

    private void Dispose(bool bDisposing)
    {
      if (!_bDisposed)
      {
        if (bDisposing)
        {
          if (_fileStream != null)
          {
            _fileStream.Close();
            _fileStream.Dispose();
          }
        }

        _fileStream = null;
        _bDisposed = true;
      }
    }

    ~UsageCountLogReader()
    {
      Dispose(false);
    }
  }
}
