using System;
using Oxigen.Core;

namespace Tests.Oxigen.Core
{
    public class ChannelsSlideInstanceFactory
    {
        public static ChannelsSlide CreateValidTransientChannelsSlide() {
            return new ChannelsSlide() {
			    Channel = null, 
				Slide = null, 
				ClickThroughURL = null, 
				DisplayDuration = -1, 
				Schedule = null, 
				PresentationConvertedSchedule = null 
            };
        }
    }
}
