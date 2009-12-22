using System;
using System.Runtime.Serialization;

namespace UnpakkDaemon.Service.DataObjects
{
	[DataContract]
	public class SubRecordData
	{
		public SubRecordData(Guid parentID, string folder, string name, long size)
		{
			ParentID = parentID;
			Folder = folder;
			Name = name;
			Size = size;
		}

		[DataMember]
		public Guid ParentID { get; private set; }

		[DataMember]
		public string Folder { get; private set; }

		[DataMember]
		public string Name { get; private set; }

		[DataMember]
		public long Size { get; private set; }
	}
}
