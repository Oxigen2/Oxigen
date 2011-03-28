using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OxigenIIAdvertising.SOAStructures;

namespace OxigenIIPresentation.CommandHandlers.Processors.Get
{
  public class UserQuotaProcessor : GetCommandProcessor
  {
    public UserQuotaProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(System.Collections.Specialized.NameValueCollection commandParameters)
    {
      User user = (User)_session["User"];

      if (user == null)
        return "0,,0,,0";

      return user.UsedBytes + ",," + (user.TotalAvailableBytes - user.UsedBytes) + ",," + user.TotalAvailableBytes;
    }
  }
}
