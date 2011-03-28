using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace OxigenIIAdvertising.ScreenSaver7
{
  public class UserInputFilter : IMessageFilter
  {
    private Point _originalPosition = Cursor.Position;

    public bool PreFilterMessage(ref Message m)
    {
      // key press
      if (m.Msg == 0x0100)
      {
        // was pressed key the space bar
        if (m.WParam == new IntPtr(0x20))
          Program.TerminateApplication(true);
        else
          Program.TerminateApplication(false);

        return true;
      }

      // mouse movement
      if (m.Msg == 0x0200)
      {
        if (Math.Abs(Cursor.Position.X - _originalPosition.X) > 20 | Math.Abs(Cursor.Position.Y - _originalPosition.Y) > 20)
        {
          Program.TerminateApplication(false);

          return true;
        }
      }

      return false;
    }
  }
}
