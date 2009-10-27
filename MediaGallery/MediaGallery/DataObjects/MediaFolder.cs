using System.Collections.Generic;
using System.Linq;
using MediaGallery.DataObjects.Serialization;

namespace MediaGallery.DataObjects
{
	public class MediaFolder : FileSystemEntry
	{
		private const int SERIALIZED_VALUES = 2;

		public MediaFolder(string name, string relativePath, MediaFolder parent, GallerySource source)
			: base(name, relativePath, parent, source)
		{
			Initialize();
		}

		public MediaFolder(GallerySource source) : base(source)
		{
			Initialize();
		}

		#region Properties

		public List<MediaFile> Files { get; private set; }
		public List<MediaFolder> SubFolders { get; private set; }
		public int InternalImageCount { get; set; }
		public int InternalVideoCount { get; set; }
		private int InternalTotalImageCount { get; set; }
		private int InternalTotalVideoCount { get; set; }

		public int ImageCount
		{
			get { return InternalImageCount; }
			set
			{
				InternalImageCount = value;
				InternalTotalImageCount += ImageCount;
			}
		}

		public int VideoCount
		{
			get { return InternalVideoCount; }
			set
			{
				InternalVideoCount = value;
				InternalTotalVideoCount += VideoCount;
			}
		}

		public int TotalImageCount
		{
			get { return InternalTotalImageCount; }
			set
			{
				int increment = value - InternalTotalImageCount;
				InternalTotalImageCount = value;
				if (Parent != null) Parent.TotalImageCount += increment;
			}
		}

		public int TotalVideoCount
		{
			get { return InternalTotalVideoCount; }
			set
			{
				int increment = value - InternalTotalVideoCount;
				InternalTotalVideoCount = value;
				if (Parent != null) Parent.TotalVideoCount += increment;
			}
		}

		#endregion

		#region Serialization

		public override string Serialize()
		{
			return Serialize(true);
		}

		public override string Serialize(bool withPrefix)
		{
			return ObjectSerializer.Serialize((withPrefix ? this : null), base.Serialize(false), ImageCount.ToString(), VideoCount.ToString());
		}

		public override string LoadFromDeserialized(string[] deserialized)
		{
			int baseItemCount = deserialized.Length - SERIALIZED_VALUES;
			ImageCount = int.Parse(deserialized[baseItemCount + 0]);
			VideoCount = int.Parse(deserialized[baseItemCount + 1]);
			return base.LoadFromDeserialized(deserialized.Take(baseItemCount).ToArray());
		}

		#endregion

		private void Initialize()
		{
			TotalImageCount = 0;
			TotalVideoCount = 0;
			ImageCount = 0;
			VideoCount = 0;
			Files = new List<MediaFile>();
			SubFolders = new List<MediaFolder>();
		}
	}
}
