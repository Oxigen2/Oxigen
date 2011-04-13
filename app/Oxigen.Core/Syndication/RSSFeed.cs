using System;
using System.IO;
using System.Net;
using System.Xml.Xsl;
using NHibernate.Validator.Constraints;
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


    }
}
