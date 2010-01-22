using System;

namespace UnpakkDaemon.DataObjects
{
	public class RootPath : IComparable<RootPath>
	{
		public RootPath(string path, string userName, string password)
		{
			Path = path;
			UserName = userName;
			Password = password;
		}

		public string Path { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }

		public bool IsUNCPath
		{
			get { return Path.StartsWith(@"\\"); }
		}

		public string Domain
		{
			get
			{
				string[] domainUserSplit = UserName.Split('\\');
				return (domainUserSplit.Length > 1 ? domainUserSplit[0] : string.Empty);
			}
		}

		public string UserNameWithoutDomain
		{
			get
			{
				string[] domainUserSplit = UserName.Split('\\');
				return (domainUserSplit.Length > 1 ? domainUserSplit[1] : domainUserSplit[0]);
			}
		}

		public int CompareTo(RootPath other)
		{
			return other.Path.CompareTo(Path);
		}

		public override bool Equals(object obj)
		{
			RootPath rootPath = obj as RootPath;
			return (rootPath != null && rootPath.Path.Equals(Path, StringComparison.CurrentCultureIgnoreCase));
		}

		public override int GetHashCode()
		{
			return Path.GetHashCode();
		}
	}
}
