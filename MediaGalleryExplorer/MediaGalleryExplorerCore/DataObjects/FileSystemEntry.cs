using System.IO;
using System.Runtime.Serialization;
using MediaGalleryExplorerCore.DataAccess;

namespace MediaGalleryExplorerCore.DataObjects
{
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/MediaGalleryExplorerCore.DataObjects", IsReference = true)]
	public class FileSystemEntry
	{
		protected FileSystemEntry(string name, string relativePath, MediaFolder parent, GallerySource source)
		{
			ID = null;
			Name = name;
			RelativePath = relativePath;
			Parent = parent;
			Source = source;
			if (!(string.IsNullOrEmpty(RelativePathName) && string.IsNullOrEmpty(Name)) && Source != null)
				CreateID();
		}

		protected FileSystemEntry() : this(string.Empty, string.Empty, null, null) {}

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

		public string DatabasePath
		{
			get { return Path.Combine((Parent != null ? Parent.DatabasePath : string.Empty), (IsFolder ? ID : string.Empty)); }
		}

		#endregion

		private void CreateID()
		{
			ID = CryptoServiceHandler.GenerateHash(Source.ID + ":" + (string.IsNullOrEmpty(RelativePathName) ? Name : RelativePathName));
		}

		public bool Exists()
		{
			Source.UpdateVolumeLetter();
			string fullPathName = Path.Combine(Source.RootedPath, RelativePathName);
			return (IsFolder ? Directory.Exists(fullPathName) : File.Exists(fullPathName));
		}
	}
}
