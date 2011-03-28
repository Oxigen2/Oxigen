using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OxigenIIAdvertising.EncryptionDecryption
{
  public static class EncryptionDecryptionHelper
  {
    /// <summary>
    /// Checks if a file is decryptable using a specified password.
    /// </summary>
    /// <param name="filePath">path of the file to check</param>
    /// <param name="password">decryption password</param>
    /// <returns>true if the file is found and is decryptable, false otherwise</returns>
    /// <exception cref="DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
    /// <exception cref="UnauthorizedAccessException">This operation is not supported on the current platform.  -or- path specified a directory.  -or- The caller does not have the required permission.</exception>
    /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
    public static bool TryIfFileDecryptable(string filePath, string password)
    {
      try
      {
        byte[] buffer = File.ReadAllBytes(filePath); // will throw a file not found exception if file doesn't exist

        Cryptography.Decrypt(buffer, password); // will throw a cryptographic exception if file is not decryptable
      }
      catch (FileNotFoundException)
      {
        return false;
      }
      catch (System.Security.Cryptography.CryptographicException)
      {
        return false;
      }

      return true;
    }
  }
}
