using System;

namespace Oxigen.Core.QueryDtos
{
    public class ChannelsSlideDto
    {
        public int Id { get; set; }
		public Channel Channel { get; set; }
		public Slide Slide { get; set; }
		public string ClickThroughURL { get; set; }
		public float DisplayDuration { get; set; }
		public string Schedule { get; set; }
		public string PresentationConvertedSchedule { get; set; }
    }
}
