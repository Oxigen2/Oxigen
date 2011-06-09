using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Core.DomainModel;

namespace Oxigen.Core.Logger
{
    public class LogEntry : Entity
    {
        [DomainSignature]
        virtual public string LogName { get; protected set; }
        [DomainSignature]
        virtual public DateTime AddDate { get; protected set; }
        [DomainSignature]
        virtual public string UserRef { get; set; }
        virtual public string Message { get; set; }
        virtual public string IpAddress { get; set; }

        public LogEntry() {}
        public LogEntry(string logName)
        {
            LogName = logName;
            AddDate = System.DateTime.Now;
        }
    }
}
