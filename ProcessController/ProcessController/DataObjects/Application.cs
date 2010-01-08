using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ProcessController.DataObjects
{
    public class Application : IXmlSerializable
    {
        public Application()
        {
            Sets = new List<string>();
        }

        public Application(string name, string path, string arguments) : this(name, path, arguments, null) {}

        public Application(string name, string path, string arguments, string group) : this()
        {
            Name = name;
            Path = path;
            Arguments = arguments;
            Group = group;
        }

        public Application(XmlReader reader) : this()
        {
            ReadXml(reader);
        }

        public string Name { get; set; }
        public string Path { get; set; }
        public string Arguments { get; set; }
        public string Group { get; set; }
        public IList<string> Sets { get; private set; }

        public bool Start()
        {
            if (IsRunning) return false;
            Process process = new Process() { StartInfo = { FileName = Path, Arguments = Arguments } };
            return process.Start();
        }

        public void Stop()
        {
            foreach (Process process in GetUnmanagedProcesses())
            {
                try { process.Kill(); } catch { }
            }
        }

        public bool IsRunning
        {
            get { return (GetUnmanagedProcesses().Count() > 0); }
        }

        private IEnumerable<Process> GetUnmanagedProcesses()
        {
            IList<Process> unmanagedProcesses = new List<Process>();
            try 
            {
                string pathOnly = System.IO.Path.GetDirectoryName(Path);
                Process[] processList = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(Path));
                foreach (Process process in processList)
                {
                    try
                    {
                        if (process.MainModule != null && System.IO.Path.GetDirectoryName(process.MainModule.FileName).Equals(pathOnly, StringComparison.CurrentCultureIgnoreCase))
                            unmanagedProcesses.Add(process);
                    }
                    catch { }
                }
            }
            catch { }
            return unmanagedProcesses;
        }

        #region Implementation of IXmlSerializable

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (!reader.IsStartElement())
                reader.ReadStartElement("Application");

            while (reader.MoveToNextAttribute())
            {
                switch (reader.Name)
                {
                    case "Name":
                        Name = reader.Value;
                        break;
                    case "Path":
                        Path = reader.Value;
                        break;
                    case "Arguments":
                        Arguments = reader.Value;
                        break;
                    case "Group":
                        Group = reader.Value;
                        if (string.IsNullOrEmpty(Group)) Group = null;
                        break;
                }
            }

            reader.MoveToElement();
            if (reader.IsEmptyElement)
                return;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Application")
                    return;

                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Set" && reader.MoveToNextAttribute() && reader.Name == "Name")
                {
                    Sets.Add(reader.Value);
                    reader.MoveToElement();
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Application");
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("Path", Path);
            writer.WriteAttributeString("Arguments", Arguments);
            writer.WriteAttributeString("Group", Group ?? string.Empty);
            foreach (string set in Sets)
            {
                writer.WriteStartElement("Set");
                writer.WriteAttributeString("Name", set);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        #endregion
    }
}
