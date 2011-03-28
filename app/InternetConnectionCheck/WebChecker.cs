using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace OxigenIIAdvertising.InternetConnectionCheck
{
  /// <summary>
  /// Provides methods to check the status of a web site / internet connection
  /// </summary>
  public static class WebChecker
  {
    /// <summary>
    /// Checks if a URL can be accessed. As it stands, the method cannot distinguish between
    /// No internet connection at all and particular site not accessible
    /// To change this, handle the WebException more thoroughly (check if the WebException's response object is null or not)
    /// </summary>
    /// <param name="targetURL">The URL to check</param>
    /// <param name="timeout">The number of seconds to wait before the request times out</param>
    /// <returns>true if connection to the site is successful, false if connection failure or no internet connection</returns>
    public static bool IsWebSiteAvailable(string targetURL, int timeout)
    {
      HttpWebRequest request = null;
      HttpWebResponse response = null;

      try
      {
        request = (HttpWebRequest)WebRequest.Create(targetURL);
        request.Timeout = timeout * 1000;
        response = (HttpWebResponse)request.GetResponse();
        response.Close();

        if (response.StatusCode == HttpStatusCode.OK)
          return true;

        return false;
      }
      catch (Exception)
      {
        return false;
      }
    }
  }
}
