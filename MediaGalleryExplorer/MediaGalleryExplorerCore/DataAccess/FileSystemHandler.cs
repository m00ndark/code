using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using Ionic.Zip;
using MediaGalleryExplorerCore.DataObjects;
using MediaGalleryExplorerCore.EventArguments;

namespace MediaGalleryExplorerCore.DataAccess
{
	public static class FileSystemHandler
	{
		public static event EventHandler<StringEventArgs> StatusUpdated;
		public static event EventHandler<MediaFolderEventArgs> MediaFolderAdded;
		public static event EventHandler<MediaFolderEventArgs> MediaFolderRemoved;

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

		#endregion

		#region General

		private static void PrepareDirectories()
		{
			Directory.CreateDirectory(ObjectPool.CompleteWorkingDirectory);
		}

		public static void ClearWorkingDirectory()
		{
			PrepareDirectories();
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

		public static void ScanFolders(GalleryDatabase galleryDatabase, GallerySource source, bool reScan)
		{
			try
			{
				ClearWorkingDirectory();
				int folderCount = 0, fileCount = 0;
				ScanSubFolder(galleryDatabase, source, source.RootFolder, reScan, ref folderCount, ref fileCount, 0);
			}
			finally
			{
				ClearWorkingDirectory();
			}
		}

		private static bool ScanSubFolder(GalleryDatabase galleryDatabase, GallerySource source, MediaFolder folder,
			bool reScan, ref int folderCount, ref int fileCount, int depth)
		{
			try
			{
				folderCount++;
				RaiseStatusUpdatedEvent("Scanning folder #" + folderCount + " at depth " + depth + ", found " + fileCount + " files...");
				GetFolders(source, folder);
				GetFiles(galleryDatabase, source, folder, reScan);

				if (folder.SubFolders.Count == 0 && folder.Files.Count == 0)
					return false;

				RaiseMediaFolderAddedEvent(folder);
				depth++;
				fileCount += folder.Files.Count;
				List<MediaFolder> emptyFolders = new List<MediaFolder>();
				foreach (MediaFolder subFolder in folder.SubFolders)
				{
					if (!ScanSubFolder(galleryDatabase, source, subFolder, reScan, ref folderCount, ref fileCount, depth))
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

		private static void GetFiles(GalleryDatabase galleryDatabase, GallerySource source, MediaFolder parentFolder, bool reScan)
		{
			List<string> files = Directory.GetFiles(Path.Combine(source.RootedPath, parentFolder.RelativePathName)).ToList();
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
							ImageFile imageFile = new ImageFile(fileName, relativePath, parentFolder, source)
								{
									FileSize = fileInfo.Length,
									ThumbnailName = fileName + ".tn.jpg"
								};
							try
							{
								imageFile.Size = ImageFileHelper.GetDimensions(file);
							}
							catch {}
							int existingIndex = parentFolder.Files.FindIndex(mediaFile => mediaFile.Name == fileName);
							if (existingIndex != -1) parentFolder.Files.RemoveAt(existingIndex);
							parentFolder.Files.Add(imageFile);
							AddDatabaseImageEntry(galleryDatabase, imageFile, reScan);
						}
					}
					else if (MediaFile.VIDEO_FILE_EXTENSIONS.Contains(extension))
					{
						VideoFile videoFile = new VideoFile(fileName, relativePath, parentFolder, source)
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
						int existingIndex = parentFolder.Files.FindIndex(mediaFile => mediaFile.Name == fileName);
						if (existingIndex != -1) parentFolder.Files.RemoveAt(existingIndex);
						parentFolder.Files.Add(videoFile);
						AddDatabaseImageEntry(galleryDatabase, videoFile, reScan);
					}
				}
				catch {}
			}
		}

		private static void GetFolders(GallerySource source, MediaFolder parent)
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

		private static void AddDatabaseImageEntry(GalleryDatabase galleryDatabase, MediaFile mediaFile, bool reScan)
		{
			string path = Path.Combine(mediaFile.Source.ID, mediaFile.RelativePath);

			if (mediaFile is VideoFile)
				AddDatabaseImageEntry(galleryDatabase, mediaFile, reScan, mediaFile.PreviewName, path);

			AddDatabaseImageEntry(galleryDatabase, mediaFile, reScan, mediaFile.ThumbnailName, path);
		}

		private static void AddDatabaseImageEntry(GalleryDatabase galleryDatabase, MediaFile mediaFile, bool reScan, string fileName, string path)
		{
			if (reScan || !galleryDatabase.EntryExists(fileName, path))
				galleryDatabase.UpdateEntry(fileName, path, mediaFile);
		}

		private static string RemoveAbsolutePath(string fullPath, string absolutePath)
		{
			return fullPath.Replace(absolutePath, string.Empty).Trim("\\".ToCharArray());
		}

		#endregion
	}
}
