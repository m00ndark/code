using System.IO;
using MediaGallery.DataAccess;
using MediaGallery.DataObjects.Serialization;

namespace MediaGallery.DataObjects
{
	public class FileSystemEntry : ISerializable
	{
		protected FileSystemEntry(string name, string relativePath, MediaFolder parent, GallerySource source)
		{
			Initialize(name, relativePath, parent, source);
		}

		protected FileSystemEntry(GallerySource source)
		{
			Initialize(string.Empty, string.Empty, null, source);
		}

		#region Properties

		public string ID { get; private set; }
		public string Name { get; private set; }
		public string RelativePath { get; private set; }
		public GallerySource Source { get; private set; }
		public MediaFolder Parent { get; private set; }

		public bool IsFolder
		{
			get { return (this is MediaFolder); }
		}

		public string RelativePathName
		{
			get { return (RelativePath != null ? Path.Combine(RelativePath, Name) : Name); }
		}

		#endregion

		#region Serialization

		public virtual string Serialize()
		{
			return Serialize(true);
		}

		public virtual string Serialize(bool withPrefix)
		{
			return ObjectSerializer.Serialize((withPrefix ? this : null), Name, RelativePath, (Parent != null ? Parent.ID : string.Empty));
		}

		public virtual string LoadFromDeserialized(string[] deserialized)
		{
			Name = deserialized[0];
			RelativePath = deserialized[1];
			CreateID();
			return deserialized[2];
		}

		#endregion

		private void Initialize(string name, string relativePath, MediaFolder parent, GallerySource source)
		{
			ID = null;
			Name = name;
			RelativePath = relativePath;
			SetParent(parent);
			Source = source;
			if (!string.IsNullOrEmpty(RelativePathName) && Source != null)
				CreateID();
		}

		private void CreateID()
		{
			ID = CryptoServiceHandler.GenerateHash(Source.ID + ":" + RelativePathName);
		}

		public void SetParent(MediaFolder parent)
		{
			Parent = parent;
			if (IsFolder)
			{
				if (Parent != null)
				{
					Parent.IncreaseTotalImageCount(((MediaFolder) this).TotalImageCount);
					Parent.IncreaseTotalVideoCount(((MediaFolder) this).TotalVideoCount);
				}
				else if (Source != null)
				{
					Source.ImageCount = ((MediaFolder) this).TotalImageCount;
					Source.VideoCount = ((MediaFolder) this).TotalVideoCount;
				}
			}
		}

		public bool Exists()
		{
			string fullPathName = Path.Combine(Source.Path, RelativePathName);
			return (IsFolder ? Directory.Exists(fullPathName) : File.Exists(fullPathName));
		}
	}
}
