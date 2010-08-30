using System;
using Microsoft.Win32;
using ProcessController.DataObjects;
using Application = ProcessController.DataObjects.Application;

namespace ProcessController.DataAccess
{
    public static class RegistryHandler
    {
        private const string APPLICATION_REGISTRY_KEY = @"SOFTWARE\MoleCode\ProcessController";
        private const string WINDOWS_RUN_REGISTRY_KEY = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        public static Configuration LoadConfiguration()
        {
            Configuration config = new Configuration();
            RegistryKey key = Registry.CurrentUser.OpenSubKey(APPLICATION_REGISTRY_KEY, false);
            if (key != null)
            {
                config.WindowVisible = bool.Parse((string) key.GetValue("Window Visible", "True"));
                config.StartWithWindows = bool.Parse((string) key.GetValue("Start With Windows", "True"));
                config.RecentUsages.Clear();
                RegistryKey recentKey = key.OpenSubKey("Recent", false);
                if (recentKey != null)
                {
                    RegistryKey recentUsageKey = recentKey.OpenSubKey("Recent 00", false);
                    while (recentUsageKey != null)
                    {
                        string id = (string) recentUsageKey.GetValue("ID", null);
                        int count = (int) recentUsageKey.GetValue("Count", 0);
                        if (id != null) config.AddRecentUsage(id, count);
                        recentUsageKey.Close();
                        recentUsageKey = recentKey.OpenSubKey("Recent " + config.RecentUsages.Count.ToString("00"), false);
                    }
                    recentKey.Close();
                }
                config.Applications.Clear();
                RegistryKey applicationsKey = key.OpenSubKey("Applications", false);
                if (applicationsKey != null)
                {
                    RegistryKey applicationKey = applicationsKey.OpenSubKey("Application 00", false);
                    while (applicationKey != null)
                    {
                        Guid id = new Guid((string) applicationKey.GetValue("ID", Guid.NewGuid().ToString()));
                        string name = (string) applicationKey.GetValue("Name", "UNKNOWN");
                        string path = (string) applicationKey.GetValue("Path", string.Empty);
                        string arguments = (string) applicationKey.GetValue("Arguments", string.Empty);
                        string group = (string) applicationKey.GetValue("Group", string.Empty);
                        Application application = new Application(id, name, path, arguments, (string.IsNullOrEmpty(group) ? null : group));
                        application.Sets.Clear();
                        string set = (string) applicationKey.GetValue("Set 00", string.Empty);
                        while (!string.IsNullOrEmpty(set))
                        {
                            application.Sets.Add(set);
                            set = (string) applicationKey.GetValue("Set " + application.Sets.Count.ToString("00"), string.Empty);
                        }
                        config.Applications.Add(application);
                        applicationKey.Close();
                        applicationKey = applicationsKey.OpenSubKey("Application " + config.Applications.Count.ToString("00"), false);
                    }
                    applicationsKey.Close();
                }
                key.Close();
            }
            return config;
        }

        public static void SaveConfiguration(Configuration config)
        {
            SaveConfiguration(config, false);
        }

        public static void SaveConfiguration(Configuration config, bool recentUsageOnly)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(APPLICATION_REGISTRY_KEY);
			if (key != null)
			{
                if (!recentUsageOnly)
                {
                    key.SetValue("Window Visible", config.WindowVisible.ToString(), RegistryValueKind.String);
                    key.SetValue("Start With Windows", config.StartWithWindows.ToString(), RegistryValueKind.String);
                    RegistryKey applicationsKey = key.CreateSubKey("Applications");
                    if (applicationsKey != null)
                    {
                        string[] subKeyNames = applicationsKey.GetSubKeyNames();
                        foreach (string subKeyName in subKeyNames)
                        {
                            if (subKeyName.StartsWith("Application "))
                                applicationsKey.DeleteSubKeyTree(subKeyName);
                        }
                        for (int i = 0; i < config.Applications.Count; i++)
                        {
                            RegistryKey applicationKey = applicationsKey.CreateSubKey("Application " + i.ToString("00"));
                            if (applicationKey != null)
                            {
                                applicationKey.SetValue("ID", config.Applications[i].ID.ToString(), RegistryValueKind.String);
                                applicationKey.SetValue("Name", config.Applications[i].Name, RegistryValueKind.String);
                                applicationKey.SetValue("Path", config.Applications[i].Path, RegistryValueKind.String);
                                applicationKey.SetValue("Arguments", config.Applications[i].Arguments ?? string.Empty, RegistryValueKind.String);
                                applicationKey.SetValue("Group", config.Applications[i].Group ?? string.Empty, RegistryValueKind.String);
                                for (int j = 0; j < config.Applications[i].Sets.Count; j++)
                                {
                                    applicationKey.SetValue("Set " + j.ToString("00"), config.Applications[i].Sets[j]);
                                }
                                applicationKey.Close();
                            }
                        }
                        applicationsKey.Close();
                    }
                }
                RegistryKey recentKey = key.CreateSubKey("Recent");
                if (recentKey != null)
                {
                    string[] subKeyNames = recentKey.GetSubKeyNames();
                    foreach (string subKeyName in subKeyNames)
                    {
                        if (subKeyName.StartsWith("Recent "))
                            recentKey.DeleteSubKeyTree(subKeyName);
                    }
                    for (int i = 0; i < config.RecentUsages.Count; i++)
                    {
                        RegistryKey recentUsageKey = recentKey.CreateSubKey("Recent " + i.ToString("00"));
                        if (recentUsageKey != null)
                        {
                            recentUsageKey.SetValue("ID", config.RecentUsages[i].ID, RegistryValueKind.String);
                            recentUsageKey.SetValue("Count", config.RecentUsages[i].Count, RegistryValueKind.DWord);
                            recentUsageKey.Close();
                        }
                    }
                    recentKey.Close();
                }
                key.Close();
            }
        }

        public static void SetWindowsStartupTrigger(bool enable)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(WINDOWS_RUN_REGISTRY_KEY, true);
			if (key != null)
			{
                if (enable)
                    key.SetValue("Process Controller", "\"" + System.Windows.Forms.Application.ExecutablePath + "\"", RegistryValueKind.String);
                else
                    key.DeleteValue("Process Controller", false);
                key.Close();
			}
        }
    }
}
