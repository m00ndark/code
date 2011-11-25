using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.Serialization;
using System.Xml;
using Ionic.Zip;
using MediaGalleryExplorerCore.DataObjects;
using MediaGalleryExplorerCore.DataObjects.Serialization;
using MediaGalleryExplorerCore.EventArguments;

namespace MediaGalleryExplorerCore.DataAccess
{
	public static class FileSystemHandler
	{
		#region Nested classes

		private class DatabaseState
		{
			public DatabaseState()
			{
				GalleryEntry = null;
				ImageEntries = new Dictionary<ZipEntry, MediaFile>();
			}

			#region Properties

			public Tuple<ZipEntry, Gallery> GalleryEntry { get; set; }
			public IDictionary<ZipEntry, MediaFile> ImageEntries { get; private set; }

			#endregion

			public void Clear()
			{
				GalleryEntry = null;
				ImageEntries.Clear();
			}
		}

		#endregion

		private const string DATABASE_FILE_EXTENSION = ".mgdb";
		private const string METAFILE_FILE_EXTENSION = ".meta";
		private static readonly string GALLERY_FILE_NAME = "gallery" + METAFILE_FILE_EXTENSION;

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

		private static readonly object _galleryAccessLock = new object();
		private static readonly DatabaseState _databaseState = new DatabaseState();
		private static readonly EncoderParameters _encoderParameters = new EncoderParameters(1);
		private static ImageCodecInfo _imageCodecInfo = null;

		public static event EventHandler<StringEventArgs> StatusUpdated;
		public static event EventHandler<MediaFolderEventArgs> MediaFolderAdded;
		public static event EventHandler<MediaFolderEventArgs> MediaFolderRemoved;
		public static event EventHandler<MediaFileEventArgs> MediaFileUpdated;

		#region Event raisers

		private static void RaiseStatusUpdatedEvent(string status)
		{
			if (StatusUpdated != null)
			{
				StatusUpdated(null, new StringEventArgs(status));
			}
		}

		private static void RaiseMediaFolderAddedEvent(MediaFolder folder)
		{
			if (MediaFolderAdded != null)
			{
				MediaFolderAdded(null, new MediaFolderEventArgs(folder));
			}
		}

		private static void RaiseMediaFolderRemovedEvent(MediaFolder folder)
		{
			if (MediaFolderRemoved != null)
			{
				MediaFolderRemoved(null, new MediaFolderEventArgs(folder));
			}
		}

		private static void RaiseMediaFileUpdatedEvent(MediaFile file)
		{
			if (MediaFileUpdated != null)
			{
				MediaFileUpdated(null, new MediaFileEventArgs(file));
			}
		}

		#endregion

		#region General

		public static void PrepareDirectories()
		{
			Directory.CreateDirectory(ObjectPool.CompleteWorkingDirectory);
		}

		public static void ClearWorkingDirectory()
		{
			try
			{
				foreach (string directory in Directory.GetDirectories(ObjectPool.CompleteWorkingDirectory))
				{
					Directory.Delete(directory, true);
				}
				foreach (string file in Directory.GetFiles(ObjectPool.CompleteWorkingDirectory))
				{
					File.Delete(file);
				}
			}
			catch { ; }
		}

		public static IDictionary<string, int> GetEncryptionAlgorithms()
		{
			IDictionary<string, int> encryptionAlgorithms = new Dictionary<string, int>()
				{
					{ "None", (int) EncryptionAlgorithm.None },
					{ "Zip 2.0", (int) EncryptionAlgorithm.PkzipWeak },
					{ "WinZip AES 128-bit", (int) EncryptionAlgorithm.WinZipAes128 },
					{ "WinZip AES 256-bit", (int) EncryptionAlgorithm.WinZipAes256 }
				};
			return encryptionAlgorithms;
		}

		public static bool PathNameIsValid(string filePath, bool checkFileExists = false)
		{
			// filePath should be a valid existing path with a valid file name

			try
			{
				if (Directory.Exists(filePath))
					return false;

				if (!Directory.Exists(Path.GetDirectoryName(filePath)))
					return false;

				if (File.Exists(filePath))
					return true;

				return !Path.GetFileName(filePath).Any(ch => Path.GetInvalidFileNameChars().Contains(ch));
			}
			catch
			{
				return false;
			}
		}

