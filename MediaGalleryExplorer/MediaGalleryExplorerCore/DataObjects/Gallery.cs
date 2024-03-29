﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using MediaGalleryExplorerCore.DataAccess;

namespace MediaGalleryExplorerCore.DataObjects
{
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/MediaGalleryExplorerCore.DataObjects", IsReference = true)]
	public class Gallery
	{
		public Gallery(string filePath)
		{
			ID = null;
			FilePath = filePath;
			Version = GalleryVersion.Instance;
			Sources = new List<GallerySource>();
			Codecs = new List<MediaCodec>();
			CreateID();
		}

		public Gallery(string filePath, string name, string password, int encryptionAlgorithm) : this(filePath)
		{
			Name = name;
			Password = password;
			EncryptionAlgorithm = encryptionAlgorithm;
		}

		#region Properties

		[DataMember] public string ID { get; private set; }
		[DataMember] public string Name { get; set; }
		[DataMember] public string FilePath { get; set; }
		[DataMember] public string Password { get; set; }
		[DataMember] public int EncryptionAlgorithm { get; set; }
		[DataMember] public GalleryVersion Version { get; private set; }
		[DataMember] public List<GallerySource> Sources { get; private set; }
		[DataMember] public List<MediaCodec> Codecs { get; private set; }

		#endregion

		private void CreateID()
		{
			ID = CryptoServiceHandler.GenerateHash(FilePath);
		}

		public bool AddSource(GallerySource source)
		{
			if (Sources.Contains(source))
				return false;

			Sources.Add(source);
			Sources.Sort();
			return true;
		}

		public void RemoveSource(GallerySource source)
		{
			Sources.Remove(source);
		}

		public void UpdateMediaCount()
		{
			Sources.ForEach(source => source.UpdateMediaCount());
		}
	}
}
