using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UnpakkDaemon;
using UnpakkDaemon.DataAccess;
using UnpakkDaemon.DataObjects;
using UnpakkDaemon.EventArguments;

namespace UnpakkDaemonTray.Workers
{
	public class SettingsWorker
	{
		public event EventHandler<StringEventArgs> ApplicationDataFolderUpdated;
		public event EventHandler<TimeSpanEventArgs> SleepTimeUpdated;
		public event EventHandler<BooleanEventArgs> UseSpecificOutputFolderUpdated;
		public event EventHandler<StringEventArgs> OutputFolderUpdated;
		public event EventHandler<RootPathListEventArgs> RootPathListUpdated;

		public string LastBrowsedRootPath { get; set; }

		#region Event raisers

		private void RaiseApplicationDataFolderUpdatedEvent(string applicationDataFolder)
		{
			if (ApplicationDataFolderUpdated != null)
			{
				ApplicationDataFolderUpdated(this, new StringEventArgs(applicationDataFolder));
			}
		}

		private void RaiseSleepTimeUpdatedEvent(TimeSpan sleepTime)
		{
			if (SleepTimeUpdated != null)
			{
				SleepTimeUpdated(this, new TimeSpanEventArgs(sleepTime));
			}
		}

		private void RaiseUseSpecificOutputFolderUpdatedEvent(bool useSpecificOutputFolder)
		{
			if (UseSpecificOutputFolderUpdated != null)
			{
				UseSpecificOutputFolderUpdated(this, new BooleanEventArgs(useSpecificOutputFolder));
			}
		}

		private void RaiseOutputFolderUpdatedEvent(string outputFolder)
		{
			if (OutputFolderUpdated != null)
			{
				OutputFolderUpdated(this, new StringEventArgs(outputFolder));
			}
		}

		private void RaiseRootPathListUpdatedEvent(IEnumerable<RootPath> rootPaths)
		{
			if (RootPathListUpdated != null)
			{
				RootPathListUpdated(this, new RootPathListEventArgs(rootPaths));
			}
		}

		#endregion

		public void Initialize()
		{
			try
			{
				EngineSettings.Load();
				RaiseApplicationDataFolderUpdatedEvent(EngineSettings.ApplicationDataFolder);
				RaiseSleepTimeUpdatedEvent(EngineSettings.SleepTime);
				RaiseUseSpecificOutputFolderUpdatedEvent(EngineSettings.UseSpecificOutputFolder);
				RaiseOutputFolderUpdatedEvent(EngineSettings.OutputFolder);
				RaiseRootPathListUpdatedEvent(EngineSettings.RootPaths);
				LastBrowsedRootPath = string.Empty;
			}
			catch (Exception ex)
			{
				CommonWorker.ShowError(ex);
			}
		}

		public void Save()
		{
			try
			{
				EngineSettings.Save();
			}
			catch (Exception ex)
			{
				CommonWorker.ShowError(ex);
			}
		}

		public void BrowseApplicationDataFolder()
		{
			try
			{
				FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				folderBrowserDialog.Description = "Please select an application data folder...";
				folderBrowserDialog.SelectedPath = EngineSettings.ApplicationDataFolderComplete;
				if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
				{
					EngineSettings.SetApplicationDataFolder(folderBrowserDialog.SelectedPath);
					RegistryHandler.SaveEngineSettings(EngineSettingsType.ApplicationDataFolder);
					RaiseApplicationDataFolderUpdatedEvent(EngineSettings.ApplicationDataFolder);
				}
			}
			catch (Exception ex)
			{
				CommonWorker.ShowError(ex);
			}
		}

		public bool SetApplicationDataFolder(string path)
		{
			try
			{
				if (FileHandler.DirectoryExists(EngineSettings.ReplacePathIdentifier(path)))
				{
					EngineSettings.SetApplicationDataFolder(path);
					RegistryHandler.SaveEngineSettings(EngineSettingsType.ApplicationDataFolder);
					RaiseApplicationDataFolderUpdatedEvent(EngineSettings.ApplicationDataFolder);
				}
				else
				{
					CommonWorker.ShowError("The specified Application Data Folder does not exist.");
					return false;
				}
			}
			catch (Exception ex)
			{
				CommonWorker.ShowError(ex);
				return false;
			}
			return true;
		}

