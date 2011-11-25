using System;
using System.Linq;
using System.Runtime.Serialization;
using MediaGalleryExplorerCore.DataObjects.Serialization;

namespace MediaGalleryExplorerCore.DataObjects
{
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/MediaGalleryExplorerCore.DataObjects", IsReference = true)]
	public class ImageFile : MediaFile
	{
		private const int SERIALIZED_VALUES = 0;

		public ImageFile(string name, string relativePath, MediaFolder parent, GallerySource source)
			: base(name, relativePath, parent, source)
		{
			Initialize();
		}

		public ImageFile(GallerySource source) : base(source)
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

		#region Serialization

		public override string Serialize()
		{
			return Serialize(true);
		}

		public override string Serialize(bool withPrefix)
		{
			return ObjectSerializer.Serialize((withPrefix ? this : null), base.Serialize(false));
		}

		public override string LoadFromDeserialized(string[] deserialized)
		{
			int baseItemCount = deserialized.Length - SERIALIZED_VALUES;
			return base.LoadFromDeserialized(deserialized.Take(baseItemCount).ToArray());
		}

		#endregion

		private void Initialize() { }
	}
}
