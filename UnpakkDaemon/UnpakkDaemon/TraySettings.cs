using UnpakkDaemon.DataAccess;

namespace UnpakkDaemon
{
	public static class TraySettings
	{
		public const string DEFAULT_WINDOW_VISIBLE = "True";
		public const string DEFAULT_START_WITH_WINDOWS = "False";

		static TraySettings()
		{
			WindowVisible = bool.Parse(DEFAULT_WINDOW_VISIBLE);
			StartWithWindows = bool.Parse(DEFAULT_START_WITH_WINDOWS);
		}

		#region Properties

		public static bool WindowVisible { get; set; }
		public static bool StartWithWindows { get; set; }

		#endregion

		public static void Load()
		{
			RegistryHandler.LoadTraySettings();
		}

		public static void Load(TraySettingsType traySettingsType)
		{
			RegistryHandler.LoadTraySettings(traySettingsType);
		}

		public static void Save()
		{
			RegistryHandler.SaveTraySettings();
		}

		public static void Save(TraySettingsType traySettingsType)
		{
			RegistryHandler.SaveTraySettings(traySettingsType);
		}
	}
}
