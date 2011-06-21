using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using NUnit.Framework;

namespace Tests.Oxigen.Web.Assets
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestKeepAlive()
        {
            //var req = WebRequest.Create("http://assets.oxigen.net/slide/11E661CE-4AE2-458C-BFA3-9F672A7F3DE4_B.swf");
            //var res = req.GetResponse();
            
           using (var client = new WebClient())
           {
               client.CachePolicy = new RequestCachePolicy(RequestCacheLevel.Revalidate);
               //client.Headers.Add(HttpRequestHeader.IfModifiedSince, DateTime.UtcNow.ToString("r"));
               client.DownloadFile("http://new.oxigen.net/Images/Default/googlechrome.png?1", "test.swf");
               client.DownloadFile("http://assets.oxigen.net/slide/09C45970-1040-4658-ABBC-352E2932BA7D_X.swf", "test2.swf");
           }
        }
    }
}
