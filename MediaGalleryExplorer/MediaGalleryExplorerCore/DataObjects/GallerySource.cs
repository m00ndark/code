using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MediaGalleryExplorerCore.DataAccess;

namespace MediaGalleryExplorerCore.DataObjects
{
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/MediaGalleryExplorerCore.DataObjects", IsReference = true)]
	public class GallerySource : IComparable
	{
		public GallerySource(string path)
		{
			Initialize(path);
		}

		#region Properties

		[DataMember] public string ID { get; private set; }
		[DataMember] public string Path { get; set; }
		[DataMember] public string VolumeLetter { get; set; }
		[DataMember] public string VolumeName { get; set; }
		[DataMember] public string VolumeSerial { get; set; }
		[DataMember] public MediaFolder RootFolder { get; set; }
		[DataMember] public DateTime ScanDate { get; set; }
		[DataMember] public List<MediaCodec> Codecs { get; private set; }

		public MediaCount MediaCount { get; private set; }

		public string RootedPath
		{
			get { return VolumeLetter + Path; }
		}

		public string DisplayPath
		{
			get { return "[" + VolumeName + "]" + Path; }
		}

		public string ScanDateStr
		{
			get { return (ScanDate == DateTime.MinValue ? string.Empty : ScanDate.ToString("yyyy-MM-dd HH:mm:ss")); }
			set { ScanDate = DateTime.Parse(value); }
		}

		#endregion

		private void Initialize(string path)
		{
			ID = null;
			string volumeLetter, volumeName, volumeSerial;
			FileSystemHandler.GetVolumeInfo(path, out volumeLetter, out volumeName, out volumeSerial);
			VolumeLetter = volumeLetter;
			VolumeName = volumeName;
			VolumeSerial = volumeSerial;
			Path = path.Substring(2);
			CreateID();
			RootFolder = new MediaFolder(Path, null, null, this);
			MediaCount = new MediaCount();
			ScanDate = DateTime.MinValue;
			Codecs = new List<MediaCodec>();
		}

		private void CreateID()
		{
			ID = CryptoServiceHandler.GenerateHash(GetIDBase());
		}

		private string GetIDBase()
		{
			return VolumeSerial + ":" + Path;
		}

		public void UpdateMediaCount()
		{
			MediaCount = RootFolder.UpdateMediaCount();
		}

		public void UpdateVolumeLetter()
		{
			try
			{
				VolumeLetter = FileSystemHandler.GetVolumeLetter(VolumeSerial);
			}
			catch (Exception) { ; }
		}

		#region Equality

		public override bool Equals(object obj)
		{
			GallerySource source = obj as GallerySource;
			return (source != null && source.GetIDBase().Equals(GetIDBase(), StringComparison.CurrentCultureIgnoreCase));
		}

		public override int GetHashCode()
		{
			return GetIDBase().ToLower().GetHashCode();
		}

		#endregion

		#region Comparable

		public int CompareTo(object obj)
		{
			GallerySource source = obj as GallerySource;
			if (source == null) return -1;
			return GetIDBase().CompareTo(source.GetIDBase());
		}

		#endregion
	}
}
