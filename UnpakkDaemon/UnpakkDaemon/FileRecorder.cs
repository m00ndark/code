using System;
using System.IO;
using System.Linq;
using UnpakkDaemon.DataAccess;
using UnpakkDaemon.DataObjects;
using UnpakkDaemon.EventArguments;

namespace UnpakkDaemon
{
	public class FileRecorder
	{
		private const string DEFAULT_RECORD_LIST_FOLDER = "";
		private const string DEFAULT_RECORD_LIST_NAME = "recordlist.xml";

		public event EventHandler<LogEntryEventArgs> LogEntry;
		public event EventHandler<RecordEventArgs> RecordAdded;
		public event EventHandler<SubRecordEventArgs> SubRecordAdded;

		public FileRecorder()
		{
			RecordListFolder = DEFAULT_RECORD_LIST_FOLDER;
			RecordListName = DEFAULT_RECORD_LIST_NAME;
			Load();
		}

		#region Properties

		public string RecordListFolder { get; set; }
		public string RecordListName { get; set; }

		public string RecordListPath
		{
			get { return Path.Combine(EngineSettings.ApplicationDataFolderComplete, RecordListFolder); }
		}

		public string RecordListPathName
		{
			get { return Path.Combine(RecordListPath, RecordListName); }
		}

		public RecordList RecordList { get; private set; }

		#endregion

		#region Event raisers

		private void RaiseLogEntryEvent(string message, Exception ex)
		{
			if (LogEntry != null)
			{
				LogEntry(this, new LogEntryEventArgs(LogType.Error, message));
				LogEntry(this, new LogEntryEventArgs(LogType.Error, ex.ToString()));
			}
		}

		private void RaiseRecordAddedEvent(Record record)
		{
			if (RecordAdded != null)
				RecordAdded(this, new RecordEventArgs(record));
		}

		private void RaiseSubRecordAddedEvent(Guid parentID, SubRecord subRecord)
		{
			if (SubRecordAdded != null)
				SubRecordAdded(this, new SubRecordEventArgs(parentID, subRecord));
		}

		#endregion

		private void Load()
		{
			try
			{
				RecordList = (FileHandler.FileExists(RecordListPathName) ? FileHandler.Deserialize<RecordList>(RecordListPathName) : new RecordList());
			}
			catch (Exception ex)
			{
				RaiseLogEntryEvent("Failed to load record list", ex);
			}
		}

		public void AddRecord(Record record)
		{
			try
			{
				Record existingRecord = RecordList.SingleOrDefault(r => (r == record));
				if (existingRecord == null)
					RecordList.Add(record);
				else
					existingRecord.CopyFrom(record);
				FileHandler.Serialize(RecordListPathName, RecordList);
				RaiseRecordAddedEvent(record);
			}
			catch (Exception ex)
			{
				RaiseLogEntryEvent("Failed to add record to list, id=" + (record != null ? record.ID.ToString() : "null"), ex);
			}
		}

		public void AddSubRecord(Guid parentID, SubRecord subRecord)
		{
			try
			{
				Record parentRecord = RecordList.Single(record => record.ID == parentID);
				SubRecord existingSubRecord = parentRecord.SubRecords.SingleOrDefault(sr => (sr == subRecord));
				if (existingSubRecord == null)
					parentRecord.SubRecords.Add(subRecord);
				else
					existingSubRecord.CopyFrom(subRecord);
				FileHandler.Serialize(RecordListPathName, RecordList);
				RaiseSubRecordAddedEvent(parentID, subRecord);
			}
			catch (Exception ex)
			{
				RaiseLogEntryEvent("Failed to add sub record to its parent, parentid=" + parentID, ex);
			}
		}
	}
}
