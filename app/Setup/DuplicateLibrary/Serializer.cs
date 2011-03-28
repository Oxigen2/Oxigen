using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Setup
{
  /// <summary>
  /// Static class with methods to encrypt serialize/decrypt deserialize an object in XML.
  /// Simple encryption/decryption without locking on files.
  /// </summary>
  public static class Serializer
  {
    /// <summary>
    /// Serializes an object to XML and encrypts it
    /// </summary>
    /// <param name="obj">The object to serialise</param>
    /// <param name="path">The path to save the xml file</param>
    /// <param name="password">The encryption password</param>
    /// <exception cref="SerializationException">thrown when an error occurs during the object's serializaton</exception>
    /// <exception cref="DirectoryNotFoundException">thrown when directory to serialize the object to is not found</exception>
    /// <exception cref="InvalidOperationException">thrown when there is a problem in serialization</exception>
    /// <exception cref="NullReferenceException">the object to serialize is null</exception>
    public static void Serialize(object obj, string path, string password)
    {
      if (obj == null)
        throw new NullReferenceException("object is null");

      XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());

      // check if directory exists
      string directory = Path.GetDirectoryName(path);

      if (!Directory.Exists(directory))
        throw new DirectoryNotFoundException("Directory " + directory + " was not found.");

      MemoryStream streamXmlHolder = new MemoryStream();

      StreamWriter encodingWriter = new StreamWriter(streamXmlHolder, Encoding.ASCII);

      try
      {
        xmlSerializer.Serialize(encodingWriter, obj);
      }
      catch (InvalidOperationException ex)
      {
        streamXmlHolder.Close();
        streamXmlHolder.Dispose();

        encodingWriter.Close();
        encodingWriter.Dispose();

        throw new InvalidOperationException("Error serializing object of type " + obj.GetType(), ex);
      }
      catch (Exception ex)
      {
        streamXmlHolder.Close();
        streamXmlHolder.Dispose();

        encodingWriter.Close();
        encodingWriter.Dispose();

        throw ex;
      }

      streamXmlHolder.Position = 0;

      byte[] dataToEncrypt = new byte[streamXmlHolder.Length + (16 - streamXmlHolder.Length % 16)];
      streamXmlHolder.Read(dataToEncrypt, 0, dataToEncrypt.Length);

      streamXmlHolder.Close();
      streamXmlHolder.Dispose();

      encodingWriter.Close();
      encodingWriter.Dispose();

      // Encrypt
      byte[] encryptedData = Cryptography.Encrypt(dataToEncrypt, password);

      File.WriteAllBytes(path, encryptedData);
    }

    /// <summary>
    /// Decrypts  and deserializes an object from XML.
    /// </summary>
    /// <param name="type">expected type of the deserialized object</param>
    /// <param name="path">The path to load the xml file from</param>
    /// <param name="password">password to decrypt the file</param>
    /// <returns>the object to deserialize</returns>
    /// <exception cref="SerializationException">thrown when an error occurs during the object's deserializaton</exception>
    /// <exception cref="FileNotFoundException">thrown if XML serialized file not found</exception>
    /// <exception cref="InvalidOperationException">thrown by XmlSerializer.Deserialize</exception>
    /// <exception cref="IOException">thrown when there is an error during read</exception>
    /// <exception cref="DirectoryNotFoundException">thrown when the directory is not found</exception>
    /// <exception cref="CryptographicException">thrown when encrypted file is corrupted</exception>
    public static object Deserialize(Type type, string path, string password)
    {
      // check if directory exists
      string directory = Path.GetDirectoryName(path);

      if (!Directory.Exists(directory))
        throw new DirectoryNotFoundException("Directory " + directory + " was not found.");
      
      if (!File.Exists(path))
        throw new FileNotFoundException("Error: File not found: " + path);

      XmlSerializer xmlSerializer = new XmlSerializer(type);

      byte[] encryptedData = File.ReadAllBytes(path);

      byte[] decryptedData = Cryptography.Decrypt(encryptedData, password);

      MemoryStream decryptedDataStream = new MemoryStream(decryptedData);

      object obj = null;

      TextReader reader = new StreamReader(decryptedDataStream, Encoding.ASCII);

      try
      {
        obj = xmlSerializer.Deserialize(reader);
      }
      catch (Exception ex) // this can happen if deserialized object obj is not of Type type
      {      
        throw new InvalidOperationException("Type mismatch between parameter type " + type.ToString() + " and type as described in XML serialized object", ex);
      }
      finally
      {
        reader.Close();
        reader.Dispose();

        decryptedDataStream.Close();
        decryptedDataStream.Dispose();
      }

      return obj;
    }
  }
}
