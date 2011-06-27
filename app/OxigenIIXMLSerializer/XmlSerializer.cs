using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using OxigenIIAdvertising.FileLocker;
using OxigenIIAdvertising.EncryptionDecryption;
using System.Runtime.Serialization.Formatters.Binary;

namespace OxigenIIAdvertising.XMLSerializer
{
  /// <summary>
  /// static class with methods to serialize/deserialize an object in XML
  /// </summary>
  public static class Serializer
  {
    /// <summary>
    /// Serializes an object to XML and encrypts it to a file that has been locked from a previous operation.
    /// The lock is then released.
    /// </summary>
    /// <param name="obj">The object to serialise</param>
    /// <param name="password">The encryption password</param>
    /// <param name="fileStream">the FileStream that has a lock on the file to serialize to</param>
    /// <exception cref="SerializationException">thrown when an error occurs during the object's serializaton</exception>
    /// <exception cref="InvalidOperationException">thrown when there is a problem in serializaton</exception>
    /// <exception cref="IOException">thrown when there is an error writing the file to disk</exception>
    /// <exception cref="NullReferenceException">the object to serialize is null</exception>
    public static void Serialize(object obj, string password, ref FileStream fileStream)
    {
      if (obj == null)
      {
        Locker.ClearFileStream(ref fileStream);
        throw new NullReferenceException("object is null");
      }

      XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());

      MemoryStream stream = new MemoryStream();

      StreamWriter encodingWriter = new StreamWriter(stream, Encoding.ASCII);

      try
      {
        xmlSerializer.Serialize(encodingWriter, obj);
      }
      catch (InvalidOperationException ex)
      {
        stream.Close();
        stream.Dispose();

        encodingWriter.Close();
        encodingWriter.Dispose();

        Locker.ClearFileStream(ref fileStream);

        throw new InvalidOperationException("Error serializing object of type " + obj.GetType(), ex);
      }
      catch (Exception ex)
      {
        stream.Close();
        stream.Dispose();

        encodingWriter.Close();
        encodingWriter.Dispose();

        Locker.ClearFileStream(ref fileStream);

        throw ex;
      }

      stream.Position = 0;

      byte[] dataToEncrypt = new byte[stream.Length + (16 - stream.Length % 16)];
      stream.Read(dataToEncrypt, 0, dataToEncrypt.Length);

      stream.Close();
      stream.Dispose();

      encodingWriter.Close();
      encodingWriter.Dispose();

      // Encrypt
      byte[] encryptedData = Cryptography.Encrypt(dataToEncrypt, password);

      int noBytes = encryptedData.Length;

      fileStream.SetLength(0);
      
      try
      {
        fileStream.Write(encryptedData, 0, noBytes);
      }
      finally
      {
        Locker.ClearFileStream(ref fileStream);
      }
    }

    /// <summary>
    /// Serializes an object to XML, encrypts it and converts it to a Memory Stream
    /// </summary>
    /// <param name="obj">the object to encrypt and convert</param>
    /// <param name="password">password used for encryption</param>
    /// <returns>a MemoryStream containing the encrypted object</returns>
    public static MemoryStream SerializeWithEncryption(object obj, string password)
    {
      // convert object to byte[]
      byte[] decryptedData = GetClearSerializedObject(obj);

      byte[] encryptedData = Cryptography.Encrypt(decryptedData, password);

      MemoryStream ms = new MemoryStream(encryptedData);

      return ms;
    }

    private static byte[] GetClearSerializedObject(object obj)
    {
      MemoryStream stream = null;
      TextWriter writer = null;

      XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());

