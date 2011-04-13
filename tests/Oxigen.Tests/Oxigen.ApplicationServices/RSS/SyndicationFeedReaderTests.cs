using System.Reflection;
using System.ServiceModel.Syndication;
using NUnit.Framework;
using Oxigen.ApplicationServices.RSS;
using Rhino.Mocks;
using SharpArch.Testing.NUnit;
using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport;
using Oxigen.Core;
using Oxigen.ApplicationServices;
using Oxigen.ApplicationServices.ViewModels;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;
using Tests.Oxigen.Core;
 

namespace Tests.Oxigen.ApplicationServices.RSS
{
    [TestFixture]
    public class SyndicationFeedReaderTests
    {
        [Test]
        public void CanReadTheTelegraph()
        {
            var stream =
                Assembly.GetExecutingAssembly().GetManifestResourceStream(
                    "Tests.Oxigen.ApplicationServices.RSS.TelegraphSample.xml");
            var rssFeedReader = new SyndicationFeedReader();
            SyndicationFeed syn = rssFeedReader.GetSyndicationFeedData(stream);
            //SyndicationItem item = syn.Items[0];
            

        }

        [Test]
        public void CanReadTheTelegraphFromURL() {
            var rssFeedReader = new SyndicationFeedReader();
            SyndicationFeed syn = rssFeedReader.GetSyndicationFeedData("http://www.telegraph.co.uk/rss");
            //SyndicationItem item = syn.Items[0];


        }

        [Test]
        public void CanReadTheGuardian() {
            var stream =
                Assembly.GetExecutingAssembly().GetManifestResourceStream(
                    "Tests.Oxigen.ApplicationServices.RSS.GuardianSample.xml");
            var rssFeedReader = new SyndicationFeedReader();
            SyndicationFeed syn = rssFeedReader.GetSyndicationFeedData(stream);
            foreach(SyndicationItem item in syn.Items)
            {
                
            }
            //SyndicationItem item = syn.Items[0];


        }
    }
}
