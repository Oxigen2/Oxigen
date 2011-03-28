using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterCommunicationStructures;
using System.IO;
using OxigenIIAdvertising.XMLSerializer;
using System.Diagnostics;

namespace OxigenIIAdvertising.DataAggregators
{
  /// <summary>
  /// Logic for manipulating same-type data files and appending them together 
  /// </summary>
  public class Appender
  {
    private string _nonAppendedFilesPath;
    private string _appendedDataPath;
    private string _filePattern;
    private int _patternLength;
    private int _approxMaxNoRows;
    private List<string> _filesToDelete;
    private EventLog _eventLog;
    
    // variables that check whether the last appended file has been "filled";
    private int _rowsGuardian;
    private string _lastAppendedFileName;

    /// <summary>
    /// Sets up a SimpleAggregator object
    /// </summary>
    /// <param name="nonAppendedFilesPath">The root path to search for non-appended files</param>
    /// <param name="appendedDataPath">Destination path where the appended data will be saved.</param>
    /// <param name="filePattern">Appended files' file pattern to search for.</param>
    /// <param name="approxMaxNoRows">The approximate number of rows the appended files must have. If this number is exceeded, the destination file will not be appended by a SimpleAggregator again.</param>
    /// <param name="eventLog">EventLog object to provide interaction with Windows event logs.</param>
    public Appender(string nonAppendedFilesPath, string appendedDataPath, string filePattern, int approxMaxNoRows, EventLog eventLog)
    {
      _nonAppendedFilesPath = nonAppendedFilesPath;
      _appendedDataPath = appendedDataPath;
      _filePattern = filePattern;
      _patternLength = _filePattern.Length;
      _approxMaxNoRows = approxMaxNoRows;
      _lastAppendedFileName = "";
      _rowsGuardian = 0;
      _eventLog = eventLog;

      _filesToDelete = new List<string>();
    }

    /// <summary>
    /// Runs the aggregation process once
    /// </summary>
    /// <exception cref="System.IO.FileNotFoundException"></exception>
    /// <exception cref="System.IO.IOException"></exception>
    /// <exception cref="System.Security.SecurityException"></exception>
    /// <exception cref="System.IO.DirectoryNotFoundException"></exception>
    /// <exception cref="System.UnauthorizedAccessException"></exception>
    /// <exception cref="System.IO.PathTooLongException"></exception>
    /// <exception cref="System.NotSupportedException"></exception>
    public void Execute()
    {
      List<string> nonAppendedFiles = GetTreeFiles();
      int noNonAppendedFiles = nonAppendedFiles.Count;

      if (noNonAppendedFiles == 0)
        return;

      // get an appended file to append the contents.
      string appendedFileName = "";

      FileStream fs = null;
      
      // if there was a previous run of the aggregation process
      // and the last file wasn't fully filled in, pick up where left off.
      if (_lastAppendedFileName != "" && _rowsGuardian < _approxMaxNoRows)
      {
        try
        {
          // try to get a lock on the last used file
          // MasterDataMarshaller may have locked the file or deleted it.
          fs = new FileStream(_lastAppendedFileName, FileMode.Append, FileAccess.Write, FileShare.None);
        }
        catch (Exception ex)
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);

          // if there was a sharing violation, create a new file
          if (ex is IOException)
          {
            appendedFileName = GetNewFileNameWithSuffix();
            fs = new FileStream(appendedFileName, FileMode.Append, FileAccess.Write, FileShare.None);
          }
        }
      }
      else
      {
        appendedFileName = GetNewFileNameWithSuffix();
        fs = new FileStream(appendedFileName, FileMode.Append, FileAccess.Write, FileShare.None);
      }
      
      StreamWriter sw = new StreamWriter(fs, Encoding.ASCII);

      int counter = 1;

      foreach (string nonAppendedFile in nonAppendedFiles)
      {
        // the sub-directory the non-appended file is in has the name
        // of the GUID of the machine the file came from.
        string machineGUID = GetFilesCurrentDirectory(nonAppendedFile);

        try
        {
          // read a non-aggregateable file and append the machineGUID
          string contents = machineGUID + "|" + File.ReadAllText(nonAppendedFile);

          // append
          sw.WriteLine(contents);

          _filesToDelete.Add(nonAppendedFile);

          _rowsGuardian++; // all files handled by a SimpleAggregator have one row

          // if maximum number of rows has been exceeded and end of non appended files has not been reached, 
          // save and close the file, then create a new appended file
          if (_rowsGuardian >= _approxMaxNoRows && counter < noNonAppendedFiles)
          {
            sw.Close();
            sw.Dispose();

            appendedFileName = GetNewFileNameWithSuffix();

            _rowsGuardian = 0;

            fs = new FileStream(appendedFileName, FileMode.Append, FileAccess.Write, FileShare.None);
            sw = new StreamWriter(fs, Encoding.ASCII);

            // delete all small files used to create this big file.
            TryDeleteNonAppendedFiles(_filesToDelete);
          }
          
          // if this is the last element, just save and close the file
          if (counter == noNonAppendedFiles)
          {
            // on the last iteration, keep the last appended file name
            _lastAppendedFileName = appendedFileName;

            sw.Close();
            sw.Dispose();

            // delete all small files used to create last written big file.
            TryDeleteNonAppendedFiles(_filesToDelete);
          }
        }
        catch
        {
          // carry on with next non-appended file
        }

        counter++;
      }
    }

    private void TryDeleteNonAppendedFiles(List<string> filesToDelete)
    {
      // delete all small files used to create this big file.
      // if one fails, catch and continue with the remaining files.
      foreach (string file in filesToDelete)
      {
        try
        {
          File.Delete(file);
        }
        catch (Exception ex)
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
      }

      filesToDelete.Clear();
    }

    // search for an appropriate file name to create with a numerical suffix that doesn't exist
    private string GetNewFileNameWithSuffix()
    {
      string partialPathWithPattern = _appendedDataPath + _filePattern;

      int incr = 1;

      while (true)
      {
        string newFileName = partialPathWithPattern + incr + ".dat";

        if (!File.Exists(newFileName))
          return newFileName;

        incr++;
      }
    }

    // gets all files one level down the given directory
    private List<string> GetTreeFiles()
    {
      List<string> h = new List<string>();

      string [] subDirectories = Directory.GetDirectories(_nonAppendedFilesPath);

      foreach (string subDirectory in subDirectories)
      {
        string[] files = Directory.GetFiles(subDirectory, _filePattern + "*");

        foreach (string file in files)
          h.Add(file);
      }

      return h;
    }

    private string GetFilesCurrentDirectory(string fullPath)
    {
      string pathWithoutFilename = Path.GetDirectoryName(fullPath);

      int length = pathWithoutFilename.Length;

      int offset = pathWithoutFilename.LastIndexOf("\\") + 1;

      string fileCurrentDirectory = pathWithoutFilename.Substring(offset, length - offset);

      return fileCurrentDirectory;
    }
  }
}
