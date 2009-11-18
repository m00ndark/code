using System;
using System.Linq;
using System.IO;

namespace UnpakkDaemon
{
	public enum LogType
	{
		Debug,
		Flow,
		Warning,
		Error
	}

	public class FileLogger
	{
		private const string APPLICATION_DATA_FOLDER = @"MoleCode\Unpakk Daemon";
		private const string DEFAULT_LOG_FOLDER = "Logs";
		private const string DEFAULT_LOG_NAME = "unpakkdaemon_%date%.log";

		private readonly string _fullAppDataFolder;
		private int _logTypeIndentationDepth;

		public FileLogger()
		{
			_fullAppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), APPLICATION_DATA_FOLDER);
			LogFolder = DEFAULT_LOG_FOLDER;
			LogName = DEFAULT_LOG_NAME;
			SetupLogTypeIndentationDepth();
		}

		#region Properties

		public string LogFolder { get; set; }
		public string LogName { get; set; }

		public string LogPath
		{
			get { return Path.Combine(_fullAppDataFolder, LogFolder); }
		}

		public string LogPathName
		{
			get { return Path.Combine(LogPath, LogName.Replace("%date%", DateTime.Now.ToString("yyyy-MM-dd"))); }
		}

		#endregion

		private void SetupLogTypeIndentationDepth()
		{
			string[] logTypeNames = Enum.GetNames(typeof(LogType));
			_logTypeIndentationDepth = logTypeNames.Max(logTypeName => logTypeName.Length);
		}

		public void WriteLogLine(string logText, Exception exception)
		{
			WriteLogLine(LogType.Error, logText);
			WriteLogLine(LogType.Error, exception.ToString());
		}

		public void WriteLogLine(LogType logType, string logText)
		{
			try
			{
				if (!Directory.Exists(LogPath))
					Directory.CreateDirectory(LogPath);

				using (StreamWriter writer = new StreamWriter(LogPathName, true))
					writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " "
						+ logType.ToString().PadRight(_logTypeIndentationDepth) + " " + logText);
			}
			catch { /* swallow and be happy :) */ }
		}
	}
}
