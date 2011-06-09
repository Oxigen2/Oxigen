using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;

namespace OxigenSU
{
  /// <summary>
  /// Static class that provides methods to test responsivess of the remote server collections that connect to
  /// client applications.
  /// 
  /// timeout parameter is universal for all server collections with which the client-side applications
  /// communicate.
  /// </summary>
  public static class ResponsiveServerDeterminator
  {
    /// <summary>
    /// Determines the first available WCF server for system-wide data, given method parameters
    /// </summary>
    /// <param name="serverType">Server type to ping</param>
    /// <param name="maxNoServers">Maximum number of servers to attempt</param>
    /// <param name="timeout">Ping timeout value</param>
    /// <param name="primaryDomainName">Primary domain to try</param>
    /// <param name="secondaryDomainName">Secondary domain if primary domain fails</param>
    /// <param name="endpointSuffix">The service namd and endpoint address to ping</param>
    /// <returns>A string with the URL of the first available server</returns>
    /// <exception cref="NotSupportedException">The request scheme specified in requestUriString has not been registered.</exception>
    /// <exception cref="System.Security.SecurityException">The caller does not have permission to connect to the requested URI or a URI that the request is redirected to.</exception>
    /// <exception cref="UriFormatException">The URI specified in requestUriString is not a valid URI.</exception>
    public static string GetResponsiveURI(ServerType serverType, int maxNoServers, int timeout, string primaryDomainName, string secondaryDomainName, string endpointSuffix)
    {
      return GetResponsiveURI(serverType, maxNoServers, timeout, "", primaryDomainName, secondaryDomainName, endpointSuffix);
    }

    /// <summary>
    /// Determines the first available WCF server given method parameters
    /// </summary>
    /// <param name="serverType">Server type to ping</param>
    /// <param name="maxNoServers">Maximum number of servers to attempt</param>
    /// <param name="timeout">Ping timeout value</param>
    /// <param name="letter">The first letter of the pinging machine's GUID or user's GUID</param>
    /// <param name="primaryDomainName">Primary domain to try</param>
    /// <param name="secondaryDomainName">Secondary domain if primary domain fails</param>
    /// <param name="endpointSuffix">The service namd and endpoint address to ping</param>
    /// <returns>A string with the URL of the first available server</returns>
    /// <exception cref="NotSupportedException">The request scheme specified in requestUriString has not been registered.</exception>
    /// <exception cref="System.Security.SecurityException">The caller does not have permission to connect to the requested URI or a URI that the request is redirected to.</exception>
    /// <exception cref="UriFormatException">The URI specified in requestUriString is not a valid URI.</exception>
    public static string GetResponsiveURI(ServerType serverType, int maxNoServers, int timeout, string letter, string primaryDomainName, string secondaryDomainName, string endpointSuffix)
    {
      // check both for existence of bDebug and value. Client machines may not have the key at all
      if (serverType == ServerType.DownloadGetFile && System.Configuration.ConfigurationSettings.AppSettings["bDebug"] != null && System.Configuration.ConfigurationSettings.AppSettings["bDebug"] == "1")
        return "https://staging-userfilemarshaller.oxigen.net/UserFileMarshaller.svc";

      string serverURIPrefix = "https://";

      switch (serverType)
      {
        case ServerType.MasterGetConfig:
          serverURIPrefix += "master-getconfig-";
          break;
        case ServerType.RelayChannelAssets:
          serverURIPrefix += "relay-channelassets-";
          break;
        case ServerType.RelayChannelData:
          serverURIPrefix += "relay-channeldata-";
          break;
        case ServerType.RelayGetConfig:
          serverURIPrefix += "relay-getconfig-";
          break;
        case ServerType.RelayLogCont:
          serverURIPrefix += "relay-sendlogcon-";
          break;
        case ServerType.RelayLogAdv:
          serverURIPrefix += "relay-sendlogadv-";
          break;
        case ServerType.RelayLogs:
          serverURIPrefix += "relay-logs-";
          break;
        case ServerType.DownloadGetFile:
          serverURIPrefix += "download-getfiles-";
          break;
      }
      
      if (letter != "")
        serverURIPrefix += letter + "-";

      return ConnectionURI(maxNoServers, timeout, serverURIPrefix, primaryDomainName, secondaryDomainName, endpointSuffix);
    }

    private static string ConnectionURI(int maxNoServers, int timeout, string serverURIPrefix, 
      string primaryDomainName, string secondaryDomainName, string endpointSuffix)
    {
      string serverURI = "";

      int[] serverNumbers = FillInArray(maxNoServers);
      
      int[] shuffledServerNumbers = Shuffler.ShuffleArray<int>(serverNumbers);

      // try primary domain
      foreach (int serverNumber in shuffledServerNumbers)
      {
        serverURI = serverURIPrefix + serverNumber + primaryDomainName + "/" + endpointSuffix;

        if (ConnectionSucceeded(serverURI, timeout))
          return serverURI;
      }

      // if no server was yielded so far, try secondary domain
      foreach (int serverNumber in shuffledServerNumbers)
      {
        serverURI = serverURIPrefix + serverNumber + secondaryDomainName + "/" + endpointSuffix;

        if (ConnectionSucceeded(serverURI, timeout))
          return serverURI;
      }

      // if secondary domain hasn't yielded results, exit with empty string
      return "";
    }

    private static int[] FillInArray(int maxNoServers)
    {
      int[] serverNumbers = new int[maxNoServers];

      for (int i = 0; i < maxNoServers; i++)
        serverNumbers[i] = i + 1;

      return serverNumbers;
    }

    private static bool ConnectionSucceeded(string serverURI, int timeout)
    {
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverURI);
      request.Timeout = timeout;

      HttpWebResponse response = null;

      try
      {
        response = (HttpWebResponse)request.GetResponse();
      }
      catch
      {
        if (response != null)
          response.Close();

        return false;
      }

      if (response.StatusCode != HttpStatusCode.OK)
      {
        response.Close();
        
        return false;
      }

      response.Close();

      return true;
    }
  }
}
