using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MediaGalleryExplorerCore.DataObjects
{
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/MediaGalleryExplorerCore.DataObjects", IsReference = true)]
	public class MediaFolder : FileSystemEntry
	{
		private int _imageCount = 0;
		private int _videoCount = 0;

		public MediaFolder(string name, string relativePath, MediaFolder parent, GallerySource source)
			: base(name, relativePath, parent, source)
		{
			Initialize(false);
		}

		public MediaFolder(MediaFolder parent) : base()
		{
			Parent = parent;
			Initialize(true);
		}

		#region Properties

		public bool IsDummy { get; private set; }
		[DataMember] public List<MediaFile> Files { get; private set; }
		[DataMember] public List<MediaFolder> SubFolders { get; private set; }

		[DataMember]
		public int ImageCount
		{
			get { return _imageCount; }
			private set
			{
				int increment = (value - _imageCount);
				_imageCount = value;
				IncreaseTotalImageCount(increment, true);
			}
		}

		[DataMember]
		public int VideoCount
		{
			get { return _videoCount; }
			private set
			{
				int increment = (value - _videoCount);
				_videoCount = value;
				IncreaseTotalVideoCount(increment, true);
			}
		}

		public int TotalImageCount { get; private set; }
		public int TotalVideoCount { get; private set; }

		#endregion

		#region Image/video count incrementation

		public void ResetCounters()
		{
			ImageCount = 0;
			TotalImageCount = 0;
			VideoCount = 0;
			TotalVideoCount = 0;
		}

		public void IncreaseImageCount()
		{
			IncreaseImageCount(1);
		}

		public void IncreaseImageCount(int increment)
		{
			ImageCount += increment;
		}

		public void IncreaseVideoCount()
		{
			IncreaseVideoCount(1);
		}

		public void IncreaseVideoCount(int increment)
		{
			VideoCount += increment;
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

		private void Initialize(bool isDummy)
		{
			IsDummy = isDummy;
			TotalImageCount = 0;
			TotalVideoCount = 0;
			ImageCount = 0;
			VideoCount = 0;
			Files = new List<MediaFile>();
			SubFolders = new List<MediaFolder>();
		}
	}
}
