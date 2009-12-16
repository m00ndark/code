using System;
using System.Runtime.Serialization;

namespace UnpakkDaemon.Service.DataObjects
{
	[DataContract]
	public class SubRecordData
	{
		public SubRecordData(Guid parentID, string name, int size)
		{
			ParentID = parentID;
			Name = name;
			Size = size;
		}

		[DataMember]
		public Guid ParentID { get; private set; }

		[DataMember]
		public string Name { get; private set; }

		[DataMember]
		public int Size { get; private set; }
	}
}
