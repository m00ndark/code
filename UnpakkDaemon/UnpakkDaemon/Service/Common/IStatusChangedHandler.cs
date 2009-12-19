using System.ServiceModel;
using UnpakkDaemon.Service.DataObjects;

namespace UnpakkDaemon.Service.Common
{
	public interface IStatusChangedHandler
	{
		[OperationContract(IsOneWay = true)]
		void Progress(ProgressData progressData);

		[OperationContract(IsOneWay = true)]
		void SubProgress(ProgressData progressData);

		[OperationContract(IsOneWay = true)]
		void Record(RecordData recordData);

		[OperationContract(IsOneWay = true)]
		void SubRecord(SubRecordData subRecordData);

		[OperationContract(IsOneWay = true)]
		void Log(LogData logData);
	}
}