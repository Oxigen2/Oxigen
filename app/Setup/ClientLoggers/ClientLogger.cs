using System.Net;

namespace Setup.ClientLoggers
{
  public abstract class ClientLogger
  {
    protected readonly string _loggingPartialUrl = "http://new.oxigen.net/log/setup5/";
    protected string _userRef = null;

    protected delegate WebResponse WebResponseGetter();

    public void Log(string message)
    {
      try
      {
        WebRequest request = WebRequest.Create(_loggingPartialUrl + _userRef + "/" + message);
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
