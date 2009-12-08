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
	}
}