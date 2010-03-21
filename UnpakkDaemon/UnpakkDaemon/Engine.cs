using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using UnpakkDaemon.DataAccess;
using UnpakkDaemon.DataObjects;
using UnpakkDaemon.EventArguments;
using UnpakkDaemon.Extraction;
using UnpakkDaemon.Service.Host;
using UnpakkDaemon.SimpleFileVerification;

namespace UnpakkDaemon
{
	public class Engine : IEngine
	{
		private readonly string _startupPath;
		private FileLogger _fileLogger;
		private FileRecorder _fileRecorder;
		private bool _shutDown;
		private string _lastRARVolume;
		private ProgressEventArgs _lastSentProgressEventArgs;
		private ProgressEventArgs _lastSentSubProgressEventArgs;

		#region Implementation of IStatusProvider

		public event EventHandler<ProgressEventArgs> Progress;
		public event EventHandler<ProgressEventArgs> SubProgress;
		public event EventHandler<RecordEventArgs> Record;
		public event EventHandler<SubRecordEventArgs> SubRecord;
		public event EventHandler<LogEntryEventArgs> Log;

		#endregion

		#region Implementation of IEngine

		public bool EngineIsPaused()
		{
			return IsPaused;
		}

		public void ResumeEngine()
		{
			IsPaused = false;
		}

		public void PauseEngine()
		{
			IsPaused = true;
		}

		#endregion

		public Engine(string startupPath)
		{
			_startupPath = startupPath;
			_fileLogger = null;
			_fileRecorder = null;
			_shutDown = false;
			_lastRARVolume = string.Empty;
			_lastSentProgressEventArgs = null;
			_lastSentSubProgressEventArgs = null;
			IsRunning = false;
			IsPaused = false;
		}

		public bool IsRunning { get; private set; }

		private bool IsPaused { get; set; }

		public void Start()
		{
			_shutDown = false;
			IsRunning = true;

			EngineSettings.Load();

			SetupLogging();
			SetupRecording();

			StatusServiceHost.Open(this);

			Thread.Sleep(2000);

			EnterMainLoop();

			StatusServiceHost.Close();

			IsRunning = false;

			WriteLogEntry("ABORTING -- Communication shut down, signing off");

			Environment.Exit(0);
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
			RaiseProgressEvent(new ProgressEventArgs(message, percent));
		}

		private void RaiseProgressEvent(string message, double percent, long current)
		{
			RaiseProgressEvent(new ProgressEventArgs(message, percent, current));
		}

		private void RaiseProgressEvent(string message, double percent, long current, long max)
		{
			RaiseProgressEvent(new ProgressEventArgs(message, percent, current, max));
		}

		private void RaiseProgressEvent(ProgressEventArgs progressEventArgs)
		{
			if (_lastSentProgressEventArgs == null || !progressEventArgs.IsSame(_lastSentProgressEventArgs))
			{
				_lastSentProgressEventArgs = progressEventArgs;
				if (Progress != null)
					Progress(this, progressEventArgs);
			}
		}

		private void RaiseSubProgressEvent(string message, double percent)
		{
			RaiseSubProgressEvent(new ProgressEventArgs(message, percent));
		}

		private void RaiseSubProgressEvent(string message, double percent, long current)
		{
			RaiseSubProgressEvent(new ProgressEventArgs(message, percent, current));
		}

		private void RaiseSubProgressEvent(string message, double percent, long current, long max)
		{
			RaiseSubProgressEvent(new ProgressEventArgs(message, percent, current, max));
		}

		private void RaiseSubProgressEvent(ProgressEventArgs progressEventArgs)
		{
			if (_lastSentSubProgressEventArgs == null || !progressEventArgs.IsSame(_lastSentSubProgressEventArgs))
			{
				_lastSentSubProgressEventArgs = progressEventArgs;
				if (SubProgress != null)
					SubProgress(this, progressEventArgs);
			}
		}

