using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowHandleWrapper
{
  public class Win32WindowHandleWrapper : IWin32Window
  {
    public IntPtr Handle
    {
      get
      {
        return handle;
      }
      set
      {
        handle = value;
      }
    }

    private IntPtr handle;
  }
}
