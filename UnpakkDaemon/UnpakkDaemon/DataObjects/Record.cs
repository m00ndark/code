using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace UnpakkDaemon.DataObjects
{
	public class Record : IXmlSerializable
	{
		public Record()
		{
			SubRecords = new List<SubRecord>();
		}

		public Record(string folder, string sfvName, string rarName, int rarCount, long rarSize)
			: this(Guid.NewGuid(), folder, sfvName, rarName, rarCount, rarSize) {}

		public Record(Guid id, string folder, string sfvName, string rarName, int rarCount, long rarSize)
		{
			ID = id;
			Folder = folder;
			SFVName = sfvName;
			RARName = rarName;
			RARCount = rarCount;
			RARSize = rarSize;
			SubRecords = new List<SubRecord>();
		}

		public Record(XmlReader xmlReader)
		{
			SubRecords = new List<SubRecord>();
			ReadXml(xmlReader);
		}

		#region Properties

		public Guid ID { get; set; }
		public string Folder { get; set; }
		public string SFVName { get; set; }
		public string RARName { get; set; }
		public int RARCount { get; set; }
		public long RARSize { get; set; }
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
					case "Folder":
						Folder = reader.Value;
						break;
					case "SFVName":
						SFVName = reader.Value;
						break;
					case "RARName":
						RARName = reader.Value;
						break;
					case "RARCount":
						RARCount = int.Parse(reader.Value);
						break;
					case "RARSize":
						RARSize = long.Parse(reader.Value);
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
			writer.WriteAttributeString("Folder", Folder);
			writer.WriteAttributeString("SFVName", SFVName);
			writer.WriteAttributeString("RARName", RARName);
			writer.WriteAttributeString("RARCount", RARCount.ToString());
			writer.WriteAttributeString("RARSize", RARSize.ToString());
			foreach (SubRecord subRecord in SubRecords)
			{
				subRecord.WriteXml(writer);
			}
			writer.WriteEndElement();
		}

		#endregion
	}
}
