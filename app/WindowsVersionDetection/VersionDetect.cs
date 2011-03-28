using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsVersionDetection
{
  /// <summary>
  /// Provides static methods to detect the version of the Windows operating system the software runs on
  /// </summary>
  public static class VersionDetect
  {
    /// <summary>
    /// Detects the version of the Windows operating system the software runs on
    /// </summary>
    /// <returns>An enumeration with the installed operating system</returns>
    public static WindowsVersion GetWindowsVersion()
    {
      OperatingSystem os = Environment.OSVersion;
      Version version = os.Version;

      // pre-NT windows
      if (os.Platform == PlatformID.Win32Windows)
      {
        switch (version.Minor)
        {
          case 0:
            return WindowsVersion.Windows95;
          case 10:
            if (version.Revision.ToString() == "2222A")
              return WindowsVersion.Windows98SE;
            return WindowsVersion.Windows98;
          case 90:
            return WindowsVersion.WindowsME;
        }
      }

      // NT windows
      if (os.Platform == PlatformID.Win32NT)
      {
        switch (version.Major)
        {
          case 3:
            return WindowsVersion.WindowsNT3_51;
          case 4:
            return WindowsVersion.WindowsNT4_0;
          case 5:
            if (version.Major == 0)
              return WindowsVersion.Windows2000;
            return WindowsVersion.WindowsXP;
          case 6:
            if (version.Minor == 0)
              return WindowsVersion.WindowsVista;
            return WindowsVersion.Windows7;
        }
      }

      throw new OxigenIIAdvertising.Exceptions.WindowsVersionDetectException("Cannot find version of operating system or it is not supported");
    }    
  }
}
