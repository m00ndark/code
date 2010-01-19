using System.ServiceModel;

namespace UnpakkDaemon.Service.Common
{
	[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IStatusChangedHandler))]
	public interface IStatusService
	{
		[OperationContract(IsInitiating = true)]
		void Subscribe();

		[OperationContract(IsTerminating = true)]
		void Unsubscribe();

		[OperationContract]
		bool IsPaused();

		[OperationContract]
		void Resume();

		[OperationContract]
		void Pause();
	}
}