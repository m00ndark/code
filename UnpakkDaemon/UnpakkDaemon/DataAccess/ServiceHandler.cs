using System.Diagnostics;
using System.ServiceProcess;

namespace UnpakkDaemon.DataAccess
{
	public static class ServiceHandler
	{
		public const string APPLICATION_SERVICE_NAME = "UnpakkDaemonService";
		public const string APPLICATION_PROCESS_NAME = "UnpakkDaemonService";

		public static bool ServiceIsRunning()
		{
			try
			{
				ServiceController service = new ServiceController(APPLICATION_SERVICE_NAME);
				return (service.Status == ServiceControllerStatus.Running);
			}
			catch
			{
				return false;
			}
		}

		public static bool ServiceIsStopped()
		{
			try
			{
				ServiceController service = new ServiceController(APPLICATION_SERVICE_NAME);
				return (service.Status == ServiceControllerStatus.Stopped && Process.GetProcessesByName(APPLICATION_PROCESS_NAME).Length == 0);
			}
			catch
			{
				return true;
			}
		}

		public static bool StartService()
		{
			try
			{
				ServiceController service = new ServiceController(APPLICATION_SERVICE_NAME);
				service.Start();
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}