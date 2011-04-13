using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace Oxigen.ApplicationServices.RSS
{
    public class SyndicationFeedReader
    {

        public SyndicationFeed GetSyndicationFeedData(string urlFeedLocation)
        {
            if (String.IsNullOrEmpty(urlFeedLocation))
                return null; 
            var request = WebRequest.Create(urlFeedLocation);
            return GetSyndicationFeedData(request.GetResponse().GetResponseStream());
        }

        public SyndicationFeed GetSyndicationFeedData(Stream stream)
        {
            
            XmlReaderSettings settings = new XmlReaderSettings
                                             {
                                                 IgnoreWhitespace = true,
                                                 CheckCharacters = true,
                                                 CloseInput = true,
                                                 IgnoreComments = true,
                                                 IgnoreProcessingInstructions = true,
                                                 //DtdProcessing = DtdProcessing.Prohibit // .NET 4.0 option
                                             };

            using (XmlReader reader = XmlReader.Create(stream, settings))
            {
                if (reader.ReadState == ReadState.Initial)
                    reader.MoveToContent();

                // now try reading...

                Atom10FeedFormatter atom = new Atom10FeedFormatter();
                // try to read it as an atom feed
                if (atom.CanRead(reader))
                {
                    atom.ReadFrom(reader);
                    return atom.Feed;
                }

                Rss20FeedFormatter rss = new Rss20FeedFormatter();
                // try reading it as an rss feed
                if (rss.CanRead(reader))
                {
                    rss.ReadFrom(reader);
                    return rss.Feed;
                }

                // neither?
                return null;
            }
        }
    }
}
