using System.ComponentModel;

namespace MediaGallery.DataObjects.Properties
{
	public class MediaFolderProperties : FileSystemEntryProperties
	{
		public MediaFolderProperties(MediaFolder mediaFolder) : base(mediaFolder)
		{
			MediaFolder = mediaFolder;
		}

		#region Properties

		[Browsable(false)]
		public MediaFolder MediaFolder { get; private set; }

		#region Browsable

		[ReadOnly(true)]
		[Category("Source Folder")]
		[DisplayName("Image Count")]
		public int ImageCount { get { return MediaFolder.ImageCount; } }

		[ReadOnly(true)]
		[Category("Source Folder")]
		[DisplayName("Video Count")]
		public int VideoCount { get { return MediaFolder.VideoCount; } }

		#endregion

		#endregion
	}
}
