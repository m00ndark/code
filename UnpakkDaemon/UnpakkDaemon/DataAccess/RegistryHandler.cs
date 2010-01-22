using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using UnpakkDaemon.DataObjects;

namespace UnpakkDaemon.DataAccess
{
	public enum EngineSettingsType
	{
		ApplicationDataFolder,
		SleepTime,
		RootPaths
	}

	public enum TraySettingsType
	{
		TrayWindowVisibility,
		TrayStartupWithWindows
	}

	public static class RegistryHandler
	{
		private const string APPLICATION_ENGINE_REGISTRY_KEY = @"Software\MoleCode\Unpakk Daemon\Engine";
		private const string APPLICATION_TRAY_REGISTRY_KEY = @"Software\MoleCode\Unpakk Daemon\Tray";
		private const string WINDOWS_RUN_REGISTRY_KEY = @"Software\Microsoft\Windows\CurrentVersion\Run";

		public static void LoadEngineSettings()
		{
			foreach (string engineSettingsType in Enum.GetNames(typeof(EngineSettingsType)))
			{
				LoadEngineSettings((EngineSettingsType) Enum.Parse(typeof(EngineSettingsType), engineSettingsType));
			}
		}

		public static void LoadEngineSettings(EngineSettingsType engineSettingsType)
		{
			RegistryKey key = Registry.LocalMachine.OpenSubKey(APPLICATION_ENGINE_REGISTRY_KEY, false);
			if (key != null)
			{
				switch (engineSettingsType)
				{
					case EngineSettingsType.ApplicationDataFolder:
						EngineSettings.SetApplicationDataFolder((string) key.GetValue("Application Data Folder", EngineSettings.DEFAULT_APPLICATION_DATA_FOLDER));
						break;

					case EngineSettingsType.SleepTime:
						EngineSettings.SleepTime = TimeSpan.Parse((string) key.GetValue("Sleep Time", EngineSettings.DEFAULT_SLEEP_TIME));
						break;

					case EngineSettingsType.RootPaths:
						EngineSettings.RootPaths.Clear();
						List<string> subKeys = key.GetSubKeyNames().ToList();
						subKeys.Sort();
						foreach (string subKey in subKeys)
						{
							if (subKey.StartsWith("Root Path "))
							{
								RegistryKey rootPathKey = key.OpenSubKey(subKey, false);
								if (rootPathKey != null)
								{
									string path = (string) rootPathKey.GetValue("Path", null);
									string userName = (string) rootPathKey.GetValue("User Name", string.Empty);
									string password = (string) rootPathKey.GetValue("Password", string.Empty);
									if (path != null) EngineSettings.AddRootPath(new RootPath(path, userName, password));
									rootPathKey.Close();
								}
							}
						}
						break;
				}
				key.Close();
			}
		}

		public static void LoadTraySettings()
		{
			foreach (string traySettingsType in Enum.GetNames(typeof(TraySettingsType)))
			{
				LoadTraySettings((TraySettingsType) Enum.Parse(typeof(TraySettingsType), traySettingsType));
			}
		}

		public static void LoadTraySettings(TraySettingsType traySettingsType)
		{
			RegistryKey key = Registry.LocalMachine.OpenSubKey(APPLICATION_TRAY_REGISTRY_KEY, false);
			if (key != null)
			{
				switch (traySettingsType)
				{
					case TraySettingsType.TrayWindowVisibility:
						TraySettings.WindowVisible = bool.Parse((string) key.GetValue("Tray Window Visible", TraySettings.DEFAULT_WINDOW_VISIBLE));
						break;

					case TraySettingsType.TrayStartupWithWindows:
						TraySettings.StartWithWindows = bool.Parse((string) key.GetValue("Start Tray With Windows", TraySettings.DEFAULT_START_WITH_WINDOWS));
						break;
				}
				key.Close();
			}
		}

		public static void SaveEngineSettings()
		{
			foreach (string engineSettingsType in Enum.GetNames(typeof(EngineSettingsType)))
			{
				SaveEngineSettings((EngineSettingsType) Enum.Parse(typeof(EngineSettingsType), engineSettingsType));
			}
		}

		public static void SaveEngineSettings(EngineSettingsType engineSettingsType)
		{
			RegistryKey key = Registry.LocalMachine.CreateSubKey(APPLICATION_ENGINE_REGISTRY_KEY);
			if (key != null)
			{
				switch (engineSettingsType)
				{
					case EngineSettingsType.ApplicationDataFolder:
						key.SetValue("Application Data Folder", EngineSettings.ApplicationDataFolder);
						break;

					case EngineSettingsType.SleepTime:
						key.SetValue("Sleep Time", EngineSettings.SleepTime.ToString());
						break;

					case EngineSettingsType.RootPaths:
						foreach (string subKey in key.GetSubKeyNames())
						{
							if (subKey.StartsWith("Root Path "))
								key.DeleteSubKeyTree(subKey);
						}
						int rootPathCount = 0;
						foreach (RootPath rootPath in EngineSettings.RootPaths)
						{
							RegistryKey rootPathKey = key.CreateSubKey("Root Path " + rootPathCount.ToString("00"));
							if (rootPathKey != null)
							{
								rootPathKey.SetValue("Path", rootPath.Path);
								rootPathKey.SetValue("User Name", rootPath.UserName);
								rootPathKey.SetValue("Password", rootPath.Password);
								rootPathKey.Close();
							}
							rootPathCount++;
						}
						break;
				}
				key.Close();
			}
		}

		public static void SaveTraySettings()
		{
			foreach (string traySettingsType in Enum.GetNames(typeof(TraySettingsType)))
			{
				SaveEngineSettings((EngineSettingsType) Enum.Parse(typeof(EngineSettingsType), traySettingsType));
			}
		}

		public static void SaveTraySettings(TraySettingsType traySettingsType)
		{
			RegistryKey key = Registry.LocalMachine.CreateSubKey(APPLICATION_TRAY_REGISTRY_KEY);
			if (key != null)
			{
				switch (traySettingsType)
				{
					case TraySettingsType.TrayWindowVisibility:
						key.SetValue("Tray Window Visible", TraySettings.WindowVisible.ToString());
						break;

					case TraySettingsType.TrayStartupWithWindows:
						key.SetValue("Start Tray With Windows", TraySettings.StartWithWindows.ToString());
						break;
				}
				key.Close();
			}
		}

		public static void SetTrayToStartWithWindows(string executablePath)
		{
			RegistryKey key = Registry.CurrentUser.CreateSubKey(WINDOWS_RUN_REGISTRY_KEY);
			if (key != null)
			{
				if (string.IsNullOrEmpty(executablePath))
				{
					key.DeleteValue("Unpakk Daemon Tray", false);
				}
				else
				{
					key.SetValue("Unpakk Daemon Tray", "\"" + executablePath + "\"", RegistryValueKind.String);
				}
				key.Close();
			}
		}
	}
}
