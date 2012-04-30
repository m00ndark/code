using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace MediaGalleryExplorerCore.DataObjects
{
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/MediaGalleryExplorerCore.DataObjects")]
	public class GalleryVersion
	{
		private static GalleryVersion _instance = null;
		private static readonly object _lock = new object();

		private GalleryVersion()
		{
			string version, buildDate;
			GetBuildInfo(out version, out buildDate);
			Version = version;
			BuildDate = buildDate;
		}

		#region Properties

		[DataMember] public string Version { get; private set; }
		[DataMember] public string BuildDate { get; private set; }

		public string MajorVersion
		{
			get
			{
				int firstDotIndex = Version.IndexOf(".");
				return Version.Substring(0, Version.IndexOf(".", firstDotIndex + 1));
			}
		}

		#endregion

		private static void GetBuildInfo(out string version, out string buildDate)
		{
			// Assumes that in AssemblyInfo.cs, the version is specified as 1.0.* or the like,
			// with only 2 numbers specified; the next two are generated from the date

			Version buildVersion = Assembly.GetExecutingAssembly().GetName().Version;
			DateTime buildDateTime = new DateTime(2000, 1, 1).AddDays(buildVersion.Build).AddSeconds(buildVersion.Revision * 2);
			version = buildVersion.ToString();
			buildDate = buildDateTime.ToString("yyyy-MM-dd");
		}

		public static GalleryVersion Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lock)
					{
						if (_instance == null)
						{
							_instance = new GalleryVersion();
						}
					}
				}
				return _instance;
			}
		}
	}
}
