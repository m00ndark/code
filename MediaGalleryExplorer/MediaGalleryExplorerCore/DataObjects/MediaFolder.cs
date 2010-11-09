using System.Collections.Generic;
using System.Linq;
using MediaGalleryExplorerCore.DataObjects.Serialization;

namespace MediaGalleryExplorerCore.DataObjects
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
			IncreaseImageCount(increment, true);
		}

		private void IncreaseImageCount(int increment, bool updateSource)
		{
			ImageCount += increment;
			IncreaseTotalImageCount(increment, updateSource);
		}

		public void IncreaseVideoCount()
		{
			IncreaseVideoCount(1);
		}

		public void IncreaseVideoCount(int increment)
		{
			IncreaseVideoCount(increment, true);
		}

		private void IncreaseVideoCount(int increment, bool updateSource)
		{
			VideoCount += increment;
			IncreaseTotalVideoCount(increment, updateSource);
		}

		public void IncreaseTotalImageCount(int increment)
		{
			IncreaseTotalImageCount(increment, false);
		}

		private void IncreaseTotalImageCount(int increment, bool updateSource)
		{
			TotalImageCount += increment;
			if (Parent != null)
				Parent.IncreaseTotalImageCount(increment, updateSource);
			else if (updateSource && Source != null)
				Source.ImageCount = TotalImageCount;
		}

		public void IncreaseTotalVideoCount(int increment)
		{
			IncreaseTotalVideoCount(increment, false);
		}

		private void IncreaseTotalVideoCount(int increment, bool updateSource)
		{
			TotalVideoCount += increment;
			if (Parent != null)
				Parent.IncreaseTotalVideoCount(increment, updateSource);
			else if (updateSource && Source != null)
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
			IncreaseImageCount(int.Parse(deserialized[baseItemCount + 0]), false);
			IncreaseVideoCount(int.Parse(deserialized[baseItemCount + 1]), false);
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
