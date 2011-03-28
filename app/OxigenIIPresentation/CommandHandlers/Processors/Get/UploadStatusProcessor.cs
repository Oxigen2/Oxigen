using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace OxigenIIPresentation.CommandHandlers.Processors.Get
{
  public class UploadStatusProcessor : GetCommandProcessor
  {
    public UploadStatusProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(System.Collections.Specialized.NameValueCollection commandParameters)
    {
      if (_session["DurationsAmended"] == null)
        return "0";

      bool bDurationsAmended = (bool)_session["DurationsAmended"];

      _session.Remove("DurationsAmended");

      return bDurationsAmended ? "1" : "0";
    }
  }
}
