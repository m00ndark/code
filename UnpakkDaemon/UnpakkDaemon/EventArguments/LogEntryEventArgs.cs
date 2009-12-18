using System;

namespace UnpakkDaemon.EventArguments
{
	public class LogEntryEventArgs : EventArgs
	{
		public LogEntryEventArgs(string logText) : this(LogType.Flow, logText) {}

		public LogEntryEventArgs(LogType logType, string logText) : this(DateTime.Now, logType, logText) {}

		public LogEntryEventArgs(DateTime logTime, LogType logType, string logText)
		{
			LogTime = logTime;
			LogType = logType;
			LogText = logText;
		}

		public DateTime LogTime { get; private set; }

		public LogType LogType { get; private set; }

		public string LogText { get; private set; }
	}
}
