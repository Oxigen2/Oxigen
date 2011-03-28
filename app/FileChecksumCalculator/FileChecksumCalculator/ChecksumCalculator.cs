using System;
using System.Security.Cryptography;
using System.IO;

namespace OxigenIIAdvertising.FileChecksumCalculator
{
  /// <summary>
  /// Static class to provide methods for calculating file checksum to check a 
  /// file for accidental or intentional contamination of data or to check if a file
  /// has been updated (e.g. in a remote location)
  /// </summary>
  public static class ChecksumCalculator
  {
    /// <summary>
    /// Calculates a checksum for a file. If the file is not found on the client machine's disk, it returns an empty string.
    /// </summary>
    /// <param name="file">The path of the file for which to calculate a checksum</param>
    /// <returns>a SHA256-based string, an empty string if the file is not found on disk</returns>
    /// <exception cref="ObjectDisposedException">The object has already been disposed</exception>
    /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
    /// <exception cref="DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive).</exception>
    /// <exception cref="UnauthorizedAccessException">path specified a directory.  -or- The caller does not have the required permission.</exception>
    /// <exception cref="NotSupportedException">path is in an invalid format.</exception>
    /// <exception cref="InvalidOperationException">The Federal Information Processing Standards (FIPS) security setting is enabled. This implementation is not part of the Windows Platform FIPS-validated cryptographicalgorithms.</exception>    
    public static string GetChecksum(string file)
    {
      FileStream stream = null;

      // it is possible that the file does not exist.
      if (!File.Exists(file))
        return "";

      try
      {
        stream = File.OpenRead(file);

        SHA256Managed sha = new SHA256Managed();
        byte[] checksum = sha.ComputeHash(stream);

        stream.Close();
        stream.Dispose();

        return BitConverter.ToString(checksum).Replace("-", String.Empty);
      }
      catch (Exception ex)
      {
        if (stream != null)
        {
          stream.Close();
          stream.Dispose();
        }

        throw new Exception("Error: Checksum calculation - Reading from File", ex);
      }
    }

    /// <summary>
    /// Calculates a checksum for a stream. The stream is left open afterwards.
    /// </summary>
    /// <param name="stream">The stream to check.</param>
    /// <returns>a SHA256-based string, an empty string if the file is not found on disk</returns>
    /// <exception cref="ObjectDisposedException">The object has already been disposed</exception>
    /// <exception cref="InvalidOperationException">The Federal Information Processing Standards (FIPS) security setting is enabled. This implementation is not part of the Windows Platform FIPS-validated cryptographicalgorithms.</exception>    
    public static string GetChecksum(Stream stream)
    {
      SHA256Managed sha = new SHA256Managed();
      byte[] checksum = sha.ComputeHash(stream);

      stream.Position = 0;

      return BitConverter.ToString(checksum).Replace("-", String.Empty);
    }
  }
}
