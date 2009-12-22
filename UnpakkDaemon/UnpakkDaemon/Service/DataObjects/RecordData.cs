using System;
using System.Runtime.Serialization;

namespace UnpakkDaemon.Service.DataObjects
{
	[DataContract]
	public class RecordData
	{
		public RecordData(Guid id, string folder, string sfvName, string rarName, int rarCount, long rarSize)
		{
			ID = id;
			Folder = folder;
			SFVName = sfvName;
			RARName = rarName;
			RARCount = rarCount;
			RARSize = rarSize;
		}

		[DataMember]
		public Guid ID { get; private set; }

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
