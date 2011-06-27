using System.Net;
using System.Net.Cache;
using NUnit.Framework;

namespace Tests.Oxigen.Web.Mdata
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void GetMdata()
        {
            using (var webClient = new WebClient())
            {
                webClient.CachePolicy = new RequestCachePolicy(RequestCacheLevel.Default);
                webClient.DownloadFile("http://mdata.oxigen.net/subscriberinfo/subscriptions/7629924F-0965-4F0F-97D3-6D8BEC7972F2_J", @"C:\Oxigen2\Oxigen\TempRepository\ss_channel_subscriptions.dat");
                webClient.DownloadFile("http://mdata.oxigen.net/subscriberinfo/demographicdata/F3D537B9-BB79-4EBA-BC16-08AD326EFEF0_N", @"C:\Oxigen2\Oxigen\TempRepository\ss_channel_subscriptions.dat");
            }
        }
    }
}
