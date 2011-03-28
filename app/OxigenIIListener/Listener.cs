using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using OxigenIIAdvertising.EncryptionDecryption;
using System.Collections.Generic;
using OxigenIICompiledRegexes;
using OxigenIIAdvertising.Singletons;
using OxigenIIAdvertising.LoggerInfo;

namespace OxigenIIListener
{
  public class Listener
  {
    private TcpListener _myListener = null;

    private int _port = -1;
    private string _webServerRoot = "";
    private string _cryptPassword = "";
    private Dictionary<string, string> _availableMIMEHeaders = new Dictionary<string, string>();
    private QueryStringSplitter _queryStringSplitter = new QueryStringSplitter();
    private bool _bRunListener = true;

    private Logger _logger = new Logger("Listener", @"C:\OxigenIIAppDataSamples\AppData\OxigenIIDebug-ListenerFiles.txt", LoggingMode.Debug);

    public Listener(string webServerRoot, int port, string cryptPassword)
    {
      _webServerRoot = webServerRoot;
      _port = port;
      _cryptPassword = cryptPassword;
      _availableMIMEHeaders = GetAvailableMIMEHeaders();
    }

    /// <summary>
    /// Starts to listen in a specified port
    /// </summary>
    public void StartListen()
    {
      TcpClient client = null;

      //start listing on the given port
      IPAddress ipa = (IPAddress)Dns.GetHostAddresses("localhost")[0];
      _myListener = new TcpListener(ipa, _port);
      _myListener.Start();

      while (_bRunListener)
      {
        //Accept a new connection
        try
        {
          client = _myListener.AcceptTcpClient();
        }
        catch (SocketException) // if a data transfer is in progress a SocketException will be thrown when stopping the listener.
        {
          if (client != null)
            client.Close();

          return;
        }

        Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
        clientThread.Start(client);
      }
    }

    private void HandleClientComm(object client)
    {
      int iStartPos = 0;

      String sRequest;
      String sRequestedGUID;
      String sRequestedFile;
      String sErrorMessage;
      String sPhysicalFilePath;
      String sResponse;

      TcpClient tcpClient = (TcpClient)client;
      Socket clientSocket = tcpClient.Client;

      //make a byte array and receive data from the client 
      Byte[] bReceive = new Byte[1024];

      int i = -1;

      try
      {
        i = clientSocket.Receive(bReceive, bReceive.Length, 0);
      }
      catch (SocketException ex) // some connections will be dropped by the Listener but the file will be server in the next iteration of the while loop
      {
        _logger.WriteError(ex);
        return;
      }

      //Convert Byte to String
      string sBuffer = Encoding.ASCII.GetString(bReceive);

      //At present we will only deal with GET type
      if (sBuffer.Substring(0, 3) != "GET")
      {
        clientSocket.Close();
        return;
      }

      // Look for HTTP request
      iStartPos = sBuffer.IndexOf("HTTP", 1);

      // Get the HTTP text and version e.g. it will return "HTTP/1.1"
      string sHttpVersion = sBuffer.Substring(iStartPos, 8);

      // Extract the Requested Type and Requested file/directory
      sRequest = sBuffer.Substring(0, iStartPos - 1);

      //Extract the requested file name
      iStartPos = sRequest.LastIndexOf("/") + 1;
      sRequestedGUID = sRequest.Substring(iStartPos);

      if (sRequestedGUID.Length == 0)
      {
        sErrorMessage = "<h2>Error: No Parameter Specified</h2>";

        ServeErrorConditionAndClose(tcpClient, sHttpVersion, sErrorMessage);
      }
      else
      {
        if (AssetWebRequestSingleton.Instance.AssetWebRequestsPending.ContainsKey(sRequestedGUID))
        {
          sRequestedFile = AssetWebRequestSingleton.Instance.AssetWebRequestsPending[sRequestedGUID];

          _logger.WriteMessage(DateTime.Now.ToLongTimeString() + " " + sRequestedFile);

          sPhysicalFilePath = _webServerRoot + sRequestedFile;

          if (File.Exists(sPhysicalFilePath) == false)
          {
            sErrorMessage = "<h2>Error: 404 File Does Not Exist</h2>";

            ServeErrorConditionAndClose(tcpClient, sHttpVersion, sErrorMessage);
          }
          else
          {
            int iTotBytes = 0;
            sResponse = "";

           MemoryStream ms = DecryptAssetFile(sPhysicalFilePath, _cryptPassword);

           // FileStream ms = File.Open(sPhysicalFilePath, FileMode.Open);

            // Create a reader that can read bytes from the FileStream.
            BinaryReader reader = new BinaryReader(ms);
            byte[] bytes = new byte[ms.Length];

            int read;

            while ((read = reader.Read(bytes, 0, bytes.Length)) != 0)
            {
              // Read from the file and write the data to the network
              sResponse = sResponse + Encoding.ASCII.GetString(bytes, 0, read);
              iTotBytes = iTotBytes + read;
            }

            reader.Close();

            ms.Dispose();

            SendHeader(sHttpVersion, _availableMIMEHeaders[Path.GetExtension(sRequestedFile)], iTotBytes, " 200 OK", ref clientSocket);

            SendToBrowser(bytes, ref clientSocket);

            tcpClient.Close();
          }
        }
        else
        {
          sErrorMessage = "<h2>Error: File cannot be served</h2>";

          ServeErrorConditionAndClose(tcpClient, sHttpVersion, sErrorMessage);
        }
      }
    }


