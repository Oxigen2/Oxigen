using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OxigenIIAdvertising.ScreenSaver
{
    public interface IPlayer
    {
        void EnableSound(bool enableSound);
        void Play();
        Control Control {get;}
    }
}
