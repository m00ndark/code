using System;
using MediaGalleryExplorerCore.DataObjects;

namespace MediaGalleryExplorerCore.EventArguments
{
	public class MediaFolderEventArgs : EventArgs
	{
		public MediaFolderEventArgs(MediaFolder folder) : base()
		{
			Folder = folder;
		}

		public MediaFolder Folder { get; private set; }
	}
}