		public static void GetVolumeInfo(string path, out string volumeLetter, out string volumeName, out string volumeSerial)
		{
			if (!Path.IsPathRooted(path))
				throw new ArgumentException("Path is not rooted", "path");

			if (path.StartsWith(@"\\"))
				throw new ArgumentException("Path is a network path", "path");

			string pathRoot = Path.GetPathRoot(path);

			if (pathRoot == null)
				throw new Exception("Unable to extract path root from <" + path + ">");

			string pathRootName = pathRoot.Substring(0, 2).ToUpper();

			ManagementObjectSearcher searcher = new ManagementObjectSearcher("select Name, VolumeSerialNumber, VolumeName from Win32_LogicalDisk where Name = '" + pathRootName + "'");
			ManagementObjectCollection mgmtObjCollection = searcher.Get();

			if (mgmtObjCollection.Count == 0)
				throw new Exception("Could not find volume with path root name <" + pathRootName + ">");

			ManagementObject mgmtObj = mgmtObjCollection.Cast<ManagementObject>().First();
			volumeLetter = mgmtObj.Properties["Name"].Value.ToString();
			volumeName = mgmtObj.Properties["VolumeName"].Value.ToString();
			volumeSerial = mgmtObj.Properties["VolumeSerialNumber"].Value.ToString();
		}

		public static string GetVolumeLetter(string volumeSerial)
		{
			ManagementObjectSearcher searcher = new ManagementObjectSearcher("select Name from Win32_LogicalDisk where VolumeSerialNumber = '" + volumeSerial + "'");
			ManagementObjectCollection mgmtObjCollection = searcher.Get();

			if (mgmtObjCollection.Count == 0)
				throw new Exception("Could not find volume with serial number <" + volumeSerial + ">");

			ManagementObject mgmtObj = mgmtObjCollection.Cast<ManagementObject>().First();
			return mgmtObj.Properties["Name"].Value.ToString();
		}

		#endregion

		#region Scanning

		public static void ScanFolders(Gallery gallery, GallerySource source, bool reScan)
		{
			try
			{
				//lock (_galleryAccessLock)
				{
					ClearWorkingDirectory();
					_databaseState.Clear();
					int folderCount = 0, fileCount = 0;
					using (ZipFile galleryDatabase = OpenGalleryDatabase(gallery, true))
					{
						ScanSubFolder(source.RootFolder, source, galleryDatabase, reScan, ref folderCount, ref fileCount, 0);
						source.ScanDate = DateTime.Now;
						CreateGalleryEntry(galleryDatabase, gallery);
						SaveGalleryDatabase(galleryDatabase);
					}
				}
				//RegistryHandler.SaveSettings(SettingsType.GallerySource);
			}
			finally
			{
				ClearWorkingDirectory();
			}
		}

		private static void SourceDatabase_SaveProgress(object sender, SaveProgressEventArgs e)
		{
			if (e.EventType == ZipProgressEventType.Saving_BeforeWriteEntry && e.CurrentEntry.Source == ZipEntrySource.Stream &&
				  e.CurrentEntry.InputStream == Stream.Null)
			{
				if (_databaseState.ImageEntries.ContainsKey(e.CurrentEntry))
					e.CurrentEntry.InputStream = GetDatabaseImageStream(_databaseState.ImageEntries[e.CurrentEntry], e.CurrentEntry.FileName);
				else if (_databaseState.GalleryEntry.Item1 == e.CurrentEntry)
					e.CurrentEntry.InputStream = GetGalleryMetadataStream(_databaseState.GalleryEntry.Item2);
				else
					e.Cancel = true;
			}
			else if (e.EventType == ZipProgressEventType.Saving_AfterWriteEntry && e.CurrentEntry.InputStreamWasJitProvided)
			{
				e.CurrentEntry.InputStream.Close();
			}
		}

