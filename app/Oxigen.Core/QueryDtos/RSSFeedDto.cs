using System;

namespace Oxigen.Core.QueryDtos
{
    public class RSSFeedDto
    {
        public int Id { get; set; }
		public string URL { get; set; }
        public string Name { get; set; }
        public bool LastRunHadError { get; set; }
    }
}
