using System.ServiceModel;
using System.ServiceModel.Channels;

namespace UnpakkDaemon.Service.Client
{
	public class StatusServiceHandler
	{
		private readonly StatusServiceClient _statusServiceClient;

		public StatusServiceHandler(StatusChangedHandler statusChangedHandler)
		{
			Binding binding = new NetNamedPipeBinding();
			EndpointAddress endpointAddress = new EndpointAddress("net.pipe://localhost/UnpakkDaemonStatus");
			InstanceContext context = new InstanceContext(statusChangedHandler);
			_statusServiceClient = new StatusServiceClient(context, binding, endpointAddress);
		}

		public void Subscribe()
		{
			_statusServiceClient.Subscribe();
		}

		public void Unsubscribe()
		{
			_statusServiceClient.Unsubscribe();
		}
	}
}