		private static bool ScanSubFolder(MediaFolder folder, GallerySource source, ZipFile sourceDatabase,
			bool reScan, ref int folderCount, ref int fileCount, int depth)
		{
			try
			{
				folderCount++;
				RaiseStatusUpdatedEvent("Scanning folder #" + folderCount + " at depth " + depth + ", found " + fileCount + " files...");
				GetFolders(folder, source);
				GetFiles(folder, source, sourceDatabase, reScan);

				if (folder.SubFolders.Count == 0 && folder.Files.Count == 0)
					return false;

				RaiseMediaFolderAddedEvent(folder);
				depth++;
				fileCount += folder.Files.Count;
				List<MediaFolder> emptyFolders = new List<MediaFolder>();
				foreach (MediaFolder subFolder in folder.SubFolders)
				{
					if (!ScanSubFolder(subFolder, source, sourceDatabase, reScan, ref folderCount, ref fileCount, depth))
						emptyFolders.Add(subFolder);
				}
				foreach (MediaFolder emptyFolder in emptyFolders)
				{
					folder.SubFolders.Remove(emptyFolder);
				}

				if (folder.SubFolders.Count == 0 && folder.Files.Count == 0)
				{
					RaiseMediaFolderRemovedEvent(folder);
					return false;
				}
			}
			catch { }
			return true;
		}

		private static void GetFiles(MediaFolder parent, GallerySource source, ZipFile sourceDatabase, bool reScan)
		{
			List<string> files = Directory.GetFiles(Path.Combine(source.RootedPath, parent.RelativePathName)).ToList();
			files.Sort();
			foreach (string file in files)
			{
				try
				{
					string extension = Path.GetExtension(file).ToLower();
					string fileName = Path.GetFileName(file);
					string relativePath = RemoveAbsolutePath(Path.GetDirectoryName(file), source.RootedPath);
					FileInfo fileInfo = new FileInfo(file);
					if (MediaFile.IMAGE_FILE_EXTENSIONS.Contains(extension))
					{
						if (!files.Any(filePath => (Path.GetFileName(filePath) == Path.GetFileNameWithoutExtension(file))))
						{
							ImageFile imageFile = new ImageFile(fileName, relativePath, parent, source)
								{
									FileSize = fileInfo.Length,
									ThumbnailName = fileName + ".tn.jpg"
								};
							try
							{
								imageFile.Size = ImageFileHelper.GetDimensions(file);
							}
							catch {}
							int existingIndex = parent.Files.FindIndex(mediaFile => mediaFile.Name == fileName);
							if (existingIndex != -1) parent.Files.RemoveAt(existingIndex);
							parent.Files.Add(imageFile);
							parent.IncreaseImageCount();
							AddDatabaseImageEntry(imageFile, sourceDatabase, reScan);
						}
					}
					else if (MediaFile.VIDEO_FILE_EXTENSIONS.Contains(extension))
					{
						VideoFile videoFile = new VideoFile(fileName, relativePath, parent, source)
							{
								FileSize = fileInfo.Length,
								ThumbnailName = fileName + ".tn.jpg",
								PreviewName = fileName + ".jpg"
							};
						try
						{
							TagLib.File tagFile = TagLib.File.Create(file);
							videoFile.Duration = tagFile.Properties.Duration;
							videoFile.Size = new Size(tagFile.Properties.VideoWidth, tagFile.Properties.VideoHeight);
							foreach (TagLib.ICodec codec in tagFile.Properties.Codecs)
							{
								MediaCodec mediaCodec = new MediaCodec(MediaCodec.TranslateCodecType(codec.MediaTypes), codec.Description);
								if (source.Codecs.Contains(mediaCodec))
									mediaCodec = source.Codecs.First(x => x.Equals(mediaCodec));
								else
									source.Codecs.Add(mediaCodec);
								videoFile.Codecs.Add(mediaCodec);
							}
						}
						catch {}
						int existingIndex = parent.Files.FindIndex(mediaFile => mediaFile.Name == fileName);
						if (existingIndex != -1) parent.Files.RemoveAt(existingIndex);
						parent.Files.Add(videoFile);
						parent.IncreaseVideoCount();
						AddDatabaseImageEntry(videoFile, sourceDatabase, reScan);
					}
				}
				catch {}
			}
		}

