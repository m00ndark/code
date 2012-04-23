using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Ionic.Zip;

namespace MediaGalleryExplorerCore.DataAccess
{
	public class GalleryDatabase : IDisposable
	{
		private static readonly IDictionary<string, ManualResetEvent> _databasesAccessible = new Dictionary<string, ManualResetEvent>();
		private readonly IDictionary<Type, object> _streamProviders;
		private readonly IDictionary<ZipEntry, object> _entries;
		private readonly bool _isWriting;
		private ZipFile _databaseFile;

		public delegate Stream StreamProvider<in T>(T dataObject, string fileName);

		private GalleryDatabase(ZipFile databaseFile, bool isWriting)
		{
			_streamProviders = new Dictionary<Type, object>();
			_entries = new Dictionary<ZipEntry, object>();
			_databaseFile = databaseFile;
			_databaseFile.SaveProgress += Database_SaveProgress;
			_isWriting = isWriting;
		}

		#region Accessibility

		private static void WaitForAccess(string databaseFilePath)
		{
			ManualResetEvent databaseAccess;
			string databaseHash = CryptoServiceHandler.GenerateHash(databaseFilePath.Trim().ToLower());
			if (_databasesAccessible.TryGetValue(databaseHash, out databaseAccess))
				databaseAccess.WaitOne();
		}

		#endregion

		#region Open

		public static GalleryDatabase Open(string filePath, int encryptionAlgorithm, string password, bool writing)
		{
			ZipFile databaseFile = null;
			if (writing)
			{
				string galleryDatabaseBackupPath = null;
				bool deleteBackup = true;
				try
				{
					if (File.Exists(filePath))
					{
						galleryDatabaseBackupPath = filePath + ".backup";
						if (File.Exists(galleryDatabaseBackupPath))
							File.Delete(galleryDatabaseBackupPath);
						File.Copy(filePath, galleryDatabaseBackupPath);
					}
					databaseFile = new ZipFile(filePath) { Encryption = (EncryptionAlgorithm) encryptionAlgorithm };
					if (databaseFile.Encryption != EncryptionAlgorithm.None)
					{
						databaseFile.Password = password;
					}
				}
				catch
				{
					try
					{
						if (galleryDatabaseBackupPath != null && File.Exists(galleryDatabaseBackupPath))
						{
							deleteBackup = false;
							Close(ref databaseFile);
							if (File.Exists(filePath))
								File.Delete(filePath);
							File.Move(galleryDatabaseBackupPath, filePath);
							deleteBackup = true;
						}
						else
							Close(ref databaseFile);
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
					}
					catch { ; }
				}
			}
			else
			{
				try
				{
					if (File.Exists(filePath))
					{
						WaitForAccess(filePath);
						databaseFile = ZipFile.Read(filePath);
						if (databaseFile.Encryption != EncryptionAlgorithm.None)
							databaseFile.Password = password;
					}
				}
				catch { ; }
			}
			return new GalleryDatabase(databaseFile, writing);
		}

		#endregion

		#region Close and dispose

		private static void Close(ref ZipFile databaseFile)
		{
			if (databaseFile != null)
			{
				databaseFile.Dispose();
				databaseFile = null;
			}
		}

		public void Close()
		{
			_databaseFile.SaveProgress -= Database_SaveProgress;
			Close(ref _databaseFile);
		}

		public void Dispose()
		{
			Close();
		}

		#endregion

		#region Stream providers

		public void RegisterStreamProvider<T>(StreamProvider<T> streamProvider)
		{
			_streamProviders[typeof(T)] = streamProvider;
		}

		private object GetStreamProvider(Type type)
		{
			while (type != null && !_streamProviders.ContainsKey(type))
				type = type.BaseType;

			if (type == null)
				throw new Exception("Stream provider not found");

			return _streamProviders[type];
		}

		#endregion

		#region Saving

		public void Save()
		{
			if (!_isWriting)
				throw new InvalidOperationException("Database not opened for writing");

			ManualResetEvent databaseAccess;
			lock (_databasesAccessible)
			{
				string databaseHash = CryptoServiceHandler.GenerateHash(_databaseFile.Name.Trim().ToLower());
				if (_databasesAccessible.TryGetValue(databaseHash, out databaseAccess))
				{
					while (!databaseAccess.Reset())
						databaseAccess.WaitOne();
				}
				else
				{
					databaseAccess = new ManualResetEvent(false);
					_databasesAccessible.Add(databaseHash, databaseAccess);
				}
			}
			try
			{
				_databaseFile.Save();
			}
			finally
			{
				databaseAccess.Set();
			}
		}

		private void Database_SaveProgress(object sender, SaveProgressEventArgs e)
		{
			if (e.EventType == ZipProgressEventType.Saving_BeforeWriteEntry && e.CurrentEntry.Source == ZipEntrySource.Stream && e.CurrentEntry.InputStream == Stream.Null)
			{
				if (_entries.ContainsKey(e.CurrentEntry))
				{
					object dataObject = _entries[e.CurrentEntry];
					object streamProvider = GetStreamProvider(dataObject.GetType());
					MethodInfo streamProviderMethod = streamProvider.GetType().GetMethod("Invoke");
					e.CurrentEntry.InputStream = (Stream) streamProviderMethod.Invoke(streamProvider, new object[] { dataObject, e.CurrentEntry.FileName });
				}
				else
					e.Cancel = true;
			}
			else if (e.EventType == ZipProgressEventType.Saving_AfterWriteEntry && e.CurrentEntry.InputStreamWasJitProvided)
			{
				e.CurrentEntry.InputStream.Close();
			}
		}

		#endregion

		#region Adding/updating entries

		public void UpdateEntry<T>(string fileName, string path, T dataObject)
		{
			if (!_streamProviders.ContainsKey(typeof(T)))
				throw new ArgumentException("No stream provider registered for data object type " + typeof(T).Name, "dataObject");

			ZipEntry entry = _databaseFile.UpdateEntry(fileName, path, Stream.Null);
			_entries[entry] = dataObject;
		}

		#endregion

		#region Entry existance

		public bool EntryExists(string fileName, string path)
		{
			return _databaseFile.EntryFileNames.Any(entry => entry.Equals(Path.Combine(path, fileName)
				.Replace(Path.DirectorySeparatorChar, '/'), StringComparison.CurrentCultureIgnoreCase));
		}

		#endregion

		#region Extracting entries

		public Stream ExtractEntry(string filePath)
		{
			MemoryStream memoryStream = new MemoryStream();
			ZipEntry zipEntry = _databaseFile[filePath];
			if (zipEntry == null) return null;
			zipEntry.Extract(memoryStream);
			memoryStream.Position = 0;
			return memoryStream;
		}

		#endregion
	}
}
