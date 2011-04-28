using System;
using System.Configuration;
using System.Xml;
using log4net;
using OxigenIIAdvertising.Exceptions;

namespace OxigenIIAdvertising.LoggerInfo
{
    public class Logger
    {
        private string _outputPath = "";
        private object _lockObj = new object();
        private string _name = "";
        private LoggingMode _loggingMode;
        private int screenNo;
        private ILog _log = null;

        /// <summary>
        /// Path of the debug file
        /// </summary>
        public string OutputPath {
            get { return _outputPath; }

            set {
                if (_outputPath != "")
                    throw new AlreadyDefinedException("Property OutputPath has alreade been defined");

                _outputPath = value;
            }
        }

        /// <summary>
        /// Name of the logger
        /// </summary>
        public string Name {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Debug or Release
        /// </summary>
        public LoggingMode LoggingMode {
            set { _loggingMode = value; }
        }

        /// <summary>
        /// Gets or sets screen number of the application the logger operates on.
        /// For Screensaver application
        /// </summary>
        public int ScreenNo {
            get { return screenNo; }
            set { screenNo = value; }
        }

        /// <summary>
        /// Constructor for logger object. Writes error messages only and not debug information
        /// </summary>
        /// <param name="name">path of the debug file</param>
        /// <param name="outputPath">the path of the debug file</param>
        public Logger(string name, string outputPath) {
            _name = name;
            _outputPath = outputPath;
            _loggingMode = LoggingMode.Release;
            ConfigureLogger(name, outputPath, "ERROR");
            _log = LogManager.GetLogger(name);
        }

        /// <summary>
        /// Constructor for logger object. Writes error and/or debug information
        /// </summary>
        /// <param name="name">name of the logger</param>
        /// <param name="outputPath">path of the debug file</param>
        /// <param name="loggingMode">Debug or Release</param>
        public Logger(string name, string outputPath, LoggingMode loggingMode) {
            _name = name;
            _outputPath = outputPath;
            _loggingMode = loggingMode;

            if (_loggingMode == LoggingMode.Debug)
                ConfigureLogger(name, outputPath, "DEBUG");
            else
                ConfigureLogger(name, outputPath, "ERROR");

            _log = LogManager.GetLogger(name);
        }

        /// <summary>
        /// Writes an exception message with a timestamp to the debug file
        /// </summary>
        /// <param name="exception">The exception to write to file</param>
        public void WriteError(Exception exception) {
            try {
                lock (_lockObj)
                {
                    _log.Error("An error occurred at " + DateTime.Now.ToString() + ": " + exception.ToString());
                }
            }
            catch {
                // ignore error
            }
        }

        /// <summary>
        /// Writes an remote server returned error message to string
        /// </summary>
        /// <param name="errorCode">Remote error code</param>
        /// <param name="errorMessage">Remote error message</param>
        public void WriteError(string errorCode, string errorMessage) {
            try {
                lock (_lockObj)
                {
                    _log.Error("An error occurred at " + DateTime.Now.ToString() + " at a relay server.");
                    _log.Error("Error Code: " + errorCode);
                    _log.Error("Error Message: " + errorMessage);
                }
            }
            catch {
                // ignore error
            }
        }

        public void WriteWarning (string errorMessage)
        {
            try
            {
                lock (_lockObj)
                {
                    _log.Warn(errorMessage);
                }
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
        public void WriteMessage(string message) {
            try {
                lock (_lockObj)
                {
                   _log.Debug(message); 
                }
            }
            catch {
                // ignore error
            }
        }

        /// <summary>
        /// Writes a timestamped meessage to log file
        /// </summary>
        /// <param name="message">the message to write</param>
        public void WriteTimestampedMessage(string message) {
            WriteMessage(DateTime.Now.ToString() + " " + message);
        }

        /// <summary>
        /// Writes an exception to the log file accompanied by a message
        /// </summary>
        /// <param name="errorMessage">The message to write to the file</param>
        /// <param name="exception">The exception that was caught</param>
        public void WriteError(string errorMessage, Exception exception) {
            try {
                lock (_lockObj)
                {
                    _log.Error("An error occurred at " + DateTime.Now.ToString() + " at a relay server.");
                    _log.Error("Error Code: " + errorMessage);
                    _log.Error("Exception Details: " + exception.ToString());
                }
            }
            catch {
                // ignore
            }
        }

        /// <summary>
        /// Writes an error message on the log file
        /// </summary>
        /// <param name="errorMessage">the error message to write</param>
        public void WriteError(string errorMessage) {
            try {
                lock (_lockObj)
                {
                    _log.Error("An error occurred at " + DateTime.Now.ToString() + " at a relay server.");
                    _log.Error("Error Code: " + errorMessage);
                }
            }
            catch {
                // ignore error
            }
        }

        private void ConfigureLogger(string name, string outputPath, string logLevel) {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
                        <log4net>
                          <appender name=""FileAppender"" type=""log4net.Appender.RollingFileAppender"">
                            <file value=""" + outputPath + @""" />
                            <appendToFile value=""true"" />
                            <param name=""RollingStyle"" value=""Size""/>
                            <maxSizeRollBackups value=""1""/>
                            <maximumFileSize value=""2MB""/>
                            <layout type=""log4net.Layout.PatternLayout"">
                              <conversionPattern value=""%date [%thread] %-5level %logger [%property{NDC}] - %message%newline"" />
                            </layout>
                          </appender> 
                          <logger name=""" + name + @""">    
                            <level value=""" + logLevel + @"""/>
                            <appender-ref ref=""FileAppender"" />
                          </logger>
                        </log4net>";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            log4net.Config.XmlConfigurator.Configure(doc.DocumentElement);
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
