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
		public int ImageCount { get; private set; }
		public int VideoCount { get; private set; }
		public int TotalImageCount { get; private set; }
		public int TotalVideoCount { get; private set; }

		#endregion

		#region Image/video count incrementation

		public void IncreaseImageCount()
		{
			IncreaseImageCount(1);
		}

		public void IncreaseImageCount(int increment)
		{
			ImageCount += increment;
			IncreaseTotalImageCount(increment);
		}

		public void IncreaseVideoCount()
		{
			IncreaseVideoCount(1);
		}

		public void IncreaseVideoCount(int increment)
		{
			VideoCount += increment;
			IncreaseTotalVideoCount(increment);
		}

		public void IncreaseTotalImageCount()
		{
			IncreaseTotalImageCount(1);
		}

		public void IncreaseTotalImageCount(int increment)
		{
			TotalImageCount += increment;
			if (Parent != null)
				Parent.IncreaseTotalImageCount(increment);
			else if (Source != null)
				Source.ImageCount = TotalImageCount;
		}

		public void IncreaseTotalVideoCount()
		{
			IncreaseTotalVideoCount(1);
		}

		public void IncreaseTotalVideoCount(int increment)
		{
			TotalVideoCount += increment;
			if (Parent != null)
				Parent.IncreaseTotalVideoCount(increment);
			else if (Source != null)
				Source.VideoCount = TotalVideoCount;
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
			IncreaseImageCount(int.Parse(deserialized[baseItemCount + 0]));
			IncreaseVideoCount(int.Parse(deserialized[baseItemCount + 1]));
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
