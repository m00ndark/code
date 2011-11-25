using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MediaGalleryExplorerCore.DataObjects;

namespace MediaGalleryExplorerCore
{
	public static class ObjectPool
	{
		private const string PROGRAM_DATA_IDENTIFIER = "%ProgramData%";
		private const string APPLICATION_PATH_IDENTIFIER = "%ApplicationPath%";

		#region Properties

		// runtime objects
		public static string LastBrowsedPath { get; set; }
		public static string CompleteWorkingDirectory { get { return ReplacePathIdentifier(WorkingDirectory); } }
		public static string CompleteVideoThumbnailsMakerPresetPath { get { return ReplacePathIdentifier(VideoThumbnailsMakerPresetPath); } }

		// persistant objects
		public static string WorkingDirectory { get; private set; }
		public static string VideoThumbnailsMakerPath { get; private set; }
		public static string VideoThumbnailsMakerPresetPath { get; private set; }
		public static List<GallerySource> Sources { get; private set; }

		#endregion

		public static void Initialize()
		{
			WorkingDirectory = string.Empty;
			VideoThumbnailsMakerPath = string.Empty;
			VideoThumbnailsMakerPresetPath = string.Empty;
			Sources = new List<GallerySource>();
		}

		public static void SetWorkingDirectory(string path)
		{
			WorkingDirectory = ReplaceWithPathIdentifier(path);
		}

		public static void SetVideoThumbnailsMakerPath(string path)
		{
			VideoThumbnailsMakerPath = path;
		}

		public static void SetVideoThumbnailsMakerPresetPath(string path)
		{
			VideoThumbnailsMakerPresetPath = ReplaceWithPathIdentifier(path);
		}

		#region Helpers

		private static string ReplaceWithPathIdentifier(string path)
		{
			string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			string applicationPath = Application.StartupPath;

			if (path.StartsWith(programData, StringComparison.CurrentCultureIgnoreCase))
				return (PROGRAM_DATA_IDENTIFIER + path.Substring(programData.Length));

			if (path.StartsWith(applicationPath, StringComparison.CurrentCultureIgnoreCase))
				return (APPLICATION_PATH_IDENTIFIER + path.Substring(applicationPath.Length));

			return path;
		}

		private static string ReplacePathIdentifier(string path)
		{
			if (path.StartsWith(PROGRAM_DATA_IDENTIFIER, StringComparison.CurrentCultureIgnoreCase))
				return path.Replace(PROGRAM_DATA_IDENTIFIER, Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));

			if (path.StartsWith(APPLICATION_PATH_IDENTIFIER, StringComparison.CurrentCultureIgnoreCase))
				return path.Replace(APPLICATION_PATH_IDENTIFIER, Application.StartupPath);

			return path;
		}

		#endregion
	}
}
