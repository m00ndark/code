using System;
using System.Runtime.Serialization;

namespace UnpakkDaemon.Service.DataObjects
{
	[DataContract]
	public class SubRecordData
	{
		public SubRecordData(Guid parentID, DateTime time, RecordStatusData status, string folder, string name, long size)
		{
			ParentID = parentID;
			Time = time;
			Status = status;
			Folder = folder;
			Name = name;
			Size = size;
		}

		[DataMember]
		public Guid ParentID { get; private set; }

		[DataMember]
		public DateTime Time { get; private set; }

		[DataMember]
		public RecordStatusData Status { get; private set; }

		[DataMember]
		public string Folder { get; private set; }

		[DataMember]
		public string Name { get; private set; }

		[DataMember]
		public long Size { get; private set; }
	}
}
