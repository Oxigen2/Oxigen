using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.SOAStructures
{
  [DataContract]
  public class RemovableContent
  {
    private string _fileName;
    private string _imagePathWinFS;
    private int _fileLength;
    
    [DataMember]
    public string FileName
    {
      get { return _fileName; }
      set { _fileName = value; }
    }

    [DataMember]
    public string ImagePathWinFS
    {
      get { return _imagePathWinFS; }
      set { _imagePathWinFS = value; }
    }

    [DataMember]
    public int FileLength
    {
      get { return _fileLength; }
      set { _fileLength = value; }
    }

    public RemovableContent() { }

    public RemovableContent(string fileName, string imagePathWinFS, int fileLength)
    {
      _fileName = fileName;
      _imagePathWinFS = imagePathWinFS;
      _fileLength = fileLength;
    }

    public RemovableContent(string fileName, string imagePathWinFS)
    {
      _fileName = fileName;
      _imagePathWinFS = imagePathWinFS;
    }
  }
}
