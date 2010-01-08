using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessController.DataObjects
{
    public class Configuration
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
    }
}
