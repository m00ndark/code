using System;
using System.ServiceModel;
using UnpakkDaemon.Service.Common;

namespace UnpakkDaemon.Service.Host
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
				//_serviceHost.Description.Behaviors.Add(new System.ServiceModel.Description.ServiceMetadataBehavior() { HttpGetEnabled = true, HttpGetUrl = new Uri("http://localhost/UnpakkDaemonStatus") });
				//_serviceHost.AddServiceEndpoint(typeof(System.ServiceModel.Description.IMetadataExchange), System.ServiceModel.Description.MetadataExchangeBindings.CreateMexHttpBinding(), "http://localhost/UnpakkDaemonStatus/MEX");
				_serviceHost.AddServiceEndpoint(typeof(IStatusService), new NetNamedPipeBinding(), string.Empty);
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