using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace UnpakkDaemon.DataObjects
{
	public class SubRecord : IXmlSerializable
	{
		public SubRecord() {}

		public SubRecord(string folder, string name, long size)
			: this(RecordStatus.InProgress, folder, name, size) {}

		public SubRecord(RecordStatus status, string folder, string name, long size)
			: this(DateTime.Now, status, folder, name, size) { }

		public SubRecord(DateTime time, RecordStatus status, string folder, string name, long size)
		{
			Time = time;
			Status = status;
			Folder = folder;
			Name = name;
			Size = size;
		}

		public SubRecord(XmlReader xmlReader)
		{
			ReadXml(xmlReader);
		}

		public void CopyFrom(SubRecord subRecord)
		{
			// do not copy name - it's the unique key
			Time = subRecord.Time;
			Status = subRecord.Status;
			Folder = subRecord.Folder;
			Size = subRecord.Size;
		}

		public SubRecord Succeed()
		{
			Status = RecordStatus.Success;
			Time = DateTime.Now;
			return this;
		}

		public SubRecord Fail()
		{
			Status = RecordStatus.Failure;
			Time = DateTime.Now;
			return this;
		}

		#region Properties

		public DateTime Time { get; set; }
		public RecordStatus Status { get; set; }
		public string Folder { get; set; }
		public string Name { get; set; }
		public long Size { get; set; }

		#endregion

		#region Implementation of IXmlSerializable

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			if (!reader.IsStartElement())
				reader.ReadStartElement("SubRecord");

			while (reader.MoveToNextAttribute())
			{
				switch (reader.Name)
				{
					case "Time":
						Time = DateTime.Parse(reader.Value);
						break;
					case "Status":
						Status = (RecordStatus) Enum.Parse(typeof(RecordStatus), reader.Value);
						break;
					case "Folder":
						Folder = reader.Value;
						break;
					case "Name":
						Name = reader.Value;
						break;
					case "Size":
						Size = long.Parse(reader.Value);
						break;
				}
			}

			reader.MoveToElement();
			if (reader.IsEmptyElement)
				return;

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "SubRecord")
					return;
			}
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("SubRecord");
			writer.WriteAttributeString("Time", Time.ToString("yyyy-MM-dd HH:mm:ss.fff"));
			writer.WriteAttributeString("Status", Status.ToString());
			writer.WriteAttributeString("Folder", Folder);
			writer.WriteAttributeString("Name", Name);
			writer.WriteAttributeString("Size", Size.ToString());
			writer.WriteEndElement();
		}

		#endregion
	}
}
