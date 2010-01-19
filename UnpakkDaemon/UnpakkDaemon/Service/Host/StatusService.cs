using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using UnpakkDaemon.DataObjects;
using UnpakkDaemon.EventArguments;
using UnpakkDaemon.Service.Common;
using UnpakkDaemon.Service.DataObjects;

namespace UnpakkDaemon.Service.Host
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	internal class StatusService : IStatusService
	{
		private readonly List<IStatusChangedHandler> _subscribers;
		private readonly EngineIsPaused _engineIsPaused;
		private readonly ResumeEngine _resumeEngine;
		private readonly PauseEngine _pauseEngine;

		#region Delegates

		public delegate bool EngineIsPaused();
		public delegate void ResumeEngine();
		public delegate void PauseEngine();

		#endregion

		public StatusService(EngineIsPaused engineIsPaused, ResumeEngine resumeEngine, PauseEngine pauseEngine)
		{
			_subscribers = new List<IStatusChangedHandler>();
			_engineIsPaused = engineIsPaused;
			_resumeEngine = resumeEngine;
			_pauseEngine = pauseEngine;
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

		public bool IsPaused()
		{
			return _engineIsPaused();
		}

		public void Resume()
		{
			_resumeEngine();
		}

		public void Pause()
		{
			_pauseEngine();
		}

		#endregion

		public void StatusProvider_Progress(object sender, ProgressEventArgs e)
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

		public void StatusProvider_SubProgress(object sender, ProgressEventArgs e)
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

		public void StatusProvider_Record(object sender, RecordEventArgs e)
		{
			foreach (IStatusChangedHandler subscriber in _subscribers.ToList())
			{
				try
				{
					subscriber.Record(new RecordData(e.Record.ID, e.Record.Time, EnumConverter.ConvertEnumValue<RecordStatus, RecordStatusData>(e.Record.Status),
						e.Record.Folder, e.Record.SFVName, e.Record.RARName, e.Record.RARCount, e.Record.RARSize));
				}
				catch
				{
					_subscribers.Remove(subscriber);
				}
			}
		}

		public void StatusProvider_SubRecord(object sender, SubRecordEventArgs e)
		{
			foreach (IStatusChangedHandler subscriber in _subscribers.ToList())
			{
				try
				{
					subscriber.SubRecord(new SubRecordData(e.ParentID, e.SubRecord.Time,
						EnumConverter.ConvertEnumValue<RecordStatus, RecordStatusData>(e.SubRecord.Status), e.SubRecord.Folder, e.SubRecord.Name, e.SubRecord.Size));
				}
				catch
				{
					_subscribers.Remove(subscriber);
				}
			}
		}

		public void StatusProvider_Log(object sender, LogEntryEventArgs e)
		{
			foreach (IStatusChangedHandler subscriber in _subscribers.ToList())
			{
				try
				{
					subscriber.Log(new LogData(e.LogTime, (LogDataType) Enum.Parse(typeof(LogDataType), e.LogType.ToString()), e.LogText));
				}
				catch
				{
					_subscribers.Remove(subscriber);
				}
			}
		}
	}
}
