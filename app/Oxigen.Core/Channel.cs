using System;
using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;
using System.Collections.Generic;

namespace Oxigen.Core
{
    public class Channel : Entity
    {
		public virtual int? CategoryID { get; set; }

		public virtual Publisher Publisher { get; set; }

		public virtual string ChannelName { get; set; }

		[DomainSignature]
		public virtual string ChannelGUID { get; set; }

		public virtual string ChannelDescription { get; set; }

		public virtual string ChannelLongDescription { get; set; }

		public virtual string Keywords { get; set; }

		public virtual string ImagePath { get; set; }

		public virtual bool bHasDefaultThumbnail { get; set; }

		public virtual bool bLocked { get; set; }

		public virtual bool bAcceptPasswordRequests { get; set; }

		public virtual string ChannelPassword { get; set; }

		public virtual string ChannelGUIDSuffix { get; set; }

		public virtual int NoContent { get; set; }

		public virtual int NoFollowers { get; set; }

		public virtual DateTime AddDate { get; set; }

		public virtual DateTime EditDate { get; set; }

		public virtual DateTime MadeDirtyLastDate { get; set; }

		public virtual DateTime ContentLastAddedDate { get; set; }

        public virtual IList<ChannelsSlide> AssignedSlides { get; set; }
    }
}
