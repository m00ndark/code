namespace UnpakkDaemon
{
	public interface IEngine : IStatusProvider
	{
		bool EngineIsPaused();
		void ResumeEngine();
		void PauseEngine();
	}
}
