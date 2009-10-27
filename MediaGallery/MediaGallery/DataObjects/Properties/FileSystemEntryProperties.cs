using System.ComponentModel;

namespace MediaGallery.DataObjects.Properties
{
	public class FileSystemEntryProperties
	{
		public FileSystemEntryProperties(FileSystemEntry fileSystemEntry)
		{
			FileSystemEntry = fileSystemEntry;
		}

		#region Properties

		[Browsable(false)]
		public FileSystemEntry FileSystemEntry { get; private set; }

		#region Browsable

		[ReadOnly(true)]
		[Category("File")]
		[DisplayName("Name")]
		public string BrowsableName { get { return FileSystemEntry.Name; } }

		[ReadOnly(true)]
		[Category("File")]
		[DisplayName("Relative Path")]
		public string BrowsableRelativePath { get { return FileSystemEntry.RelativePath; } }

		#endregion

		#endregion
	}
}