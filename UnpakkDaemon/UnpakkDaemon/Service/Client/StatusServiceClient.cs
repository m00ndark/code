using System.ServiceModel;
using System.ServiceModel.Channels;
using UnpakkDaemon.Service.Common;

namespace UnpakkDaemon.Service.Client
{
	public class StatusServiceClient : DuplexClientBase<IStatusService>, IStatusService
	{
		public StatusServiceClient(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
			: base(callbackInstance, binding, remoteAddress) { }

		#region Implementation of IStatusService

		public void Subscribe()
		{
			Channel.Subscribe();
		}

		public void Unsubscribe()
		{
			Channel.Unsubscribe();
		}

		#endregion
	}
}
