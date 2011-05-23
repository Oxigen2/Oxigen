using System;
using System.IO;
using System.Xml;
using CSScriptLibrary;
using HtmlAgilityPack;

namespace Oxigen.Core.Syndication
{
    public class SlideFeedParser
    {
        private XmlDocument _dom;

        public SlideFeedParser(string xml)
        {
            _dom = new XmlDocument();
            _dom.LoadXml(xml);
        }

        public SlideFeedParser(Stream stream)
        {
            _dom = new XmlDocument();
            _dom.Load(stream);            
        }
        public SlideFeed Parse(string lastGuid)
        {
            if (_dom.DocumentElement.Name == "items") return ParseLegacy(lastGuid);

            var slideFeed = new SlideFeed();
            var items = _dom.SelectNodes(@"slidefeed/item");
            foreach (XmlNode item in items)
            {
                string guid = item.Attributes["guid"].Value;
                if (guid == lastGuid) break;
                //try
                //{
                    SlideFeedItem slideFeedItem = GetSlideFeedItem(item);
                    if (slideFeedItem != null) slideFeed.Items.Add(slideFeedItem);
                //}

            }
            
            return slideFeed;
        }

        private SlideFeedItem GetSlideFeedItem(XmlNode item)
        {
            var slideFeedItem = new SlideFeedItem()
                                    {
                                        Guid = item.Attributes["guid"].Value
                                    };
            var parameterNodes = item.SelectNodes(@"./parameter");
            foreach (XmlNode parameterNode in parameterNodes)
            {
                string parameterData = null;
                string parametername = parameterNode.Attributes["name"].Value;
                
                //Check if we need to run a script to get the data
                var callScriptNode = parameterNode.SelectSingleNode(@"./call-script");
                if (callScriptNode != null)
                {
                    string functionName = callScriptNode.Attributes["name"].Value;
                    string functionParam = parameterNode.InnerText.Trim();
                    parameterData = RunScript(_dom.SelectSingleNode(@"slidefeed/script").InnerText, functionName, functionParam);
                    //if (parameterData == null)
                    //{
                        //throw new Exception("Could not get " + parametername + " for " + slideFeedItem.Guid);
                    //}
                    if (parameterData == null) return null;
                }
                else
                {
                    parameterData = parameterNode.InnerText.Trim();
                        
                }
                Parameter parameter = null;
                

                switch (parameterNode.Attributes["type"].Value)
                {
                    case "text":
                        parameter = new TextParameter(parametername, parameterData);
                        break;
                    case "date":
                        parameter = new DateParameter(parametername, parameterData, parameterNode.Attributes["format"].Value);
                        break;
                    case "image":
                        parameter = new ImageParameter(parametername, parameterData);
                        break;

                }

                slideFeedItem.Add(parameter);
            }
            return slideFeedItem;
        }

        private SlideFeed ParseLegacy(string lastGuid)
        {
            var slideFeed = new SlideFeed();
            foreach (XmlNode item in _dom.DocumentElement.ChildNodes)
            {
                string guid = item.SelectSingleNode("guid").InnerText;
                if (guid == lastGuid) break;

                var slideFeedItem = new SlideFeedItem()
                                        {
                                            Guid = item.SelectSingleNode("guid").InnerText
                                        };
                slideFeedItem.Add(new DateParameter("PublishedDate", item.SelectSingleNode("date").InnerText, "d MMMM yyyy HH:mm"));
                slideFeedItem.Add(new TextParameter("ClickThroughUrl", item.SelectSingleNode("url").InnerText));
                slideFeedItem.Add(new TextParameter("TitleText", item.SelectSingleNode("title").InnerText));
                slideFeedItem.Add(new ImageParameter("MasterImage", item.SelectSingleNode("image").InnerText));
                if (item.SelectSingleNode("credit") != null)
                    slideFeedItem.Add(new TextParameter("ImageCreditText", item.SelectSingleNode("credit").InnerText));
                slideFeed.Items.Add(slideFeedItem);
            }
            return slideFeed;
        }

        private string RunScript(string script, string functionName, string functionParam)
        {
            CSScript.CacheEnabled = true;
            AsmHelper helper = new AsmHelper(CSScript.LoadCode(script, null, false));
            return (string)helper.Invoke("Script." + functionName, functionParam);
        }

        //Keep at least on refeference to the HtmlAgilityPack to force the Web Deployment Package to include the assembly
        //This is currenly only used by the runtime scripts
        private void ReferenceHtmlAgilityPack()
        {
            var hw = new HtmlWeb();
        }
    }
}