using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace UnpakkDaemon
{
	public class EngineSettings
	{
		private const string APPLICATION_REGISTRY_PATH = @"Software\MoleCode\Unpakk Daemon";

		private const string DEFAULT_SLEEP_TIME = "00:00:10";
		private const string DEFAULT_ROOT_SCAN_PATH = "";

		public EngineSettings()
		{
			SleepTime = TimeSpan.Parse(DEFAULT_SLEEP_TIME);
			RootScanPath = DEFAULT_ROOT_SCAN_PATH;
		}

		#region Properties

		public TimeSpan SleepTime { get; set; }
		public string RootScanPath { get; set; }

		#endregion

		public void Load()
		{
			RegistryKey regKey = Registry.CurrentUser.OpenSubKey(APPLICATION_REGISTRY_PATH);
         if (regKey != null)
         {
				SleepTime = TimeSpan.Parse((string) regKey.GetValue("Sleep Time", DEFAULT_SLEEP_TIME));
				RootScanPath = (string) regKey.GetValue("Root Scan Path", DEFAULT_ROOT_SCAN_PATH);
         }
		}
	}
}