      try
      {
        stream = new MemoryStream();

        writer = new StreamWriter(stream);

        xmlSerializer.Serialize(writer, obj);

        byte[] buffer = stream.ToArray();

        return buffer;
      }
      finally
      {
        if (writer != null)
          writer.Close();

        if (stream != null)
          stream.Close();
      }
    }

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
    /// Serializes an object to XML to a Memory Stream without encryption.
    /// </summary>
    /// <param name="obj">the object to serialize</param>
    /// <returns>a MemoryStream holding the XML-serialized object</returns>
    /// <exception cref="SerializationException">thrown when an error occurs during the object's serializaton</exception>
    /// <exception cref="InvalidOperationException">thrown when there is a problem in serializaton</exception>
    /// <exception cref="NullReferenceException">object to serialize is null</exception>
    public static MemoryStream Serialize(object obj)
    {
      if (obj == null)
        throw new NullReferenceException("object is null");

      XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());

      MemoryStream stream = new MemoryStream();

      try
      {
        xmlSerializer.Serialize(stream, obj);
      }
      catch (InvalidOperationException ex)
      {
        stream.Close();
        stream.Dispose();

        throw new InvalidOperationException("Error serializing object of type " + obj.GetType(), ex);
      }
      catch (Exception ex)
      {
        stream.Close();
        stream.Dispose();

        throw ex;
      }

      stream.Position = 0;

      return stream;
    }

    /// <summary>
    /// Decrypts (if encrypted) and deserializes an object from XML. Unlocks the file after operation.
    /// Operation is expected to succeed, i.e. use this call for important serialized XML objects i.e. non-log files
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
      FileStream fileStream = null;

      object obj = null;

      try
      {
        obj = Deserialize(type, path, password, ref fileStream, true, false);
      }
      finally
      {
        Locker.ClearFileStream(ref fileStream);
      }

      return obj;
    }

    /// <summary>
    /// Decrypts (if encrypted) and deserializes an object from XML file and keeps the file locked until released manually.
    /// fileStream is released if file is not critical (bCritical is false) and returns a blank object
    /// </summary>
    /// <param name="type">expected type of the deserialized object</param>
    /// <param name="path">The path to load the xml file from</param>
    /// <param name="password">password to decrypt the file</param>
    /// <param name="fileStream">the file stream that handles the file</param>
    /// <param name="bCritical">Operation is expected to succeed. If this is true, throws an error to the caller if file is not found</param>
    /// <param name="bKeepLock">In non-critical files (where it is acceptable for files not to be found or be empty), if this is set to true, the fileStream won't be released. If it is set to false, and file or data is not found, the fileStream is closed and dereferenced. Use when a lock is needed on the file after the method exits (e.g. when reading a log file and then appending data on it).</param>
    /// <exception cref="SerializationException">thrown when an error occurs during the object's deserializaton</exception>
    /// <exception cref="FileNotFoundException">thrown if XML serialized file not found</exception>
    /// <exception cref="InvalidOperationException">thrown by XmlSerializer.Deserialize</exception>
    /// <exception cref="IOException">thrown when there is an error during read</exception>
    /// <exception cref="DirectoryNotFoundException">thrown when the directory is not found</exception>
    /// <exception cref="CryptographicException">thrown when encrypted file is corrupted</exception>
    public static object Deserialize(Type type, string path, string password, ref FileStream fileStream, bool bCritical, bool bKeepLock)
    {
      // check if directory exists
      string directory = Path.GetDirectoryName(path);

      if (!Directory.Exists(directory))
        throw new DirectoryNotFoundException("Directory " + directory + " was not found.");

      if (bCritical)
      {
        // check if file exists
        if (!File.Exists(path))
          throw new FileNotFoundException("Error: File not found: " + path);
      }

      XmlSerializer xmlSerializer = new XmlSerializer(type);

      return Deserialize(type, ref fileStream, xmlSerializer, path, password, bCritical, bKeepLock);
    }

    // this method locks the deserialized file and is the overload that should be used
    // in release builds
    private static object Deserialize(Type type, ref FileStream fileStream, 
      XmlSerializer xmlSerializer, string path, string password, bool bCritical, bool bKeepLock)
    {
      object obj = null;

      MemoryStream decryptedDataStream = Locker.ReadDecryptFile(ref fileStream, path, password, bCritical, bKeepLock);

      // if it is likely that object may not exist (i.e. non-important serialized object), do not throw an error but create a blank instance
      if (decryptedDataStream == null && !bCritical)
      {
        obj = Activator.CreateInstance(type);
        return obj;
      }

      TextReader reader = new StreamReader(decryptedDataStream, Encoding.ASCII);

      try
      {
        obj = xmlSerializer.Deserialize(reader);
      }
      catch (Exception ex) // this can happen if deserialized object obj is not of Type type
      {
        Locker.ClearFileStream(ref fileStream);
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

    /// <summary>
    /// Serializes an object to clear text
    /// </summary>
    /// <param name="obj">the object to serialize</param>
    /// <param name="path">path to serialize the object to</param>
    /// <returns></returns>
    public static MemoryStream SerializeClear(object obj)
    {
      TextWriter writer = null;

      MemoryStream stream = new MemoryStream();
      XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());

      try
      {
        writer = new StreamWriter(stream);

        xmlSerializer.Serialize(writer, obj);
        stream.Position = 0;
      }
      catch (Exception ex)
      {
        if (writer != null)
          writer.Close();

        if (stream != null)
          stream.Close();

        throw ex;
      }

      return stream;
    }

    public static byte[] SerializeClearToByteArray(object obj)
    {
        byte[] bytes;

        using (MemoryStream ms = SerializeClear(obj))
        {
            bytes = ms.ToArray();
        }

        return bytes;
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
  }
}
