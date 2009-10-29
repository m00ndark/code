using System;
using System.Collections.Generic;
using System.Linq;
using MediaGallery.DataObjects;
using Microsoft.Win32;

namespace MediaGallery.DataAccess
{
	public enum SettingsType
	{
		DatabaseLocation,
		WorkingDirectory,
		VideoThumbnailsMakerPath,
		VideoThumbnailsMakerPresetPath,
		GallerySource
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
			if (key != null)
			{
				switch (settingsType)
				{
					case SettingsType.DatabaseLocation:
						ObjectPool.SetDatabaseLocation((string) key.GetValue("Database Location", DEFAULT_DATABASE_PATH));
						break;

					case SettingsType.WorkingDirectory:
						ObjectPool.SetWorkingDirectory((string) key.GetValue("Working Directory", DEFAULT_WORKING_DIRECTORY));
						break;

					case SettingsType.VideoThumbnailsMakerPath:
						ObjectPool.SetVideoThumbnailsMakerPath((string) key.GetValue("Video Thumbnails Maker Path", DEFAULT_VIDEO_THUMBNAILS_MAKER_PATH));
						break;

					case SettingsType.VideoThumbnailsMakerPresetPath:
						ObjectPool.SetVideoThumbnailsMakerPresetPath((string) key.GetValue("Video Thumbnails Maker Preset Path", DEFAULT_VIDEO_THUMBNAILS_MAKER_PRESET_PATH));
						break;

					case SettingsType.GallerySource:
						ObjectPool.Sources.Clear();
						List<string> subKeys = key.GetSubKeyNames().ToList();
						subKeys.Sort();
						foreach (string subKey in subKeys)
						{
							if (subKey.StartsWith("Gallery Source "))
							{
								RegistryKey sourceKey = key.OpenSubKey(subKey, false);
								if (sourceKey != null)
								{
									string sourcePath = (string) sourceKey.GetValue("Path", null);
									int imageCount = (int) sourceKey.GetValue("Image Count", 0);
									int videoCount = (int) sourceKey.GetValue("Video Count", 0);
									ObjectPool.Sources.Add(new GallerySource(sourcePath) { ImageCount = imageCount, VideoCount = videoCount });
									sourceKey.Close();
								}
							}
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
					case SettingsType.DatabaseLocation:
						key.SetValue("Database Location", ObjectPool.DatabaseLocation);
						break;

					case SettingsType.WorkingDirectory:
						key.SetValue("Working Directory", ObjectPool.WorkingDirectory);
						break;

					case SettingsType.VideoThumbnailsMakerPath:
						key.SetValue("Video Thumbnails Maker Path", ObjectPool.VideoThumbnailsMakerPath);
						break;

					case SettingsType.VideoThumbnailsMakerPresetPath:
						key.SetValue("Video Thumbnails Maker Preset Path", ObjectPool.VideoThumbnailsMakerPresetPath);
						break;

					case SettingsType.GallerySource:
						foreach (string subKey in key.GetSubKeyNames())
						{
							if (subKey.StartsWith("Gallery Source "))
							{
								key.DeleteSubKeyTree(subKey);
							}
						}
						int sourceCount = 0;
						foreach (GallerySource source in ObjectPool.Sources)
						{
							RegistryKey sourceKey = key.CreateSubKey("Gallery Source " + sourceCount.ToString("00"));
							if (sourceKey != null)
							{
								sourceKey.SetValue("Path", source.Path);
								sourceKey.SetValue("Image Count", source.ImageCount);
								sourceKey.SetValue("Video Count", source.VideoCount);
								sourceKey.Close();
							}
							sourceCount++;
						}
						break;
				}
				key.Close();
			}
		}
	}
}
