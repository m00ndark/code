using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace UnpakkDaemon
{
	public class Engine
	{
		private bool _shutDown;
		private readonly string _startupPath;

		public Engine(string startupPath)
		{
			_startupPath = startupPath;
			_shutDown = false;
			IsRunning = false;
		}

		public bool IsRunning { get; private set; }

		public void Start()
		{
			_shutDown = false;
			IsRunning = true;

			TrayHandler.LaunchTray(_startupPath);
			EnterMainLoop(new EngineSettings());

			IsRunning = false;
		}

		private void EnterMainLoop(EngineSettings settings)
		{
			while (!_shutDown)
			{
				settings.Load();

				//string[] sfvFiles = Directory.GetFiles(settings.RootScanPath, "*.sfv", SearchOption.AllDirectories);

				Console.WriteLine("Going to sleep: " + settings.SleepTime);
				Thread.Sleep(settings.SleepTime);
			}
		}

		public void ShutDown()
		{
			_shutDown = true;
		}
	}
}
