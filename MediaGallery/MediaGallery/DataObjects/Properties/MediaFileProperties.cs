using System.ComponentModel;

namespace MediaGallery.DataObjects.Properties
{
	public class MediaFileProperties : FileSystemEntryProperties
	{
		public MediaFileProperties(MediaFile mediaFile) : base(mediaFile)
		{
			MediaFile = mediaFile;
		}

		#region Properties

		[Browsable(false)]
		public MediaFile MediaFile { get; private set; }

		#region Browsable

		[ReadOnly(true)]
		[Category("Source Folder")]
		[DisplayName("Image Count")]
		public int ImageCount { get { return MediaFile.Parent.ImageCount; } }

		[ReadOnly(true)]
		[Category("Source Folder")]
		[DisplayName("Video Count")]
		public int VideoCount { get { return MediaFile.Parent.VideoCount; } }

		[ReadOnly(true)]
		[Category("File")]
		[DisplayName("Size")]
		public string FileSize { get { return GetShortFileSize(MediaFile.FileSize); } }

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
