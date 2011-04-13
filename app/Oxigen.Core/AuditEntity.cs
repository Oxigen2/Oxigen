using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Core.DomainModel;

namespace Oxigen.Core
{
    public abstract class AuditEntity : Entity
    {
        public virtual DateTime AddDate { get; set; }
        public virtual DateTime? EditDate { get; set; }
    }
}
