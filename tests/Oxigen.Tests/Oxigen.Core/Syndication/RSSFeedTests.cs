using NUnit.Framework;
using SharpArch.Testing;
using SharpArch.Testing.NUnit;
using Oxigen.Core.Syndication;
using System.Xml;
using System.Reflection;
using System.IO;

namespace Tests.Oxigen.Core.Syndication
{
	[TestFixture]
    public class RSSFeedTests
    {
        [Test]
        public void CanCompareRSSFeeds() {
            RSSFeed instance = new RSSFeed();
            instance.URL = "http://www.telegraph.co.uk/rss";

            RSSFeed instanceToCompareTo = new RSSFeed();
            instanceToCompareTo.URL = "http://www.telegraph.co.uk/rss";

            instance.ShouldEqual(instanceToCompareTo);
        }

        [Test]
        public void CanProcessFeed() {
            RSSFeed instance = new RSSFeed();
            
            instance.XSLT = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(
                    "Tests.Oxigen.Core.Syndication.Telegraph.xslt")).ReadToEnd();
            var telegraphFeed = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                    "Tests.Oxigen.Core.Syndication.TelegraphSample.xml");
            RSSFeed.GetFeed = (URL) => new XmlTextReader(telegraphFeed);
            System.Console.Write(instance.ApplyXSLT());
        }


    }
}
