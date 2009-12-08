using System;
using UnpakkDaemon.EventArguments;
using UnpakkDaemon.Service.Common;
using UnpakkDaemon.Service.DataObjects;

namespace UnpakkDaemon.Service.Client
{
	public class StatusChangedHandler : IStatusChangedHandler
	{
		public event EventHandler<ProgressEventArgs> ProgressChanged;
		public event EventHandler<ProgressEventArgs> SubProgressChanged;

		#region Implementation of IStatusChangedHandler

		public void Progress(ProgressData progressData)
		{
			RaiseProgressChangedEvent(progressData);
		}

		public void SubProgress(ProgressData progressData)
		{
			RaiseSubProgressChangedEvent(progressData);
		}

		#endregion

		#region Event raisers

		private void RaiseProgressChangedEvent(ProgressData progressData)
		{
			if (ProgressChanged != null)
				ProgressChanged(this, new ProgressEventArgs(progressData.Message, progressData.Percent, progressData.Current, progressData.Max));
		}

		private void RaiseSubProgressChangedEvent(ProgressData progressData)
		{
			if (SubProgressChanged != null)
				SubProgressChanged(this, new ProgressEventArgs(progressData.Message, progressData.Percent, progressData.Current, progressData.Max));
		}

		#endregion
	}
}