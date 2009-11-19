using System.Collections.Generic;
using System.ServiceModel;
using UnpakkDaemon.EventArguments;
using UnpakkDaemon.Service.DataObjects;

namespace UnpakkDaemon.Service
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	internal class StatusService : IStatusService
	{
		private readonly List<IClientStatusChangedHandler> _subscribers;

		public StatusService()
		{
			_subscribers = new List<IClientStatusChangedHandler>();
		}

		#region Implementation of IStatusService

		public void Subscribe()
		{
			_subscribers.Add(OperationContext.Current.GetCallbackChannel<IClientStatusChangedHandler>());
		}

		public void Unsubscribe()
		{
			IClientStatusChangedHandler caller = OperationContext.Current.GetCallbackChannel<IClientStatusChangedHandler>();
			_subscribers.RemoveAll(subscriber => (subscriber == caller));
		}

		#endregion

		public void statusProvider_Progress(object sender, ProgressEventArgs e)
		{
			foreach (IClientStatusChangedHandler subscriber in _subscribers)
			{
				subscriber.Progress(new ProgressData(e.Message, e.Percent, e.Current, e.Max));
			}
		}

		public void statusProvider_SubProgress(object sender, ProgressEventArgs e)
		{
		}
	}
}
