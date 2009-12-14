using System;

namespace UnpakkDaemon.EventArguments
{
	public class TimeSpanEventArgs : EventArgs
	{
		public TimeSpanEventArgs(TimeSpan value) : base()
		{
			Value = value;
		}

		public TimeSpan Value { get; private set; }
	}
}
