using System.ComponentModel;

namespace MediaGalleryExplorerCore.DataObjects.Properties
{
	public class MediaFolderProperties : FileSystemEntryProperties
	{
		public MediaFolderProperties(MediaFolder mediaFolder) : base()
		{
			MediaFolder = mediaFolder;
		}

		#region Properties

		[Browsable(false)]
		public MediaFolder MediaFolder { get; private set; }

		#region Browsable

		[ReadOnly(true)]
		[Category("Count")]
		[DisplayName("Images")]
		public int ImageCount { get { return MediaFolder.ImageCount; } }

		[ReadOnly(true)]
		[Category("Count")]
		[DisplayName("Videos")]
		public int VideoCount { get { return MediaFolder.VideoCount; } }

		[ReadOnly(true)]
		[Category("Folder")]
		[DisplayName("Name")]
		public string BrowsableName { get { return MediaFolder.Name; } }

		[ReadOnly(true)]
		[Category("Folder")]
		[DisplayName("Relative Path")]
		public string BrowsableRelativePath { get { return MediaFolder.RelativePath; } }

		[ReadOnly(true)]
		[Category("Source")]
		[DisplayName("Root Path")]
		public string BrowsableRootPath { get { return MediaFolder.Source.Path; } }

		[ReadOnly(true)]
		[Category("Source")]
		[DisplayName("Scanned")]
		public string BrowsableScanned { get { return MediaFolder.Source.ScanDateStr; } }

		#endregion

		#endregion
	}
}
