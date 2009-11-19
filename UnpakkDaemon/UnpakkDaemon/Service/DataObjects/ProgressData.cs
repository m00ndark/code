using System.Runtime.Serialization;

namespace UnpakkDaemon.Service.DataObjects
{
	[DataContract]
	public class ProgressData
	{
		public ProgressData(string message, double percent, long current, long max)
		{
			Message = message;
			Percent = percent;
			Current = current;
			Max = max;
		}

		[DataMember]
		public string Message { get; private set; }

		[DataMember]
		public double Percent { get; private set; }

		[DataMember]
		public long Current { get; private set; }

		[DataMember]
		public long Max { get; private set; }
	}
}
