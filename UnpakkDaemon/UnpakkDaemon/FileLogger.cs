using System;
using System.Linq;
using System.IO;
using UnpakkDaemon.DataAccess;
using UnpakkDaemon.EventArguments;

namespace UnpakkDaemon
{
	public enum LogType
	{
		Debug = 0,
		Flow = 1,
		Warning = 2,
		Error = 3
	}

	public class FileLogger
	{
		private const string DEFAULT_LOG_FOLDER = "Logs";
		private const string DEFAULT_LOG_NAME = "unpakkdaemon_%date%.log";

		private static int _logTypeIndentationDepth;

		public event EventHandler<LogEntryEventArgs> LogEntryWritten;
		public static event EventHandler<LogEntryEventArgs> LogEntryRead;

		static FileLogger()
		{
			SetupLogTypeIndentationDepth();
		}

		public FileLogger()
		{
			LogFolder = DEFAULT_LOG_FOLDER;
			LogName = DEFAULT_LOG_NAME;
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
			get { return Path.Combine(LogPath, MakeLogFileName(DateTime.Now)); }
		}

		#endregion

		#region Event raisers

		private void RaiseLogEntryWrittenEvent(DateTime logTime, LogType logType, string logText)
		{
			if (LogEntryWritten != null)
				LogEntryWritten(this, new LogEntryEventArgs(logTime, logType, logText));
		}

		private static void RaiseLogEntryReadEvent(DateTime logTime, LogType logType, string logText)
		{
			if (LogEntryRead != null)
				LogEntryRead(null, new LogEntryEventArgs(logTime, logType, logText));
		}

		#endregion

		private static void SetupLogTypeIndentationDepth()
		{
			string[] logTypeNames = Enum.GetNames(typeof(LogType));
			_logTypeIndentationDepth = logTypeNames.Max(logTypeName => logTypeName.Length);
		}

		public string MakeLogFileName(DateTime date)
		{
			return LogName.Replace("%date%", date.ToString("yyyy-MM-dd"));
		}

		public void Load(int daysBack, LogType leastLogType)
		{
			try
			{
				for (int i = daysBack; i >= 0; i--)
				{
					string logPathName = Path.Combine(LogPath, MakeLogFileName(DateTime.Now.AddDays(-i)));
					if (FileHandler.FileExists(logPathName))
					{
						string[] logLines = FileHandler.FileReadLines(logPathName);
						foreach (string logLine in logLines)
						{
							try
							{
								string[] split = logLine.Split(">".ToCharArray(), 2);
								DateTime logTime = DateTime.Parse(split[0].Trim());
								split = split[1].Trim().Split(" ".ToCharArray(), 2);
								LogType logType = (LogType) Enum.Parse(typeof(LogType), split[0].Trim());
								string logText = split[1].Trim();
								if (logType >= leastLogType)
									RaiseLogEntryReadEvent(logTime, logType, logText);
							}
							catch { /* ignore.. could be e.g. exception stack trace line */ }
						}
					}
				}
			}
			catch (Exception ex)
			{
				RaiseLogEntryReadEvent(DateTime.Now, LogType.Error, "Failed to read log: " + ex.Message);
			}
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