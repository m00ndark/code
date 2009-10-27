using System;
using MediaGallery.DataObjects;

namespace MediaGallery.EventArguments
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
