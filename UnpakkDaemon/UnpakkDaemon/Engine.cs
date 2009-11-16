using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace UnpakkDaemon
{
	public class Engine
	{
		private bool _shutDown;

		public Engine()
		{
			_shutDown = false;
			IsRunning = false;
		}

		public bool IsRunning { get; private set; }

		public void Start()
		{
			_shutDown = false;
			IsRunning = true;
			EngineSettings settings = new EngineSettings();
			while (!_shutDown)
			{
				settings.Load();


				Thread.Sleep(settings.SleepTime);
			}
			IsRunning = false;
		}

		public void ShutDown()
		{
			_shutDown = true;
		}
	}
}
