using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.SOAStructures
{
  [Serializable]
  [DataContract]
  public abstract class Content
  {
    protected string _fileName;
    protected string _fileNameNoPath;
    protected string _fileNameWithoutExtension;
    protected string _extension;
    protected string _imagePathWinFS;
    protected string _subDir;
    protected string _imageName;

    [DataMember]
    public string FileName
    {
      get { return _fileName; }
      set { _fileName = value; }
    }

    [DataMember]
    public string FileNameNoPath
    {
      get { return _fileNameNoPath; }
      set { _fileNameNoPath = value; }
    }

    [DataMember]
    public string FileNameWithoutExtension
    {
      get { return _fileNameWithoutExtension; }
      set { _fileNameWithoutExtension = value; }
    }

    [DataMember]
    public string Extension
    {
      get { return _extension; }
      set { _extension = value; }
    }

    [DataMember]
    public string ImagePathWinFS
    {
      get { return _imagePathWinFS; }
      set { _imagePathWinFS = value; }
    }

    [DataMember]
    public string SubDir
    {
      get { return _subDir; }
      set { _subDir = value; }
    }

    [DataMember]
    public string ImageName
    {
      get { return _imageName; }
      set { _imageName = value; }
    }
  }
}