		private static void GetFolders(MediaFolder parent, GallerySource source)
		{
			List<string> directories = Directory.GetDirectories(Path.Combine(source.RootedPath, parent.RelativePathName)).ToList();
			directories.Sort();
			foreach (string directory in directories)
			{
				string path = directory.TrimEnd(Path.PathSeparator);
				string folderName = Path.GetFileName(path);
				string relativePath = RemoveAbsolutePath(Path.GetDirectoryName(path), source.RootedPath);
				int existingIndex = parent.SubFolders.FindIndex(mediaFile => mediaFile.Name == folderName);
				if (existingIndex != -1) parent.SubFolders.RemoveAt(existingIndex);
				parent.SubFolders.Add(new MediaFolder(folderName, relativePath, parent, source));
			}
		}

		private static void AddDatabaseImageEntry(MediaFile mediaFile, ZipFile sourceDatabase, bool reScan)
		{
			ZipEntry zipEntry;
			string path = Path.Combine(mediaFile.Source.ID, mediaFile.RelativePath);
			if (mediaFile is VideoFile)
			{
				// obs! hitta ett sätt att läsa upp image size utan att läsa in bilden
				if (reScan || !sourceDatabase.EntryFileNames.Any(entry => entry.Equals(Path.Combine(path, mediaFile.PreviewName)
					.Replace(Path.DirectorySeparatorChar, '/'), StringComparison.CurrentCultureIgnoreCase)))
				{
					zipEntry = sourceDatabase.UpdateEntry(mediaFile.PreviewName, path, Stream.Null);
					_databaseState.ImageEntries.Add(zipEntry, mediaFile);
				}
			}
			if (reScan || !sourceDatabase.EntryFileNames.Any(entry => entry.Equals(Path.Combine(path, mediaFile.ThumbnailName)
				.Replace(Path.DirectorySeparatorChar, '/'), StringComparison.CurrentCultureIgnoreCase)))
			{
				zipEntry = sourceDatabase.UpdateEntry(mediaFile.ThumbnailName, path, Stream.Null);
				_databaseState.ImageEntries.Add(zipEntry, mediaFile);
			}
		}

		private static string RemoveAbsolutePath(string fullPath, string absolutePath)
		{
			return fullPath.Replace(absolutePath, string.Empty).Trim("\\".ToCharArray());
		}

		#endregion

		#region Thumbnails

		public static void InitializeImageProcessor(long thumbnailQuality)
		{
			_encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, thumbnailQuality);
			_imageCodecInfo = GetEncoderInfo("image/jpeg");
		}

		private static ImageCodecInfo GetEncoderInfo(String mimeType)
		{
			ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
			for (int i = 0; i < encoders.Length; ++i)
			{
				if (encoders[i].MimeType == mimeType)
					return encoders[i];
			}
			return null;
		}

