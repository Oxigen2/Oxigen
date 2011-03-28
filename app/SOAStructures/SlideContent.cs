using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.SOAStructures
{
  [Serializable]
  [DataContract]
  public class SlideContent : Content
  {
    private string _correspondingAssetContentFilenameWithPath;
    private string _filenameNoPath;
    private string _assetImagePathWinFS;

    [DataMember]
    public string CorrespondingAssetContentFilenameWithPath
    {
      get { return _correspondingAssetContentFilenameWithPath; }
      set { _correspondingAssetContentFilenameWithPath = value; }
    }

    [DataMember]
    public string FilenameNoPath
    {
      get { return _filenameNoPath; }
      set { _filenameNoPath = value; }
    }

    [DataMember]
    public string AssetImagePathWinFS
    {
      get { return _assetImagePathWinFS; }
      set { _assetImagePathWinFS = value; }
    }

    public SlideContent(string fileName, string fileNameWithoutExtension,
      string extension, string correspondingAssetContentFilenameWithPath, 
      string filenameNoPath, string imagePathWinFS,
      string subDir, string assetImagePathWinFS, string imageName)
    {
      _fileName = fileName;
      _fileNameWithoutExtension = fileNameWithoutExtension;
      _extension = extension;
      _imagePathWinFS = imagePathWinFS;
      _correspondingAssetContentFilenameWithPath = correspondingAssetContentFilenameWithPath;
      _filenameNoPath = filenameNoPath;
      _subDir = subDir;
      _assetImagePathWinFS = assetImagePathWinFS;
      _imageName = imageName;
    }

    public SlideContent() { }
  }
}
