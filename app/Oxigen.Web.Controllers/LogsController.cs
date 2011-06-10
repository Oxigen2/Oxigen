using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Oxigen.Core.Logger;
using Oxigen.Core.RepositoryInterfaces;
using SharpArch.Core;

namespace Oxigen.Web.Controllers
{
    public class LogsController : Controller
    {
        private ILogEntryRepository logEntryRepository;

        public LogsController(ILogEntryRepository logEntryRepository) {
            Check.Require(logEntryRepository != null, "logEntryRepository may not be null");

            this.logEntryRepository = logEntryRepository;
        }
        public ActionResult Log(string logName, string userRef, string message)
        {
            var logEntry = new LogEntry(logName)
                               {
                                   UserRef = userRef,
                                   Message = message,
                                   IpAddress = Request.ServerVariables["REMOTE_ADDR"]
                               };
            logEntryRepository.SaveOrUpdate(logEntry);
            return new EmptyResult();
        }
    }


}
