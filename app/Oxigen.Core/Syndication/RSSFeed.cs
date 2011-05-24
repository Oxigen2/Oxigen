using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Drawing;
using System.Xml.Xsl;
using ImageExtraction;
using NHibernate.Validator.Constraints;
using Oxigen.Core.Flash;
using SharpArch.Core.DomainModel;
using System.Xml;




namespace Oxigen.Core.Syndication
{
    public class RSSFeed : Entity
    {
		[DomainSignature]
		public virtual string URL { get; set; }
        public virtual string Name { get; set; }
		public virtual DateTime? LastChecked { get; set; }

		public virtual string LastItem { get; set; }
        [NotNull]
		public virtual Template Template { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual Channel Channel { get; set; }
        public virtual SlideFolder SlideFolder { get; set; }
        public virtual DateTime? LastErrorDate { get; set; }
        public virtual bool LastRunHadError { get; set; }
		public virtual string XSLT { get; set; }
       
        public static Func<string, XmlReader> GetFeed = (URL) => new XmlTextReader(WebRequest.Create(URL).GetResponse().GetResponseStream());

        public virtual void Run()
        {
            try
            {
                LastRunHadError = false;
                SlideFeed slideFeed = GetSlideFeed();
                var channelssidesList = new List<ChannelsSlide>();

                foreach (var item in slideFeed.Items)
                {
                    try
                    {
                        AddSlide(item, channelssidesList);
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException(string.Format("Error creating slide/r/nURL: {0}/r/nGUID: {1}", URL, item.Guid), ex));
                        LastErrorDate = DateTime.Now;
                        LastRunHadError = true;
                    }
                }
                LastChecked = DateTime.Now;
                if (slideFeed.Items.Count > 0)
                {
                    LastItem = slideFeed.Items[0].Guid;
                    Channel.ContentLastAddedDate = DateTime.Now;
                    Channel.MadeDirtyLastDate = DateTime.Now;
                }
                foreach (var channelssilde in channelssidesList)
                {
                    Channel.AssignedSlides.Add(channelssilde);
                }
            }
            catch(Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException(string.Format("Error running feed/r/nURL: {0}", URL), ex));
                LastErrorDate = DateTime.Now;
                LastRunHadError = true;
            }

        }



        private SlideFeed GetSlideFeed()
        {
            var stringWriter = new StringWriter();
            try
            {
                var xslt = new XslCompiledTransform();
                var xmlReader = new XmlTextReader(new StringReader(XSLT));
                xslt.Load(xmlReader);
                xslt.Transform(GetFeed(URL), null, stringWriter);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Error transforming\r\nURL: {0}\r\nXSLT: {1}", URL, XSLT), ex);
            }
            try
            {
                var transformedFeed = stringWriter.ToString();
                var slideFeedParser = new SlideFeedParser(transformedFeed);
                return slideFeedParser.Parse(LastItem);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Error parsing slideFeed\r\nURL: {0}\r\nXSLT: {1}", URL, stringWriter.ToString()), ex);

            }
        }

        private void AddSlide(SlideFeedItem item, List<ChannelsSlide> channelssidesList)
        {
            string title = item.Parameters["TitleText"].GetValue();
            string url = item.Parameters["ClickThroughUrl"].GetValue();
            DateTime date = ((DateParameter)item.Parameters["PublishedDate"]).Date;
            Image img = ((ImageParameter)item.Parameters["MasterImage"]).GetImage();
            var slide = new Slide(".swf");
            slide.SlideFolderID = SlideFolder.Id;
            slide.Caption = title.Length > 400 ? title.Substring(0, 400 - 3) + "..." : title;
            slide.ClickThroughURL = url;
            slide.Creator = Publisher.DisplayName;
            slide.DisplayDuration = Template.DisplayDuration;
            slide.UserGivenDate = date;
            slide.Name = title.Length > 50 ? title.Substring(0, 50 - 3) + "..." : title;
            slide.PreviewType = "Flash";
            slide.PlayerType = "Flash";

            var slideFromTemplate = new SWAFile(Template.FileFullPathName);
            foreach (var parameter in item.Parameters.Values)
            {
                parameter.PassTo(slideFromTemplate);
            }

            //Aspose has a bug with getting thumbnail so use the asset content image for now
            //var image = slideFromTemplate.GetThumbnail();
            ImageUtilities.Crop(img, 100, 75, AnchorPosition.Center).Save(slide.ThumbnailFullPathName);
            slideFromTemplate.Save(slide.FileFullPathName);
            slide.Length = (int)new FileInfo(slide.FileFullPathName).Length;
            //Add slides in reverse order
            channelssidesList.Insert(0, new ChannelsSlide(Channel, slide));
        }
    }
}
