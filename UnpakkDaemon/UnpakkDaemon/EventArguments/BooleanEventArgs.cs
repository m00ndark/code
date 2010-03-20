using System;

namespace UnpakkDaemon.EventArguments
{
	public class BooleanEventArgs : EventArgs
	{
		public BooleanEventArgs(bool value) : base()
		{
			Value = value;
		}

		public bool Value { get; set; }
	}
}
