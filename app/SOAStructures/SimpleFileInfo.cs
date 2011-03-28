using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.SOAStructures
{
  [DataContract]
  public class SimpleFileInfo
  {
    private int _fileID;
    private string _filenameNoPath;
    private string _fileNameWithPath;

    [DataMember]
    public int FileID
    {
      get { return _fileID; }
      set { _fileID = value; }
    }

    [DataMember]
    public string FilenameNoPath
    {
      get { return _filenameNoPath; }
      set { _filenameNoPath = value; }
    }

    [DataMember]
    public string FileNameWithPath
    {
      get { return _fileNameWithPath; }
      set { _fileNameWithPath = value; }
    }
  }
}
