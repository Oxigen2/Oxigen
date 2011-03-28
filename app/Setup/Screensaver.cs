using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Setup
{
  public static class ScreenSaver
  {
    // Signatures for unmanaged calls

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool SystemParametersInfo(
       int uAction, int uParam, ref int lpvParam,
       int flags);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool SystemParametersInfo(
       int uAction, int uParam, ref bool lpvParam,
       int flags);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int PostMessage(IntPtr hWnd,
       int wMsg, int wParam, int lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr OpenDesktop(
       string hDesktop, int Flags, bool Inherit,
       uint DesiredAccess);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool CloseDesktop(
       IntPtr hDesktop);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool EnumDesktopWindows(
       IntPtr hDesktop, EnumDesktopWindowsProc callback,
       IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool IsWindowVisible(
       IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetForegroundWindow();

    // Callbacks
    private delegate bool EnumDesktopWindowsProc(IntPtr hDesktop, IntPtr lParam);

    // Constants
    private const int SPI_GETSCREENSAVERACTIVE = 16;
    private const int SPI_SETSCREENSAVERACTIVE = 17;
    private const int SPI_GETSCREENSAVERTIMEOUT = 14;
    private const int SPI_SETSCREENSAVERTIMEOUT = 15;
    private const int SPI_GETSCREENSAVERRUNNING = 114;
    private const int SPIF_SENDWININICHANGE = 2;

    private const uint DESKTOP_WRITEOBJECTS = 0x0080;
    private const uint DESKTOP_READOBJECTS = 0x0001;
    private const int WM_CLOSE = 16;
        
    // Returns TRUE if the screen saver is active 
    // (enabled, but not necessarily running).
    public static bool GetScreenSaverActive()
    {
      bool isActive = false;

      SystemParametersInfo(SPI_GETSCREENSAVERACTIVE, 0, ref isActive, 0);
      return isActive;
    }

    // Pass in TRUE(1) to activate or FALSE(0) to deactivate
    // the screen saver.
    public static void SetScreenSaverActive(int Active)
    {
      int nullVar = 0;

      SystemParametersInfo(SPI_SETSCREENSAVERACTIVE, Active, ref nullVar, SPIF_SENDWININICHANGE);
    }

    public static void SetScreenSaver(string ScreenSaverName)
    {
      string systemFolder = SetupHelper.GetSystemDirectory() + "\\";

      GenericRegistryAccess.SetRegistryValue(@"HKEY_CURRENT_USER\Control Panel\Desktop", "SCRNSAVE.EXE", systemFolder + ScreenSaverName + ".scr");
    }

    public static Int32 GetScreenSaverTimeout()
    {
      Int32 value = 0;

      SystemParametersInfo(SPI_GETSCREENSAVERTIMEOUT, 0,
         ref value, 0);
      return value;
    }

    public static void SetScreenSaverTimeout(Int32 Value)
    {
      int nullVar = 0;

      SystemParametersInfo(SPI_SETSCREENSAVERTIMEOUT,
         Value, ref nullVar, SPIF_SENDWININICHANGE);
    }
  }
}
