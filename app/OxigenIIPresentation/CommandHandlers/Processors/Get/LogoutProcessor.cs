using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Collections.Specialized;

namespace OxigenIIPresentation.CommandHandlers.Processors.Get
{
  public class LogoutProcessor : GetCommandProcessor
  {
    public LogoutProcessor(HttpSessionState session) : base(session) { }
    
    internal override string Execute(NameValueCollection commandParameters)
    {
      _session.Remove("PcProfileToken");
      _session.Remove("User");

      return "1";
    }
  }
}
