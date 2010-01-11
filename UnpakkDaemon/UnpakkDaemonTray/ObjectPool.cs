using UnpakkDaemon;
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

		public static int LogFilterDaysBack { get; set; }

		public static LogType LogFilterLeastLogType { get; set; }
	}
}
