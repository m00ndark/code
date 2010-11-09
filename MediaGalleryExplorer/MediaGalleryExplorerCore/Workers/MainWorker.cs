using System;
using System.Collections.Generic;
using System.Threading;
using MediaGalleryExplorerCore.DataAccess;
using MediaGalleryExplorerCore.DataObjects;
using MediaGalleryExplorerCore.EventArguments;

namespace MediaGalleryExplorerCore.Workers
{
	public class MainWorker
	{
		#region Enumeration

		public enum OperationType
		{
			ScanSource,
			LoadSources,
			LoadThumbnails
		}

		#endregion

		public event EventHandler<StringEventArgs> StatusUpdated;
		public event EventHandler<SourceListEventArgs> SourceListUpdated;
		public event EventHandler<MediaFolderEventArgs> TreeNodeAdded;
		public event EventHandler<MediaFolderEventArgs> TreeNodeRemoved;
		public event EventHandler<MediaFileEventArgs> ThumbnailAvailable;
		public event EventHandler<OperationTypeEventArgs> DatabaseOperationCompleted;

		public MainWorker()
		{
			SelectedFile = null;
			FileSystemHandler.StatusUpdated += FileSystemHandler_StatusUpdated;
			FileSystemHandler.MediaFolderAdded += FileSystemHandler_MediaFolderAdded;
			FileSystemHandler.MediaFolderRemoved += FileSystemHandler_MediaFolderRemoved;
			FileSystemHandler.MediaFileUpdated += FileSystemHandler_MediaFileUpdated;
		}

		#region Properties

		public MediaFile SelectedFile { get; set; }

		#endregion

		#region Event raisers

		private void RaiseStatusUpdatedEvent(string status)
		{
			if (StatusUpdated != null)
			{
				StatusUpdated(this, new StringEventArgs(status));
			}
		}

		private void RaiseSourceListUpdatedEvent(List<GallerySource> sources)
		{
			if (SourceListUpdated != null)
			{
				SourceListUpdated(this, new SourceListEventArgs(sources));
			}
		}

		private void RaiseTreeNodeAddedEvent(MediaFolder folder)
		{
			if (TreeNodeAdded != null)
			{
				TreeNodeAdded(this, new MediaFolderEventArgs(folder));
			}
		}

		private void RaiseTreeNodeRemovedEvent(MediaFolder folder)
		{
			if (TreeNodeRemoved != null)
			{
				TreeNodeRemoved(this, new MediaFolderEventArgs(folder));
			}
		}

		private void RaiseThumbnailAvailableEvent(MediaFile file)
		{
			if (ThumbnailAvailable != null)
			{
				ThumbnailAvailable(this, new MediaFileEventArgs(file));
			}
		}

		private void RaiseDatabaseOperationCompletedEvent(OperationType operationType)
		{
			if (DatabaseOperationCompleted != null)
			{
				DatabaseOperationCompleted(this, new OperationTypeEventArgs(operationType));
			}
		}

		#endregion

		#region Event handlers

		private void FileSystemHandler_StatusUpdated(object sender, StringEventArgs e)
		{
			RaiseStatusUpdatedEvent(e.Value);
		}

		private void FileSystemHandler_MediaFolderAdded(object sender, MediaFolderEventArgs e)
		{
			RaiseTreeNodeAddedEvent(e.Folder);
		}

		private void FileSystemHandler_MediaFolderRemoved(object sender, MediaFolderEventArgs e)
		{
			RaiseTreeNodeRemovedEvent(e.Folder);
		}

		private void FileSystemHandler_MediaFileUpdated(object sender, MediaFileEventArgs e)
		{
			RaiseThumbnailAvailableEvent(e.File);
		}

		#endregion

		#region Operations

		#region Initialize

		public void Initialize()
		{
			ObjectPool.Initialize();
			RegistryHandler.LoadSettings();
			RaiseSourceListUpdatedEvent(ObjectPool.Sources);
		}

		public void LoadSources()
		{
			new Thread(LoadSourcesThread).Start();
		}

		private void LoadSourcesThread()
		{
			try
			{
				RaiseStatusUpdatedEvent("Loading sources...");
				FileSystemHandler.LoadMetaDatabases(ObjectPool.Sources);
				RaiseDatabaseOperationCompletedEvent(OperationType.LoadSources);
			}
			catch (Exception ex)
			{
				CommonWorker.ShowError(ex);
			}
		}

		#endregion

		#region Clean up

		public void CleanUp()
		{
			FileSystemHandler.ClearWorkingDirectory();
		}

		#endregion

		#region Scan source

		public void ScanSource(GallerySource source, bool reScan)
		{
			new Thread(ScanSourceThread).Start(new object[] { source, reScan });
		}

		private void ScanSourceThread(object data)
		{
			try
			{
				object[] parameters = data as object[];
				if (parameters != null && parameters.Length == 2)
				{
					GallerySource source = (parameters[0] as GallerySource);
					if (source != null && parameters[1] is bool)
					{
						FileSystemHandler.PrepareDirectories();
						FileSystemHandler.InitializeImageProcessor(90L);
						FileSystemHandler.ScanFolders(source, (bool) parameters[1]);
					}
				}
				RaiseDatabaseOperationCompletedEvent(OperationType.ScanSource);
			}
			catch (Exception ex)
			{
				CommonWorker.ShowError(ex);
			}
		}

		#endregion

		#region Load thumbnails

		public void LoadThumbnails(MediaFolder folder)
		{
			new Thread(LoadThumbnailsThread).Start(folder);
		}

		private void LoadThumbnailsThread(object data)
		{
			try
			{
				RaiseStatusUpdatedEvent("Loading thumbnails...");
				MediaFolder folder = (data as MediaFolder);
				if	(folder != null)
				{
					FileSystemHandler.LoadThumbnails(folder);
				}
				RaiseDatabaseOperationCompletedEvent(OperationType.LoadThumbnails);
			}
			catch (Exception ex)
			{
				CommonWorker.ShowError(ex);
			}
		}

		#endregion

		#region File actions

		public void OpenMediaFile(MediaFile mediaFile)
		{
			OpenMediaFile(mediaFile, false);
		}

		public void OpenMediaFile(MediaFile mediaFile, bool preview)
		{
			FileSystemHandler.OpenMediaFile(mediaFile, preview);
		}

		#endregion

		#endregion
	}
}