		private void RaiseLogEvent(LogType logType, string logText)
		{
			RaiseLogEvent(new LogEntryEventArgs(logType, logText));
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
					WaitIfPaused();
					WriteLogEntry("======================================================");
					WriteLogEntry("Waking up, reloading settings..");
					EngineSettings.Load();

					// scan root folders for sfv files..
					RaiseSubProgressEvent(string.Empty, 0);
					RaiseProgressEvent("Scanning root folders...", 0);
					IDictionary<RootPath, List<string>> scannedRootPaths = new Dictionary<RootPath, List<string>>();
					int totalPaths = 0;
					foreach (RootPath rootPath in EngineSettings.RootPaths)
					{
						if (_shutDown) break;
						UNC unc;
						if (!OpenUNC(rootPath, out unc)) continue;
						try
						{
							WriteLogEntry("Scanning root folder, path=" + rootPath.Path);
							List<string> sfvFilePaths = Directory.GetFiles(rootPath.Path, "*.sfv", SearchOption.AllDirectories).ToList();
							sfvFilePaths.Sort();
							totalPaths += sfvFilePaths.Count;
							scannedRootPaths.Add(rootPath, sfvFilePaths);
						}
						finally
						{
							CloseUNC(ref unc);
						}
					}

					// process found sfv files..
					int pathCounter = 0;
					foreach (RootPath rootPath in scannedRootPaths.Keys.OrderBy(x => x))
					{
						WaitIfPaused();
						UNC unc;
						if (!OpenUNC(rootPath, out unc)) continue;
						try
						{
							List<string> sfvFilePaths = scannedRootPaths[rootPath];
							for (int i = 0; i < sfvFilePaths.Count && !_shutDown; i++)
							{
								WaitIfPaused();
								if (_shutDown) break;
								RaiseSubProgressEvent(string.Empty, 0);
								RaiseProgressEvent("Processing SFV file: " + Path.GetFileName(sfvFilePaths[i]), 100 * (pathCounter / (double) totalPaths), pathCounter + 1, totalPaths);
								ProcessSFVFile(sfvFilePaths[i], rootPath);
								pathCounter++;
							}
						}
						finally
						{
							CloseUNC(ref unc);
						}
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

				DateTime sleepUntil = DateTime.Now + EngineSettings.SleepTime;
				while (DateTime.Now < sleepUntil && !_shutDown)
					Thread.Sleep(100);

				if (_shutDown)
				{
					WriteLogEntry("ABORTING -- Shutdown initiated");
					RaiseSubProgressEvent(string.Empty, 100);
					RaiseProgressEvent("Unpakk Daemon Service is shutting down...", 100);
				}
			}
		}

		private static bool OpenUNC(RootPath rootPath, out UNC unc)
		{
			unc = null;
			if (rootPath.IsUNCPath && !string.IsNullOrEmpty(rootPath.UserName))
			{
				unc = new UNC(rootPath.Path, rootPath.Domain, rootPath.UserNameWithoutDomain, rootPath.Password);
				return unc.Open();
			}
			return true;
		}

		private static void CloseUNC(ref UNC unc)
		{
			if (unc != null)
			{
				unc.Close();
				unc.Dispose();
				unc = null;
			}
		}

		private void WaitIfPaused()
		{
			if (IsPaused)
			{
				RaiseSubProgressEvent(string.Empty, 0);
				RaiseProgressEvent("Paused, awaiting resume instruction...", 0);
			}
			while (IsPaused && !_shutDown) Thread.Sleep(100);
		}

