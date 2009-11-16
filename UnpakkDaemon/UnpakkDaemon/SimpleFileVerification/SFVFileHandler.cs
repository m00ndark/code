using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnpakkDaemon.EventArguments;

namespace UnpakkDaemon.SimpleFileVerification
{
	public static class SFVFileHandler
	{
		public static event EventHandler<LogEntryEventArgs> LogEntry;

		#region Event raisers

		private static void RaiseLogEntryEvent(string logText)
		{
			if (LogEntry != null)
			{
				LogEntry(null, new LogEntryEventArgs(logText));
			}
		}

		#endregion


	}
}
