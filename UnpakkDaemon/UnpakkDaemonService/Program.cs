using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using UnpakkDaemon;

namespace UnpakkDaemonService
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
			if (Environment.CommandLine.Contains("/debug"))
			{
				new Engine(Application.StartupPath).Start();
			}
			else
			{
				ServiceBase[] servicesToRun = new ServiceBase[] { new UnpakkDaemonService() };
				ServiceBase.Run(servicesToRun);
			}
		}
	}
}
