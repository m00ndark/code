using System;
using System.Collections.Generic;
using System.Linq;
using MediaGalleryExplorerCore.DataObjects.Serialization;

namespace MediaGalleryExplorerCore.DataObjects
{
	public class VideoFile : MediaFile
	{
		private const int SERIALIZED_VALUES = 4;

		public VideoFile(string name, string relativePath, MediaFolder parent, GallerySource source)
			: base(name, relativePath, parent, source)
		{
			Initialize();
		}

		public VideoFile(GallerySource source) : base(source)
		{
			Initialize();
		}

		#region Properties

		public override string ThumbnailName { get; set; }
		public override string PreviewName { get; set; }
		public TimeSpan Duration { get; set; }
		public List<MediaCodec> Codecs { get; private set; }

		#endregion

		#region Serialization

		public override string Serialize()
		{
			return Serialize(true);
		}

		public override string Serialize(bool withPrefix)
		{
			return ObjectSerializer.Serialize((withPrefix ? this : null), base.Serialize(false),
				ObjectSerializer.Serialize(null, Codecs.Select(codec => codec.ID).ToArray()),
				Codecs.Count.ToString(), ThumbnailName, PreviewName, Duration.Ticks.ToString());
		}

		public override string LoadFromDeserialized(string[] deserialized)
		{
			int baseItemCount = deserialized.Length - SERIALIZED_VALUES;
			int codecCount = int.Parse(deserialized[baseItemCount + 0]);
			ThumbnailName = deserialized[baseItemCount + 1];
			PreviewName = deserialized[baseItemCount + 2];
			Duration = new TimeSpan(long.Parse(deserialized[baseItemCount + 3]));
			for (int i = (baseItemCount - codecCount); i < baseItemCount; i++)
			{
				string codecID = deserialized[i];
				MediaCodec mediaCodec = Source.Codecs.FirstOrDefault(codec => (codec.ID == codecID));
				if (mediaCodec == null) throw new Exception("Failed to deserialize file system entry of source database; referenced codec missing");
				Codecs.Add(mediaCodec);
			}
			return base.LoadFromDeserialized(deserialized.Take(baseItemCount - codecCount).ToArray());
		}

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
