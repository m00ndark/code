using System;
using System.Collections.Generic;
using MediaGalleryExplorerCore.DataObjects;

namespace MediaGalleryExplorerCore.EventArguments
{
	public class SourceListEventArgs : EventArgs
	{
		public SourceListEventArgs(List<GallerySource> sources) : base()
		{
			Sources = sources;
		}

		public List<GallerySource> Sources { get; private set; }
	}
}
