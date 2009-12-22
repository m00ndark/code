using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace UnpakkDaemon.DataObjects
{
	public class SubRecord : IXmlSerializable
	{
		public SubRecord() {}

		public SubRecord(string folder, string name, long size)
		{
			Folder = folder;
			Name = name;
			Size = size;
		}

		public SubRecord(XmlReader xmlReader)
		{
			ReadXml(xmlReader);
		}

		#region Properties

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
			writer.WriteAttributeString("Folder", Folder);
			writer.WriteAttributeString("Name", Name);
			writer.WriteAttributeString("Size", Size.ToString());
			writer.WriteEndElement();
		}

		#endregion
	}
}
