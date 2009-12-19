using System;
using UnpakkDaemon.DataObjects;

namespace UnpakkDaemon.EventArguments
{
	public class SubRecordEventArgs : EventArgs
	{
		public SubRecordEventArgs(Guid parentID, SubRecord subRecord)
		{
			ParentID = parentID;
			SubRecord = subRecord;
		}

		public Guid ParentID { get; private set; }

		public SubRecord SubRecord { get; private set; }
	}
}
