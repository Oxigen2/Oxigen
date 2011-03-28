using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OxigenIIAdvertising.EncryptionDecryption;
using System.Security.Cryptography;

namespace OxigenIIAdvertising.FileLocker
{
  public static class Locker
  {
    /// <summary>
    ///  Reads raw (not serialized classes) and decrypts log files to a memory stream. If file doesn't exist, it is created.
    ///  If a file is not critical, i.e. it is acceptable for a requested file to not be found and filled with data 
    ///  and the file is indeed not found or is empty, lock is released from the file (the fileStream is closed and dereferenced).
    ///  Can be used for serialized classes as long as deserialization is not needed (i.e. upload to server).
    /// </summary>
    /// <param name="fileStream">stream reference to handle (and lock) log file. When the method returns, the fileStream will have a hold on the file unless there is no data in the file, in which case the fileStream is closed and dereferenced.</param>
    /// <param name="inputPath">path to log file</param>
    /// <param name="decryptionPassword">password to decrypt log file</param>
    /// <param name="bCritical">File is expected to be found. If this is true, throws an error to the caller if file is not found</param>
    /// <returns>a MemoryStream that has the log file or null if file is not critical and is not found or is empty</returns>
    /// <exception cref="CryptographicException">File is corrupted</exception>
    /// <exception cref="IOException">File is locked by another process</exception>
    /// <exception cref="DirectoryNotFoundException">Directory is not found</exception>
    public static MemoryStream ReadDecryptFile(ref FileStream fileStream, string inputPath, string decryptionPassword, bool bCritical)
    {
      return ReadDecryptFile(ref fileStream, inputPath, decryptionPassword, bCritical, false);
    }

    /// <summary>
    ///  Reads raw (not serialized classes) and decrypts log files to a memory stream. If file doesn't exist, it is created.
    ///  Can be used for serialized classes as long as deserialization is not needed (i.e. upload to server).
    /// </summary>
    /// <param name="fileStream">stream reference to handle (and lock) log file. When the method returns, the fileStream will have a hold on the file unless there is no data in the file, if it pre-existed before this method created it</param>
    /// <param name="inputPath">path to log file</param>
    /// <param name="decryptionPassword">password to decrypt log file</param>
    /// <param name="bCritical">File is expected to be found. If this is true, throws an error to the caller if file is not found</param>
    /// <param name="bKeepLock">In non-critical files (where it is acceptable for files not to be found or be empty), if this is set to true, the fileStream won't be released. If it is set to false, and file or data is not found, the fileStream is closed and dereferenced. Use when a lock is needed on the file after the method exits (e.g. when reading a log file and then appending data on it).</param>
    /// <returns>a MemoryStream that has the log file or null if file is not critical and is not found or is empty</returns>
    /// <exception cref="CryptographicException">File is corrupted</exception>
    /// <exception cref="IOException">File is locked by another process</exception>
    /// <exception cref="DirectoryNotFoundException">Directory is not found</exception>
    public static MemoryStream ReadDecryptFile(ref FileStream fileStream, string inputPath, string decryptionPassword, bool bCritical, bool bKeepLock)
    {
      byte[] dataToDecrypt = Locker.ReadToBuffer(ref fileStream, inputPath);

      // if the file is critical (i.e. important serialized object), File must exist and data must be inside.
      // if it isn't critical, release locks and return null
      // any other eventuality is handled in the outside lock
      if (!bCritical)
      {
        // no data (decrypted file is empty), exit
        if (dataToDecrypt.Length == 0)
        {
          if (!bKeepLock)
            ClearFileStream(ref fileStream);

          return null;
        }
      }

      byte[] decryptedData = null;

      try
      {
        decryptedData = Cryptography.Decrypt(dataToDecrypt, decryptionPassword);
      }
      catch (CryptographicException ex)
      {
        // file is corrupted, truncate it and throw an exception
        fileStream.SetLength(0);
        Locker.ClearFileStream(ref fileStream);

        throw new CryptographicException("File " + inputPath + " is corrupted.", ex);
      }

      // if the file is critical (i.e. important serialized object), File must exist and data must be inside.
      // if it isn't critical, release locks and return null
      // any other eventuality is handled in the outside lock
      if (!bCritical)
      {
        // no data (decrypted data are empty), exit
        if (decryptedData.Length == 0)
        {
          if (!bKeepLock)
            ClearFileStream(ref fileStream);

          return null;
        }
      }

      MemoryStream decryptedDataStream = new MemoryStream(decryptedData);

      return decryptedDataStream;
    }    

