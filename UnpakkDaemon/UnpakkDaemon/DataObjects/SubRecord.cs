using System.Xml;
using System.Xml.Schema;

namespace UnpakkDaemon.DataObjects
{
	public class SubRecord : IRecord
	{
		public SubRecord() {}

		public SubRecord(XmlReader xmlReader)
		{
			ReadXml(xmlReader);
		}

		#region Properties

		public string Name { get; set; }
		public int Size { get; set; }

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
					case "Name":
						Name = reader.Value;
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
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "SubRecord")
					return;
			}
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("SubRecord");
			writer.WriteAttributeString("Name", Name);
			writer.WriteAttributeString("Size", Size.ToString());
			writer.WriteEndElement();
		}

		#endregion
	}
}
