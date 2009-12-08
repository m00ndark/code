using UnpakkDaemon.Service.Client;

namespace UnpakkDaemonTray
{
	public static class ObjectPool
	{
		static ObjectPool()
		{
			StatusServiceHandler = null;
		}

		public static StatusServiceHandler StatusServiceHandler { get; set; }
	}
}
