using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ProcessController.DataObjects
{
    public class Configuration : IXmlSerializable
    {
        public Configuration()
        {
            WindowVisible = true;
            StartWithWindows = false;
            Applications = new List<Application>();
        }

        public bool WindowVisible { get; set; }

        public bool StartWithWindows { get; set; }

        public IList<Application> Applications { get; private set; }

        public IEnumerable<string> Groups
        {
            get { return Applications.Select(app => app.Group).Where(group => (group != null)).Distinct(StringComparer.CurrentCultureIgnoreCase).OrderBy(group => group); }
        }

        public IEnumerable<string> Sets
        {
            get { return Applications.Select(app => app.Sets).Aggregate((x, y) => (x.Concat(y).ToList())).Distinct(StringComparer.CurrentCultureIgnoreCase).OrderBy(set => set); }
        }

        #region Implementation of IXmlSerializable

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (!reader.IsStartElement())
                reader.ReadStartElement("Configuration");

            if (reader.IsEmptyElement)
                return;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Configuration")
                    return;

                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Application")
                    Applications.Add(new Application(reader));
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (Application application in Applications)
            {
                application.WriteXml(writer);
            }
        }

        #endregion
    }
}
