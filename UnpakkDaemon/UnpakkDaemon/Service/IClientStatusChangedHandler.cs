using System.ServiceModel;
using UnpakkDaemon.Service.DataObjects;

namespace UnpakkDaemon.Service
{
	public interface IClientStatusChangedHandler
	{
		[OperationContract(IsOneWay = true)]
		void Progress(ProgressData progressData);
	}
}
