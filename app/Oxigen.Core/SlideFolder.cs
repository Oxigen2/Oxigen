using System;
using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;

namespace Oxigen.Core
{
    public class SlideFolder : AuditEntity
    {
		[DomainSignature]
		public virtual string SlideFolderName { get; set; }

		[DomainSignature]
		public virtual Publisher Publisher { get; set; }
    }
}
