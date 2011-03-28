using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace OxigenIIAdvertising.ComponentInfo
{
  public static class ComponentInfoDeterminator
  {
    /// <summary>
    /// Gets the file version info of all the files in a directory.
    /// </summary>
    /// <param name="path">the path to look in</param>
    /// <returns>a ComponentInfo array with all binary files and their version info.</returns>
    public static ComponentInfo[] GetComponents(string path)
    {
      string[] files = Directory.GetFiles(path);

      int length = files.Length;

      ComponentInfo[] components = new ComponentInfo[length];

      FileVersionInfo fileVersionInfo;

      for (int counter = 0; counter < length; counter++)
      {
        ComponentInfo ci = new ComponentInfo();

        fileVersionInfo = FileVersionInfo.GetVersionInfo(files[counter]);

        ci.ComponentFile = Path.GetFileName(files[counter]);
        ci.ComponentVersion = fileVersionInfo.FileVersion;

        components[counter] = ci;
      }

      return components;
    }
  }
}