		public bool SetSleepTime(string sleepTimeMinutes)
		{
			try
			{
				EngineSettings.SleepTime = TimeSpan.FromMinutes(double.Parse(sleepTimeMinutes));
				RegistryHandler.SaveEngineSettings(EngineSettingsType.SleepTime);
				RaiseSleepTimeUpdatedEvent(EngineSettings.SleepTime);
			}
			catch (FormatException)
			{
				CommonWorker.ShowError("Incorrect format of Sleep Time value.");
				return false;
			}
			catch (Exception ex)
			{
				CommonWorker.ShowError(ex);
				return false;
			}
			return true;
		}

		public void ToggleUseSpecificOutputFolder()
		{
			try
			{
				EngineSettings.UseSpecificOutputFolder = !EngineSettings.UseSpecificOutputFolder;
				RegistryHandler.SaveEngineSettings(EngineSettingsType.UseSpecificOutputFolder);
				RaiseUseSpecificOutputFolderUpdatedEvent(EngineSettings.UseSpecificOutputFolder);
			}
			catch (Exception ex)
			{
				CommonWorker.ShowError(ex);
			}
		}

		public void BrowseOutputFolder()
		{
			try
			{
				FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				folderBrowserDialog.Description = "Please select an output folder...";
				folderBrowserDialog.SelectedPath = EngineSettings.OutputFolderComplete;
				if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
				{
					EngineSettings.SetOutputFolder(folderBrowserDialog.SelectedPath);
					RegistryHandler.SaveEngineSettings(EngineSettingsType.OutputFolder);
					RaiseOutputFolderUpdatedEvent(EngineSettings.OutputFolder);
				}
			}
			catch (Exception ex)
			{
				CommonWorker.ShowError(ex);
			}
		}

		public bool SetOutputFolder(string path)
		{
			try
			{
				if (FileHandler.DirectoryExists(EngineSettings.ReplacePathIdentifier(path)))
				{
					EngineSettings.SetOutputFolder(path);
					RegistryHandler.SaveEngineSettings(EngineSettingsType.OutputFolder);
					RaiseOutputFolderUpdatedEvent(EngineSettings.OutputFolder);
				}
				else
				{
					CommonWorker.ShowError("The specified Output Folder does not exist.");
					return false;
				}
			}
			catch (Exception ex)
			{
				CommonWorker.ShowError(ex);
				return false;
			}
			return true;
		}

		public void AddRootPath(RootPath rootPath)
		{
			try
			{
				EngineSettings.AddRootPath(rootPath);
				RegistryHandler.SaveEngineSettings(EngineSettingsType.RootPaths);
				RaiseRootPathListUpdatedEvent(EngineSettings.RootPaths);
			}
			catch (Exception ex)
			{
				CommonWorker.ShowError(ex);
			}
		}

		public void UpdateRootPath()
		{
			try
			{
				RegistryHandler.SaveEngineSettings(EngineSettingsType.RootPaths);
				RaiseRootPathListUpdatedEvent(EngineSettings.RootPaths);
			}
			catch (Exception ex)
			{
				CommonWorker.ShowError(ex);
			}
		}

		public void RemoveRootPath(params RootPath[] rootPaths)
		{
			try
			{
				foreach (RootPath rootPath in rootPaths)
				{
					EngineSettings.RemoveRootPath(rootPath);
				}
				RegistryHandler.SaveEngineSettings(EngineSettingsType.RootPaths);
				RaiseRootPathListUpdatedEvent(EngineSettings.RootPaths);
			}
			catch (Exception ex)
			{
				CommonWorker.ShowError(ex);
			}
		}
	}
}
