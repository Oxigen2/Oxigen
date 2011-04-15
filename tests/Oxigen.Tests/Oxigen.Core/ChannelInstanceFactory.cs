using System;
using Oxigen.Core;

namespace Tests.Oxigen.Core
{
    public class ChannelInstanceFactory
    {
        public static Channel CreateValidTransientChannel() {
            return new Channel() {
			    CategoryID = null, 
				Publisher = null, 
				ChannelName = null, 
				ChannelGUID = "0FEB4452-E74D-4B89-B4B2-2ED667024878_A", 
				ChannelDescription = null, 
				ChannelLongDescription = null, 
				Keywords = null, 
				ImagePath = null, 
				bHasDefaultThumbnail = false, 
				bLocked = false, 
				bAcceptPasswordRequests = true, 
				ChannelPassword = null, 
				NoContent = 0, 
				NoFollowers = 0, 
				AddDate = DateTime.Parse("4/12/2011 2:09:36 PM"), 
				EditDate = DateTime.Parse("4/12/2011 2:09:36 PM"), 
				MadeDirtyLastDate = DateTime.Parse("4/12/2011 2:09:36 PM"), 
				ContentLastAddedDate = DateTime.Parse("4/12/2011 2:09:36 PM") 
            };
        }
    }
}
