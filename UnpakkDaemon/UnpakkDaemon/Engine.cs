using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using UnpakkDaemon.DataObjects;
using UnpakkDaemon.EventArguments;
using UnpakkDaemon.Extraction;
using UnpakkDaemon.Service.Host;
using UnpakkDaemon.SimpleFileVerification;

namespace UnpakkDaemon
{
	public class Engine : IStatusProvider
	{
		private readonly string _startupPath;
		private FileLogger _fileLogger;
		private FileRecorder _fileRecorder;
		private bool _shutDown;

		#region Implementation of IStatusProvider

		public event EventHandler<ProgressEventArgs> Progress;
		public event EventHandler<ProgressEventArgs> SubProgress;
		public event EventHandler<RecordEventArgs> Record;
		public event EventHandler<SubRecordEventArgs> SubRecord;
		public event EventHandler<LogEntryEventArgs> Log;

		#endregion

		public Engine(string startupPath)
		{
			_startupPath = startupPath;
			_fileLogger = null;
			_fileRecorder = null;
			_shutDown = false;
			IsRunning = false;
		}

		public bool IsRunning { get; private set; }

		public void Start()
		{
			_shutDown = false;
			IsRunning = true;

			SetupLogging();
			SetupRecording();

			StatusServiceHost.Open(this);

			//Thread.Sleep(1000000);

//#if !DEBUG
//			TrayHandler.LaunchTray(_startupPath);
			Thread.Sleep(2000);
//#endif

			EnterMainLoop();

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
				_fileLogger.LogEntryWritten += LogEntryWritten;
				SFVFile.LogEntry += LogEntry;
			}
		}

		private void LogEntryWritten(object sender, LogEntryEventArgs e)
		{
			RaiseLogEvent(e);
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

		#region Recording

		private void SetupRecording()
		{
			if (_fileRecorder == null)
			{
				_fileRecorder = new FileRecorder();
				_fileRecorder.LogEntry += LogEntry;
				_fileRecorder.RecordAdded += RecordAdded;
				_fileRecorder.SubRecordAdded += SubRecordAdded;
			}
		}

		private void RecordAdded(object sender, RecordEventArgs e)
		{
			RaiseRecordEvent(e);
		}

		private void SubRecordAdded(object sender, SubRecordEventArgs e)
		{
			RaiseSubRecordEvent(e);
		}

		private void AddRecord(Record record)
		{
			if (_fileRecorder != null)
				_fileRecorder.AddRecord(record);
			else
				throw new SystemException("Recording not setup correctly.");
		}

		private void AddSubRecord(Record parentRecord, SubRecord subRecord)
		{
			AddSubRecord(parentRecord.ID, subRecord);
		}

		private void AddSubRecord(Guid parentID, SubRecord subRecord)
		{
			if (_fileRecorder != null)
				_fileRecorder.AddSubRecord(parentID, subRecord);
			else
				throw new SystemException("Recording not setup correctly.");
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

		private void RaiseLogEvent(LogType logType, string logText)
		{
			if (Log != null)
				Log(this, new LogEntryEventArgs(logType, logText));
		}

		private void RaiseLogEvent(LogEntryEventArgs e)
		{
			if (Log != null)
				Log(this, e);
		}

		private void RaiseRecordEvent(RecordEventArgs e)
		{
			if (Record != null)
				Record(this, e);
		}

		private void RaiseSubRecordEvent(SubRecordEventArgs e)
		{
			if (SubRecord != null)
				SubRecord(this, e);
		}

		#endregion

		private void EnterMainLoop()
		{
			while (!_shutDown)
			{
				try
				{
					// TODO: make recordings

					WriteLogEntry("======================================================");
					WriteLogEntry("Waking up, reloading settings..");
					EngineSettings.Load();

					RaiseSubProgressEvent(string.Empty, 0);
					RaiseProgressEvent("Scanning root folders...", 0);
					List<string> sfvFilePaths = new List<string>();
					foreach (string rootPath in EngineSettings.RootPaths)
					{
						WriteLogEntry("Scanning root folder, path=" + rootPath);
						sfvFilePaths.AddRange(Directory.GetFiles(rootPath, "*.sfv", SearchOption.AllDirectories));
						if (_shutDown) break;
					}

					for (int i = 0; i < sfvFilePaths.Count && !_shutDown; i++)
					{
						RaiseSubProgressEvent(string.Empty, 0);
						RaiseProgressEvent("Processing SFV file: " + Path.GetFileName(sfvFilePaths[i]), 100 * (i / (double) sfvFilePaths.Count), i + 1, sfvFilePaths.Count);
						ProcessSFVFile(sfvFilePaths[i]);
					}

					if (!_shutDown)
					{
						WriteLogEntry("Going to sleep, time=" + EngineSettings.SleepTime);
						RaiseSubProgressEvent(string.Empty, 100);
						RaiseProgressEvent("Done, going to sleep...", 100);
					}
				}
				catch (Exception ex)
				{
					WriteLogEntry("An exception occurred in main loop", ex);
				}

				if (_shutDown)
				{
					WriteLogEntry("ABORTING -- Shutdown initiated");
					RaiseSubProgressEvent(string.Empty, 100);
					RaiseProgressEvent("Unpakk Daemon Service is shutting down...", 100);
				}
				else
					Thread.Sleep(EngineSettings.SleepTime);
			}
		}

		private void ProcessSFVFile(string sfvFilePath)
		{
			try
			{
				WriteLogEntry("Validating SFV file references, sfvfile=" + sfvFilePath);
				RaiseSubProgressEvent("Validating SFV file references...", 0);
				SFVFile sfvFile = new SFVFile(sfvFilePath);
				string rarFilePath = sfvFile.ContainedFilePaths.FirstOrDefault(filePath => filePath.EndsWith(".rar", StringComparison.CurrentCultureIgnoreCase));
				if (!string.IsNullOrEmpty(rarFilePath))
				{
					if (sfvFile.Validate())
					{
						WriteLogEntry("Validation OK, proceeding with extraction...");
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
			RaiseSubProgressEvent("Extracting file: " + e.FileName, e.PercentComplete, e.BytesExtracted, e.FileSize);
		}

		#endregion

		private bool ValidateExtractedFile(string filePath, long fileSize, long fileChecksum)
		{
			WriteLogEntry("Validating extracted file...");
			RaiseSubProgressEvent("Validating extracted file: " + Path.GetFileName(filePath), 100);

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
			WriteLogEntry("Deleting archive files...");
			RaiseSubProgressEvent("Deleting archive files...", 100);

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
