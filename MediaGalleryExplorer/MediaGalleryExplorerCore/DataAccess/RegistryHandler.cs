using System;
using Microsoft.Win32;

namespace MediaGalleryExplorerCore.DataAccess
{
	public enum SettingsType
	{
		DatabaseLocation,
		WorkingDirectory,
		VideoThumbnailsMakerPath,
		VideoThumbnailsMakerPresetPath
	}

	public static class RegistryHandler
	{
		private const string APPLICATION_REGISTRY_KEY = @"Software\MoleCode\Media Gallery";
		private const string DEFAULT_DATABASE_PATH = @"%ProgramData%\MoleCode\Media Gallery\Database";
		private const string DEFAULT_WORKING_DIRECTORY = @"%ApplicationPath%\Working Directory";
		private const string DEFAULT_VIDEO_THUMBNAILS_MAKER_PATH = @"C:\Program Files (x86)\Video Thumbnails Maker\VideoThumbnailsMaker.exe";
		private const string DEFAULT_VIDEO_THUMBNAILS_MAKER_PRESET_PATH = @"%ApplicationPath%\Presets\vtm_preset.vtm";

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
			switch (settingsType)
			{
				case SettingsType.WorkingDirectory:
					ObjectPool.SetWorkingDirectory(key != null ? (string) key.GetValue("Working Directory", DEFAULT_WORKING_DIRECTORY) : DEFAULT_WORKING_DIRECTORY);
					break;

				case SettingsType.VideoThumbnailsMakerPath:
					ObjectPool.SetVideoThumbnailsMakerPath(key != null ? (string) key.GetValue("Video Thumbnails Maker Path", DEFAULT_VIDEO_THUMBNAILS_MAKER_PATH) : DEFAULT_VIDEO_THUMBNAILS_MAKER_PATH);
					break;

				case SettingsType.VideoThumbnailsMakerPresetPath:
					ObjectPool.SetVideoThumbnailsMakerPresetPath(key != null ? (string) key.GetValue("Video Thumbnails Maker Preset Path", DEFAULT_VIDEO_THUMBNAILS_MAKER_PRESET_PATH) : DEFAULT_VIDEO_THUMBNAILS_MAKER_PRESET_PATH);
					break;
			}
			if (key != null) key.Close();
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
					case SettingsType.WorkingDirectory:
						key.SetValue("Working Directory", ObjectPool.WorkingDirectory);
						break;

					case SettingsType.VideoThumbnailsMakerPath:
						key.SetValue("Video Thumbnails Maker Path", ObjectPool.VideoThumbnailsMakerPath);
						break;

					case SettingsType.VideoThumbnailsMakerPresetPath:
						key.SetValue("Video Thumbnails Maker Preset Path", ObjectPool.VideoThumbnailsMakerPresetPath);
						break;
				}
				key.Close();
			}
		}
	}
}
