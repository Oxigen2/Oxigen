using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FilenameMakerLib
{
  /// <summary>
  /// Provides static methods to create a unique filename
  /// </summary>
  public static class FilenameFromGUID
  {
    /// <summary>
    /// Makes a filename from an existing filename by creating the GUID plus
    /// an underscore and the first letter of an existing filename.
    /// </summary>
    /// <param name="originalFilename">the original filename</param>
    /// <param name="newFilename">the new filename in the format of GUID_&lt;first letter&gt; and the original filename's extension</param>
    /// <param name="newFilenameWithoutExtension">the new filename in the format of GUID_&lt;first letter&gt; without an extension</param>
    /// <param name="firstLetter">the first letter of the original filename</param>
    public static void MakeFilenameAndFolder(string originalFilename, out string newFilename, 
      out string newFilenameWithoutExtension, out string firstLetter)
    {
      if (originalFilename == String.Empty)
      {
        newFilename = String.Empty;
        firstLetter = String.Empty;
      }

      firstLetter = originalFilename.Substring(0, 1);

      int firstletterInt;

      if (int.TryParse(firstLetter, out firstletterInt))
        firstLetter = GetRandomLetter().ToString();

      string fileExtension = System.IO.Path.GetExtension(originalFilename);

      newFilenameWithoutExtension = System.Guid.NewGuid().ToString() + "_" + firstLetter.ToUpper();

      newFilename = newFilenameWithoutExtension + fileExtension;
    }

    /// <summary>
    /// Takes a GUID with a suffixed underscore and a letter and returns the letter after the underscore
    /// </summary>
    /// <param name="GUIDFilename">a string of the format GUID_[letter]</param>
    public static string GetGUIDSuffix(string GUIDFilename)
    {
      return GUIDFilename.Substring(GUIDFilename.LastIndexOf('_') + 1, 1);
    }

    private static char GetRandomLetter()
    {
        string rand = Path.GetRandomFileName();

        short alpha = (short)'A';
        short zed = (short)'Z';

        foreach (char c in rand)
        {
            char cUpper = c.ToString().ToUpper()[0];

            if ((short)cUpper >= alpha && (short)cUpper <= zed)
                return cUpper;
        }

        return 'Z';
    }
  }
}
