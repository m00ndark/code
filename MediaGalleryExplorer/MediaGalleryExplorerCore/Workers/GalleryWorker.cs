using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using MediaGalleryExplorerCore.DataAccess;
using MediaGalleryExplorerCore.DataObjects;
using MediaGalleryExplorerCore.EventArguments;

namespace MediaGalleryExplorerCore.Workers
{
	public class GalleryWorker
	{
		#region Enumeration

		public enum OperationType
		{
			ScanSource,
			LoadGallery,
			LoadThumbnails
		}

		#endregion

		private readonly HashSet<MediaFolder> _expandedFolders;

		public event EventHandler<EventArgs> GalleryLoaded;
		public event EventHandler<StringEventArgs> StatusUpdated;
		public event EventHandler<MediaFolderEventArgs> TreeNodeAdded;
		public event EventHandler<MediaFolderEventArgs> TreeNodeRemoved;
		public event EventHandler<MediaFileEventArgs> ThumbnailAvailable;
		public event EventHandler<OperationTypeEventArgs> DatabaseOperationCompleted;

		public GalleryWorker(Gallery gallery)
		{
			_expandedFolders = new HashSet<MediaFolder>();
			Gallery = gallery;
			SelectedFile = null;
			FileSystemHandler.StatusUpdated += FileSystemHandler_StatusUpdated;
			FileSystemHandler.MediaFolderAdded += FileSystemHandler_MediaFolderAdded;
			FileSystemHandler.MediaFolderRemoved += FileSystemHandler_MediaFolderRemoved;
			FileSystemHandler.MediaFileUpdated += FileSystemHandler_MediaFileUpdated;
		}

		#region Properties

		public Gallery Gallery { get; private set; }
		public MediaFile SelectedFile { get; set; }

		#endregion

		#region Event raisers

		private void RaiseGalleryLoadedEvent()
		{
			if (GalleryLoaded != null)
			{
				GalleryLoaded(this, new EventArgs());
			}
		}

		private void RaiseStatusUpdatedEvent(string status)
		{
			if (StatusUpdated != null)
			{
				StatusUpdated(this, new StringEventArgs(status));
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
			lock (_expandedFolders)
			{
				if (e.Folder.Parent == null || _expandedFolders.Contains(e.Folder.Parent))
				{
					RaiseTreeNodeAddedEvent(e.Folder);
					if (e.Folder.SubFolders.Count > 0)
						RaiseTreeNodeAddedEvent(new MediaFolder(e.Folder));
				}
				else if (_expandedFolders.Contains(e.Folder.Parent.Parent) && e.Folder.Parent.SubFolders.Count == 1)
				{
					RaiseTreeNodeAddedEvent(new MediaFolder(e.Folder.Parent));
				}
			}
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

		#region Loading gallery

		public void LoadGallery()
		{
			new Thread(LoadGalleryThread).Start();
		}

		private void LoadGalleryThread()
		{
			try
			{
				RaiseStatusUpdatedEvent("Loading gallery...");
				Gallery gallery = Gallery;
				FileSystemHandler.LoadGallery(ref gallery);
				Gallery = gallery;
				RaiseGalleryLoadedEvent();
				RaiseDatabaseOperationCompletedEvent(OperationType.LoadGallery);
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

		#region Add source

		public void AddSource()
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog() { Description = "Please select a source root folder..." };
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				GallerySource source = new GallerySource(folderBrowserDialog.SelectedPath);
				if (!Gallery.Sources.Any(s => s.Equals(source)))
				{
					Gallery.AddSource(new GallerySource(folderBrowserDialog.SelectedPath));
					FileSystemHandler.SaveGallery(Gallery);
				}
			}
		}

		#endregion

		#region Scan source

		public void ScanSource(GallerySource source, bool reScan)
		{
			new Thread(ScanSourceThread).Start(new object[] { source, reScan });
		}

		private void ScanSourceThread(object inParam)
		{
			try
			{
				object[] inParams = (object[]) inParam;
				GallerySource source = (GallerySource) inParams[0];
				bool reScan = (bool) inParams[1];
				FileSystemHandler.PrepareDirectories();
				FileSystemHandler.InitializeImageProcessor(90L);
				FileSystemHandler.ScanFolders(Gallery, source, reScan);
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
					FileSystemHandler.LoadThumbnails(Gallery, folder);
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

		public void FolderExpanded(MediaFolder folder)
		{
			lock (_expandedFolders)
			{
				if (!_expandedFolders.Contains(folder))
				{
					_expandedFolders.Add(folder);
					folder.SubFolders.ForEach(RaiseTreeNodeAddedEvent);
					folder.SubFolders.Where(subFolder => subFolder.SubFolders.Count > 0).ToList()
						.ForEach(subFolder => RaiseTreeNodeAddedEvent(new MediaFolder(subFolder)));
				}
			}
		}

		public void FolderCollapsed(MediaFolder folder)
		{
			//_expandedFolders.Remove(folder);
		}

		#endregion
	}
}
