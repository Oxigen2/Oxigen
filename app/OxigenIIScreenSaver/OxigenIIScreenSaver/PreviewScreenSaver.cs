using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using OxigenIIAdvertising.LoggerInfo;

namespace OxigenIIAdvertising.ScreenSaver
{
  public partial class PreviewScreenSaver : Form, IScreenSaver
  {
    [DllImport("user32.dll")]
    private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

    private Logger _logger = null;
    private IntPtr _previewHandle;

    public PreviewScreenSaver(IntPtr previewHandle, string debugFilePath)
    {
      InitializeComponent();

      _logger = new Logger("ScreenSaver in preview mode", debugFilePath, LoggingMode.Debug);

      _previewHandle = previewHandle;
    }

    private void ScreenSaver_Load(object sender, EventArgs e)
    {
      //set the preview window as the parent of this window
      SetParent(this.Handle, _previewHandle);

      _logger.WriteMessage(DateTime.Now.ToString() + " successfully set the display control panel as the parent of the Screensaver.");

      //make this a child window, so when the select screen saver dialog closes, this will also close
      SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

      _logger.WriteMessage(DateTime.Now.ToString() + " successfully set the Screensaver as child of the display control panel so it terminates when the display control panel closes.");

      //set window's size to the size of our window's new parent
      Rectangle ParentRect;
      GetClientRect(_previewHandle, out ParentRect);

      Rectangle rect = new Rectangle(0, 0, ParentRect.Size.Width, ParentRect.Size.Height);

      this.Bounds = rect;

      _logger.WriteMessage(DateTime.Now.ToString() + " successfully set size and position of the Screensaver to the size and position of the display control panel.");

      pictureBox.Bounds = rect;
    }
  }
}
