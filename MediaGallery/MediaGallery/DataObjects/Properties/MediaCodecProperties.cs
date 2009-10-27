using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MediaGallery.DataObjects.Properties
{
	[TypeConverter(typeof(MediaCodecPropertiesConverter))]
	public class MediaCodecProperties
	{
		public MediaCodecProperties(List<MediaCodec> codecs)
		{
			Codecs = codecs;
		}

		#region Properties

		[Browsable(false)]
		public List<MediaCodec> Codecs { get; private set; }

		#region Browsable

		[ReadOnly(true)]
		[DisplayName("Audio")]
		public string AudioCodecProperty
		{
			get
			{
				MediaCodec codec = Codecs.FirstOrDefault(mediaCodec => mediaCodec.Type == MediaCodec.CodecType.Audio) ??
					Codecs.FirstOrDefault(mediaCodec => mediaCodec.Type == MediaCodec.CodecType.AudioVideo);
				return (codec != null ? codec.Name : string.Empty);
			}
		}

		[ReadOnly(true)]
		[DisplayName("Video")]
		public string VideoCodecProperty
		{
			get
			{
				MediaCodec codec = Codecs.FirstOrDefault(mediaCodec => mediaCodec.Type == MediaCodec.CodecType.Video) ??
					Codecs.FirstOrDefault(mediaCodec => mediaCodec.Type == MediaCodec.CodecType.AudioVideo);
				return (codec != null ? codec.Name : string.Empty);
			}
		}

		#endregion

		#endregion
	}
}