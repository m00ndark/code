using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace ProcessController.DataObjects
{
    public class Application
    {
        public Application(string name, string path, string arguments) : this(name, path, arguments, null) {}

        public Application(string name, string path, string arguments, string group)
        {
            Name = name;
            Path = path;
            Arguments = arguments;
            Group = group;
            Sets = new List<string>();
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
    }
}
