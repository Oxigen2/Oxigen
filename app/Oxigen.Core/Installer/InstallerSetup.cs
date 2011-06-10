using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oxigen.Core.Installer
{
    public class InstallerSetup
    {
        private string _text = "";
        private string _extractorFileName = "";
        private string _folderBaseName = "";
        private  string _urlKey = "";

        public string ExtractorFileName
        {
            get { return _extractorFileName; }
        }

        public string UrlKey {
            get { return _urlKey; }
        }

        public string FolderName
        {
            get { return _folderBaseName + "-" + _extractorFileName; }
        }

        public void Add(int channelId, string channelGuid, string channelName, int weighting)
        {
            _text = _text + string.Format("{0},,{1},,{2},,{3}\r\n", channelId, channelGuid, channelName, weighting);
            _folderBaseName = _folderBaseName + string.Format("{0}-{1}", channelId, weighting);
            
            if (_urlKey != "") _urlKey = _urlKey + "+";
            _urlKey = _urlKey + channelId + ((Channel.DefaultWeighting == weighting) ? "" : "." + weighting) + "-" + channelName.Replace(" ", "-");
            
            if (_extractorFileName =="")
                _extractorFileName = channelName.Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
            else
            {
                _extractorFileName = "OxigenInstaller";
            }
        }

        public string GetSetupText()
        {
            return _text;
        }
    }
}
