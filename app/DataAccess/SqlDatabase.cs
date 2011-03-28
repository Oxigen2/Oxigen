using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace OxigenIIAdvertising.DataAccess
{
  /// <summary>
  /// Represents an SQL Server database context
  /// </summary>
  public class SqlDatabase : IDisposable
  {
    private SqlConnection _sqlConnection = null;
    private SqlCommand _sqlCommand = null;
    bool _bDisposed = false;
    private SqlParameter _sqlReturnParameter = null;

    public static string strConn
    {
      get
      {
        return System.Configuration.ConfigurationManager.ConnectionStrings["strConn"].ConnectionString;
      }
    }

    /// <summary>
    /// Gets the integer value of the return parameter returned by the last query
    /// </summary>
    public int ReturnValue
    {
      get { return (int)_sqlReturnParameter.Value; }
    }

    public SqlDatabase()
    {
      _sqlCommand = new SqlCommand();
    }

    public SqlDatabase(SqlConnection con) : this()
    {
      _sqlConnection = con;
    }

    /// <summary>
    /// Opens a connection to the database specified in the connection string
    /// </summary>
    public void Open()
    {
      if (_sqlConnection == null)
      {
        _sqlConnection = new SqlConnection(strConn);
        _sqlConnection.Open();
      }
    }

    ~SqlDatabase()
    {
      Dispose(false);
    }

    /// <summary>
    /// Releases all resources used in OxigenIIAdvertising.DataAccess.SqlDatabase
    /// </summary>
    public void Dispose()
    {
      Dispose(true);

      // Use SupressFinalize in case a subclass
      // of this type implements a finalizer.
      GC.SuppressFinalize(this);
    }

    public void Dispose(bool bDisposing)
    {
      if (!_bDisposed)
      {
        if (bDisposing)
        {
          if (_sqlCommand != null)
          {
            _sqlCommand.Dispose();
            _sqlCommand = null;
          }

          if (_sqlConnection != null)
          {
            _sqlConnection.Close();
            _sqlConnection.Dispose();
            _sqlConnection = null;
          }
        }

        _bDisposed = true;
      }
    }

    public SqlDataReader ExecuteReader(string strStoredProcedureName)
    {
      _sqlCommand.Connection = _sqlConnection;
      _sqlCommand.CommandType = CommandType.StoredProcedure;
      _sqlCommand.CommandText = strStoredProcedureName;
      return _sqlCommand.ExecuteReader();
    }

    public SqlDataReader ExecuteReaderWithText(string strSQL)
    {
      _sqlCommand.Connection = _sqlConnection;
      _sqlCommand.CommandType = CommandType.Text;
      _sqlCommand.CommandText = strSQL;
      return _sqlCommand.ExecuteReader();
    }

    public int ExecuteNonQuery(string strStoredProcedureName)
    {
      _sqlCommand.Connection = _sqlConnection;
      _sqlCommand.CommandType = CommandType.StoredProcedure;
      _sqlCommand.CommandText = strStoredProcedureName;

      int RowsAffected = _sqlCommand.ExecuteNonQuery();
      return RowsAffected;
    }

    public void ClearParameters()
    {
      _sqlCommand.Parameters.Clear();
    }

    public void AddReturnParameter()
    {
      _sqlReturnParameter = new SqlParameter("@ReturnValue", null);

      _sqlReturnParameter.Direction = ParameterDirection.ReturnValue;

      _sqlCommand.Parameters.Add(_sqlReturnParameter);
    }

    public void AddInputParameter(string parameterName, object Value)
    {
      SqlParameter sqlParameter = new SqlParameter(parameterName, Value);

      sqlParameter.Direction = ParameterDirection.Input;

      _sqlCommand.Parameters.Add(sqlParameter);
    }

    public void RemoveParameter(string parameterName)
    {
      if (_sqlCommand.Parameters.Contains(parameterName))
      {
        SqlParameter parameter = _sqlCommand.Parameters[parameterName];
        _sqlCommand.Parameters.Remove(parameter);
      }
    }

    public void AddInputParameter(string parameterName)
    {
      AddInputParameter(parameterName, null);
    }

    public void EditInputParameter(string parameterName, object newValue)
    {
      _sqlCommand.Parameters[parameterName].Value = newValue;
    }

    public SqlParameter AddOutputParameter(string parameterName, SqlDbType sqlDbType, int size, object Value)
    {
      SqlParameter sqlParameter = new SqlParameter(parameterName, Value);

      sqlParameter.SqlDbType = sqlDbType;
      sqlParameter.Size = size;
      sqlParameter.Direction = ParameterDirection.Output;

      _sqlCommand.Parameters.Add(sqlParameter);

      return sqlParameter;
    }

    public DataView FetchDataView(string StoredProc)
    {
      _sqlCommand.Connection = _sqlConnection;
      _sqlCommand.CommandType = CommandType.StoredProcedure;
      _sqlCommand.CommandText = StoredProc;

      SqlDataAdapter sqlAdapter = new SqlDataAdapter();
      sqlAdapter.SelectCommand = _sqlCommand;

      DataTable ListDT = new DataTable();
      DataView ListDV = new DataView(ListDT);

      sqlAdapter.Fill(ListDV.Table);

      _sqlConnection.Close();

      return ListDV;
    }
  }
}
