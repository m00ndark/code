namespace MediaGalleryExplorerCore.DataObjects
{
	public class MediaCount
	{
		public MediaCount() : this(0, 0) {}

		public MediaCount(int imageCount, int videoCount)
		{
			ImageCount = imageCount;
			VideoCount = videoCount;
		}

		#region Properties

		public int ImageCount { get; set; }
		public int VideoCount { get; set; }

		#endregion

		public static MediaCount operator +(MediaCount arg1, MediaCount arg2)
		{
			return new MediaCount(arg1.ImageCount + arg2.ImageCount, arg1.VideoCount + arg2.VideoCount);
		}
	}
}
