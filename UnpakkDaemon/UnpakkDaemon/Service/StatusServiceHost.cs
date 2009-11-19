using System;
using System.ServiceModel;

namespace UnpakkDaemon.Service
{
	public static class StatusServiceHost
	{
		private static ServiceHost _serviceHost = null;
		private static StatusService _statusService = null;
		private static IStatusProvider _statusProvider;

		public static void Open(IStatusProvider statusProvider)
		{
			if (_serviceHost == null || _serviceHost.State == CommunicationState.Closed || _serviceHost.State == CommunicationState.Faulted)
			{
				_statusProvider = statusProvider;
				_statusService = new StatusService();
				_statusProvider.Progress += _statusService.statusProvider_Progress;
				_statusProvider.SubProgress += _statusService.statusProvider_SubProgress;
				_serviceHost = new ServiceHost(_statusService, new Uri("net.pipe://localhost/UnpakkDaemonStatus"));
				_serviceHost.AddServiceEndpoint(typeof(IStatusService), new NetNamedPipeBinding(), new Uri("net.pipe://localhost/UnpakkDaemonStatus"));
				_serviceHost.Open();
			}
		}

		public static void Close()
		{
			if (_serviceHost != null)
			{
				_serviceHost.Close();
				_serviceHost = null;
				_statusProvider.Progress -= _statusService.statusProvider_Progress;
				_statusProvider.SubProgress -= _statusService.statusProvider_SubProgress;
				_statusService = null;
			}
		}
	}
}
