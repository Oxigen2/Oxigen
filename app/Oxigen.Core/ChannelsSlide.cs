using System;
using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;

namespace Oxigen.Core
{
    public class ChannelsSlide : Entity
    {
		[DomainSignature]
		public virtual Channel Channel { get; set; }

		[DomainSignature]
		public virtual Slide Slide { get; set; }

		public virtual string ClickThroughURL { get; set; }

		public virtual float DisplayDuration { get; set; }

		public virtual string Schedule { get; set; }

		public virtual string PresentationConvertedSchedule { get; set; }
    }
}
