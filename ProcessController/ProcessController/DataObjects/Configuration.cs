using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using ProcessController.DataAccess;

namespace ProcessController.DataObjects
{
    public class Configuration : IXmlSerializable
    {
        public const int MAX_VISIBLE_RECENT_USAGE_COUNT = 10;

        public Configuration()
        {
            WindowVisible = true;
            StartWithWindows = true;
            Applications = new List<Application>();
            RecentUsages = new List<RecentUsage>();
        }

        #region Properties

        public bool WindowVisible { get; set; }

        public bool StartWithWindows { get; set; }

        public IList<Application> Applications { get; private set; }

        public IList<RecentUsage> RecentUsages { get; private set; }

        public IEnumerable<string> Groups
        {
            get { return Applications.Select(app => app.Group).Where(group => (group != null)).Distinct(StringComparer.CurrentCultureIgnoreCase).OrderBy(group => group); }
        }

        public IEnumerable<string> Sets
        {
            get { return Applications.Select(app => app.Sets).Aggregate(new List<string>(), (x, y) => (x.Concat(y).ToList())).Distinct(StringComparer.CurrentCultureIgnoreCase).OrderBy(set => set); }
        }

        #endregion

        #region Recent support

        public void AddRecentUsage(string id)
        {
            AddRecentUsage(id, 1, true);
        }

        public void AddRecentUsage(string id, int increment)
        {
            AddRecentUsage(id, increment, false);
        }

        private void AddRecentUsage(string id, int increment, bool saveChanges)
        {
            RecentUsage recentUsage = RecentUsages.FirstOrDefault(recent => (recent.ID == id));
            if (recentUsage != null)
                recentUsage.Count += increment;
            else
            {
                recentUsage = new RecentUsage(id, increment);
                RecentUsages.Add(recentUsage);
            }
            RecentUsages = RecentUsages.ToDictionary(recent => recent, recent => ApplicationControl.GetApplicationByID(recent.ID))
                .OrderByDescending(recentPair => recentPair.Key.Count)
                .ThenBy(recentPair => (recentPair.Value != null ? recentPair.Value.Name : recentPair.Key.ID))
                .Select(recentPair => recentPair.Key).ToList();
            if (saveChanges)
                RegistryHandler.SaveConfiguration(this, true);
        }

        #endregion

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

                if (reader.NodeType == XmlNodeType.Element && reader.Name == "RecentUsage")
                    RecentUsages.Add(new RecentUsage(reader));
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (Application application in Applications)
                application.WriteXml(writer);

            foreach (RecentUsage recentUsage in RecentUsages)
                recentUsage.WriteXml(writer);
        }

        #endregion
    }
}
