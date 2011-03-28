using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.SOAStructures;

namespace OxigenIIPresentation.CommandHandlers.Processors.Get
{
  public class IsLoggedOnProcessor : GetCommandProcessor
  {
    public IsLoggedOnProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(System.Collections.Specialized.NameValueCollection commandParameters)
    {
      if (_session["User"] == null)
        return "0";

      return (((User)_session["User"]).FirstName);
    }
  }
}
