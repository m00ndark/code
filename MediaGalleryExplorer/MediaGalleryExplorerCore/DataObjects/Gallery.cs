﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using MediaGalleryExplorerCore.DataAccess;
using MediaGalleryExplorerCore.DataObjects.Serialization;

namespace MediaGalleryExplorerCore.DataObjects
{
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/MediaGalleryExplorerCore.DataObjects")]
	public class Gallery
	{
		public Gallery(string filePath)
		{
			ID = null;
			FilePath = filePath;
			Version = GalleryVersion.Instance;
			Sources = new List<GallerySource>();
			CreateID();
		}

		public Gallery(string filePath, string name, string password, int encryptionAlgorithm)
		{
			ID = null;
			Name = name;
			FilePath = filePath;
			Password = password;
			EncryptionAlgorithm = encryptionAlgorithm;
			Version = GalleryVersion.Instance;
			Sources = new List<GallerySource>();
			CreateID();
		}

		#region Properties

		[DataMember] public string ID { get; private set; }
		[DataMember] public string Name { get; set; }
		[DataMember] public string FilePath { get; set; }
		[DataMember] public string Password { get; set; }
		[DataMember] public int EncryptionAlgorithm { get; set; }
		[DataMember] public GalleryVersion Version { get; private set; }
		[DataMember] public List<GallerySource> Sources { get; private set; }

		#endregion

		#region Serialization

		//public virtual string Serialize()
		//{
		//   return Serialize(true);
		//}

		//public virtual string Serialize(bool withPrefix)
		//{
		//   List<string> values = new List<string>() { Name, EncryptionAlgorithm.ToString() };
		//   values.AddRange(Sources.Select(source => source.ID));
		//   return ObjectSerializer.Serialize((withPrefix ? this : null), values.ToArray());
		//}

		//public string LoadFromDeserialized(string[] deserialized)
		//{
		//   ScanDateStr = deserialized[2];
		//   return deserialized[1];
		//}

		#endregion

		private void CreateID()
		{
			ID = CryptoServiceHandler.GenerateHash(FilePath);
		}

		public void AddSource(GallerySource source)
		{
			if (!Sources.Contains(source))
			{
				Sources.Add(source);
				Sources.Sort();
			}
		}

		public void RemoveSource(GallerySource source)
		{
			Sources.Remove(source);
		}
	}
}
