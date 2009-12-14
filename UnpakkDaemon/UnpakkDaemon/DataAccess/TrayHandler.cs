using System.Diagnostics;
using System.IO;

namespace UnpakkDaemon.DataAccess
{
	public static class TrayHandler
	{
		private const string TRAY_APPLICATION_FILE_NAME = "UnpakkDaemonTray.exe";

		public static void LaunchTray(string path)
		{
			string trayPathName = Path.Combine(path, TRAY_APPLICATION_FILE_NAME);

			if (!File.Exists(trayPathName))
				throw new FileNotFoundException("Could not find Unpakk Daemon Tray application @ " + trayPathName);

			Process.Start(trayPathName);
		}
	}
}