using System;
using System.Linq;
using System.IO;
using UnpakkDaemon.DataAccess;
using UnpakkDaemon.EventArguments;

namespace UnpakkDaemon
{
	public enum LogType
	{
		Debug,
		Flow,
		Warning,
		Error
	}

	public class FileLogger
	{
		private const string DEFAULT_LOG_FOLDER = "Logs";
		private const string DEFAULT_LOG_NAME = "unpakkdaemon_%date%.log";

		private int _logTypeIndentationDepth;

		public event EventHandler<LogEntryEventArgs> LogEntryWritten;

		public FileLogger()
		{
			LogFolder = DEFAULT_LOG_FOLDER;
			LogName = DEFAULT_LOG_NAME;
			SetupLogTypeIndentationDepth();
		}

		#region Properties

		public string LogFolder { get; set; }
		public string LogName { get; set; }

		public string LogPath
		{
			get { return Path.Combine(EngineSettings.ApplicationDataFolderComplete, LogFolder); }
		}

		public string LogPathName
		{
			get { return Path.Combine(LogPath, LogName.Replace("%date%", DateTime.Now.ToString("yyyy-MM-dd"))); }
		}

		#endregion

		#region Event raisers

		private void RaiseLogEntryWrittenEvent(DateTime logTime, LogType logType, string logText)
		{
			if (LogEntryWritten != null)
				LogEntryWritten(this, new LogEntryEventArgs(logTime, logType, logText));
		}

		#endregion

		private void SetupLogTypeIndentationDepth()
		{
			string[] logTypeNames = Enum.GetNames(typeof(LogType));
			_logTypeIndentationDepth = logTypeNames.Max(logTypeName => logTypeName.Length);
		}

		public void WriteLogLine(string logText, Exception exception)
		{
			WriteLogLine(LogType.Error, logText);
			WriteLogLine(LogType.Error, exception.ToString());
		}

		public void WriteLogLine(LogType logType, string logText)
		{
			try
			{
				DateTime logTime = DateTime.Now;
				FileHandler.MakeDirectory(LogPath);
				FileHandler.FileWriteLine(LogPathName, logTime.ToString("yyyy-MM-dd HH:mm:ss.fff")
					+ " > " + logType.ToString().PadRight(_logTypeIndentationDepth) + " " + logText);
				RaiseLogEntryWrittenEvent(logTime, logType, logText);
			}
			catch { /* swallow and be happy :) */ }
		}
	}
}