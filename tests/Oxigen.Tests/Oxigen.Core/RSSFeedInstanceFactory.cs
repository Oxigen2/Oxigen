using System;
using Oxigen.Core;
using Oxigen.Core.Syndication;

namespace Tests.Oxigen.Core
{
    public class RSSFeedInstanceFactory
    {
        public static RSSFeed CreateValidTransientRSSFeed() {
            return new RSSFeed() {
			    URL = "http://www.telegraph.co.uk/rss", 
				LastChecked = null, 
				LastItem = null, 
				Template = null, 
				XSLT = null 
            };
        }
    }
}
