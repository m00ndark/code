using System;
using System.Collections.Generic;
using UnpakkDaemon.DataAccess;
using UnpakkDaemon.DataObjects;

namespace UnpakkDaemon
{
	public static class EngineSettings
	{
		private const string PROGRAM_DATA_IDENTIFIER = "%ProgramData%";

		public const string DEFAULT_APPLICATION_DATA_FOLDER = @"%ProgramData%\MoleCode\Unpakk Daemon";
		public const string DEFAULT_SLEEP_TIME = "00:00:10";
		public const string DEFAULT_USE_SPECIFIC_OUTPUT_FOLDER = "False";
		public const string DEFAULT_OUTPUT_FOLDER = "";

		static EngineSettings()
		{
			RootPaths = new List<RootPath>();
			SleepTime = TimeSpan.Parse(DEFAULT_SLEEP_TIME);
			SetApplicationDataFolder(DEFAULT_APPLICATION_DATA_FOLDER);
			UseSpecificOutputFolder = bool.Parse(DEFAULT_USE_SPECIFIC_OUTPUT_FOLDER);
			SetOutputFolder(DEFAULT_OUTPUT_FOLDER);
		}

		#region Properties

		// runtime objects
		public static string ApplicationDataFolderComplete { get { return ReplacePathIdentifier(ApplicationDataFolder); } }
		public static string OutputFolderComplete { get { return ReplacePathIdentifier(OutputFolder); } }

		// persistant objects
		public static string ApplicationDataFolder { get; private set; }
		public static TimeSpan SleepTime { get; set; }
		public static string OutputFolder { get; private set; }
		public static bool UseSpecificOutputFolder { get; set; }
		public static List<RootPath> RootPaths { get; private set; }

		#endregion

		public static void SetApplicationDataFolder(string path)
		{
			ApplicationDataFolder = ReplaceWithPathIdentifier(path);
		}

		public static void SetOutputFolder(string path)
		{
			OutputFolder = ReplaceWithPathIdentifier(path);
		}

		public static void AddRootPath(RootPath rootPath)
		{
			if (rootPath != null && !RootPaths.Contains(rootPath))
			{
				RootPaths.Add(rootPath);
				RootPaths.Sort();
			}
		}

		public static void RemoveRootPath(RootPath rootPath)
		{
			RootPaths.Remove(rootPath);
		}

		public static void Load()
		{
			RegistryHandler.LoadEngineSettings();
		}

		public static void Load(EngineSettingsType engineSettingsType)
		{
			RegistryHandler.LoadEngineSettings(engineSettingsType);
		}

		public static void Save()
		{
			RegistryHandler.SaveEngineSettings();
		}

		public static void Save(EngineSettingsType engineSettingsType)
		{
			RegistryHandler.SaveEngineSettings(engineSettingsType);
		}

		#region Helpers

		private static string ReplaceWithPathIdentifier(string path)
		{
			string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

			if (path.StartsWith(programData, StringComparison.CurrentCultureIgnoreCase))
				return (PROGRAM_DATA_IDENTIFIER + path.Substring(programData.Length));

			return path;
		}

		public static string ReplacePathIdentifier(string path)
		{
			if (path.StartsWith(PROGRAM_DATA_IDENTIFIER, StringComparison.CurrentCultureIgnoreCase))
				return path.Replace(PROGRAM_DATA_IDENTIFIER, Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));

			return path;
		}

		#endregion
	}
}
