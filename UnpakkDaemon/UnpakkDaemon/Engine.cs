using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Schematrix;
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
				SFVFile.LogEntry += LogEntry;
			}
		}

		private void LogEntry(object sender, LogEntryEventArgs e)
		{
			WriteLogEntry(e.LogType, e.LogText);
		}

		private void WriteLogEntry(string logText)
		{
			WriteLogEntry(LogType.Flow, logText);
		}

		private void WriteLogEntry(LogType logType, string logText)
		{
			if (_fileLogger != null)
				_fileLogger.WriteLogLine(logType, logText);
			else
				throw new SystemException("Logging not setup correctly.");
		}

		#endregion

		private void EnterMainLoop(EngineSettings settings)
		{
			while (!_shutDown)
			{
				settings.Load();

				string[] sfvFilePaths = Directory.GetFiles(settings.RootScanPath, "*.sfv", SearchOption.AllDirectories);
				sfvFilePaths.ToList().ForEach(ProcessSFVFile);

				WriteLogEntry("Going to sleep: " + settings.SleepTime);
				Thread.Sleep(settings.SleepTime);
			}
		}

		private void ProcessSFVFile(string sfvFilePath)
		{
			WriteLogEntry("Validating SFV file references, sfvfile=" + sfvFilePath);
			SFVFile sfvFile = new SFVFile(sfvFilePath);
			string rarFilePath = sfvFile.FilePaths.FirstOrDefault(filePath => filePath.EndsWith(".rar", StringComparison.CurrentCultureIgnoreCase));
			if (!string.IsNullOrEmpty(rarFilePath))
			{
				if (sfvFile.Validate())
				{
					WriteLogEntry("Validation OK, proceeding with extraction..");
					if (ExtractRARContent(rarFilePath))
					{
						
					}
				}
				else
				{
					WriteLogEntry("Validation FAILED");
				}
			}
			else
			{
				WriteLogEntry("SFV file does not refer to any .rar file, skipping further processing");
			}
		}

		private bool ExtractRARContent(string rarFilePath)
		{
			bool success = true;
			Unrar unrar = new Unrar(rarFilePath) { DestinationPath = Path.GetDirectoryName(rarFilePath) };
			try
			{
				unrar.Open(Unrar.OpenMode.Extract);
				while (success && unrar.ReadHeader())
				{
					WriteLogEntry("Extracting file, name=" + unrar.CurrentFile.FileName);
					unrar.Extract();
					success = ValidateExtractedFile(Path.Combine(unrar.DestinationPath, unrar.CurrentFile.FileName),
						unrar.CurrentFile.UnpackedSize, unrar.CurrentFile.FileCRC);
				}
			}
			catch (Exception ex)
			{
				WriteLogEntry("An exception occurred while extracting from RAR file, path=" + rarFilePath);
			}
			finally
			{
				unrar.Close();
			}
			return success;
		}

		private bool ValidateExtractedFile(string filePath, long fileSize, long fileChecksum)
		{
			WriteLogEntry("Validating extracted file..");

			if (!File.Exists(filePath))
			{
				WriteLogEntry("Extracted file missing: " + filePath);
				return false;
			}

			FileInfo fileInfo = new FileInfo(filePath);
			if (fileInfo.Length != fileSize)
			{
				WriteLogEntry("Extracted file size mismatch, reference=" + fileSize + ", actual=" + fileInfo.Length);
				return false;
			}

			CRC32 crc32 = new CRC32();
			using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
			{
				crc32.ComputeHash(fileStream);
				if (!crc32.HashValueStr.Equals(CRC32.ToString(Convert.ToUInt32(fileChecksum)), StringComparison.CurrentCultureIgnoreCase))
				{
					WriteLogEntry("Extracted file CRC checksum mismatch, reference=" + fileChecksum + ", actual=" + crc32.HashValueStr.ToLower());
					return false;
				}
			}

			WriteLogEntry("Validation OK");
			return true;
		}
	}
}
