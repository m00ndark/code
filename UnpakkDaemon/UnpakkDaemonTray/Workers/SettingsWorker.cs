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

		public void SetApplicationDataFolder()
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