    private void ServeErrorConditionAndClose(TcpClient tcpClient, string sHttpVersion, string sErrorMessage)
    {
      Socket clientSocket = tcpClient.Client;

      SendHeader(sHttpVersion, "", sErrorMessage.Length, " 404 Not Found", ref clientSocket);

      SendToBrowser(sErrorMessage, ref clientSocket);

      tcpClient.Close();
    }

    private void SendHeader(string sHttpVersion, string sMIMEHeader, int iTotBytes,
      string sStatusCode, ref Socket clientSocket)
    {
      String sBuffer = "";

      // if Mime type is not provided set default to text/html
      if (sMIMEHeader.Length == 0)
        sMIMEHeader = "text/html"; // Default Mime Type is text/html

      sBuffer = sBuffer + sHttpVersion + sStatusCode + "\r\n";
      sBuffer = sBuffer + "Server: cx1193719-b\r\n";
      sBuffer = sBuffer + "Content-Type: " + sMIMEHeader + "\r\n";
      sBuffer = sBuffer + "Accept-Ranges: bytes\r\n";
      sBuffer = sBuffer + "Content-Length: " + iTotBytes + "\r\n\r\n";

      Byte[] bSendData = Encoding.ASCII.GetBytes(sBuffer);

      SendToBrowser(bSendData, ref clientSocket);
    }

    private void SendToBrowser(String sData, ref Socket clientSocket)
    {
      SendToBrowser(Encoding.ASCII.GetBytes(sData), ref clientSocket);
    }

    private void SendToBrowser(Byte[] bSendData, ref Socket clientSocket)
    {
      try
      {
        if (clientSocket.Connected)
          clientSocket.Send(bSendData, bSendData.Length, 0);
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);
      }
    }

    /// <summary>
    /// Stop the listening process
    /// </summary>
    public void StopListen()
    {
      _bRunListener = false;
      _myListener.Stop(); // stop listener explicitly as AcceptSocket() in the while loop is a blocking method.
    }

    /// <summary>
    /// Decrypts an asset file from disk using the specified decryption password
    /// </summary>
    /// <param name="assetPath">path of the asset file to decrypt</param>
    /// <param name="decryptionPassword">the password to use for decryption</param>
    /// <returns>a MemoryStream with the decrypted file</returns>
    /// <exception cref="System.Security.Cryptography.CryptographicException">Encrypted file is corrupted</exception>
    /// <exception cref="PathTooLongException"></exception>
    /// <exception cref="DirectoryNotFoundException"></exception>
    /// <exception cref="IOException"></exception>
    /// <exception cref="UnauthorizedAccessException"></exception>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="System.Security.SecurityException"></exception>
    private MemoryStream DecryptAssetFile(string assetPath, string decryptionPassword)
    {
      byte[] encryptedBuffer = File.ReadAllBytes(assetPath);

      byte[] decryptedBuffer = Cryptography.Decrypt(encryptedBuffer, decryptionPassword);

      return new MemoryStream(decryptedBuffer);
    }

    private Dictionary<string, string> GetAvailableMIMEHeaders()
    {
      Dictionary<string, string> availableMIMEHeaders = new Dictionary<string, string>();

      availableMIMEHeaders.Add(".wmv", "audio/x-ms-wmv");
      availableMIMEHeaders.Add(".avi", "video/x-msvideo");
      availableMIMEHeaders.Add(".swf", "application/x-shockwave-flash");
      availableMIMEHeaders.Add(".mov", "video/quicktime");

      return availableMIMEHeaders;
    }
  }
}
