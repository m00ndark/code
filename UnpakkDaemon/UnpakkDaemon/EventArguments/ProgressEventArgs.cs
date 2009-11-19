using System;

namespace UnpakkDaemon.EventArguments
{
	public class ProgressEventArgs : EventArgs
	{
		public ProgressEventArgs(string message, double percent)
			: this(message, percent, -1, -1) { }

		public ProgressEventArgs(string message, double percent, long current)
			: this(message, percent, current, (int) (current / (percent / 100))) { }

		public ProgressEventArgs(string message, double percent, long current, long max)
		{
			Message = message;
			Percent = percent;
			Current = current;
			Max = max;
		}

		public string Message { get; private set; }
		public double Percent { get; private set; }
		public long Current { get; private set; }
		public long Max { get; private set; }
	}
}
