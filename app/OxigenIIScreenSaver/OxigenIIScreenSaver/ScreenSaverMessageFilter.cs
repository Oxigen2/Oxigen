using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using OxigenIIAdvertising.LoggerInfo;

namespace OxigenIIAdvertising.ScreenSaver
{
  public class ScreenSaverMessageFilter : IMessageFilter
  {
    private Point _originalPosition = Cursor.Position;

    public bool PreFilterMessage(ref Message m)
    {      
      // key press
      if (m.Msg == 0x0100)
      {
          // was pressed key the space bar
          if (m.WParam == new IntPtr(0x20))
          {
              Program.TerminateApplication(true);
              return true;
          }
          
      }
        //else
        //  Program.TerminateApplication(false);

        return false;
      }

      // mouse movement
      //if (m.Msg == 0x0200)
      //{
      //  if (Math.Abs(Cursor.Position.X - _originalPosition.X) > 20 | Math.Abs(Cursor.Position.Y - _originalPosition.Y) > 20)
      //  {
      //    Program.TerminateApplication(false);

      //    return true;
      //  }
      //}

      //return false;
    //}
  }
}
