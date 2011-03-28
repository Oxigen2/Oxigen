using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;

namespace Oxigen.Core
{
    public class Publisher : Entity
    {
		[DomainSignature]
		public virtual int UserID { get; set; }

		public virtual string FirstName { get; set; }

		public virtual string LastName { get; set; }

		public virtual string DisplayName { get; set; }

		public virtual string EmailAddress { get; set; }

		public virtual long UsedBytes { get; set; }

		public virtual long TotalAvailableBytes { get; set; }

        public virtual IList<Template> AssignedTemplates { get; set; }
    }
}
