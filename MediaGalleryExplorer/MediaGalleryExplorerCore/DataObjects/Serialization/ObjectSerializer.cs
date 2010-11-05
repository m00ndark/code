using System.Linq;

namespace MediaGallery.DataObjects.Serialization
{
	public class ObjectSerializer
	{
		public static string Serialize(object obj, params string[] values)
		{
			return (obj != null ? ObjectPrefix(obj) : string.Empty) + (values.Length > 0 ? values.Aggregate((a, b) => (a + "|" + b)) : string.Empty);
		}

		public static string[] Deserialize(string serialized, out string prefix)
		{
			int index = serialized.IndexOf(":");
			prefix = serialized.Substring(0, index);
			return serialized.Substring(index + 1).Split("|".ToCharArray());
		}

		private static string ObjectPrefix(object obj)
		{
			string prefix = "??";
			if (obj is GalleryVersion) prefix = "GV";
			if (obj is GallerySource) prefix = "GS";
			if (obj is MediaFolder) prefix = "FO";
			if (obj is ImageFile) prefix = "IF";
			if (obj is VideoFile) prefix = "VF";
			if (obj is MediaCodec) prefix = "MC";
			return prefix + ":";
		}
	}
}