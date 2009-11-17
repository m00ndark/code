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

		#endregion

		private readonly string _sfvFilePath;
		private readonly string _path;
		private readonly IDictionary<string, string> _crcFiles;

		public SFVFile(string sfvFilePath)
		{
			_sfvFilePath = sfvFilePath;
			_path = Path.GetDirectoryName(_sfvFilePath);
			_crcFiles = new Dictionary<string, string>();
			Load();
		}

		public IList<string> FilePaths
		{
			get { return _crcFiles.Keys.Select(fileName => Path.Combine(_path, fileName)).ToList(); }
		}

		private void Load()
		{
			string[] lines = File.ReadAllLines(_sfvFilePath);
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
					RaiseLogEntryEvent("File missing: " + fileName);
					return false;
				}

				using (FileStream fileStream = File.Open(filePath, FileMode.Open))
				{
					crc32.ComputeHash(fileStream);
					if (!crc32.HashValueStr.Equals(_crcFiles[fileName], StringComparison.CurrentCultureIgnoreCase))
					{
						RaiseLogEntryEvent(fileName + ": " + crc32.HashValueStr + " != " + _crcFiles[fileName].ToLower());
						return false;
					}
					RaiseLogEntryEvent(fileName + ": " + crc32.HashValueStr + " == " + _crcFiles[fileName].ToLower());
				}
			}
			return true;
		}
	}
}
