﻿using System.ComponentModel;

namespace MediaGalleryExplorerCore.DataObjects.Properties
{
	public class ImageFileProperties : MediaFileProperties
	{
		public ImageFileProperties(ImageFile imageFile) : base(imageFile)
		{
			ImageFile = imageFile;
		}

		#region Properties

		[Browsable(false)]
		public ImageFile ImageFile { get; private set; }

		#endregion
	}
}
