using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace UnpakkDaemon.DataObjects
{
	public class RecordList : List<Record>, IXmlSerializable
	{
		#region Implementation of IXmlSerializable

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			if (!reader.IsStartElement())
				reader.ReadStartElement("RecordList");

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "RecordList")
					return;

				if (reader.NodeType == XmlNodeType.Element && reader.Name == "Record")
					Add(new Record(reader));
			}
		}

		public void WriteXml(XmlWriter writer)
		{
			foreach (Record record in this)
			{
				record.WriteXml(writer);
			}
		}

		#endregion
	}
}
