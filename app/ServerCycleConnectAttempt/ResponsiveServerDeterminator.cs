﻿using System;
using System.Net;

namespace OxigenIIAdvertising.ServerConnectAttempt
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
    public static string GetResponsiveURI(ServerType serverType, int maxNoServers, int timeout, string letter, string primaryDomainName, string secondaryDomainName, string endpointSuffix, LoggerInfo.Logger logger)
    {
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
      }
      
      if (letter != "")
        serverURIPrefix += letter + "-";

      return ConnectionURI(maxNoServers, timeout, serverURIPrefix, primaryDomainName, secondaryDomainName, endpointSuffix, logger);
    }

    private static string ConnectionURI(int maxNoServers, int timeout, string serverURIPrefix,
      string primaryDomainName, string secondaryDomainName, string endpointSuffix, LoggerInfo.Logger logger) 
    {
        string serverURI = "";

        int[] serverNumbers = FillInArray(maxNoServers);

        int[] shuffledServerNumbers = Shuffler.ShuffleArray<int>(serverNumbers);

        // try primary domain
        foreach (int serverNumber in shuffledServerNumbers) {
            serverURI = serverURIPrefix + serverNumber + primaryDomainName + "/" + endpointSuffix;

            if (logger != null)
                logger.WriteTimestampedMessage("Attempting to connect to: " + serverURI);

            if (ConnectionSucceeded(serverURI, timeout, logger))
                return serverURI;
        }

        // if no server was yielded so far, try secondary domain
        foreach (int serverNumber in shuffledServerNumbers) 
        {
            serverURI = serverURIPrefix + serverNumber + secondaryDomainName + "/" + endpointSuffix;

            if (logger != null)
                logger.WriteTimestampedMessage("Attempting to connect to: " + serverURI);

            if (ConnectionSucceeded(serverURI, timeout, logger))
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

    private static bool ConnectionSucceeded(string serverURI, int timeout, LoggerInfo.Logger logger)
    {
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverURI);
      request.Timeout = timeout;

      HttpWebResponse response = null;

      try
      {
          request.Proxy = WebProxy.GetDefaultProxy();
          if (request.Proxy != null)
            request.Proxy.Credentials = CredentialCache.DefaultCredentials;
        
          response = (HttpWebResponse)request.GetResponse();
      }
      catch (Exception ex)
      {
        if (response != null)
          response.Close();

        logger.WriteError(ex);
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
