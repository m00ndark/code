using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
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

		private const string GALLERY_FILE_NAME = "gallery.meta";

		private static readonly List<Type> _serializableDataObjectTypes = new List<Type>()
			{
				typeof(Gallery),
				typeof(GalleryVersion),
				typeof(GallerySource),
				typeof(MediaCodec),
				typeof(FileSystemEntry),
				typeof(MediaFolder),
				typeof(MediaFile),
				typeof(ImageFile),
				typeof(VideoFile)
			};

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
				RaiseStatusUpdatedEvent("Loading gallery (" + Gallery.FilePath + ")...");
				using (GalleryDatabase database = GalleryDatabase.Open(Gallery.FilePath, Gallery.EncryptionAlgorithm, Gallery.Password, false))
				{
					using (Stream stream = database.ExtractEntry(GALLERY_FILE_NAME))
					{
						XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas());
						DataContractSerializer serializer = new DataContractSerializer(typeof(Gallery), _serializableDataObjectTypes);
						Gallery = (Gallery) serializer.ReadObject(reader, true);
						reader.Close();
					}
				}
				RaiseGalleryLoadedEvent();
				Gallery.Sources.ForEach(source => FolderAdded(source.RootFolder));
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
				if (Gallery.AddSource(source))
				{
					using (GalleryDatabase database = GalleryDatabase.Open(Gallery.FilePath, Gallery.EncryptionAlgorithm, Gallery.Password, true))
					{
						database.RegisterStreamProvider<Gallery>(GalleryMetadataStreamProvider);
						database.UpdateEntry(GALLERY_FILE_NAME, string.Empty, Gallery);
						database.Save();
					}
					FolderAdded(source.RootFolder);
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
				MediaFolder folder = (MediaFolder) data;
				using (GalleryDatabase database = GalleryDatabase.Open(Gallery.FilePath, Gallery.EncryptionAlgorithm, Gallery.Password, false))
				{
					foreach (MediaFile mediaFile in folder.Files)
					{
						bool thumbnailLoaded = false;
						string thumbnailPathName = Path.Combine(mediaFile.Source.ID, mediaFile.RelativeThumbnailPathName);
						using (Stream stream = database.ExtractEntry(thumbnailPathName))
						{
							if (stream != null && stream.Length > 0)
							{
								mediaFile.ThumbnailImage = Image.FromStream(stream);
								thumbnailLoaded = true;
							}
						}
						if (thumbnailLoaded)
							FileUpdated(mediaFile);
					}
				}
				RaiseDatabaseOperationCompletedEvent(OperationType.LoadThumbnails);
			}
			catch (Exception ex)
			{
				CommonWorker.ShowError(ex);
			}
		}

		#endregion

		#region File changes

		private void FileUpdated(MediaFile file)
		{
			RaiseThumbnailAvailableEvent(file);
		}

		#endregion

		#region File actions

		public void OpenMediaFile(MediaFile mediaFile, bool preview = false)
		{
			FileSystemHandler.OpenMediaFile(mediaFile, preview);
		}

		#endregion

		#region Folder changes

		private void FolderAdded(MediaFolder folder)
		{
			lock (_expandedFolders)
			{
				if (folder.Parent == null || _expandedFolders.Contains(folder.Parent))
				{
					RaiseTreeNodeAddedEvent(folder);
					if (folder.SubFolders.Count > 0)
						RaiseTreeNodeAddedEvent(new MediaFolder(folder));
				}
				else if (_expandedFolders.Contains(folder.Parent.Parent) && folder.Parent.SubFolders.Count == 1)
				{
					RaiseTreeNodeAddedEvent(new MediaFolder(folder.Parent));
				}
			}
		}

		private void FolderRemoved(MediaFolder folder)
		{
			RaiseTreeNodeRemovedEvent(folder);
		}

		#endregion

		#region Folder actions

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

		#endregion

		#region Stream providers

		public static Stream GalleryMetadataStreamProvider(Gallery gallery, string fileName)
		{
			MemoryStream memoryStream = new MemoryStream();
			XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(memoryStream);
			DataContractSerializer serializer = new DataContractSerializer(typeof(Gallery), _serializableDataObjectTypes);
			serializer.WriteObject(writer, gallery);
			//writer.Flush();
			writer.Close();
			memoryStream.Position = 0;
			return memoryStream;
		}


		#endregion
	}
}
