using NUnit.Framework;
using SharpArch.Testing;
using SharpArch.Testing.NUnit;
using Oxigen.Core.Syndication;

namespace Tests.Oxigen.Core
{
    [TestFixture]
    public class SlideFeedParserTests
    {
        [Test]
        public void CanParseXML()
        {
            var slideFeedParser = new SlideFeedParser(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<slidefeed>
  <script>
    //css_ref HtmlAgilityPack.dll;
    using HtmlAgilityPack;
    using System.Text.RegularExpressions;
    public class Script {
        static public string ScrapeImage(string url) {
            var hw = new HtmlWeb();
            var doc = hw.Load(url);
            var node = doc.DocumentNode.SelectSingleNode(""//div[@id=\""articleImageHolder\""]//img"");
            var src = node.Attributes[""src""].Value;
            src = Regex.Replace(src, ""/scaleToFit/\\d+/\\d+/"", ""/scaleToFit/854/570/"");
            return src;
        }
    }
  </script>

  <item guid=""**GUID**"">
    <parameter name=""ClickThroughUrl"" type=""text"">
      Click trough URL
    </parameter>
    <parameter name=""TitleText"" type=""text"">
      Title of Slide
    </parameter>
    <parameter name=""PublishedDate"" type=""date"" format=""d MMMM yyyy HH:mm"">
      2011-05-17T13:56 
    </parameter>
    <parameter name=""MasterImage"" type=""image"">
      <call-script name=""ScrapeImage"">
        <wih-param name=""url"">http://www.mediaweek.co.uk/news/rss/1067882/IPC-Global-amplify-Ford-Fiestas-summer/</wih-param>
      </call-script>
    </parameter>
      
  </item>
  
</slidefeed>
            ");
            var slideFeed = slideFeedParser.Parse("");
            slideFeed.Items.Count.ShouldEqual(1);
            var item = slideFeed.Items[0];
            item.Guid = "**GUID**";
            item.Parameters.Count.ShouldEqual(4);
            item.Parameters["ClickThroughUrl"].GetValue().ShouldEqual("Click trough URL");
            item.Parameters["PublishedDate"].GetValue().ShouldEqual("17 May 2011 13:56");
            item.Parameters["MasterImage"].GetValue().ShouldEqual(
                "http://cached.imagescaler.hbpl.co.uk/resize/scaleToFit/854/570/?sURL=http://offlinehbpl.hbpl.co.uk/news/OWM/B662E6EC-D287-2E41-A137FE30F1A23DD6.JPG");

        }
    }
}
