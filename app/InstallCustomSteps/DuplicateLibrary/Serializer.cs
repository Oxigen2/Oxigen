using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace InstallCustomSteps.DuplicateLibrary
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

      for (int counter = 0; counter < 10; counter++)
      {
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

        try
        {
          File.WriteAllBytes(path, encryptedData);

          break;
        }
        catch
        {
          // ignore and repeat loop
        }
      }
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
      object obj = null;

      // try to deserialize 10 times in case another application is using the file
      for (int counter = 0; counter < 10; counter++)
      {
        // check if directory exists
        string directory = Path.GetDirectoryName(path);

        if (!Directory.Exists(directory))
          throw new DirectoryNotFoundException("Directory " + directory + " was not found.");

        if (!File.Exists(path))
          throw new FileNotFoundException("Error: File not found: " + path);

        XmlSerializer xmlSerializer = new XmlSerializer(type);

        MemoryStream decryptedDataStream = null;
        TextReader reader = null;

        try
        {
          byte[] encryptedData = File.ReadAllBytes(path);

          byte[] decryptedData = Cryptography.Decrypt(encryptedData, password);

          decryptedDataStream = new MemoryStream(decryptedData);

          reader = new StreamReader(decryptedDataStream, Encoding.ASCII);

          obj = xmlSerializer.Deserialize(reader);

          break; // succesful deserialization; exit loop.
        }
        catch (Exception ex) // this can happen if deserialized object obj is not of Type type
        {
          System.Threading.Thread.Sleep(100);
        }
        finally
        {
          if (reader != null)
          {
            reader.Close();
            reader.Dispose();
          }

          if (decryptedDataStream != null)
          {
            decryptedDataStream.Close();
            decryptedDataStream.Dispose();
          }
        }
      }

      return obj;
    }
  }
}
