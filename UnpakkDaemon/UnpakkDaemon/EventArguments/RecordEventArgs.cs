using System;
using UnpakkDaemon.DataObjects;

namespace UnpakkDaemon.EventArguments
{
	public class RecordEventArgs : EventArgs
	{
		public RecordEventArgs(Record record)
		{
			Record = record;
		}

		public Record Record { get; private set; }
	}
}
