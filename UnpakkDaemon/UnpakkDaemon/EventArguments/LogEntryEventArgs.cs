using System;
using UnpakkDaemon.DataAccess;

namespace UnpakkDaemon.EventArguments
{
	public class LogEntryEventArgs : EventArgs
	{
		public LogEntryEventArgs(string logText)
		{
			LogType = LogType.Flow;
			LogText = logText;
		}

		public LogEntryEventArgs(LogType logType, string logText)
		{
			LogType = logType;
			LogText = logText;
		}

		public LogType LogType { get; private set; }

		public string LogText { get; private set; }
	}
}
