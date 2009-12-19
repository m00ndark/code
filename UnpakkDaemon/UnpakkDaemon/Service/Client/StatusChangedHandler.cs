using System;
using UnpakkDaemon.DataObjects;
using UnpakkDaemon.EventArguments;
using UnpakkDaemon.Service.Common;
using UnpakkDaemon.Service.DataObjects;

namespace UnpakkDaemon.Service.Client
{
	public class StatusChangedHandler : IStatusChangedHandler
	{
		public event EventHandler<ProgressEventArgs> ProgressChanged;
		public event EventHandler<ProgressEventArgs> SubProgressChanged;
		public event EventHandler<RecordEventArgs> RecordAdded;
		public event EventHandler<SubRecordEventArgs> SubRecordAdded;
		public event EventHandler<LogEntryEventArgs> LogEntryAdded;

		#region Implementation of IStatusChangedHandler

		public void Progress(ProgressData progressData)
		{
			RaiseProgressChangedEvent(progressData);
		}

		public void SubProgress(ProgressData progressData)
		{
			RaiseSubProgressChangedEvent(progressData);
		}

		public void Record(RecordData recordData)
		{
			RaiseRecordAddedEvent(recordData);
		}

		public void SubRecord(SubRecordData subRecordData)
		{
			RaiseSubRecordAddedEvent(subRecordData);
		}

		public void Log(LogData logData)
		{
			RaiseLogEntryAddedEvent(logData);
		}

		#endregion

		#region Event raisers

		private void RaiseProgressChangedEvent(ProgressData progressData)
		{
			if (ProgressChanged != null)
				ProgressChanged(this, new ProgressEventArgs(progressData.Message, progressData.Percent, progressData.Current, progressData.Max));
		}

		private void RaiseSubProgressChangedEvent(ProgressData progressData)
		{
			if (SubProgressChanged != null)
				SubProgressChanged(this, new ProgressEventArgs(progressData.Message, progressData.Percent, progressData.Current, progressData.Max));
		}

		private void RaiseRecordAddedEvent(RecordData recordData)
		{
			if (RecordAdded != null)
				RecordAdded(this, new RecordEventArgs(new Record(recordData.ID, recordData.Name, recordData.Folder, recordData.Size)));
		}

		private void RaiseSubRecordAddedEvent(SubRecordData subRecordData)
		{
			if (SubRecordAdded != null)
				SubRecordAdded(this, new SubRecordEventArgs(subRecordData.ParentID, new SubRecord(subRecordData.Name, subRecordData.Size)));
		}

		private void RaiseLogEntryAddedEvent(LogData logData)
		{
			if (LogEntryAdded != null)
				LogEntryAdded(this, new LogEntryEventArgs(logData.LogTime, (LogType) Enum.Parse(typeof(LogType), logData.LogType.ToString()), logData.LogText));
		}

		#endregion
	}
}