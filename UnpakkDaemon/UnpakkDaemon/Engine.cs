using System;
using System.IO;
using System.Linq;
using System.Threading;
using UnpakkDaemon.EventArguments;
using UnpakkDaemon.Extraction;
using UnpakkDaemon.Service;
using UnpakkDaemon.SimpleFileVerification;

namespace UnpakkDaemon
{
	public class Engine : IStatusProvider
	{
		private readonly string _startupPath;
		private FileLogger _fileLogger;
		private bool _shutDown;

		#region Implementation of IStatusProvider

		public event EventHandler<ProgressEventArgs> Progress;
		public event EventHandler<ProgressEventArgs> SubProgress;

		#endregion

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

			StatusServiceHost.Open(this);

#if !DEBUG
			TrayHandler.LaunchTray(_startupPath);
#endif

			Thread.Sleep(1000000);

			EnterMainLoop(new EngineSettings());

			StatusServiceHost.Close();

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

		private void WriteLogEntry(Exception exception)
		{
			if (_fileLogger != null)
				_fileLogger.WriteLogLine(string.Empty, exception);
			else
				throw new SystemException("Logging not setup correctly.");
		}

		private void WriteLogEntry(string logText, Exception exception)
		{
			if (_fileLogger != null)
				_fileLogger.WriteLogLine(logText, exception);
			else
				throw new SystemException("Logging not setup correctly.");
		}

		#endregion

		#region Event raisers

		private void RaiseProgressEvent(string message, double percent)
		{
			if (Progress != null)
				Progress(this, new ProgressEventArgs(message, percent));
		}

		private void RaiseProgressEvent(string message, double percent, long current)
		{
			if (Progress != null)
				Progress(this, new ProgressEventArgs(message, percent, current));
		}

		private void RaiseProgressEvent(string message, double percent, long current, long max)
		{
			if (Progress != null)
				Progress(this, new ProgressEventArgs(message, percent, current, max));
		}

		private void RaiseSubProgressEvent(string message, double percent)
		{
			if (SubProgress != null)
				SubProgress(this, new ProgressEventArgs(message, percent));
		}

		private void RaiseSubProgressEvent(string message, double percent, long current)
		{
			if (SubProgress != null)
				SubProgress(this, new ProgressEventArgs(message, percent, current));
		}

		private void RaiseSubProgressEvent(string message, double percent, long current, long max)
		{
			if (SubProgress != null)
				SubProgress(this, new ProgressEventArgs(message, percent, current, max));
		}

		#endregion

		private void EnterMainLoop(EngineSettings settings)
		{
			while (!_shutDown)
			{
				try
				{
					WriteLogEntry("======================================================");
					WriteLogEntry("Waking up, reloading settings..");
					settings.Load();

					WriteLogEntry("Scanning root folder, path=" + settings.RootScanPath);
					string[] sfvFilePaths = Directory.GetFiles(settings.RootScanPath, "*.sfv", SearchOption.AllDirectories);
					sfvFilePaths.ToList().ForEach(ProcessSFVFile);

					WriteLogEntry("Going to sleep, time=" + settings.SleepTime);
					Thread.Sleep(settings.SleepTime);
				}
				catch (Exception ex)
				{
					WriteLogEntry("An exception occurred in main loop", ex);
				}
			}
		}

		private void ProcessSFVFile(string sfvFilePath)
		{
			try
			{
				WriteLogEntry("Validating SFV file references, sfvfile=" + sfvFilePath);
				SFVFile sfvFile = new SFVFile(sfvFilePath);
				string rarFilePath = sfvFile.ContainedFilePaths.FirstOrDefault(filePath => filePath.EndsWith(".rar", StringComparison.CurrentCultureIgnoreCase));
				if (!string.IsNullOrEmpty(rarFilePath))
				{
					if (sfvFile.Validate())
					{
						WriteLogEntry("Validation OK, proceeding with extraction..");
						if (ExtractRARContent(rarFilePath))
						{
							DeleteFiles(sfvFile);
						}
					}
					else
					{
						WriteLogEntry(LogType.Warning, "Validation FAILED, skipping archive");
					}
				}
				else
				{
					WriteLogEntry(LogType.Warning, "SFV file does not refer to any .rar file, skipping further processing");
				}
			}
			catch (Exception ex)
			{
				WriteLogEntry("An exception occurred while processing SFV file, path=" + sfvFilePath, ex);
			}
		}

