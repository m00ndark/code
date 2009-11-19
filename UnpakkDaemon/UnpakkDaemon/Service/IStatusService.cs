using System.ServiceModel;

namespace UnpakkDaemon.Service
{
	[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IClientStatusChangedHandler))]
	public interface IStatusService
	{
		[OperationContract(IsInitiating = true)]
		void Subscribe();

		[OperationContract(IsTerminating = true)]
		void Unsubscribe();
	}
}
