using System;
using System.Runtime.Serialization;

namespace UnpakkDaemon.Service.DataObjects
{
	[DataContract]
	public class RecordData
	{
		public RecordData(Guid id, string name, string folder, int size)
		{
			ID = id;
			Name = name;
			Folder = folder;
			Size = size;
		}

		[DataMember]
		public Guid ID { get; private set; }

		[DataMember]
		public string Name { get; private set; }

		[DataMember]
		public string Folder { get; private set; }

		[DataMember]
		public int Size { get; private set; }
	}
}
