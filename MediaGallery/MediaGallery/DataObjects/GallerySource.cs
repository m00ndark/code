using System;
using System.Collections.Generic;
using MediaGallery.DataAccess;
using MediaGallery.DataObjects.Serialization;

namespace MediaGallery.DataObjects
{
	public class GallerySource : IComparable, ISerializable
	{
		public GallerySource(string path)
		{
			Initialize(path);
		}

		public GallerySource(string[] deserialized)
		{
			Initialize(deserialized[0]);
			ScanDateStr = deserialized[2];
		}

		#region Properties

		public string ID { get; private set; }
		public string Path { get; set; }
		public int ImageCount { get; set; }
		public int VideoCount { get; set; }
		public MediaFolder RootFolder { get; set; }
		public DateTime ScanDate { get; set; }
		public List<MediaCodec> Codecs { get; private set; }

		public string ScanDateStr
		{
			get { return (ScanDate == DateTime.MinValue ? string.Empty : ScanDate.ToString("yyyy-MM-dd HH:mm:ss")); }
			set { ScanDate = DateTime.Parse(value); }
		}

		#endregion

		#region Serialization

		public virtual string Serialize()
		{
			return Serialize(true);
		}

		public virtual string Serialize(bool withPrefix)
		{
			return ObjectSerializer.Serialize((withPrefix ? this : null), Path, RootFolder.ID, ScanDateStr, ImageCount.ToString(), VideoCount.ToString());
		}

		public string LoadFromDeserialized(string[] deserialized)
		{
			ScanDateStr = deserialized[2];
			return deserialized[1];
		}

		#endregion

		private void Initialize(string path)
		{
			ID = null;
			Path = path;
			ImageCount = 0;
			VideoCount = 0;
			RootFolder = null;
			ScanDate = DateTime.MinValue;
			Codecs = new List<MediaCodec>();
			CreateID();
		}

		private void CreateID()
		{
			ID = CryptoServiceHandler.GenerateHash(Path);
		}

		public override bool Equals(object obj)
		{
			GallerySource source = obj as GallerySource;
			return (source != null && source.Path.Equals(Path, StringComparison.CurrentCultureIgnoreCase));
		}

		public int CompareTo(object obj)
		{
			GallerySource source = obj as GallerySource;
			if (source == null) return -1;
			return Path.CompareTo(source.Path);
		}
	}
}
