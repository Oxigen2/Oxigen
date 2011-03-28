using System;

namespace Oxigen.Core.QueryDtos
{
    public class PublisherDto
    {
        public int Id { get; set; }
		public int UserID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string DisplayName { get; set; }
		public string EmailAddress { get; set; }
		public long UsedBytes { get; set; }
		public long TotalAvailableBytes { get; set; }
    }
}
