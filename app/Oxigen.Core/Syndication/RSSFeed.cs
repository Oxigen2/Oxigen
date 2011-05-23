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
using Newtonsoft.Json.Linq;


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

		public virtual string XSLT { get; set; }

        //public virtual string ApplyXSLT()
        //{
        //    XslCompiledTransform xslt = new XslCompiledTransform();
        //    var xmlReader = new XmlTextReader(new StringReader(XSLT));
        //    xslt.Load(xmlReader);
        //    var output = new StringWriter();
        //    xslt.Transform(GetFeed(URL), null, output);
        //    var jsonString = output.ToString();
        //    Console.Write(jsonString);
        //    JObject jsonFeed = JObject.Parse(jsonString);
        //    foreach (JToken item in jsonFeed["items"]) {
        //        DateTime date = item["date"].Value<DateTime>();
        //        string guid = (string)item["guid"].Value<string>();
        //        string url = (string)item["url"].Value<string>();
        //        string title = (string)item["title"].Value<string>();
        //        string image = (string)item["image"].Value<string>();
        //        string credit = item["credit"] == null ? null : item["credit"].Value<string>();
        //        
        //    }
        //    
        //    return output.ToString();
        //}
        
        public static Func<string, XmlReader> GetFeed = (URL) => new XmlTextReader(WebRequest.Create(URL).GetResponse().GetResponseStream());


        public virtual void Run()
        {
            var xslt = new XslCompiledTransform();
            var xmlReader = new XmlTextReader(new StringReader(XSLT));
            xslt.Load(xmlReader);
            var output = new MemoryStream();
            xslt.Transform(GetFeed(URL), null, output);
            var dom = new XmlDocument();
            output.Seek(0, SeekOrigin.Begin);
            var slideFeedParser = new SlideFeedParser(output);
            var slideFeed = slideFeedParser.Parse(LastItem);
            var channelssidesList = new List<ChannelsSlide>();
      
            foreach (var item in slideFeed.Items)
            {
                string title = item.Parameters["TitleText"].GetValue();
                string url = item.Parameters["ClickThroughUrl"].GetValue();
                DateTime date = ((DateParameter) item.Parameters["PublishedDate"]).Date;
                Image img = ((ImageParameter) item.Parameters["MasterImage"]).GetImage();
                var slide = new Slide(".swf");
                slide.SlideFolderID = SlideFolder.Id;
                slide.Caption = title.Length > 400 ?  title.Substring(0, 400-3) + "..." : title;
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
            LastChecked = DateTime.Now;
            if (slideFeed.Items.Count>0)
            {
                LastItem = slideFeed.Items[0].Guid;
                Channel.ContentLastAddedDate = DateTime.Now;
                Channel.MadeDirtyLastDate = DateTime.Now;
            }
            foreach(var channelssilde in channelssidesList)
            {    
                Channel.AssignedSlides.Add(channelssilde);
            }

        }
    }
}
