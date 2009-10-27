using System;
using System.Drawing;
using System.IO;
using System.Linq;
using MediaGallery.DataObjects.Serialization;

namespace MediaGallery.DataObjects
{
	public abstract class MediaFile : FileSystemEntry
	{
		private const int SERIALIZED_VALUES = 3;
		public const string IMAGE_FILE_EXTENSIONS = ".jpg;.gif;.bmp;.png";
		public const string VIDEO_FILE_EXTENSIONS = ".avi;.wmv;.mpg;.mpeg;.mkv;.mp4;.asf;.rm;.rmvb;.m1v;.m2v;.m3v;.m4v;.mov;.qt;.flv";

		protected MediaFile(string name, string relativePath, MediaFolder parent, GallerySource source)
			: base(name, relativePath, parent, source)
		{
			Initialize();
		}

		protected MediaFile(GallerySource source) : base(source)
		{
			Initialize();
		}

		#region Properties

		public long FileSize { get; set; }
		public Size Size { get; set; }
		public Image ThumbnailImage { get; set; }

		public virtual string ThumbnailName
		{
			get { return Name; }
			set { throw new Exception("Set accessor of MediaFile.ThumbnailName property should not be used."); }
		}

		public virtual string PreviewName
		{
			get { return Name; }
			set { throw new Exception("Set accessor of MediaFile.PreviewName property should not be used."); }
		}

		public string RelativeThumbnailPathName
		{
			get { return (RelativePath != null ? Path.Combine(RelativePath, ThumbnailName) : ThumbnailName); }
		}

		public string RelativePreviewPathName
		{
			get { return (RelativePath != null ? Path.Combine(RelativePath, PreviewName) : PreviewName); }
		}

		#endregion

		#region Serialization

		public override string Serialize()
		{
			return Serialize(true);
		}

		public override string Serialize(bool withPrefix)
		{
			return ObjectSerializer.Serialize((withPrefix ? this : null), base.Serialize(false), FileSize.ToString(), Size.Width.ToString(), Size.Height.ToString());
		}

		public override string LoadFromDeserialized(string[] deserialized)
		{
			int baseItemCount = deserialized.Length - SERIALIZED_VALUES;
			FileSize = int.Parse(deserialized[baseItemCount + 0]);
			Size = new Size(int.Parse(deserialized[baseItemCount + 1]), int.Parse(deserialized[baseItemCount + 2]));
			return base.LoadFromDeserialized(deserialized.Take(baseItemCount).ToArray());
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
