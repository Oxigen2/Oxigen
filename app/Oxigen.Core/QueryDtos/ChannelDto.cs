using System;

namespace Oxigen.Core.QueryDtos
{
    public class ChannelDto
    {
        public int Id { get; set; }
		public int? CategoryID { get; set; }
		public Publisher Publisher { get; set; }
		public string ChannelName { get; set; }
		public string ChannelGUID { get; set; }
		public string ChannelDescription { get; set; }
		public string ChannelLongDescription { get; set; }
		public string Keywords { get; set; }
		public string ImagePath { get; set; }
		public bool bHasDefaultThumbnail { get; set; }
		public bool bLocked { get; set; }
		public bool bAcceptPasswordRequests { get; set; }
		public string ChannelPassword { get; set; }
		public string ChannelGUIDSuffix { get; set; }
		public int NoContent { get; set; }
		public int NoFollowers { get; set; }
		public DateTime AddDate { get; set; }
		public DateTime EditDate { get; set; }
		public DateTime MadeDirtyLastDate { get; set; }
		public DateTime ContentLastAddedDate { get; set; }
    }
}
