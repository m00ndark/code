using System;

namespace UnpakkDaemon.EventArguments
{
	public class LogEntryEventArgs : EventArgs
	{
		public LogEntryEventArgs(string logText)
		{
			LogText = logText;
		}

		public string LogText { get; private set; }
	}
}
