using System.ComponentModel;

namespace MediaGallery.DataObjects.Properties
{
	public class VideoFileProperties : MediaFileProperties
	{
		public VideoFileProperties(VideoFile videoFile) : base(videoFile)
		{
			VideoFile = videoFile;
		}

		#region Properties

		[Browsable(false)]
		public VideoFile VideoFile { get; private set; }

		#region Browsable

		[ReadOnly(true)]
		[Category("Media")]
		[DisplayName("Duration")]
		public string Duration { get { return VideoFile.Duration.Hours.ToString("00") + ":" + VideoFile.Duration.Minutes.ToString("00") + ":" + VideoFile.Duration.Seconds.ToString("00"); } }

		[ReadOnly(true)]
		[Category("Media")]
		[DisplayName("Codecs")]
		public MediaCodecProperties MediaCodec { get { return new MediaCodecProperties(VideoFile.Codecs); } }

		#endregion

		#endregion
	}
}
