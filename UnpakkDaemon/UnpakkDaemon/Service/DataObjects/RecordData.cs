using System;
using System.Runtime.Serialization;

namespace UnpakkDaemon.Service.DataObjects
{
	[DataContract]
	public class RecordData
	{
		public RecordData(Guid id, DateTime time, RecordStatusData status, string folder, string sfvName, string rarName, int rarCount, long rarSize)
		{
			ID = id;
			Time = time;
			Status = status;
			Folder = folder;
			SFVName = sfvName;
			RARName = rarName;
			RARCount = rarCount;
			RARSize = rarSize;
		}

		[DataMember]
		public Guid ID { get; private set; }

		[DataMember]
		public DateTime Time { get; private set; }

		[DataMember]
		public RecordStatusData Status { get; private set; }

		[DataMember]
		public string Folder { get; private set; }

		[DataMember]
		public string SFVName { get; private set; }

		[DataMember]
		public string RARName { get; private set; }

		[DataMember]
		public int RARCount { get; private set; }

		[DataMember]
		public long RARSize { get; private set; }
	}
}
