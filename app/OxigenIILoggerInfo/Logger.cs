﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceErrorReporting;
using OxigenIIAdvertising.Exceptions;
using System.IO;

namespace OxigenIIAdvertising.LoggerInfo
{
  public class Logger
  {
    private string _outputPath = "";
    private string _name = "";
    private LoggingMode _loggingMode;
    private int screenNo;

    /// <summary>
    /// Path of the debug file
    /// </summary>
    public string OutputPath
    {
      get { return _outputPath; }

      set 
      {
        if (_outputPath != "")
          throw new AlreadyDefinedException("Property OutputPath has alreade been defined");

        _outputPath = value; 
      }
    }

    /// <summary>
    /// Name of the logger
    /// </summary>
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    /// <summary>
    /// Debug or Release
    /// </summary>
    public LoggingMode LoggingMode
    {
      set { _loggingMode = value; }
    }

    /// <summary>
    /// Gets or sets screen number of the application the logger operates on.
    /// For Screensaver application
    /// </summary>
    public int ScreenNo
    {
      get { return screenNo; }
      set { screenNo = value; }
    }

    /// <summary>
    /// Constructor for logger object. Writes error messages only and not debug information
    /// </summary>
    /// <param name="name">path of the debug file</param>
    /// <param name="outputPath">the path of the debug file</param>
    public Logger(string name, string outputPath)
    {
      _name = name;
      _outputPath = outputPath;
      _loggingMode = LoggingMode.Release;
    }

    /// <summary>
    /// Constructor for logger object. Writes error and/or debug information
    /// </summary>
    /// <param name="name">name of the logger</param>
    /// <param name="outputPath">path of the debug file</param>
    /// <param name="loggingMode">Debug or Release</param>
    public Logger(string name, string outputPath, LoggingMode loggingMode)
    {
      _name = name;
      _outputPath = outputPath;
      _loggingMode = loggingMode;
    }

    /// <summary>
    /// Writes an exception message with a timestamp to the debug file
    /// </summary>
    /// <param name="exception">The exception to write to file</param>
    public void WriteError(Exception exception)
    {
      try
      {
        StreamWriter output = File.AppendText(_outputPath);

        output.WriteLine("LOGGER: " + _name + ": An error occurred at " + DateTime.Now.ToString() + ": " + exception.ToString());

        output.Close();
        output.Dispose();
        //
        //TODO: open error log file and decrypt. Append client side' timestamp.
        // if file doesn't exist, create.
        // do multiple output.write instead of concatenating strings as this is more efficient.
      }
      catch
      {
        // ignore error
      }
    }

    /// <summary>
    /// Writes an remote server returned error message to string
    /// </summary>
    /// <param name="errorWrapper">The error wrapper object to save</param>
    public void WriteError(IErrorReporting errorWrapper)
    {
      try
      {
        StreamWriter output = File.AppendText(_outputPath);

        output.WriteLine("LOGGER: " + _name + ": An error occurred at " + DateTime.Now.ToString() + " at a relay server.");
        output.WriteLine("LOGGER: " + _name + " Error Code: " + errorWrapper.ErrorCode);
        output.WriteLine("LOGGER: " + _name + " Error Message: " + errorWrapper.Message);

        output.Close();
        output.Dispose();
        //
        //
        //TODO: open error log file and decrypt. Append client side' timestamp.
        // if file doesn't exist, create.
      }
      catch
      {
        // ignore error
      }
    }

    /// <summary>
    /// Writes an information message to log file
    /// </summary>
    /// <param name="message">the message to write</param>
    public void WriteMessage(string message)
    {
      if (_loggingMode == LoggingMode.Release)
        return;

      try
      {
        StreamWriter output = File.AppendText(_outputPath);

        output.WriteLine("LOGGER: " + _name + ": " + message);

        output.Close();
        output.Dispose();

      }
      catch
      {
        // ignore error
      }      
    }

    /// <summary>
    /// Writes a timestamped meessage to log file
    /// </summary>
    /// <param name="message">the message to write</param>
    public void WriteTimestampedMessage(string message)
    {
      WriteMessage(DateTime.Now.ToString() + " " + message);
    }

    /// <summary>
    /// Writes an exception to the log file accompanied by a message
    /// </summary>
    /// <param name="errorMessage">The message to write to the file</param>
    /// <param name="exception">The exception that was caught</param>
    public void WriteError(string errorMessage, Exception exception)
    {
      try
      {
        StreamWriter output = File.AppendText(_outputPath);

        output.WriteLine("LOGGER: " + _name + ": An error occurred at " + DateTime.Now.ToString());
        output.WriteLine("LOGGER: " + _name + " Error Message: " + errorMessage);
        output.WriteLine("LOGGER: " + _name + " Exception Details: " + exception.ToString());

        output.Close();
        output.Dispose();
      }
      catch
      {
        // ignore
      }
    }

    /// <summary>
    /// Writes an error message on the log file
    /// </summary>
    /// <param name="errorMessage">the error message to write</param>
    public void WriteError(string errorMessage)
    {
      try
      {
        StreamWriter output = File.AppendText(_outputPath);

        output.WriteLine("LOGGER: " + _name + ": An error occurred at " + DateTime.Now.ToString());
        output.WriteLine("LOGGER: " + _name + " Error Message: " + errorMessage);

        output.Close();
        output.Dispose();
        //
        //
        //TODO: open error log file and decrypt. Append client side' timestamp.
        // if file doesn't exist, create.
      }
      catch
      {
        // ignore error
      }
    }

    /// <summary>
    /// Writes an warning message on the log file
    /// </summary>
    /// <param name="errorMessage">the warning message to write</param>
    public void WriteWarning(string warningMessage)
    {
      if (_loggingMode == LoggingMode.Release)
        return;

      try
      {
        StreamWriter output = File.AppendText(_outputPath);

        output.WriteLine("LOGGER: " + _name + " WARNING: " + warningMessage);

        output.Close();
        output.Dispose();
        //
        //
        //TODO: open error log file and decrypt. Append client side' timestamp.
        // if file doesn't exist, create.
      }
      catch
      {
        // ignore error
      }
    }
  }

  /// <summary>
  /// Mode the logger object should operate on
  /// </summary>
  public enum LoggingMode
  {
    /// <summary>
    /// In this mode, logger writes errors, warnings and information messages
    /// </summary>
    Debug,

    /// <summary>
    /// In this mode, logger writes warnings and errors only
    /// </summary>
    Warning,
    
    /// <summary>
    /// In this mode, logger writes errors only
    /// </summary>
    Release
  }
}
