using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using MediaGalleryExplorerCore.DataAccess;
using MediaGalleryExplorerCore.DataObjects;
using MediaGalleryExplorerCore.EventArguments;

namespace MediaGalleryExplorerCore.Workers
{
	public class SettingsWorker
	{
		public event EventHandler<StringEventArgs> DatabaseLocationUpdated;
		public event EventHandler<StringEventArgs> WorkingDirectoryUpdated;
		public event EventHandler<StringEventArgs> VideoThumbnailsMakerUpdated;
		public event EventHandler<StringEventArgs> VideoThumbnailsMakerPresetUpdated;
		public event EventHandler<SourceListEventArgs> SourceListUpdated;

		#region Event raisers

		private void RaiseDatabaseLocationUpdatedEvent(string databasePath)
		{
			if (DatabaseLocationUpdated != null)
			{
				DatabaseLocationUpdated(this, new StringEventArgs(databasePath));
			}
		}

		private void RaiseWorkingDirectoryUpdatedEvent(string workingDirectory)
		{
			if (WorkingDirectoryUpdated != null)
			{
				WorkingDirectoryUpdated(this, new StringEventArgs(workingDirectory));
			}
		}

		private void RaiseVideoThumbnailsMakerUpdatedEvent(string videoThumbnailsMakerPath)
		{
			if (VideoThumbnailsMakerUpdated != null)
			{
				VideoThumbnailsMakerUpdated(this, new StringEventArgs(videoThumbnailsMakerPath));
			}
		}

		private void RaiseVideoThumbnailsMakerPresetUpdatedEvent(string videoThumbnailsMakerPresetPath)
		{
			if (VideoThumbnailsMakerPresetUpdated != null)
			{
				VideoThumbnailsMakerPresetUpdated(this, new StringEventArgs(videoThumbnailsMakerPresetPath));
			}
		}

		private void RaiseSourceListUpdatedEvent(List<GallerySource> sources)
		{
			if (SourceListUpdated != null)
			{
				SourceListUpdated(this, new SourceListEventArgs(sources));
			}
		}

		#endregion

		#region Operations

		public void Initialize()
		{
			//RaiseDatabaseLocationUpdatedEvent(ObjectPool.DatabaseLocation);
			RaiseWorkingDirectoryUpdatedEvent(ObjectPool.WorkingDirectory);
			RaiseVideoThumbnailsMakerUpdatedEvent(ObjectPool.VideoThumbnailsMakerPath);
			RaiseVideoThumbnailsMakerPresetUpdatedEvent(ObjectPool.VideoThumbnailsMakerPresetPath);
			RaiseSourceListUpdatedEvent(ObjectPool.Sources);
		}

		public void Close()
		{
			RegistryHandler.SaveSettings();
		}

		public void SetDatabaseLocation()
		{
			//try
			//{
			//   FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			//   folderBrowserDialog.Description = "Please select a database root folder...";
			//   folderBrowserDialog.SelectedPath = ObjectPool.CompleteDatabaseLocation;
			//   if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			//   {
			//      ObjectPool.SetDatabaseLocation(folderBrowserDialog.SelectedPath);
			//      RegistryHandler.SaveSettings(SettingsType.DatabaseLocation);
			//      RaiseDatabaseLocationUpdatedEvent(ObjectPool.DatabaseLocation);
			//   }
			//}
			//catch (Exception ex)
			//{
			//   CommonWorker.ShowError(ex);
			//}
		}

		public void SetWorkingDirectory()
		{
			try
			{
				FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				folderBrowserDialog.Description = "Please select a working directory...";
				folderBrowserDialog.SelectedPath = ObjectPool.CompleteWorkingDirectory;
				if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
				{
					ObjectPool.SetWorkingDirectory(folderBrowserDialog.SelectedPath);
					RegistryHandler.SaveSettings(SettingsType.WorkingDirectory);
					RaiseWorkingDirectoryUpdatedEvent(ObjectPool.WorkingDirectory);
				}
			}
			catch (Exception ex)
			{
				CommonWorker.ShowError(ex);
			}
		}

		public void SetVideoThumbnailsMaker()
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Title = "Please select the Video Thumbnails Maker executable...";
				openFileDialog.InitialDirectory = Path.GetDirectoryName(ObjectPool.VideoThumbnailsMakerPath);
				openFileDialog.FileName = Path.GetFileName(ObjectPool.VideoThumbnailsMakerPath);
				openFileDialog.Filter = "VideoThumbnailsMaker.exe|VideoThumbnailsMaker.exe|All files (*.*)|*.*";
				openFileDialog.FilterIndex = 0;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					ObjectPool.SetVideoThumbnailsMakerPath(openFileDialog.FileName);
					RegistryHandler.SaveSettings(SettingsType.VideoThumbnailsMakerPath);
					RaiseVideoThumbnailsMakerUpdatedEvent(ObjectPool.VideoThumbnailsMakerPath);
				}
			}
			catch (Exception ex)
			{
				CommonWorker.ShowError(ex);
			}
		}

		public void SetVideoThumbnailsMakerPreset()
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Title = "Please select a Video Thumbnails Maker preset file...";
				openFileDialog.InitialDirectory = Path.GetDirectoryName(ObjectPool.CompleteVideoThumbnailsMakerPresetPath);
				openFileDialog.FileName = Path.GetFileName(ObjectPool.CompleteVideoThumbnailsMakerPresetPath);
				openFileDialog.Filter = "Video Thumbnails Maker presets (*.vtm)|*.vtm|All files (*.*)|*.*";
				openFileDialog.FilterIndex = 0;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					ObjectPool.SetVideoThumbnailsMakerPresetPath(openFileDialog.FileName);
					RegistryHandler.SaveSettings(SettingsType.VideoThumbnailsMakerPresetPath);
					RaiseVideoThumbnailsMakerPresetUpdatedEvent(ObjectPool.VideoThumbnailsMakerPresetPath);
				}
			}
			catch (Exception ex)
			{
				CommonWorker.ShowError(ex);
			}
		}

		#endregion
	}
}
