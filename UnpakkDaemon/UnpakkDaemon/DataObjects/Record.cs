using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace UnpakkDaemon.DataObjects
{
	public class Record : IRecord
	{
		public Record()
		{
			SubRecords = new List<SubRecord>();
		}

		public Record(Guid id, string name, string folder, int size)
		{
			ID = id;
			Name = name;
			Folder = folder;
			Size = size;
			SubRecords = new List<SubRecord>();
		}

		public Record(XmlReader xmlReader)
		{
			SubRecords = new List<SubRecord>();
			ReadXml(xmlReader);
		}

		#region Properties

		public Guid ID { get; set; }
		public string Name { get; set; }
		public string Folder { get; set; }
		public int Size { get; set; }
		public IList<SubRecord> SubRecords { get; private set; }

		#endregion

		#region Implementation of IXmlSerializable

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			if (!reader.IsStartElement())
				reader.ReadStartElement("Record");

			while (reader.MoveToNextAttribute())
			{
				switch (reader.Name)
				{
					case "ID":
						ID = new Guid(reader.Value);
						break;
					case "Name":
						Name = reader.Value;
						break;
					case "Folder":
						Folder = reader.Value;
						break;
					case "Size":
						Size = int.Parse(reader.Value);
						break;
				}
			}

			reader.MoveToElement();
			if (reader.IsEmptyElement)
				return;

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Record")
					return;

				if (reader.NodeType == XmlNodeType.Element && reader.Name == "SubRecord")
					SubRecords.Add(new SubRecord(reader));
			}
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Record");
			writer.WriteAttributeString("ID", ID.ToString());
			writer.WriteAttributeString("Name", Name);
			writer.WriteAttributeString("Folder", Folder);
			writer.WriteAttributeString("Size", Size.ToString());
			foreach (SubRecord subRecord in SubRecords)
			{
				subRecord.WriteXml(writer);
			}
			writer.WriteEndElement();
		}

		#endregion
	}
}
