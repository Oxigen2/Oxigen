using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace OxigenSU
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
    public static object DeserializeNoLock(Type type, string path, string password)
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

    /// <summary>
    /// Deserializes an object from an XML string in memoty
    /// </summary>
    /// <param name="type">type to deserialize to</param>
    /// <param name="xmlObj">the string holding the XML to be deserialized</param>
    /// <returns>Deserialized object</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static object DeserializeFromString(Type type, string xmlObj)
    {
      object obj = null;
      TextReader reader = null;

      XmlSerializer xmlSerializer = new XmlSerializer(type);

      try
      {
        reader = new StringReader(xmlObj);

        obj = xmlSerializer.Deserialize(reader);
      }
      finally
      {
        if (reader != null)
          reader.Close();
      }

      return obj;
    }

    /// <summary>
    /// Deserializes a clear text file to an underlying object
    /// </summary>
    /// <param name="type">type of the object to deserialize to</param>
    /// <param name="path">path where the text file resides</param>
    /// <returns>An object of the given type</returns>
    /// <exception cref="System.IO.FileNotFoundException"></exception>
    /// <exception cref="System.IO.IOException"></exception>
    /// <exception cref="System.Security.SecurityException"></exception>
    /// <exception cref="System.IO.DirectoryNotFoundException"></exception>
    /// <exception cref="System.UnauthorizedAccessException"></exception>
    /// <exception cref="System.IO.PathTooLongException"></exception>
    /// <exception cref="System.IO.NotSupportedException"></exception>
    public static object DeserializeClearText(Type type, string path)
    {
      object obj = null;
      FileStream fs = null;
      TextReader reader = null;

      XmlSerializer xmlSerializer = new XmlSerializer(type);

      try
      {
        fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);

        reader = new StreamReader(fs, Encoding.ASCII);

        obj = xmlSerializer.Deserialize(reader);
      }
      finally
      {
        if (reader != null)
          reader.Close();

        if (fs != null)
          fs.Close();
      }

      return obj;
    }

    /// <summary>
    /// Serializes an object to clear text
    /// </summary>
    /// <param name="obj">the object to serialize</param>
    /// <param name="path">path to serialize the object to</param>
    /// <exception cref="System.IO.FileNotFoundException"></exception>
    /// <exception cref="System.IO.IOException"></exception>
    /// <exception cref="System.Security.SecurityException"></exception>
    /// <exception cref="System.IO.DirectoryNotFoundException"></exception>
    /// <exception cref="System.UnauthorizedAccessException"></exception>
    /// <exception cref="System.IO.PathTooLongException"></exception>
    public static void SerializeToClearText(object obj, string path)
    {
      FileStream fs = null;
      TextWriter writer = null;

      XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());

      try
      {
        fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);

        writer = new StreamWriter(fs);

        xmlSerializer.Serialize(writer, obj);
      }
      finally
      {
        if (writer != null)
          writer.Close();

        if (fs != null)
          fs.Close();
      }
    }
  }
}
