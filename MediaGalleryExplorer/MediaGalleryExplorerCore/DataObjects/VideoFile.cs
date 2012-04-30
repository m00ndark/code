using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MediaGalleryExplorerCore.DataObjects
{
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/MediaGalleryExplorerCore.DataObjects", IsReference = true)]
	public class VideoFile : MediaFile
	{
		public VideoFile(string name, string relativePath, MediaFolder parent, GallerySource source)
			: base(name, relativePath, parent, source)
		{
			Initialize();
		}

		#region Properties

		[DataMember] private string Thumbnail { get; set; }
		[DataMember] private string Preview { get; set; }
		[DataMember] public TimeSpan Duration { get; set; }
		[DataMember] public List<MediaCodec> Codecs { get; private set; }

		public override string ThumbnailName { get { return Thumbnail; } set { Thumbnail = value; } }
		public override string PreviewName { get { return Preview; } set { Preview = value; } }

		#endregion

		private void Initialize()
		{
			ThumbnailName = string.Empty;
			PreviewName = string.Empty;
			Duration = new TimeSpan(0, 0, 0);
			Codecs = new List<MediaCodec>();
		}
	}
}
