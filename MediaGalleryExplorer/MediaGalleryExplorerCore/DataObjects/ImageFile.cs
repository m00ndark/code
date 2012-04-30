using System;
using System.Runtime.Serialization;

namespace MediaGalleryExplorerCore.DataObjects
{
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/MediaGalleryExplorerCore.DataObjects", IsReference = true)]
	public class ImageFile : MediaFile
	{
		public ImageFile(string name, string relativePath, MediaFolder parent, GallerySource source)
			: base(name, relativePath, parent, source)
		{
			Initialize();
		}

		#region Properties

		[DataMember] private string Thumbnail { get; set; }

		public override string ThumbnailName
		{
			get { return Thumbnail; }
			set { Thumbnail = value; }
		}

		public override string PreviewName
		{
			get { return Name; }
			set { throw new Exception("Set accessor of MediaFile.PreviewName property should not be used."); }
		}

		#endregion

		private void Initialize() { }
	}
}
