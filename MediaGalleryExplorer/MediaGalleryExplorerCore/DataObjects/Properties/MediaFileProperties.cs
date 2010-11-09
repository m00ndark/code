using System.ComponentModel;

namespace MediaGalleryExplorerCore.DataObjects.Properties
{
	public class MediaFileProperties : FileSystemEntryProperties
	{
		public MediaFileProperties(MediaFile mediaFile) : base()
		{
			MediaFile = mediaFile;
		}

		#region Properties

		[Browsable(false)]
		public MediaFile MediaFile { get; private set; }

		#region Browsable

		[ReadOnly(true)]
		[Category("Count")]
		[DisplayName("Images")]
		public int ImageCount { get { return MediaFile.Parent.ImageCount; } }

		[ReadOnly(true)]
		[Category("Count")]
		[DisplayName("Videos")]
		public int VideoCount { get { return MediaFile.Parent.VideoCount; } }

		[ReadOnly(true)]
		[Category("File")]
		[DisplayName("Name")]
		public string BrowsableName { get { return MediaFile.Name; } }

		[ReadOnly(true)]
		[Category("File")]
		[DisplayName("Relative Path")]
		public string BrowsableRelativePath { get { return MediaFile.RelativePath; } }

		[ReadOnly(true)]
		[Category("File")]
		[DisplayName("Size")]
		public string FileSize { get { return GetShortFileSize(MediaFile.FileSize); } }

		[ReadOnly(true)]
		[Category("Source")]
		[DisplayName("Root Path")]
		public string BrowsableRootPath { get { return MediaFile.Source.Path; } }

		[ReadOnly(true)]
		[Category("Source")]
		[DisplayName("Scanned")]
		public string BrowsableScanned { get { return MediaFile.Source.ScanDateStr; } }

		[ReadOnly(true)]
		[Category("Media")]
		[DisplayName("Size")]
		public string Size { get { return MediaFile.Size.Width + "x" + MediaFile.Size.Height; } }

		#endregion

		#endregion

		private static string GetShortFileSize(long fileSize)
		{
			int units = 0;
			double shortFileSize = fileSize;
			while (shortFileSize >= 1024)
			{
				shortFileSize = shortFileSize / 1024;
				units++;
			}
			string unit = string.Empty;
			switch (units)
			{
				case 0: unit = "bytes"; break;
				case 1: unit = "kB"; break;
				case 2: unit = "MB"; break;
				case 3: unit = "GB"; break;
				case 4: unit = "TB"; break;
			}
			return shortFileSize.ToString("0.0") + " " + unit;
		}
	}
}