    /// <summary>
    ///  Reads a file to an array of bytes
    /// </summary>
    /// <param name="fileStream">stream reference to handle (and lock) log file</param>
    /// <param name="path">path to log file</param>
    /// <returns>an array of bytes containing the log file</returns>
    private static byte[] ReadToBuffer(ref FileStream fileStream, string path)
    {
      try
      {
        // open log file for reading, writing (in order to truncate later), fileshare: none -> lock the file
        // FileMode.OpenOrCreate if file doesn't exist, create it
        fileStream = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
      }
      catch (Exception ex)
      {
        ClearFileStream(ref fileStream);

        throw ex;
      }

      int length = (int)fileStream.Length;

      byte[] buffer = new byte[length];

      int noBytesToRead = length;

      int noBytesRead = 0;

      int n = -1;

      while (noBytesToRead > 0)
      {
        try
        {
          n = fileStream.Read(buffer, noBytesRead, noBytesToRead);
        }
        catch (Exception ex)
        {
          ClearFileStream(ref fileStream);

          throw ex;
        }

        // Break when the end of the file is reached.
        if (n == 0)
          break;

        noBytesRead += n;
        noBytesToRead -= n;
      }

      return buffer;
    }

    /// <summary>
    /// Encrypts a string into a file that exists and a lock is on it, then releases the lock.
    /// </summary>
    /// <param name="fileStream">active FileStream that is locking the file. fileStream will be closed at the end of the operation</param>
    /// <param name="stringToEncryptAndWrite">string to encrypt and write to disk</param>
    /// <param name="encryptionPassword">password with which to encrypt the file</param>
    /// <exception cref="IOException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    public static void WriteEncryptString(ref FileStream fileStream, string stringToEncryptAndWrite, string encryptionPassword)
    {
      WriteEncryptString(ref fileStream, stringToEncryptAndWrite, encryptionPassword, false);
    }

    /// <summary>
    /// Encrypts a string into a file that exists and a lock is on it.
    /// </summary>
    /// <param name="fileStream">active FileStream that is locking the file. fileStream will be closed at the end of the operation</param>
    /// <param name="stringToEncryptAndWrite">string to encrypt and write to disk</param>
    /// <param name="encryptionPassword">password with which to encrypt the file</param>
    /// <param name="bKeepLock">if this is set to false, the lock won't be released.</param>
    /// <exception cref="IOException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    public static void WriteEncryptString(ref FileStream fileStream, string stringToEncryptAndWrite, string encryptionPassword, bool bKeepLock)
    {
      byte[] encryptedBytes = Cryptography.Encrypt(ASCIIEncoding.ASCII.GetBytes(stringToEncryptAndWrite), encryptionPassword);

      int encryptedBytesLength = encryptedBytes.Length;

      try
      {
        fileStream.SetLength(0); // truncate file
        fileStream.Write(encryptedBytes, 0, encryptedBytesLength);
      }
      finally
      {
        if (!bKeepLock)
          ClearFileStream(ref fileStream);
      }
    }

    /// <summary>
    /// Closes the file stream, if it's open, and unlocks the underlying file.
    /// </summary>
    /// <param name="fileStream">the FileStream to close</param>
    public static void ClearFileStream(ref FileStream fileStream)
    {
      if (fileStream != null)
      {
        fileStream.Close();
        fileStream.Dispose();
        fileStream = null;
      }
    }
  }
}
