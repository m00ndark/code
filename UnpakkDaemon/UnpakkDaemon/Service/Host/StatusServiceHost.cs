using System;
using System.ServiceModel;
using UnpakkDaemon.Service.Common;

namespace UnpakkDaemon.Service.Host
{
	public static class StatusServiceHost
	{
		private static ServiceHost _serviceHost = null;
		private static StatusService _statusService = null;
		private static IEngine _engine;

		public static void Open(IEngine engine)
		{
			if (_serviceHost == null || _serviceHost.State == CommunicationState.Closed || _serviceHost.State == CommunicationState.Faulted)
			{
				_engine = engine;
				_statusService = new StatusService(_engine.EngineIsPaused, _engine.ResumeEngine, _engine.PauseEngine);
				_engine.Progress += _statusService.StatusProvider_Progress;
				_engine.SubProgress += _statusService.StatusProvider_SubProgress;
				_engine.Record += _statusService.StatusProvider_Record;
				_engine.SubRecord += _statusService.StatusProvider_SubRecord;
				_engine.Log += _statusService.StatusProvider_Log;
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
				_engine.Progress -= _statusService.StatusProvider_Progress;
				_engine.SubProgress -= _statusService.StatusProvider_SubProgress;
				_statusService = null;
			}
		}
	}
}