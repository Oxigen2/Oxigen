using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Xml;
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
        private readonly string _text;
        private readonly string _format;

        public DateParameter(string name, string text, string format) : base(name)
        {
            _text = text;
            _format = format;
        }

        public override string GetValue()
        {
            return Date.ToString(_format);
        }

        public DateTime Date
        {
            get { return DateTime.Parse(_text); }
        }

    }

    public class ImageParameter : Parameter
    {
        private readonly string _url;

        public ImageParameter(string name, string url) : base(name)
        {
            _url = url;
        }

        public override string GetValue() {
            return _url;
        }

        public override void PassTo(SWAFile template) {
            template.UpdateBitmap(Name, GetImage());
        }

        public Image GetImage()
        {
            WebRequest req = WebRequest.Create(_url);
            WebResponse response = req.GetResponse();
            Stream stream = response.GetResponseStream();
            Image img = Image.FromStream(stream);
            stream.Close();
            return img;
        }
    }
}