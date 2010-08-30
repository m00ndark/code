using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ProcessController.DataObjects
{
    public class RecentUsage : IXmlSerializable
    {
        public RecentUsage(string id) : this(id, 1) { }

        public RecentUsage(string id, int count)
        {
            ID = id;
            Count = count;
        }

        public RecentUsage(XmlReader reader)
        {
            ReadXml(reader);
        }

        #region Properties

        public string ID { get; private set; }
        public int Count { get; set; }

        public string Name { get { return GetName(ID); } }

        #endregion

        public static string GetName(string id)
        {
            Application application = ApplicationControl.GetApplicationByID(id);
            return (application != null ? (application.Group != null ? application.Group + " " : string.Empty) + application.Name : id);
        }

        #region Implementation of IXmlSerializable

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (!reader.IsStartElement())
                reader.ReadStartElement("RecentUsage");

            while (reader.MoveToNextAttribute())
            {
                switch (reader.Name)
                {
                    case "ID":
                        ID = reader.Value;
                        break;
                    case "Count":
                        Count = int.Parse(reader.Value);
                        break;
                }
            }

            reader.MoveToElement();
            if (reader.IsEmptyElement)
                return;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "RecentUsage")
                    return;
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("RecentUsage");
            writer.WriteAttributeString("ID", ID);
            writer.WriteAttributeString("Count", Count.ToString());
            writer.WriteEndElement();
        }

        #endregion
    }
}
