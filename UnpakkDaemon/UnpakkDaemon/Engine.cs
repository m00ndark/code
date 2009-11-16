using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnpakkDaemon.EventArguments;
using UnpakkDaemon.SimpleFileVerification;

namespace UnpakkDaemon
{
	public class Engine
	{
		private readonly string _startupPath;
		private FileLogger _fileLogger;
		private bool _shutDown;

		public Engine(string startupPath)
		{
			_startupPath = startupPath;
			_fileLogger = null;
			_shutDown = false;
			IsRunning = false;
		}

		public bool IsRunning { get; private set; }

		public void Start()
		{
			_shutDown = false;
			IsRunning = true;

			SetupLogging();
#if !DEBUG
			TrayHandler.LaunchTray(_startupPath);
#endif
			EnterMainLoop(new EngineSettings());

			IsRunning = false;
		}

		public void ShutDown()
		{
			_shutDown = true;
		}

		#region Logging

		private void SetupLogging()
		{
			if (_fileLogger == null)
			{
				_fileLogger = new FileLogger();
				SFVFileHandler.LogEntry += LogEntry;
			}
		}

		private void LogEntry(object sender, LogEntryEventArgs e)
		{
			WriteLogEntry(e.LogText);
		}

		private void WriteLogEntry(string logText)
		{
			if (_fileLogger != null)
				_fileLogger.WriteLogLine(logText);
		}

		#endregion

		private void EnterMainLoop(EngineSettings settings)
		{
			while (!_shutDown)
			{
				settings.Load();

				string[] sfvFiles = Directory.GetFiles(settings.RootScanPath, "*.sfv", SearchOption.AllDirectories);
				foreach (string sfvFile in sfvFiles)
				{
					WriteLogEntry(sfvFile);
				}

				WriteLogEntry("Going to sleep: " + settings.SleepTime);
				Thread.Sleep(settings.SleepTime);
			}
		}
	}
}
