using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections;

namespace LibraryTester
{
  public partial class ServerCycleConnect : Form
  {
    Hashtable _attemptedServerNumbers = null;

    public ServerCycleConnect()
    {
      _attemptedServerNumbers = new Hashtable();

      InitializeComponent();
    }

    private void btnAttempt_Click(object sender, EventArgs e)
    {
      lblSuccessfulLink.Text = ConnectionConfigURI(4, 1000);

      lblFailedOnes.Text = "";

      foreach (DictionaryEntry de in _attemptedServerNumbers)
        lblFailedOnes.Text += de.Key + ", ";

      btnAttempt.Text = "Attempt Again";
    }

    /// <summary>
    /// Determines the first available HTTP server in a collection of HTTP servers
    /// </summary>
    /// <param name="maxNoConfigServers">maximum known number of the servers that host a configuration service, in the Relay server collection</param>
    /// <param name="timeout">the timeout (in milliseconds) before another server is attempted</param>
    /// <returns>the URI address of the first available HTTP server</returns>
    public string ConnectionConfigURI(int maxNoConfigServers, int timeout)
    {
      // use http instead of https as at this point only the service's responsiveness is of ionterest
      string serverURI = "http://getconfig-";

      return ConnectionURI(maxNoConfigServers, timeout, serverURI);
    }

    /// <summary>
    /// Determines the first available HTTP server in a collection of HTTP servers
    /// </summary>
    /// <param name="maxNoConfigServers">maximum known number of the servers that host a channel data service, in the Relay server collection</param>
    /// <param name="timeout">the timeout (in milliseconds) before another server is attempted</param>
    /// <returns>the URI address of the first available HTTP server</returns>
    public string ConnectionChannelURI(int maxNoChannelServers, int timeout, string channelNameFirstLetter)
    {
      // use http instead of https as at this point only the service's responsiveness is of ionterest
      string serverURI = "http://channelData-" + channelNameFirstLetter;

      return ConnectionURI(maxNoChannelServers, timeout, serverURI);
    }

    private string ConnectionURI(int maxNoServers, int timeout, string serverURI)
    {
      Random random = new Random();

      int serverNumber = -1;

      // run this loop until a connection has succeeded
      // or all servers have been attempted
      while (_attemptedServerNumbers.Count < maxNoServers)
      {
        serverNumber = random.Next(1, maxNoServers + 1);

        if (!_attemptedServerNumbers.Contains(serverNumber))
        {
          serverURI += serverNumber + ".oxigen.net";

          if (ConnectionSucceeded(serverURI, timeout))
            return serverURI;
          else
            _attemptedServerNumbers.Add(serverNumber, "");
        }
      }

      return "All servers have failed";
    }

    private bool ConnectionSucceeded(string serverURI, int timeout)
    {
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverURI);
      request.Timeout = timeout;

      HttpWebResponse response = null;

      try
      {
        response = (HttpWebResponse)request.GetResponse();
      }
      catch (Exception ex)
      {
        string strex = ex.ToString();

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

    private void btnAttemptExisting_Click(object sender, EventArgs e)
    {
      lblFailedOnes.Text = "";

      if (ConnectionSucceeded("http://oxigen-userdatamarshaller.obs-group.co.uk/UserDataMarshaller.svc", 500))
      {
        lblFailedOnes.Text = "";
        lblSuccessfulLink.Text = "succeeded";
      }
      else
      {
        lblSuccessfulLink.Text = "failed";
      }
    }
  }

  public enum ServerType { Relay, Master }
}
