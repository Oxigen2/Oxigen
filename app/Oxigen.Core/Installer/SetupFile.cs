using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oxigen.Core.Installer
{
    public class SetupFile
    {
        private string _text = "";

        public void Add(int channelId, string channelGuid, string channelName, int weighting)
        {
            _text = _text + string.Format("{0},,{1},,{2},,{3}\r\n", channelId, channelGuid, channelName, weighting);
        }

        public string GetSetupText()
        {
            return _text;
        }
    }
}
