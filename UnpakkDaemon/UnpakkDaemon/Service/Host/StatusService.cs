using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using UnpakkDaemon.EventArguments;
using UnpakkDaemon.Service.Common;
using UnpakkDaemon.Service.DataObjects;

namespace UnpakkDaemon.Service.Host
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	internal class StatusService : IStatusService
	{
		private readonly List<IStatusChangedHandler> _subscribers;

		public StatusService()
		{
			_subscribers = new List<IStatusChangedHandler>();
		}

		#region Implementation of IStatusService

		public void Subscribe()
		{
			_subscribers.Add(OperationContext.Current.GetCallbackChannel<IStatusChangedHandler>());
		}

		public void Unsubscribe()
		{
			IStatusChangedHandler caller = OperationContext.Current.GetCallbackChannel<IStatusChangedHandler>();
			_subscribers.RemoveAll(subscriber => (subscriber == caller));
		}

		#endregion

		public void statusProvider_Progress(object sender, ProgressEventArgs e)
		{
			foreach (IStatusChangedHandler subscriber in _subscribers.ToList())
			{
				try
				{
					subscriber.Progress(new ProgressData(e.Message, e.Percent, e.Current, e.Max));
				}
				catch
				{
					_subscribers.Remove(subscriber);
				}
			}
		}

		public void statusProvider_SubProgress(object sender, ProgressEventArgs e)
		{
			foreach (IStatusChangedHandler subscriber in _subscribers.ToList())
			{
				try
				{
					subscriber.SubProgress(new ProgressData(e.Message, e.Percent, e.Current, e.Max));
				}
				catch
				{
					_subscribers.Remove(subscriber);
				}
			}
		}
	}
}
