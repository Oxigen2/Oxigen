using System;

namespace Oxigen.Core.QueryDtos
{
    public class SlideFolderDto
    {
        public int Id { get; set; }
		public string SlideFolderName { get; set; }
		public int PublisherID { get; set; }
        public int SlideCount { get; set; }
        public int MaxSlideCount { get; set; }
    }
}
