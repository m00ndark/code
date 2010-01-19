using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;

namespace UnpakkDaemon.Service.Client
{
	public class StatusServiceHandler
	{
		private readonly StatusChangedHandler _statusChangedHandler;
		private StatusServiceClient _statusServiceClient;
		private bool _shutdownInitiated;

		public StatusServiceHandler(StatusChangedHandler statusChangedHandler)
		{
			_statusChangedHandler = statusChangedHandler;
			SetupClient();
		}

		private void SetupClient()
		{
			Binding binding = new NetNamedPipeBinding();
			EndpointAddress endpointAddress = new EndpointAddress("net.pipe://localhost/UnpakkDaemonStatus");
			InstanceContext context = new InstanceContext(_statusChangedHandler);
			_statusServiceClient = new StatusServiceClient(context, binding, endpointAddress);
		}

		public void Start()
		{
			_shutdownInitiated = false;
			new Thread(ConnectionWatcher).Start();
		}

		public void Stop()
		{
			_shutdownInitiated = true;
			try { _statusServiceClient.Unsubscribe(); } catch {}
		}

		private void ConnectionWatcher()
		{
			while (!_shutdownInitiated)
			{
				if (_statusServiceClient.State == CommunicationState.Faulted)
					_statusServiceClient.Abort();

				if (_statusServiceClient.State == CommunicationState.Closed)
					SetupClient();

				if (_statusServiceClient.State == CommunicationState.Created)
					try { _statusServiceClient.Subscribe(); } catch {}

				Thread.Sleep(1000);
			}
		}

		public bool EngineIsPaused()
		{
			return (_shutdownInitiated || _statusServiceClient.State != CommunicationState.Opened ? true : _statusServiceClient.IsPaused());
		}

		public void ResumeEngine()
		{
			if (!_shutdownInitiated && _statusServiceClient.State == CommunicationState.Opened)
				_statusServiceClient.Resume();
		}

		public void PauseEngine()
		{
			if (!_shutdownInitiated && _statusServiceClient.State == CommunicationState.Opened)
				_statusServiceClient.Pause();
		}
	}
}
