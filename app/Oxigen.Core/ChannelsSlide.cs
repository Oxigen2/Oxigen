using System;
using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;

namespace Oxigen.Core
{
    public class ChannelsSlide : Entity
    {
        public ChannelsSlide(Channel channel, Slide slide)
        {
            Channel = Channel;
            ClickThroughURL = slide.ClickThroughURL;
            DisplayDuration = slide.DisplayDuration;
            Slide = slide;
            Schedule =
                "date >= 9/03/2011 and date <= 9/03/2013 and dayofweek = monday || date >= 9/03/2011 and date <= 9/03/2013 and dayofweek = tuesday || date >= 9/03/2011 and date <= 9/03/2013 and dayofweek = wednesday || date >= 9/03/2011 and date <= 9/03/2013 and dayofweek = thursday || date >= 9/03/2011 and date <= 9/03/2013 and dayofweek = friday || date >= 9/03/2011 and date <= 9/03/2013 and dayofweek = saturday || date >= 9/03/2011 and date <= 9/03/2013 and dayofweek = sunday"
                ;
            PresentationConvertedSchedule =
                "[\"1\",\"1\",\"1\",\"1\",\"1\",\"1\",\"1\"],[\"9/03/2011\",\"9/03/2013\",\"\",\"\"]";
        }

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
