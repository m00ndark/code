using System.Drawing;
using System.IO;
using System.Runtime.Serialization;

namespace MediaGalleryExplorerCore.DataObjects
{
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/MediaGalleryExplorerCore.DataObjects", IsReference = true)]
	public abstract class MediaFile : FileSystemEntry
	{
		public const string IMAGE_FILE_EXTENSIONS = ".jpg;.gif;.bmp;.png";
		public const string VIDEO_FILE_EXTENSIONS = ".avi;.wmv;.mpg;.mpeg;.mkv;.mp4;.asf;.rm;.rmvb;.m1v;.m2v;.m3v;.m4v;.mov;.qt;.flv";

		protected MediaFile(string name, string relativePath, MediaFolder parent, GallerySource source)
			: base(name, relativePath, parent, source)
		{
			Initialize();
		}

		#region Properties

		[DataMember] public long FileSize { get; set; }
		[DataMember] public Size Size { get; set; }
		public Image ThumbnailImage { get; set; }

		public abstract string ThumbnailName { get; set; }
		public abstract string PreviewName { get; set; }

		public string RelativeThumbnailPathName
		{
			get { return (RelativePath != null ? Path.Combine(RelativePath, ThumbnailName) : ThumbnailName); }
		}

		public string RelativePreviewPathName
		{
			get { return (RelativePath != null ? Path.Combine(RelativePath, PreviewName) : PreviewName); }
		}

		#endregion

		private void Initialize()
		{
			FileSize = 0;
			Size = new Size(0, 0);
			ThumbnailImage = null;
		}
	}
}
