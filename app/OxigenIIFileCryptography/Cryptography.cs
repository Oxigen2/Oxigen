using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace OxigenIIAdvertising.FileCryptography
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
      byte[] encryptedData = stream.ToArray();
      return encryptedData;
    }

    // Returns Rijndael encrypted string using 128-bit encryption
    public static string Encrypt(string inputData, string pwd)
    {
      byte[] Bytes = System.Text.Encoding.Unicode.GetBytes(inputData);
      PasswordDeriveBytes pwdBytes = new PasswordDeriveBytes(pwd, new byte[] { 0x10, 0x40, 0x00, 0x34, 0x1A, 0x70, 0x01, 0x34, 0x56, 0xFF, 0x99, 0x77, 0x4C, 0x22, 0x49 });

      byte[] encryptedData = Encrypt(Bytes, pwdBytes.GetBytes(16), pwdBytes.GetBytes(16));
      return Convert.ToBase64String(encryptedData);
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
      cStream.Close();
      byte[] decryptedData = stream.ToArray();
      return decryptedData;
    }

    // Decrypt the string using 128-bit key
    public static string Decrypt(string str, string pwd)
    {
      byte[] Bytes = Convert.FromBase64String(str);
      PasswordDeriveBytes pwdBytes = new PasswordDeriveBytes(pwd, new byte[] { 0x10, 0x40, 0x00, 0x34, 0x1A, 0x70, 0x01, 0x34, 0x56, 0xFF, 0x99, 0x77, 0x4C, 0x22, 0x49 });

      byte[] decryptedData = Decrypt(Bytes, pwdBytes.GetBytes(16), pwdBytes.GetBytes(16));
      return System.Text.Encoding.Unicode.GetString(decryptedData);      
    } 
  }
}
