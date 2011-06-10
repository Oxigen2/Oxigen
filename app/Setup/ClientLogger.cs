using System;
using System.Net;
using System.Threading;

namespace Setup
{
  public class ClientLogger : IClientLogger
  {
    private static string _loggingPartialUrl;
    private static string _userRef;
  
    static ClientLogger()
    {
      _userRef = System.Guid.NewGuid().ToString();
      _loggingPartialUrl = "http://new.oxigen.net/log/setup/" + _userRef + "/";
    }

    private delegate WebResponse WebResponseGetter();
    
    public void Log(string stageName)
    {
      try
      {
        WebRequest request = WebRequest.Create(_loggingPartialUrl + stageName);
        WebResponseGetter getter = new WebResponseGetter(request.GetResponse);

        AsyncHelper.FireAndForget(getter, null);
      }
      catch 
      {
        // suppress all errors
      }
    }
  }
}
