using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.SqlClient;
using OxigenIIAdvertising.DataAccess;

namespace OxigenIIAdvertising.SOAStructures
{
  /// <summary>
  /// Represents a category of sub-categories or sub-category of channels
  /// </summary>
  [DataContract]
  public class Category
  {
    private int _categoryID;
    private string _categoryName;
    private bool _bHasChildren;

    /// <summary>
    /// The category's unique identifier
    /// </summary>
    [DataMember]
    public int CategoryID
    {
      get { return _categoryID; }
      set { _categoryID = value; }
    }

    /// <summary>
    /// The category's name
    /// </summary>
    [DataMember]
    public string CategoryName
    {
      get { return _categoryName; }
      set { _categoryName = value; }
    }

    /// <summary>
    /// Category has sub-categories
    /// </summary>
    [DataMember]
    public bool HasChildren
    {
      get { return _bHasChildren; }
      set { _bHasChildren = value; }
    }

    public Category(int categoryID, string categoryName, bool bHasChildren)
    {
      _categoryID = categoryID;
      _categoryName = categoryName;
      _bHasChildren = bHasChildren;
    }

    public Category() { }

    public static Category GetCategory(int categoryID)
    {
      SqlDatabase sqlDatabase = new SqlDatabase();
      SqlDataReader sqlDataReader = null;

      Category category = new Category();

      try
      {
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@CategoryID", categoryID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getCategory");

        if (sqlDataReader.Read())
        {
          category._categoryID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CATEGORY_ID"));
          category._categoryName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("CategoryName"));
          category._bHasChildren = sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("bHasChildren"));
        }
      }
      catch (Exception exception)
      {
        throw new Exception("Error: Reading Category", exception);
      }
      finally
      {
        if (sqlDataReader != null)
          sqlDataReader.Close();

        sqlDatabase.Dispose();
      }

      return category;
    }
  }
}