		private void ProcessSFVFile(string sfvFilePath, RootPath rootPath)
		{
			SFVFile sfvFile = null;
			try
			{
				WriteLogEntry("Validating SFV file references, sfvfile=" + sfvFilePath);
				RaiseSubProgressEvent("Validating SFV file references...", 0);
				sfvFile = new SFVFile(sfvFilePath);
				sfvFile.Progress += sfvFile_Progress;
				string rarFilePath = sfvFile.ContainedFilePaths.OrderBy(filePath => filePath)
					.FirstOrDefault(filePath => filePath.EndsWith(".rar", StringComparison.CurrentCultureIgnoreCase));
				Record record = new Record(Path.GetDirectoryName(sfvFile.SFVFilePath), Path.GetFileName(sfvFile.SFVFilePath),
					Path.GetFileName(rarFilePath), sfvFile.ContainedFilePaths.Count, FileHandler.GetTotalFileSize(sfvFile.ContainedFilePaths));
				AddRecord(record);
				if (!string.IsNullOrEmpty(rarFilePath))
				{
					if (sfvFile.Validate())
					{
						WriteLogEntry("Validation OK, proceeding with extraction...");
						AddRecord(record.Succeed());
						if (ExtractRARContent(rarFilePath, rootPath, record))
						{
							DeleteFiles(sfvFile);
						}
					}
					else
					{
						WriteLogEntry(LogType.Warning, "Validation FAILED, skipping archive");
						AddRecord(record.Fail());
					}
				}
				else
				{
					WriteLogEntry(LogType.Warning, "SFV file does not refer to any .rar file, skipping further processing");
					AddRecord(record.Fail());
				}
			}
			catch (Exception ex)
			{
				WriteLogEntry("An exception occurred while processing SFV file, path=" + sfvFilePath, ex);
				AddRecord(new Record(RecordStatus.Failure, Path.GetDirectoryName(sfvFilePath), Path.GetFileName(sfvFilePath), string.Empty, 0, 0));
			}
			finally
			{
				if (sfvFile != null)
					sfvFile.Progress -= sfvFile_Progress;
			}
		}

		#region SFVFile event handlers

		private void sfvFile_Progress(object sender, ProgressEventArgs e)
		{
			RaiseSubProgressEvent("Validating SFV file references...", e.Percent, e.Current, e.Max);
		}

		#endregion

		private bool ExtractRARContent(string rarFilePath, RootPath rootPath, Record record)
		{
			bool success = true;
			SubRecord subRecord = null;
			_lastRARVolume = string.Empty;
			Unrar unrar = new Unrar(rarFilePath)
			{
				DestinationPath = (EngineSettings.UseSpecificOutputFolder
					? Path.Combine(EngineSettings.OutputFolder, FileHandler.GetDeltaPath(Path.GetDirectoryName(rarFilePath), rootPath.Path))
					: Path.GetDirectoryName(rarFilePath))
			};
			unrar.ExtractionProgress += unrar_ExtractionProgress;
			unrar.MissingVolume += unrar_MissingVolume;
			unrar.NewVolume += unrar_NewVolume;
			unrar.PasswordRequired += unrar_PasswordRequired;
			try
			{
				unrar.Open(Unrar.OpenMode.Extract);
				while (success && unrar.ReadHeader())
				{
					WriteLogEntry("Extracting file, name=" + unrar.CurrentFile.FileName + ", size=" + unrar.CurrentFile.UnpackedSize + ", path=" + unrar.DestinationPath);
					subRecord = new SubRecord(unrar.DestinationPath, unrar.CurrentFile.FileName, unrar.CurrentFile.UnpackedSize);
					AddSubRecord(record.ID, subRecord);
					unrar.Extract();
					success = ValidateExtractedFile(Path.Combine(unrar.DestinationPath, unrar.CurrentFile.FileName), 
						unrar.CurrentFile.UnpackedSize, GetRARFileCRC(_lastRARVolume, unrar.CurrentFile.FileName));
					if (!success)
						WriteLogEntry(LogType.Warning, "Validation FAILED, aborting extraction");
					AddSubRecord(record.ID, (success ? subRecord.Succeed() : subRecord.Fail()));
				}
			}
			catch (Exception ex)
			{
				WriteLogEntry("An exception occurred while extracting from RAR file, path=" + rarFilePath + ", destination=" + unrar.DestinationPath, ex);
				if (subRecord != null) AddSubRecord(record.ID, subRecord.Fail());
				success = false;
			}
			finally
			{
				unrar.Close();
				unrar.ExtractionProgress -= unrar_ExtractionProgress;
				unrar.MissingVolume -= unrar_MissingVolume;
				unrar.NewVolume -= unrar_NewVolume;
				unrar.PasswordRequired -= unrar_PasswordRequired;
			}
			return success;
		}

