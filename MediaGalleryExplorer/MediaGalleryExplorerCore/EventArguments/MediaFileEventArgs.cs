using System;
using MediaGallery.DataObjects;

namespace MediaGallery.EventArguments
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