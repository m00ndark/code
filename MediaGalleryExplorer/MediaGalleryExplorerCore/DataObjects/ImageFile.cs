using System.Linq;
using MediaGallery.DataObjects.Serialization;

namespace MediaGallery.DataObjects
{
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
