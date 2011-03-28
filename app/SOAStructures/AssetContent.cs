using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.SqlClient;
using OxigenIIAdvertising.DataAccess;

namespace OxigenIIAdvertising.SOAStructures
{
  [Serializable]
  [DataContract]
  public class AssetContent : Content
  {
    private int _assetContentID;
    private string _imagePath;
    private string _name;
    private string _caption;
    private string _creator;
    private DateTime? _userGivenDate;
    private string _url;
    private float _displayDuration;
    private int _length;
    private PreviewType _previewType;

    /// <summary>
    /// The unique database ID of the custom content
    /// </summary>
    [DataMember]
    public int AssetContentID
    {
      get { return _assetContentID; }
      set { _assetContentID = value; }
    }       

    /// <summary>
    /// Title of Raw Content
    /// </summary>
    [DataMember]
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    /// <summary>
    /// Raw Content's Caption
    /// </summary>
    [DataMember]
    public string Caption
    {
      get { return _caption; }
      set { _caption = value; }
    }

    /// <summary>
    /// Raw Content's Caption
    /// </summary>
    [DataMember]
    public string Creator
    {
      get { return _creator; }
      set { _creator = value; }
    }

    /// <summary>
    /// Path to content's thumbnail
    /// </summary>
    [DataMember]
    public string ImagePath
    {
      get { return _imagePath; }
      set { _imagePath = value; }
    }

    /// <summary>
    /// Date
    /// </summary>
    [DataMember]
    public DateTime? UserGivenDate
    {
      get { return _userGivenDate; }
      set { _userGivenDate = value; }
    }

    /// <summary>
    /// Credit URL
    /// </summary>
    [DataMember]
    public string Url
    {
      get { return _url; }
      set { _url = value; }
    }

    /// <summary>
    /// Length of display
    /// </summary>
    [DataMember]
    public float DisplayDuration
    {
      get { return _displayDuration; }
      set { _displayDuration = value; }
    }

    /// <summary>
    /// Length of file
    /// </summary>
    [DataMember]
    public int Length
    {
      get { return _length; }
      set { _length = value; }
    }

    [DataMember]
    public PreviewType PreviewType
    {
      get { return _previewType; }
      set { _previewType = value; }
    }

    public AssetContent() { }

    public AssetContent(string title, string fileName, string fileNameWithoutExtension, string extension, string imagePath, string imagePathWinFS, string subDir, string imageName,
      string caption, string creator, DateTime? userGivenDate, string url, float displayDuration, int length, PreviewType previewType)
    {
      _name = title;
      _fileName = fileName;
      _fileNameWithoutExtension = fileNameWithoutExtension;
      _extension = extension;
      _imagePath = imagePath;
      _imagePathWinFS = imagePathWinFS;
      _subDir = subDir;
      _imageName = imageName;
      _caption = caption;
      _creator = creator;
      _userGivenDate = userGivenDate;
      _url = url;
      _displayDuration = displayDuration;
      _length = length;
      _previewType = previewType;
    }
  }
}
