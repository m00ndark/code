using System;
using System.Windows.Forms;
using Microsoft.Win32;
using ProcessController.DataObjects;
using Application=ProcessController.DataObjects.Application;

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
                config.StartWithWindows = bool.Parse((string) key.GetValue("Start With Windows", "False"));
                config.Applications.Clear();
                RegistryKey applicationKey = key.OpenSubKey("Application 00", false);
                while (applicationKey != null)
                {
                    string name = (string) applicationKey.GetValue("Name", "UNKNOWN");
                    string path = (string) applicationKey.GetValue("Path", string.Empty);
                    string arguments = (string) applicationKey.GetValue("Arguments", string.Empty);
                    string group = (string) applicationKey.GetValue("Group", string.Empty);
                    Application application = new Application(name, path, arguments, (string.IsNullOrEmpty(group) ? null : group));
                    application.Sets.Clear();
                    string set = (string) applicationKey.GetValue("Set 00", string.Empty);
                    while (!string.IsNullOrEmpty(set))
                    {
                        application.Sets.Add(set);
                        set = (string) applicationKey.GetValue("Set " + application.Sets.Count.ToString("00"), string.Empty);
                    }
                    config.Applications.Add(application);
                    applicationKey.Close();
                    applicationKey = key.OpenSubKey("Application " + config.Applications.Count.ToString("00"), false);
                }
                key.Close();
            }
            return config;
        }

        public static void SaveConfiguration(Configuration config)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(APPLICATION_REGISTRY_KEY);
			if (key != null)
			{
                key.SetValue("Window Visible", config.WindowVisible.ToString(), RegistryValueKind.String);
                key.SetValue("Start With Windows", config.StartWithWindows.ToString(), RegistryValueKind.String);
                string[] subKeyNames = key.GetSubKeyNames();
                foreach (string subKeyName in subKeyNames)
                {
                    if (subKeyName.StartsWith("Application "))
                        key.DeleteSubKeyTree(subKeyName);
                }
			    for (int i = 0; i < config.Applications.Count; i++)
			    {
			        RegistryKey applicationKey = key.CreateSubKey("Application " + i.ToString("00"));
                    if (applicationKey != null)
                    {
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
                key.Close();
            }
        }

        public static void SetWindowsStartupTrigger(bool enable)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(WINDOWS_RUN_REGISTRY_KEY, true);
			if (key != null)
			{
                if (enable)
    			    key.SetValue("Process Controller", System.Windows.Forms.Application.ExecutablePath, RegistryValueKind.String);
                else
                    key.DeleteValue("Process Controller", false);
                key.Close();
			}
        }
    }
}
