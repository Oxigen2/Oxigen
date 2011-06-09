using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using Oxigen.Core.Flash;

namespace Oxigen.Core.Syndication
{
    public class SlideFeedItem
    {
        public Dictionary<string, Parameter> Parameters = new Dictionary<string, Parameter>();
        public string Guid { get; set; }

        public void Add(Parameter parameter)
        {
            Parameters.Add(parameter.Name, parameter);
        }
    }

    public abstract class Parameter
    {
        protected Parameter(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public abstract string GetValue();

        public virtual void PassTo(SWAFile template)
        {
            template.UpdateText(Name, GetValue());
        }
    }

    public class TextParameter : Parameter
    {
        private readonly string _text;

        public TextParameter(string name, string text) : base(name)
        {
            _text = text;
        }

        public override string GetValue()
        {
            return _text;
        }
    }

    public class DateParameter : Parameter
    {
        private readonly string _format;
        private readonly string _text;

        public DateParameter(string name, string text, string format) : base(name)
        {
            _text = text;
            _format = format;
        }

        public DateTime Date
        {
            get { return DateTime.Parse(_text); }
        }

        public override string GetValue()
        {
            return Date.ToString(_format);
        }
    }

    public class ImageParameter : Parameter
    {
        private readonly string _url;

        public ImageParameter(string name, string url) : base(name)
        {
            _url = url;
        }

        public override string GetValue()
        {
            return _url;
        }

        public override void PassTo(SWAFile template)
        {
            template.UpdateBitmap(Name, GetImage());
        }

        public Image GetImage()
        {
            try
            {
                WebRequest webRequest = WebRequest.Create(_url);
                webRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
                var webResponse = (HttpWebResponse) webRequest.GetResponse();

                using (Stream stream = GetStreamForResponse(webResponse))
                {
                    Image img = Image.FromStream(stream);
                    return img;
                }
            }
            catch(Exception ex)
            {
                throw new Exception("failed to get image: " + _url + "/r/n", ex);
            }
        }

        private static Stream GetStreamForResponse(HttpWebResponse webResponse)
        {
            Stream stream;
            switch (webResponse.ContentEncoding.ToUpperInvariant())
            {
                case "GZIP":
                    stream = new GZipStream(webResponse.GetResponseStream(), CompressionMode.Decompress);
                    break;
                case "DEFLATE":
                    stream = new DeflateStream(webResponse.GetResponseStream(), CompressionMode.Decompress);
                    break;

                default:
                    stream = webResponse.GetResponseStream();
                    stream.ReadTimeout = 10000;
                    break;
            }
            return stream;
        }
    }
}