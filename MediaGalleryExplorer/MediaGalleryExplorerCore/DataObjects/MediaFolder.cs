using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MediaGalleryExplorerCore.DataObjects
{
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/MediaGalleryExplorerCore.DataObjects", IsReference = true)]
	public class MediaFolder : FileSystemEntry
	{
		public MediaFolder(string name, string relativePath, MediaFolder parent, GallerySource source)
			: base(name, relativePath, parent, source)
		{
			Initialize(false);
		}

		public MediaFolder(MediaFolder parent) : base()
		{
			Parent = parent;
			Initialize(true);
		}

		#region Properties

		public bool IsDummy { get; private set; }
		[DataMember] public List<MediaFile> Files { get; private set; }
		[DataMember] public List<MediaFolder> SubFolders { get; private set; }

		public MediaCount MediaCount { get; private set; }
		public MediaCount TotalMediaCount { get; private set; }

		#endregion

		private void Initialize(bool isDummy)
		{
			IsDummy = isDummy;
			MediaCount = new MediaCount();
			TotalMediaCount = new MediaCount();
			Files = new List<MediaFile>();
			SubFolders = new List<MediaFolder>();
		}

		public MediaCount UpdateMediaCount()
		{
			TotalMediaCount = MediaCount = new MediaCount(Files.Count(file => file is ImageFile), Files.Count(file => file is VideoFile));
			SubFolders.ForEach(folder => TotalMediaCount += folder.UpdateMediaCount());
			return TotalMediaCount;
		}
	}
}
