using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

namespace UnpakkDaemon.DataAccess
{
	public enum SettingsType
	{
		ApplicationDataFolder,
		SleepTime,
		RootPaths
	}

	public static class RegistryHandler
	{
		private const string APPLICATION_REGISTRY_KEY = @"Software\MoleCode\Unpakk Daemon";
		private const string DEFAULT_APPLICATION_DATA_FOLDER = @"%ProgramData%\MoleCode\Unpakk Daemon";
		private const string DEFAULT_SLEEP_TIME = "00:00:10";

		public static void LoadSettings()
		{
			foreach (string settingsType in Enum.GetNames(typeof(SettingsType)))
			{
				LoadSettings((SettingsType) Enum.Parse(typeof(SettingsType), settingsType));
			}
		}

		public static void LoadSettings(SettingsType settingsType)
		{
			RegistryKey key = Registry.CurrentUser.OpenSubKey(APPLICATION_REGISTRY_KEY, false);
			if (key != null)
			{
				switch (settingsType)
				{
					case SettingsType.ApplicationDataFolder:
						EngineSettings.SetApplicationDataFolder((string) key.GetValue("Application Data Folder", DEFAULT_APPLICATION_DATA_FOLDER));
						break;

					case SettingsType.SleepTime:
						EngineSettings.SleepTime = TimeSpan.Parse((string) key.GetValue("Sleep Time", DEFAULT_SLEEP_TIME));
						break;

					case SettingsType.RootPaths:
						EngineSettings.RootPaths.Clear();
						RegistryKey subKey = key.OpenSubKey("Root Paths", false);
						if (subKey != null)
						{
							List<string> valueNames = subKey.GetValueNames().ToList();
							valueNames.Sort();
							foreach (string valueName in valueNames)
							{
								if (valueName.StartsWith("Root Path "))
									EngineSettings.AddRootPath((string) subKey.GetValue(valueName, null));
							}
							subKey.Close();
						}
						break;
				}
				key.Close();
			}
		}

		public static void SaveSettings()
		{
			foreach (string settingsType in Enum.GetNames(typeof(SettingsType)))
			{
				SaveSettings((SettingsType) Enum.Parse(typeof(SettingsType), settingsType));
			}
		}

		public static void SaveSettings(SettingsType settingsType)
		{
			RegistryKey key = Registry.CurrentUser.CreateSubKey(APPLICATION_REGISTRY_KEY);
			if (key != null)
			{
				switch (settingsType)
				{
					case SettingsType.ApplicationDataFolder:
						key.SetValue("Application Data Folder", EngineSettings.ApplicationDataFolder);
						break;

					case SettingsType.SleepTime:
						key.SetValue("Sleep Time", EngineSettings.SleepTime.ToString());
						break;

					case SettingsType.RootPaths:
						RegistryKey subKey = key.CreateSubKey("Root Paths");
						if (subKey != null)
						{
							foreach (string valueName in subKey.GetValueNames())
							{
								subKey.DeleteValue(valueName);
							}
							int rootPathCount = 0;
							foreach (string rootPath in EngineSettings.RootPaths)
							{
								subKey.SetValue("Root Path " + rootPathCount.ToString("00"), rootPath);
								rootPathCount++;
							}
						}
						break;
				}
				key.Close();
			}
		}
	}
}
