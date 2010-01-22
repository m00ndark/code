using System;
using System.Runtime.InteropServices;
using DWORD = System.UInt32;
using LPWSTR = System.String;
using NET_API_STATUS = System.UInt32;

namespace UnpakkDaemon.DataAccess
{
	public class UNC : IDisposable
	{
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct USE_INFO_2
		{
			internal LPWSTR ui2_local;
			internal LPWSTR ui2_remote;
			internal LPWSTR ui2_password;
			internal DWORD ui2_status;
			internal DWORD ui2_asg_type;
			internal DWORD ui2_refcount;
			internal DWORD ui2_usecount;
			internal LPWSTR ui2_username;
			internal LPWSTR ui2_domainname;
		}

		[DllImport("NetApi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		internal static extern NET_API_STATUS NetUseAdd(LPWSTR uncServerName, DWORD level, ref USE_INFO_2 buf, out DWORD paramError);

		[DllImport("NetApi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		internal static extern NET_API_STATUS NetUseDel(LPWSTR uncServerName, LPWSTR useName, DWORD forceCond);

		private readonly string _uncPath;
		private readonly string _domain;
		private readonly string _userName;
		private readonly string _password;
		private bool _disposed;

		public UNC(string uncPath, string domain, string userName, string password)
		{
			_disposed = false;
			_uncPath = uncPath;
			_domain = domain;
			_userName = userName;
			_password = password;
			LastError = 0;
		}

		~UNC()
		{
			Dispose();
		}

		public void Dispose()
		{
			if (!_disposed)
			{
				Close();
			}
			_disposed = true;
			GC.SuppressFinalize(this);
		}

		public int LastError { get; private set; }

		public bool Open()
		{
			try
			{
				USE_INFO_2 useInfo = new USE_INFO_2();
				useInfo.ui2_remote = _uncPath;
				useInfo.ui2_domainname = _domain;
				useInfo.ui2_username = _userName;
				useInfo.ui2_password = _password;
				useInfo.ui2_asg_type = 0;
				useInfo.ui2_usecount = 1;
				uint paramErrorIndex;
				uint returnCode = NetUseAdd(null, 2, ref useInfo, out paramErrorIndex);
				return ((LastError = (int) returnCode) == 0);
			}
			catch
			{
				LastError = Marshal.GetLastWin32Error();
				return false;
			}
		}

		public bool Close()
		{
			try
			{
				uint returnCode = NetUseDel(null, _uncPath, 2);
				return ((LastError = (int) returnCode) == 0);
			}
			catch
			{
				LastError = Marshal.GetLastWin32Error();
				return false;
			}
		}
	}
}
