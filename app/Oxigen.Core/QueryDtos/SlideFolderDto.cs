using System;

namespace Oxigen.Core.QueryDtos
{
    public class SlideFolderDto
    {
        public int Id { get; set; }
		public string SlideFolderName { get; set; }
		public Publisher Publisher { get; set; }
    }
}