		private static string GetRARFileCRC(string rarVolumePath, string fileName)
		{
			if (!FileHandler.FileExists(rarVolumePath))
				return string.Empty;

			Unrar unrar = new Unrar(rarVolumePath);
			try
			{
				unrar.Open();

				while (unrar.ReadHeader() && unrar.CurrentFile.FileName != fileName)
					unrar.Skip();

				if (unrar.CurrentFile != null && unrar.CurrentFile.FileName == fileName)
					return unrar.CurrentFile.FileCRC.ToString("X8").ToLower();
			}
			finally
			{
				unrar.Close();
			}
			return string.Empty;
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

		private void unrar_NewVolume(object sender, NewVolumeEventArgs e)
		{
			_lastRARVolume = e.VolumeName;
			e.ContinueOperation = true;
		}

		private void unrar_ExtractionProgress(object sender, ExtractionProgressEventArgs e)
		{
			RaiseSubProgressEvent("Extracting file: " + e.FileName, e.PercentComplete, e.BytesExtracted, e.FileSize);
		}

		#endregion

		private bool ValidateExtractedFile(string filePath, long fileSize, string referenceChecksum)
		{
			WriteLogEntry("Validating extracted file...");
			RaiseSubProgressEvent("Validating extracted file: " + Path.GetFileName(filePath), 0);

			if (!FileHandler.FileExists(filePath))
			{
				WriteLogEntry(LogType.Warning, "Extracted file missing, path=" + filePath);
				return false;
			}

			long actualFileSize = FileHandler.FileSize(filePath);
			if (actualFileSize != fileSize)
			{
				WriteLogEntry(LogType.Warning, "Extracted file size mismatch, reference=" + fileSize + ", actual=" + actualFileSize);
				return false;
			}

			CRC32 crc32 = new CRC32(Path.GetFileName(filePath), actualFileSize);
			crc32.Progress += crc32_Progress;
			try
			{
				using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
				{
					crc32.ComputeHash(fileStream);
					if (!crc32.HashValueStr.Equals(referenceChecksum, StringComparison.CurrentCultureIgnoreCase))
					{
						WriteLogEntry(LogType.Warning, "CRC checksum mismatch, reference=" + referenceChecksum + ", actual=" + crc32.HashValueStr.ToLower());
						return false;
					}
					WriteLogEntry(LogType.Debug, "CRC checksum match, reference=" + referenceChecksum + ", actual=" + crc32.HashValueStr.ToLower());
				}
			}
			finally
			{
				crc32.Progress -= crc32_Progress;
			}
			WriteLogEntry("Validation OK");
			return true;
		}

		#region CRC32 event handlers

		private void crc32_Progress(object sender, ProgressEventArgs e)
		{
			RaiseSubProgressEvent("Validating extracted file: " + e.Message, e.Percent, e.Current, e.Max);
		}

		#endregion

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
						if (FileHandler.FileExists(filePath))
						{
							WriteLogEntry(LogType.Debug, "Deleting archive file, path=" + filePath);
							FileHandler.DeleteFile(filePath);
						}
					}
					catch (Exception ex)
					{
						WriteLogEntry("An exception occurred while deleting archive file, path=" + filePath, ex);
					}
				}
				if (FileHandler.FileExists(sfvFile.SFVFilePath))
				{
					WriteLogEntry(LogType.Debug, "Deleting SFV file, path=" + sfvFile.SFVFilePath);
					FileHandler.DeleteFile(sfvFile.SFVFilePath);
				}
			}
			catch (Exception ex)
			{
				WriteLogEntry("An exception occurred while deleting archive files, sfvfile=" + sfvFile.SFVFilePath, ex);
			}
		}
	}
}