		private static Stream GetDatabaseImageStream(MediaFile mediaFile, string relativeFilePathName)
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
							Process vtmProcess = new Process();
							vtmProcess.StartInfo.FileName = ObjectPool.VideoThumbnailsMakerPath;
							vtmProcess.StartInfo.Arguments = "\"" + ObjectPool.CompleteVideoThumbnailsMakerPresetPath + "\" \"" + filePathName + "\"";
							vtmProcess.Start();
							if (!vtmProcess.WaitForExit(30000)) vtmProcess.Kill();
							if (File.Exists(previewPathName))
								File.Move(previewPathName, workingPreviewPathName);
						}
						if (File.Exists(workingPreviewPathName))
						{
							image = Image.FromFile(workingPreviewPathName);
							makeThumbnail = (Path.GetFileName(relativeFilePathName) == videoFile.ThumbnailName);
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

		public static void LoadThumbnails(Gallery gallery, MediaFolder folder)
		{
			lock (_galleryAccessLock)
			{
				if (!File.Exists(gallery.FilePath)) return;

				using (ZipFile galleryDatabase = OpenGalleryDatabase(gallery, false))
				{
					foreach (MediaFile mediaFile in folder.Files)
					{
						MemoryStream memoryStream = new MemoryStream();
						string thumbnailPathName = Path.Combine(mediaFile.Source.ID, mediaFile.RelativeThumbnailPathName);
						ZipEntry zipEntry = galleryDatabase[thumbnailPathName];
						if (zipEntry != null)
						{
							zipEntry.Extract(memoryStream);
							if (memoryStream.Length > 0)
							{
								memoryStream.Position = 0;
								mediaFile.ThumbnailImage = Image.FromStream(memoryStream);
								memoryStream.Close();
								RaiseMediaFileUpdatedEvent(mediaFile);
							}
						}
					}
				}
			}
		}

		#endregion

		#region Source database

		private static void SerializeSource(StreamWriter writer, GallerySource source)
		{
			writer.WriteLine(source.Serialize());
			foreach (MediaCodec mediaCodec in source.Codecs)
			{
				writer.WriteLine(mediaCodec.Serialize());
			}
			SerializeSubFolders(writer, source.RootFolder);
		}

		private static void SerializeSubFolders(StreamWriter writer, MediaFolder folder)
		{
			writer.WriteLine(folder.Serialize());
			foreach (MediaFile mediaFile in folder.Files)
			{
				writer.WriteLine(mediaFile.Serialize());
			}
			foreach (MediaFolder subFolder in folder.SubFolders)
			{
				SerializeSubFolders(writer, subFolder);
			}
		}

		private static void DeserializeSource(StreamReader reader, GallerySource source)
		{
			string objectPrefix;
			string[] deserializedObject = ObjectSerializer.Deserialize(reader.ReadLine(), out objectPrefix);
			if (objectPrefix != "GS") throw new Exception("Failed to deserialize source database; gallery source missing");
			string rootFolderID = source.LoadFromDeserialized(deserializedObject);

			IDictionary<string, FileSystemEntry> fileSystemEntries = new Dictionary<string, FileSystemEntry>();
			string serializedObject = reader.ReadLine();
			while (serializedObject != null)
			{
				try
				{
					deserializedObject = ObjectSerializer.Deserialize(serializedObject, out objectPrefix);
					switch (objectPrefix)
					{
						case "MC":
							MediaCodec mediaCodec = new MediaCodec();
							mediaCodec.LoadFromDeserialized(deserializedObject);
							source.Codecs.Add(mediaCodec);
							break;
						default:
							DeserializeSubFolders(fileSystemEntries, objectPrefix, deserializedObject, source);
							break;
					}
				}
				catch { }
				if (fileSystemEntries.Count % 100 == 0)
				{
					RaiseStatusUpdatedEvent("Loading source (" + source.RootedPath + ")... " + (100 * (double) fileSystemEntries.Count / (source.ImageCount + source.VideoCount)).ToString("0.0") + "%");
				}
				serializedObject = reader.ReadLine();
			}
			if (!fileSystemEntries.ContainsKey(rootFolderID)) throw new Exception("Failed to deserialize source database; root object does not exist");
			MediaFolder rootFolder = fileSystemEntries[rootFolderID] as MediaFolder;
			if (rootFolder == null) throw new Exception("Failed to deserialize source database; parent root object is not a folder");
			source.RootFolder = rootFolder;
		}

		private static void DeserializeSubFolders(IDictionary<string, FileSystemEntry> fileSystemEntries, string objectPrefix, string[] deserializedObject, GallerySource source)
		{
			FileSystemEntry fileSystemEntry = null;
			switch (objectPrefix)
			{
				case "FO":
					fileSystemEntry = new MediaFolder(source);
					break;
				case "IF":
					fileSystemEntry = new ImageFile(source);
					break;
				case "VF":
					fileSystemEntry = new VideoFile(source);
					break;
			}
			if (fileSystemEntry == null) throw new Exception("Failed to deserialize file system entry of source database; invalid object type");
			string parentID = fileSystemEntry.LoadFromDeserialized(deserializedObject);
			if (!string.IsNullOrEmpty(parentID))
			{
				if (!fileSystemEntries.ContainsKey(parentID)) throw new Exception("Failed to deserialize file system entry of source database; parent object does not exist");
				MediaFolder parentFolder = fileSystemEntries[parentID] as MediaFolder;
				if (parentFolder == null) throw new Exception("Failed to deserialize file system entry of source database; parent object is not a folder");
				fileSystemEntry.SetParent(parentFolder);
				if (fileSystemEntry is MediaFolder)
				{
					parentFolder.SubFolders.Add((MediaFolder) fileSystemEntry);
				}
				else
				{
					parentFolder.Files.Add((MediaFile) fileSystemEntry);
				}
			}
			fileSystemEntries.Add(fileSystemEntry.ID, fileSystemEntry);
			if (fileSystemEntry is MediaFolder)
				RaiseMediaFolderAddedEvent((MediaFolder) fileSystemEntry);
		}

		#endregion

		#region Source media files

		public static void OpenMediaFile(MediaFile mediaFile, bool preview)
		{
			//string filePath = string.Empty;
			//if (mediaFile is ImageFile || !preview)
			//{
			//   filePath = Path.Combine(mediaFile.Source.Path, mediaFile.RelativePathName);
			//}
			//else if (mediaFile is VideoFile)
			//{
			//   ClearWorkingDirectory();
			//   string databaseFileName = Path.ChangeExtension(mediaFile.Source.ID, DATABASE_FILE_EXTENSION);
			//   string databaseFilePath = Path.Combine(ObjectPool.CompleteDatabaseLocation, databaseFileName);
			//   if (File.Exists(databaseFilePath))
			//   {
			//      using (ZipFile sourceDatabase = ZipFile.Read(databaseFilePath))
			//      {
			//         string relativePreviewPathName = Path.Combine(mediaFile.Source.ID, mediaFile.RelativePreviewPathName);
			//         ZipEntry zipEntry = sourceDatabase[relativePreviewPathName];
			//         zipEntry.Extract(ObjectPool.CompleteWorkingDirectory, ExtractExistingFileAction.OverwriteSilently);
			//         filePath = Path.Combine(ObjectPool.CompleteWorkingDirectory, relativePreviewPathName);
			//      }
			//   }
			//}
			//if (File.Exists(filePath))
			//{
			//   Process process = new Process() { StartInfo = { FileName = filePath, Verb = "Open" } };
			//   process.Start();
			//}
		}

		#endregion

		#region Gallery database

		public static void LoadGallery(ref Gallery gallery)
		{
			lock (_galleryAccessLock)
			{
				using (ZipFile galleryDatabase = OpenGalleryDatabase(gallery, false))
				{
					RaiseStatusUpdatedEvent("Loading gallery (" + gallery.FilePath + ")...");
					MemoryStream memoryStream = new MemoryStream();
					ZipEntry zipEntry = galleryDatabase[GALLERY_FILE_NAME];
					zipEntry.Extract(memoryStream);
					memoryStream.Position = 0;
					XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(memoryStream, new XmlDictionaryReaderQuotas());
					DataContractSerializer serializer = new DataContractSerializer(typeof(Gallery), _serializableDataObjectTypes);
					gallery = (Gallery) serializer.ReadObject(reader, true);
					reader.Close();
					memoryStream.Close();
				}
			}
			gallery.Sources.ForEach(source => RaiseMediaFolderAddedEvent(source.RootFolder));


			//try
			//{
			//   if (File.Exists(gallery.FilePath))
			//   {
			//      using (ZipFile sourceDatabase = ZipFile.Read(gallery.FilePath))
			//      {
			//         foreach (GallerySource source in gallery.Sources)
			//         {
			//            RaiseStatusUpdatedEvent("Loading source (" + source.DisplayPath + ")...");
			//            MemoryStream memoryStream = new MemoryStream();
			//            ZipEntry zipEntry = sourceDatabase[Path.ChangeExtension(source.ID, METAFILE_FILE_EXTENSION)];
			//            zipEntry.Extract(memoryStream);
			//            memoryStream.Position = 0;
			//            StreamReader reader = new StreamReader(memoryStream);

			//            string objectPrefix;
			//            string[] deserializedObject = ObjectSerializer.Deserialize(reader.ReadLine(), out objectPrefix);
			//            if (objectPrefix != "GV")
			//               throw new Exception("Failed to deserialize source database; gallery version missing");
			//            GalleryVersion version = new GalleryVersion(deserializedObject);

			//            DeserializeSource(reader, source);

			//            reader.Close();
			//            memoryStream.Close();
			//         }
			//      }
			//   }
			//}
			//catch
			//{
			//}
		}

		public static void SaveGallery(Gallery gallery)
		{
			//lock (_galleryAccessLock)
			{
				_databaseState.Clear();
				using (ZipFile galleryDatabase = OpenGalleryDatabase(gallery, true))
				{
					CreateGalleryEntry(galleryDatabase, gallery);
					//foreach (GallerySource source in gallery.Sources)
					//{
					//   ZipEntry sourceEntry = galleryDatabase.UpdateEntry(Path.ChangeExtension(source.ID, METAFILE_FILE_EXTENSION), string.Empty, Stream.Null);
					//   _databaseState.SourceEntries.Add(sourceEntry, source);
					//   RaiseMediaFolderAddedEvent(source.RootFolder);
					//}
					SaveGalleryDatabase(galleryDatabase);
				}
			}
			gallery.Sources.ForEach(source => RaiseMediaFolderAddedEvent(source.RootFolder));
		}

		public static Stream GetGalleryMetadataStream(Gallery gallery)
		{
			MemoryStream memoryStream = new MemoryStream();
			XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(memoryStream);
			DataContractSerializer serializer = new DataContractSerializer(typeof(Gallery), _serializableDataObjectTypes);
			serializer.WriteObject(writer, gallery);
			writer.Flush();
			memoryStream.Position = 0;
			return memoryStream;


			//MemoryStream memoryStream = new MemoryStream();
			//StreamWriter writer = new StreamWriter(memoryStream);

			//writer.WriteLine(GalleryVersion.Instance.Serialize());
			//SerializeSource(writer, source);

			//writer.Flush();
			//memoryStream.Position = 0;
			//return memoryStream;
		}

		private static void CreateGalleryEntry(ZipFile galleryDatabase, Gallery gallery)
		{
			ZipEntry galleryEntry = galleryDatabase.UpdateEntry(GALLERY_FILE_NAME, string.Empty, Stream.Null);
			_databaseState.GalleryEntry = new Tuple<ZipEntry, Gallery>(galleryEntry, gallery);
		}

		private static ZipFile OpenGalleryDatabase(Gallery gallery, bool writing)
		{
			ZipFile galleryDatabase = null;
			if (writing)
			{
				string galleryDatabaseBackupPath = null;
				bool deleteBackup = true;
				try
				{
					if (File.Exists(gallery.FilePath))
					{
						RaiseStatusUpdatedEvent("Backing up existing source database...");
						galleryDatabaseBackupPath = gallery.FilePath + ".backup";
						if (File.Exists(galleryDatabaseBackupPath))
							File.Delete(galleryDatabaseBackupPath);
						File.Copy(gallery.FilePath, galleryDatabaseBackupPath);
					}
					galleryDatabase = new ZipFile(gallery.FilePath) { Encryption = (EncryptionAlgorithm) gallery.EncryptionAlgorithm };
					if (galleryDatabase.Encryption != EncryptionAlgorithm.None)
					{
						galleryDatabase.Password = gallery.Password;
					}
				}
				catch
				{
					try
					{
						if (galleryDatabaseBackupPath != null && File.Exists(galleryDatabaseBackupPath))
						{
							deleteBackup = false;
							CloseGalleryDatabase(galleryDatabase);
							if (File.Exists(gallery.FilePath))
								File.Delete(gallery.FilePath);
							File.Move(galleryDatabaseBackupPath, gallery.FilePath);
							deleteBackup = true;
						}
						else
							CloseGalleryDatabase(galleryDatabase);
					}
					catch { ; }
					throw;
				}
				finally
				{
					try
					{
						if (deleteBackup && galleryDatabaseBackupPath != null && File.Exists(galleryDatabaseBackupPath))
							File.Delete(galleryDatabaseBackupPath);
						_databaseState.Clear();
					}
					catch { ; }
				}
			}
			else
			{
				try
				{
					if (File.Exists(gallery.FilePath))
					{
						galleryDatabase = ZipFile.Read(gallery.FilePath);
						if (galleryDatabase.Encryption != EncryptionAlgorithm.None)
							galleryDatabase.Password = gallery.Password;
					}
				}
				catch { ; }
			}
			return galleryDatabase;
		}

		private static void SaveGalleryDatabase(ZipFile galleryDatabase)
		{
			if (galleryDatabase == null)
				throw new ArgumentNullException("galleryDatabase");

			lock (_galleryAccessLock)
			{
				galleryDatabase.SaveProgress += SourceDatabase_SaveProgress;
				galleryDatabase.Save();
				galleryDatabase.SaveProgress -= SourceDatabase_SaveProgress;
			}
		}

		private static void CloseGalleryDatabase(ZipFile galleryDatabase)
		{
			if (galleryDatabase != null)
				galleryDatabase.Dispose();
		}

		#endregion
	}
}
