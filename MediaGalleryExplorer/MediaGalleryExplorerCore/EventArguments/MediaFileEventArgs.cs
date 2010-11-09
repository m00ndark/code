using System;
using MediaGalleryExplorerCore.DataObjects;

namespace MediaGalleryExplorerCore.EventArguments
{
	public class MediaFileEventArgs : EventArgs
	{
		public MediaFileEventArgs(MediaFile file) : base()
		{
			File = file;
		}

		public MediaFile File { get; set; }
	}
}