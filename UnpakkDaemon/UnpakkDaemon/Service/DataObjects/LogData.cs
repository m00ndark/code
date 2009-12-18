using System;
using System.Runtime.Serialization;

namespace UnpakkDaemon.Service.DataObjects
{
	public enum LogDataType
	{
		Debug,
		Flow,
		Warning,
		Error
	}

	[DataContract]
	public class LogData
	{
		public LogData(DateTime logTime, LogDataType logType, string logText)
		{
			LogTime = logTime;
			LogType = logType;
			LogText = logText;
		}

		[DataMember]
		public DateTime LogTime { get; private set; }

		[DataMember]
		public LogDataType LogType { get; private set; }

		[DataMember]
		public string LogText { get; private set; }
	}
}
