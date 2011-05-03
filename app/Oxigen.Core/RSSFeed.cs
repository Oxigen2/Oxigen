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


namespace Oxigen.Core
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

        public virtual string ApplyXSLT()
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            var xmlReader = new XmlTextReader(new StringReader(XSLT));
            xslt.Load(xmlReader);
            var output = new StringWriter();
            xslt.Transform(GetFeed(URL), null, output);
            var jsonString = output.ToString();
            Console.Write(jsonString);
            JObject jsonFeed = JObject.Parse(jsonString);
            foreach (JToken item in jsonFeed["items"]) {
                DateTime date = item["date"].Value<DateTime>();
                string guid = (string)item["guid"].Value<string>();
                string url = (string)item["url"].Value<string>();
                string title = (string)item["title"].Value<string>();
                string image = (string)item["image"].Value<string>();
                string credit = item["credit"] == null ? null : item["credit"].Value<string>();
                
            }
            
            return output.ToString();
        }
        
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
            dom.Load(output);
            bool bFirst = true;
            var previousLastItem = LastItem;
            var channelssidesList = new List<ChannelsSlide>();
            foreach (XmlNode item in dom.DocumentElement.ChildNodes)
            {
                string guid = item.SelectSingleNode("guid").InnerText;
                if (guid == previousLastItem) break;
                if (bFirst)
                {
                    bFirst = false;
                    LastItem = guid;
                }
                
                DateTime date = DateTime.Parse(item.SelectSingleNode("date").InnerText);
                
                string url = item.SelectSingleNode("url").InnerText;
                string title = item.SelectSingleNode("title").InnerText;
                string imageUrl = item.SelectSingleNode("image").InnerText;
                string credit = item.SelectSingleNode("credit") == null ? null : item.SelectSingleNode("credit").InnerText;

                WebRequest req = WebRequest.Create(imageUrl);
                WebResponse response = req.GetResponse();       
                Stream stream = response.GetResponseStream();
                Image img = Image.FromStream(stream);
                stream.Close();

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
                slideFromTemplate.UpdateBitmap("MasterImage", img);
                slideFromTemplate.UpdateText("TitleText", title);
                slideFromTemplate.UpdateText("ImageCreditText", credit);
                slideFromTemplate.UpdateText("PublishedDate", date.ToString("d MMMM yyyy HH:mm"));
                //Aspose has a bug with getting thumbnail so use the asset content image for now
                //var image = slideFromTemplate.GetThumbnail();
                ImageUtilities.Crop(img, 100, 75, AnchorPosition.Center).Save(slide.ThumbnailFullPathName);
                slideFromTemplate.Save(slide.FileFullPathName);
                slide.Length = (int)new FileInfo(slide.FileFullPathName).Length;
                //Add slides in reverse order
                channelssidesList.Insert(0, new ChannelsSlide
                                               {
                                                   Channel = Channel,
                                                   ClickThroughURL = slide.ClickThroughURL,
                                                   DisplayDuration = slide.DisplayDuration,
                                                   Slide = slide,
                                                   Schedule =
                                                       "date >= 9/03/2011 and date <= 9/03/2013 and dayofweek = monday || date >= 9/03/2011 and date <= 9/03/2013 and dayofweek = tuesday || date >= 9/03/2011 and date <= 9/03/2013 and dayofweek = wednesday || date >= 9/03/2011 and date <= 9/03/2013 and dayofweek = thursday || date >= 9/03/2011 and date <= 9/03/2013 and dayofweek = friday || date >= 9/03/2011 and date <= 9/03/2013 and dayofweek = saturday || date >= 9/03/2011 and date <= 9/03/2013 and dayofweek = sunday",
                                                   PresentationConvertedSchedule =
                                                       "[\"1\",\"1\",\"1\",\"1\",\"1\",\"1\",\"1\"],[\"9/03/2011\",\"9/03/2013\",\"\",\"\"]"

                                               });

            }
            LastChecked = DateTime.Now;
            if (previousLastItem != LastItem)
            { 
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
