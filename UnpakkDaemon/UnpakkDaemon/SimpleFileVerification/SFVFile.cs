using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnpakkDaemon.EventArguments;

namespace UnpakkDaemon.SimpleFileVerification
{
	public class SFVFile
	{
		public static event EventHandler<LogEntryEventArgs> LogEntry;

		#region Event raisers

		private static void RaiseLogEntryEvent(string logText)
		{
			if (LogEntry != null)
			{
				LogEntry(null, new LogEntryEventArgs(logText));
			}
		}

		private static void RaiseLogEntryEvent(LogType logType, string logText)
		{
			if (LogEntry != null)
			{
				LogEntry(null, new LogEntryEventArgs(logType, logText));
			}
		}

		#endregion

		private readonly string _path;
		private readonly IDictionary<string, string> _crcFiles;

		public SFVFile(string sfvFilePath)
		{
			SFVFilePath = sfvFilePath;
			_path = Path.GetDirectoryName(SFVFilePath);
			_crcFiles = new Dictionary<string, string>();
			Load();
		}

		public string SFVFilePath { get; private set; }

		public IList<string> ContainedFilePaths
		{
			get { return _crcFiles.Keys.Select(fileName => Path.Combine(_path, fileName)).ToList(); }
		}

		private void Load()
		{
			string[] lines = File.ReadAllLines(SFVFilePath);
			foreach (string l in lines)
			{
				string line = l.Trim();
				if (!string.IsNullOrEmpty(line) && !line.StartsWith(";"))
				{
					string[] lineSplit = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
					if (lineSplit.Length == 2)
					{
						_crcFiles.Add(lineSplit[0], lineSplit[1]);
					}
				}
			}
		}

		public bool Validate()
		{
			foreach (string fileName in _crcFiles.Keys)
			{
				CRC32 crc32 = new CRC32();
				string filePath = Path.Combine(_path, fileName);

				if (!File.Exists(filePath))
				{
					RaiseLogEntryEvent(LogType.Warning, "File missing, name=" + fileName);
					return false;
				}

				using (FileStream fileStream = File.Open(filePath, FileMode.Open))
				{
					crc32.ComputeHash(fileStream);
					if (!crc32.HashValueStr.Equals(_crcFiles[fileName], StringComparison.CurrentCultureIgnoreCase))
					{
						RaiseLogEntryEvent(LogType.Warning, "CRC checksum mismatch, file=" + fileName + ", reference=" + _crcFiles[fileName].ToLower() + ", actual=" + crc32.HashValueStr);
						return false;
					}
					RaiseLogEntryEvent(LogType.Debug, "CRC checksum match, file=" + fileName + ", reference=" + _crcFiles[fileName].ToLower() + ", actual=" + crc32.HashValueStr);
				}
			}
			return true;
		}
	}
}
