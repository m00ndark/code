using System.IO;
using System.Runtime.Serialization;
using MediaGalleryExplorerCore.DataAccess;
using MediaGalleryExplorerCore.DataObjects.Serialization;
using ISerializable = MediaGalleryExplorerCore.DataObjects.Serialization.ISerializable;

namespace MediaGalleryExplorerCore.DataObjects
{
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/MediaGalleryExplorerCore.DataObjects", IsReference = true)]
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

		[DataMember] public string ID { get; private set; }
		[DataMember] public string Name { get; private set; }
		[DataMember] public string RelativePath { get; private set; }
		[DataMember] public GallerySource Source { get; private set; }
		[DataMember] public MediaFolder Parent { get; protected set; }

		public bool IsFolder
		{
			get { return (this is MediaFolder); }
		}

		public string RelativePathName
		{
			get { return (RelativePath != null ? Path.Combine(RelativePath, Name) : string.Empty); }
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
			Parent = parent;
			Source = source;
			if (!(string.IsNullOrEmpty(RelativePathName) && string.IsNullOrEmpty(Name)) && Source != null)
				CreateID();
		}

		private void CreateID()
		{
			ID = CryptoServiceHandler.GenerateHash(Source.ID + ":" + (string.IsNullOrEmpty(RelativePathName) ? Name : RelativePathName));
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
			}
		}

		public bool Exists()
		{
			string fullPathName = Path.Combine(Source.RootedPath, RelativePathName);
			return (IsFolder ? Directory.Exists(fullPathName) : File.Exists(fullPathName));
		}
	}
}
