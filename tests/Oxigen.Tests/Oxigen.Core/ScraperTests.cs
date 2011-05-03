using System;
using SharpArch.Testing;
using SharpArch.Testing.NUnit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using NUnit.Framework;

namespace Tests.Oxigen.Core
{
    [TestFixture]
    public class ScraperTests
    {
        [Test]
        public void CanExtractImageForMediaWeak()
        {
            var hw = new HtmlWeb();
            var doc = hw.Load("http://www.mediaweek.co.uk/news/rss/1067882/IPC-Global-amplify-Ford-Fiestas-summer/");
            var node = doc.DocumentNode.SelectSingleNode("//div[@id=\"articleImageHolder\"]//img");
            var src = node.Attributes["src"].Value;
            src.ShouldEqual(
                "http://cached.imagescaler.hbpl.co.uk/resize/scaleToFit/427/285/?sURL=http://offlinehbpl.hbpl.co.uk/news/OWM/B662E6EC-D287-2E41-A137FE30F1A23DD6.JPG");
        }
    }
}
