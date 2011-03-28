using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using OxigenIIAdvertising.DataAccess;
using System.Data.SqlClient;

namespace OxigenIIAdvertising.SOAStructures
{
  /// <summary>
  /// Represents a Content Folder for an Oxigen User
  /// </summary>
  [DataContract]
  public class CreateContentGenericFolder
  {
    private int _folderID;
    private string _folderName;

    /// <summary>
    /// The unique database ID of the content folder
    /// </summary>
    [DataMember]
    public int FolderID
    {
      get { return _folderID; }
      set { _folderID = value; }
    }

    /// <summary>
    /// The folder's name
    /// </summary>
    [DataMember]
    public string FolderName
    {
      get { return _folderName; }
      set { _folderName = value; }
    }

    public CreateContentGenericFolder(int folderID, string folderName)
    {
      _folderID = folderID;
      _folderName = folderName;
    }

    public CreateContentGenericFolder() { }
  }
}
