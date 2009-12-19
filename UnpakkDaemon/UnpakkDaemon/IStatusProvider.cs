using System;
using UnpakkDaemon.EventArguments;

namespace UnpakkDaemon
{
	public interface IStatusProvider
	{
		event EventHandler<ProgressEventArgs> Progress;
		event EventHandler<ProgressEventArgs> SubProgress;
		event EventHandler<RecordEventArgs> Record;
		event EventHandler<SubRecordEventArgs> SubRecord;
		event EventHandler<LogEntryEventArgs> Log;
	}
}