		private bool ExtractRARContent(string rarFilePath)
		{
			bool success = true;
			Unrar unrar = new Unrar(rarFilePath) { DestinationPath = Path.GetDirectoryName(rarFilePath) };
			unrar.ExtractionProgress += unrar_ExtractionProgress;
			unrar.MissingVolume += unrar_MissingVolume;
			unrar.PasswordRequired += unrar_PasswordRequired;
			try
			{
				unrar.Open(Unrar.OpenMode.Extract);
				while (success && unrar.ReadHeader())
				{
					WriteLogEntry("Extracting file, name=" + unrar.CurrentFile.FileName + ", size=" + unrar.CurrentFile.UnpackedSize);
					unrar.Extract();
					success = ValidateExtractedFile(Path.Combine(unrar.DestinationPath, unrar.CurrentFile.FileName),
						unrar.CurrentFile.UnpackedSize, unrar.CurrentFile.FileCRC);
					if (!success)
						WriteLogEntry(LogType.Warning, "Validation FAILED, aborting extraction");
				}
			}
			catch (Exception ex)
			{
				WriteLogEntry("An exception occurred while extracting from RAR file, path=" + rarFilePath, ex);
				success = false;
			}
			finally
			{
				unrar.Close();
				unrar.ExtractionProgress -= unrar_ExtractionProgress;
				unrar.MissingVolume -= unrar_MissingVolume;
				unrar.PasswordRequired -= unrar_PasswordRequired;
			}
			return success;
		}

		#region Unrar event handlers

		private void unrar_PasswordRequired(object sender, PasswordRequiredEventArgs e)
		{
			WriteLogEntry(LogType.Warning, "Password required to extract file, aborting");
			e.ContinueOperation = true;
		}

		private void unrar_MissingVolume(object sender, MissingVolumeEventArgs e)
		{
			WriteLogEntry(LogType.Warning, "RAR volume missing, aborting, volume=" + e.VolumeName);
			e.ContinueOperation = true;
		}

		private void unrar_ExtractionProgress(object sender, ExtractionProgressEventArgs e)
		{
			RaiseSubProgressEvent("Extracting " + e.FileName, e.PercentComplete, e.BytesExtracted, e.FileSize);
		}

		#endregion

		private bool ValidateExtractedFile(string filePath, long fileSize, long fileChecksum)
		{
			WriteLogEntry("Validating extracted file..");

			if (!File.Exists(filePath))
			{
				WriteLogEntry(LogType.Warning, "Extracted file missing, path=" + filePath);
				return false;
			}

			FileInfo fileInfo = new FileInfo(filePath);
			if (fileInfo.Length != fileSize)
			{
				WriteLogEntry(LogType.Warning, "Extracted file size mismatch, reference=" + fileSize + ", actual=" + fileInfo.Length);
				return false;
			}

			CRC32 crc32 = new CRC32();
			using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
			{
				crc32.ComputeHash(fileStream);
				string referenceChecksum = CRC32.ToString(Convert.ToUInt32(fileChecksum));
				if (!crc32.HashValueStr.Equals(referenceChecksum, StringComparison.CurrentCultureIgnoreCase))
				{
					WriteLogEntry(LogType.Warning, "CRC checksum mismatch, reference=" + referenceChecksum + ", actual=" + crc32.HashValueStr.ToLower());
					return false;
				}
				WriteLogEntry(LogType.Debug, "CRC checksum match, reference=" + referenceChecksum + ", actual=" + crc32.HashValueStr.ToLower());
			}

			WriteLogEntry("Validation OK");
			return true;
		}

		private void DeleteFiles(SFVFile sfvFile)
		{
			WriteLogEntry("Deleting archive files..");

			try
			{
				foreach (string filePath in sfvFile.ContainedFilePaths)
				{
					try
					{
						if (File.Exists(filePath))
						{
							WriteLogEntry(LogType.Debug, "Deleting archive file, path=" + filePath);
							File.Delete(filePath);
						}
					}
					catch (Exception ex)
					{
						WriteLogEntry("An exception occurred while deleting archive file, path=" + filePath, ex);
					}
				}
				if (File.Exists(sfvFile.SFVFilePath))
				{
					WriteLogEntry(LogType.Debug, "Deleting SFV file, path=" + sfvFile.SFVFilePath);
					File.Delete(sfvFile.SFVFilePath);
				}
			}
			catch (Exception ex)
			{
				WriteLogEntry("An exception occurred while deleting archive files, sfvfile=" + sfvFile.SFVFilePath, ex);
			}
		}
	}
}
