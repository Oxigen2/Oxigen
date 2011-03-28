using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OxigenIIPresentation.CommandHandlers.Processors;
using System.Web.SessionState;

namespace OxigenIIPresentation.CommandHandlers
{
  internal class GetCommandHandler : CommandHandler
  {
    public override bool IsReusable
    {
      get { return false; }
    }

    public override void ProcessRequest(HttpContext context)
    {
      base.ProcessRequest(context);
    }
  }
}
