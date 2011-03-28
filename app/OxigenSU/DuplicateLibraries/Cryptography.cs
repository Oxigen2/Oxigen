using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace OxigenSU
{
  public static class Cryptography
  {
    // Encrypting array of bytes
    private static byte[] Encrypt(byte[] inputData, byte[] key, byte[] iv)
    {
      // store bytes in memory stream
      MemoryStream stream = new MemoryStream();

      Rijndael rij = Rijndael.Create();
      rij.Key = key;
      rij.IV = iv;
      CryptoStream cStream = new CryptoStream(stream, rij.CreateEncryptor(), CryptoStreamMode.Write);

      cStream.Write(inputData, 0, inputData.Length);
      cStream.Close();
      cStream.Dispose();
      byte[] encryptedData = stream.ToArray();
      return encryptedData;
    }

    // Returns Rijndael encrypted string using 128-bit encryption
    public static byte[] Encrypt(byte[] inputData, string pwd)
    {
      PasswordDeriveBytes pwdBytes = new PasswordDeriveBytes(pwd, new byte[] { 0x10, 0x40, 0x00, 0x34, 0x1A, 0x70, 0x01 });

      byte[] encryptedData = Encrypt(inputData, pwdBytes.GetBytes(16), pwdBytes.GetBytes(16));

      return encryptedData;
    }

    // Decrypt the byte array
    private static byte[] Decrypt(byte[] outputData, byte[] key, byte[] iv)
    {
      MemoryStream stream = new MemoryStream();
      Rijndael rij = Rijndael.Create();
      rij.Key = key;
      rij.IV = iv;
      CryptoStream cStream = new CryptoStream(stream, rij.CreateDecryptor(), CryptoStreamMode.Write);
      cStream.Write(outputData, 0, outputData.Length);

      // user could have tampered with the encrypted file, so clean up after Cryptographic Exception
      try
      {
        cStream.Close();
      }
      finally
      {
        cStream.Dispose();
      }

      byte[] decryptedData = stream.ToArray();
      return decryptedData;
    }

    /// <summary>
    /// Decrypts a string in an array of bytes using 128-bit key 
    /// </summary>
    /// <param name="encryptedData">an array of bytes containing the stirng to decrypt</param>
    /// <param name="pwd">password to use for decryption</param>
    /// <returns>an array of bytes with the decrypted string</returns>
    /// <exception cref="CryptographicException"></exception>
    public static byte[] Decrypt(byte[] encryptedData, string pwd)
    {
      PasswordDeriveBytes pwdBytes = new PasswordDeriveBytes(pwd, new byte[] { 0x10, 0x40, 0x00, 0x34, 0x1A, 0x70, 0x01 });

      byte[] decryptedData = Decrypt(encryptedData, pwdBytes.GetBytes(16), pwdBytes.GetBytes(16));

      return decryptedData;
    }
  }
}
