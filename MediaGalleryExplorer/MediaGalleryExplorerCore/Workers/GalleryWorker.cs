using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using MediaGalleryExplorerCore.DataAccess;
using MediaGalleryExplorerCore.DataObjects;
using MediaGalleryExplorerCore.EventArguments;
using Encoder = System.Drawing.Imaging.Encoder;

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
		private static EncoderParameters _encoderParameters;
		private static ImageCodecInfo _imageCodecInfo = null;

		public event EventHandler<EventArgs> GalleryLoaded;
		public event EventHandler<MediaFolderEventArgs> TreeNodeAdded;
		public event EventHandler<MediaFolderEventArgs> TreeNodeRemoved;
		public event EventHandler<MediaFileEventArgs> ThumbnailAvailable;
		public event EventHandler<OperationTypeEventArgs> DatabaseOperationCompleted;
		public static event EventHandler<StringEventArgs> StatusUpdated;

		public GalleryWorker(Gallery gallery)
		{
			_expandedFolders = new HashSet<MediaFolder>();
			Gallery = gallery;
			SelectedFile = null;
			InitializeImageProcessor(90L);
			FileSystemHandler.StatusUpdated += FileSystemHandler_StatusUpdated;
			FileSystemHandler.MediaFolderAdded += FileSystemHandler_MediaFolderAdded;
			FileSystemHandler.MediaFolderRemoved += FileSystemHandler_MediaFolderRemoved;
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

		private static void RaiseStatusUpdatedEvent(string status)
		{
			if (StatusUpdated != null)
			{
				StatusUpdated(null, new StringEventArgs(status));
			}
		}

		#endregion

		#region Event handlers

		private static void FileSystemHandler_StatusUpdated(object sender, StringEventArgs e)
		{
			RaiseStatusUpdatedEvent(e.Value);
		}

		private void FileSystemHandler_MediaFolderAdded(object sender, MediaFolderEventArgs e)
		{
			FolderAdded(e.Folder);
		}

		private void FileSystemHandler_MediaFolderRemoved(object sender, MediaFolderEventArgs e)
		{
			FolderRemoved(e.Folder);
		}

		#endregion

		#region Initialization

		public static void InitializeImageProcessor(long thumbnailQuality)
		{
			_encoderParameters = new EncoderParameters(1);
			_encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, thumbnailQuality);
			_imageCodecInfo = GetEncoderInfo("image/jpeg");
		}

		private static ImageCodecInfo GetEncoderInfo(String mimeType)
		{
			ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
			return encoders.FirstOrDefault(t => t.MimeType == mimeType);
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
						XmlReader reader = XmlReader.Create(stream, new XmlReaderSettings() { CheckCharacters = false });
						DataContractSerializer serializer = new DataContractSerializer(typeof(Gallery), _serializableDataObjectTypes);
						Gallery = (Gallery) serializer.ReadObject(reader, true);
						Gallery.UpdateMediaCount();
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
			new Thread(() => ScanSourceThread(source, reScan)).Start();
		}

		private void ScanSourceThread(GallerySource source, bool reScan)
		{
			try
			{
				using (GalleryDatabase database = GalleryDatabase.Open(Gallery.FilePath, Gallery.EncryptionAlgorithm, Gallery.Password, true))
				{
					database.RegisterStreamProvider<Gallery>(GalleryMetadataStreamProvider);
					database.RegisterStreamProvider<MediaFile>(MediaFileStreamProvider);
					FileSystemHandler.ScanFolders(database, source, reScan);
					source.ScanDate = DateTime.Now;
					source.UpdateMediaCount();
					database.UpdateEntry(GALLERY_FILE_NAME, string.Empty, Gallery);
					database.Save();
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
			new Thread(() => LoadThumbnailsThread(folder)).Start();
		}

		private void LoadThumbnailsThread(MediaFolder folder)
		{
			try
			{
				RaiseStatusUpdatedEvent("Loading thumbnails...");
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
			string filePath = string.Empty;
			if (mediaFile is ImageFile || !preview)
			{
				filePath = Path.Combine(mediaFile.Source.RootedPath, mediaFile.RelativePathName);
			}
			else if (mediaFile is VideoFile)
			{
				FileSystemHandler.ClearWorkingDirectory();
				using (GalleryDatabase database = GalleryDatabase.Open(Gallery.FilePath, Gallery.EncryptionAlgorithm, Gallery.Password, false))
				{
					string relativePreviewPathName = Path.Combine(mediaFile.Source.ID, mediaFile.RelativePreviewPathName);
					filePath = database.ExtractEntry(relativePreviewPathName, ObjectPool.CompleteWorkingDirectory);
				}
			}
			if (File.Exists(filePath))
			{
				Process process = new Process() { StartInfo = { FileName = filePath, Verb = "Open" } };
				process.Start();
			}
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
			XmlWriterSettings writerSettings = new XmlWriterSettings() { CloseOutput = false, Encoding = Encoding.UTF8, Indent = true, IndentChars = "\t", CheckCharacters = false };
			XmlWriter writer = XmlWriter.Create(memoryStream, writerSettings);
			DataContractSerializer serializer = new DataContractSerializer(typeof(Gallery), _serializableDataObjectTypes);
			serializer.WriteObject(writer, gallery);
			writer.Close();
			memoryStream.Position = 0;
			return memoryStream;
		}

		public static Stream MediaFileStreamProvider(MediaFile mediaFile, string fileName)
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				Image image = null;
				bool makeThumbnail = true;
				const double maxThumbnailSize = 200.0;
				string filePathName = Path.Combine(mediaFile.Source.RootedPath, mediaFile.RelativePathName);
				RaiseStatusUpdatedEvent("Generating thumbnail for " + (mediaFile is ImageFile ? "image" : "video") + " file named \""
					+ mediaFile.Name + "\" in " + Path.Combine(mediaFile.Source.RootedPath, mediaFile.Parent.RelativePathName));
				if (mediaFile is ImageFile)
				{
					ImageFile imageFile = (mediaFile as ImageFile);
					image = Image.FromFile(filePathName);
					if (imageFile.Size.Width == 0 && imageFile.Size.Height == 0)
						imageFile.Size = image.Size;
				}
				else if (mediaFile is VideoFile)
				{
					VideoFile videoFile = (mediaFile as VideoFile);
					try
					{
						string previewPathName = Path.Combine(Path.GetDirectoryName(filePathName), videoFile.PreviewName);
						string workingPreviewPathName = Path.Combine(ObjectPool.CompleteWorkingDirectory, videoFile.PreviewName);
						if (!File.Exists(workingPreviewPathName))
						{
							Process vtmProcess = new Process()
								{
									StartInfo =
										{
											FileName = ObjectPool.VideoThumbnailsMakerPath,
											Arguments = "\"" + ObjectPool.CompleteVideoThumbnailsMakerPresetPath + "\" \"" + filePathName + "\""
										}
								};
							vtmProcess.Start();
							if (!vtmProcess.WaitForExit(30000))
								vtmProcess.Kill();
							if (File.Exists(previewPathName))
								File.Move(previewPathName, workingPreviewPathName);
						}
						if (File.Exists(workingPreviewPathName))
						{
							image = Image.FromFile(workingPreviewPathName);
							makeThumbnail = (Path.GetFileName(fileName) == videoFile.ThumbnailName);
						}
					}
					catch { ; }
				}
				if (image != null)
				{
					if (makeThumbnail)
					{
						double factor = Math.Min(maxThumbnailSize / image.Size.Width, maxThumbnailSize / image.Size.Height);
						Image thumbnailImage = image.GetThumbnailImage((int) Math.Round(factor * image.Size.Width, MidpointRounding.AwayFromZero),
							(int) Math.Round(factor * image.Size.Height, MidpointRounding.AwayFromZero), () => false, IntPtr.Zero);
						image.Dispose();
						image = thumbnailImage;
					}
					image.Save(memoryStream, _imageCodecInfo, _encoderParameters);
					image.Dispose();
					memoryStream.Position = 0;
				}
			}
			catch { ; }
			return memoryStream;
		}

		#endregion
	}
}
