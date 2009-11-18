using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using System.Threading;


/*  Author:  Michael A. McCloskey
 *  Company: Schematrix
 *  Version: 20040714
 *  
 *  Personal Comments:
 *  I created this unrar wrapper class for personal use 
 *  after running into a number of issues trying to use
 *  another COM unrar product via COM interop.  I hope it 
 *  proves as useful to you as it has to me and saves you
 *  some time in building your own products.
 */

namespace UnpakkDaemon.Extraction
{
	/// <summary>
	/// Represents the method that will handle data available events
	/// </summary>
	public delegate void DataAvailableHandler(object sender, DataAvailableEventArgs e);

	/// <summary>
	/// Represents the method that will handle extraction progress events
	/// </summary>
	public delegate void ExtractionProgressHandler(object sender, ExtractionProgressEventArgs e);

	/// <summary>
	/// Represents the method that will handle missing archive volume events
	/// </summary>
	public delegate void MissingVolumeHandler(object sender, MissingVolumeEventArgs e);

	/// <summary>
	/// Represents the method that will handle new volume events
	/// </summary>
	public delegate void NewVolumeHandler(object sender, NewVolumeEventArgs e);

	/// <summary>
	/// Represents the method that will handle new file notifications
	/// </summary>
	public delegate void NewFileHandler(object sender, NewFileEventArgs e);

	/// <summary>
	/// Represents the method that will handle password required events
	/// </summary>
	public delegate void PasswordRequiredHandler(object sender, PasswordRequiredEventArgs e);

	/// <summary>
	/// Wrapper class for unrar DLL supplied by RARSoft.  
	/// Calls unrar DLL via platform invocation services (pinvoke).
	/// DLL is available at http://www.rarlab.com/rar/UnRARDLL.exe
	/// </summary>
	public class Unrar : IDisposable
	{
		#region Unrar DLL enumerations

		/// <summary>
		/// Mode in which archive is to be opened for processing.
		/// </summary>
		public enum OpenMode
		{
			/// <summary>
			/// Open archive for listing contents only
			/// </summary>
			List = 0,
			/// <summary>
			/// Open archive for testing or extracting contents
			/// </summary>
			Extract = 1
		}

		private enum RarError : uint
		{
			EndOfArchive = 10,
			InsufficientMemory = 11,
			BadData = 12,
			BadArchive = 13,
			UnknownFormat = 14,
			OpenError = 15,
			CreateError = 16,
			CloseError = 17,
			ReadError = 18,
			WriteError = 19,
			BufferTooSmall = 20,
			UnknownError = 21
		}

		private enum Operation : uint
		{
			Skip = 0,
			Test = 1,
			Extract = 2
		}

		private enum VolumeMessage : uint
		{
			Ask = 0,
			Notify = 1
		}

		[Flags]
		private enum ArchiveFlags : uint
		{
			Volume = 0x1,                 // Volume attribute (archive volume)
			CommentPresent = 0x2,         // Archive comment present
			Lock = 0x4,                   // Archive lock attribute
			SolidArchive = 0x8,           // Solid attribute (solid archive)
			NewNamingScheme = 0x10,       // New volume naming scheme ('volname.partN.rar')
			AuthenticityPresent = 0x20,   // Authenticity information present
			RecoveryRecordPresent = 0x40, // Recovery record present
			EncryptedHeaders = 0x80,      // Block headers are encrypted
			FirstVolume = 0x100           // 0x0100  - First volume (set only by RAR 3.0 and later)
		}

		private enum CallbackMessages : uint
		{
			VolumeChange = 0,
			ProcessData = 1,
			NeedPassword = 2
		}

		#endregion

		#region Unrar DLL structure definitions

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
		private struct RARHeaderData
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string ArcName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string FileName;
			public uint Flags;
			public uint PackSize;
			public uint UnpSize;
			public uint HostOS;
			public uint FileCRC;
			public uint FileTime;
			public uint UnpVer;
			public uint Method;
			public uint FileAttr;
			[MarshalAs(UnmanagedType.LPStr)]
			public string CmtBuf;
			public uint CmtBufSize;
			public uint CmtSize;
			public uint CmtState;

			public void Initialize()
			{
				CmtBuf = new string((char) 0, 65536);
				CmtBufSize = 65536;
			}
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct RARHeaderDataEx
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
			public string ArcName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
			public string ArcNameW;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
			public string FileName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
			public string FileNameW;
			public uint Flags;
			public uint PackSize;
			public uint PackSizeHigh;
			public uint UnpSize;
			public uint UnpSizeHigh;
			public uint HostOS;
			public uint FileCRC;
			public uint FileTime;
			public uint UnpVer;
			public uint Method;
			public uint FileAttr;
			[MarshalAs(UnmanagedType.LPStr)]
			public string CmtBuf;
			public uint CmtBufSize;
			public uint CmtSize;
			public uint CmtState;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
			public uint[] Reserved;

			public void Initialize()
			{
				CmtBuf = new string((char) 0, 65536);
				CmtBufSize = 65536;
			}
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
		public struct RAROpenArchiveData
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string ArcName;
			public uint OpenMode;
			public uint OpenResult;
			[MarshalAs(UnmanagedType.LPStr)]
			public string CmtBuf;
			public uint CmtBufSize;
			public uint CmtSize;
			public uint CmtState;

			public void Initialize()
			{
				CmtBuf = new string((char) 0, 65536);
				CmtBufSize = 65536;
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RAROpenArchiveDataEx
		{
			[MarshalAs(UnmanagedType.LPStr)]
			public string ArcName;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string ArcNameW;
			public uint OpenMode;
			public uint OpenResult;
			[MarshalAs(UnmanagedType.LPStr)]
			public string CmtBuf;
			public uint CmtBufSize;
			public uint CmtSize;
			public uint CmtState;
			public uint Flags;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public uint[] Reserved;

			public void Initialize()
			{
				CmtBuf = new string((char) 0, 65536);
				CmtBufSize = 65536;
				Reserved = new uint[32];
			}
		}

		#endregion

		#region Unrar function declarations

		[DllImport("unrar.dll")]
		private static extern IntPtr RAROpenArchive(ref RAROpenArchiveData archiveData);

		[DllImport("unrar.dll")]
		private static extern IntPtr RAROpenArchiveEx(ref RAROpenArchiveDataEx archiveData);

		[DllImport("unrar.dll")]
		private static extern int RARCloseArchive(IntPtr hArcData);

		[DllImport("unrar.dll")]
		private static extern int RARReadHeader(IntPtr hArcData, ref RARHeaderData headerData);

		[DllImport("unrar.dll")]
		private static extern int RARReadHeaderEx(IntPtr hArcData, ref RARHeaderDataEx headerData);

		[DllImport("unrar.dll")]
		private static extern int RARProcessFile(IntPtr hArcData, int operation, [MarshalAs(UnmanagedType.LPStr)] string destPath, [MarshalAs(UnmanagedType.LPStr)] string destName);

		[DllImport("unrar.dll")]
		private static extern void RARSetCallback(IntPtr hArcData, UnrarCallback callback, int userData);

		[DllImport("unrar.dll")]
		private static extern void RARSetPassword(IntPtr hArcData, [MarshalAs(UnmanagedType.LPStr)] string password);

		// Unrar callback delegate signature
		private delegate int UnrarCallback(uint msg, int userData, IntPtr p1, int p2);

		#endregion

		#region Public event declarations

		/// <summary>
		/// Event that is raised when a new chunk of data has been extracted
		/// </summary>
		public event DataAvailableHandler DataAvailable;
		/// <summary>
		/// Event that is raised to indicate extraction progress
		/// </summary>
		public event ExtractionProgressHandler ExtractionProgress;
		/// <summary>
		/// Event that is raised when a required archive volume is missing
		/// </summary>
		public event MissingVolumeHandler MissingVolume;
		/// <summary>
		/// Event that is raised when a new file is encountered during processing
		/// </summary>
		public event NewFileHandler NewFile;
		/// <summary>
		/// Event that is raised when a new archive volume is opened for processing
		/// </summary>
		public event NewVolumeHandler NewVolume;
		/// <summary>
		/// Event that is raised when a password is required before continuing
		/// </summary>
		public event PasswordRequiredHandler PasswordRequired;

		#endregion

		#region Private fields

		private string _archivePathName = string.Empty;
		private IntPtr _archiveHandle = new IntPtr(0);
		private bool _retrieveComment = true;
		private string _password = string.Empty;
		private string _comment = string.Empty;
		private ArchiveFlags _archiveFlags = 0;
		private RARHeaderDataEx _header = new RARHeaderDataEx();
		private string _destinationPath = string.Empty;
		private RARFileInfo _currentFile = null;
		private readonly UnrarCallback _callback = null;

		#endregion

		#region Object lifetime procedures

		public Unrar()
		{
			_callback = new UnrarCallback(RARCallback);
		}

		public Unrar(string archivePathName) : this()
		{
			_archivePathName = archivePathName;
		}

		~Unrar()
		{
			if (_archiveHandle != IntPtr.Zero)
			{
				RARCloseArchive(_archiveHandle);
				_archiveHandle = IntPtr.Zero;
			}
		}

		public void Dispose()
		{
			if (_archiveHandle != IntPtr.Zero)
			{
				RARCloseArchive(_archiveHandle);
				_archiveHandle = IntPtr.Zero;
			}
		}

		#endregion

		#region Public Properties

		public bool Pause { get; set; }
		public bool Abort { get; set; }

		/// <summary>
		/// Path and name of RAR archive to open
		/// </summary>
		public string ArchivePathName
		{
			get { return _archivePathName; }
			set { _archivePathName = value; }
		}

		/// <summary>
		/// Archive comment 
		/// </summary>
		public string Comment
		{
			get { return _comment; }
		}

		/// <summary>
		/// Current file being processed
		/// </summary>
		public RARFileInfo CurrentFile
		{
			get { return _currentFile; }
		}

		/// <summary>
		/// Default destination path for extraction
		/// </summary>
		public string DestinationPath
		{
			get { return _destinationPath; }
			set { _destinationPath = value; }
		}

		/// <summary>
		/// Password for opening encrypted archive
		/// </summary>
		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
				if (_archiveHandle != IntPtr.Zero)
					RARSetPassword(_archiveHandle, value);
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Close the currently open archive
		/// </summary>
		public void Close()
		{
			// Exit without exception if no archive is open
			if (_archiveHandle == IntPtr.Zero)
				return;

			// Close archive
			int result = RARCloseArchive(_archiveHandle);

			// Check result
			if (result != 0)
			{
				ProcessFileError(result);
			}
			else
			{
				_archiveHandle = IntPtr.Zero;
			}
		}

		/// <summary>
		/// Opens archive specified by the ArchivePathName property for testing or extraction
		/// </summary>
		public void Open()
		{
			if (ArchivePathName.Length == 0)
				throw new IOException("Archive name has not been set.");
			Open(ArchivePathName, OpenMode.Extract);
		}

		/// <summary>
		/// Opens archive specified by the ArchivePathName property with a specified mode
		/// </summary>
		/// <param name="openMode">Mode in which archive should be opened</param>
		public void Open(OpenMode openMode)
		{
			if (ArchivePathName.Length == 0)
				throw new IOException("Archive name has not been set.");
			Open(ArchivePathName, openMode);
		}

		/// <summary>
		/// Opens specified archive using the specified mode.  
		/// </summary>
		/// <param name="archivePathName">Path of archive to open</param>
		/// <param name="openMode">Mode in which to open archive</param>
		public void Open(string archivePathName, OpenMode openMode)
		{
			IntPtr handle = IntPtr.Zero;

			// Close any previously open archives
			if (_archiveHandle != IntPtr.Zero)
				Close();

			// Prepare extended open archive struct
			ArchivePathName = archivePathName;
			RAROpenArchiveDataEx openStruct = new RAROpenArchiveDataEx();
			openStruct.Initialize();
			openStruct.ArcName = _archivePathName + "\0";
			openStruct.ArcNameW = _archivePathName + "\0";
			openStruct.OpenMode = (uint) openMode;
			if (_retrieveComment)
			{
				openStruct.CmtBuf = new string((char) 0, 65536);
				openStruct.CmtBufSize = 65536;
			}
			else
			{
				openStruct.CmtBuf = null;
				openStruct.CmtBufSize = 0;
			}

			// Open archive
			handle = RAROpenArchiveEx(ref openStruct);

			// Check for success
			if (openStruct.OpenResult != 0)
			{
				switch ((RarError) openStruct.OpenResult)
				{
					case RarError.InsufficientMemory:
						throw new OutOfMemoryException("Insufficient memory to perform operation.");

					case RarError.BadData:
						throw new IOException("Archive header broken");

					case RarError.BadArchive:
						throw new IOException("File is not a valid archive.");

					case RarError.OpenError:
						throw new IOException("File could not be opened.");
				}
			}

			// Save handle and flags
			_archiveHandle = handle;
			_archiveFlags = (ArchiveFlags) openStruct.Flags;

			// Set callback
			RARSetCallback(_archiveHandle, _callback, GetHashCode());

			// If comment retrieved, save it
			if (openStruct.CmtState == 1)
				_comment = openStruct.CmtBuf;

			// If password supplied, set it
			if (_password.Length != 0)
				RARSetPassword(_archiveHandle, _password);

			// Fire NewVolume event for first volume
			OnNewVolume(_archivePathName);
		}

		/// <summary>
		/// Reads the next archive header and populates CurrentFile property data
		/// </summary>
		/// <returns></returns>
		public bool ReadHeader()
		{
			// Throw exception if archive not open
			if (_archiveHandle == IntPtr.Zero)
				throw new IOException("Archive is not open.");

			// Initialize header struct
			_header = new RARHeaderDataEx();
			_header.Initialize();

			// Read next entry
			_currentFile = null;
			int result = RARReadHeaderEx(_archiveHandle, ref _header);

			// Check for error or end of archive
			if ((RarError) result == RarError.EndOfArchive)
				return false;
			if ((RarError) result == RarError.BadData)
				throw new IOException("Archive data is corrupt.");

			// Determine if new file
			if (((_header.Flags & 0x01) != 0) && _currentFile != null)
				_currentFile.ContinuedFromPrevious = true;
			else
			{
				// New file, prepare header
				_currentFile = new RARFileInfo();
				_currentFile.FileName = _header.FileNameW;
				if ((_header.Flags & 0x02) != 0)
					_currentFile.ContinuedOnNext = true;
				if (_header.PackSizeHigh != 0)
					_currentFile.PackedSize = (_header.PackSizeHigh * 0x100000000) + _header.PackSize;
				else
					_currentFile.PackedSize = _header.PackSize;
				if (_header.UnpSizeHigh != 0)
					_currentFile.UnpackedSize = (_header.UnpSizeHigh * 0x100000000) + _header.UnpSize;
				else
					_currentFile.UnpackedSize = _header.UnpSize;
				_currentFile.HostOS = (int) _header.HostOS;
				_currentFile.FileCRC = _header.FileCRC;
				_currentFile.FileTime = FromMSDOSTime(_header.FileTime);
				_currentFile.VersionToUnpack = (int) _header.UnpVer;
				_currentFile.Method = (int) _header.Method;
				_currentFile.FileAttributes = (int) _header.FileAttr;
				_currentFile.BytesExtracted = 0;
				if ((_header.Flags & 0xE0) == 0xE0)
					_currentFile.IsDirectory = true;
				OnNewFile();
			}

			// Return success
			return true;
		}

		/// <summary>
		/// Returns array of file names contained in archive
		/// </summary>
		/// <returns></returns>
		public string[] ListFiles()
		{
			ArrayList fileNames = new ArrayList();
			while (ReadHeader())
			{
				if (!_currentFile.IsDirectory)
					fileNames.Add(_currentFile.FileName);
				Skip();
			}
			string[] files = new string[fileNames.Count];
			fileNames.CopyTo(files);
			return files;
		}

		/// <summary>
		/// Moves the current archive position to the next available header
		/// </summary>
		public void Skip()
		{
			int result = RARProcessFile(_archiveHandle, (int) Operation.Skip, string.Empty, string.Empty);

			// Check result
			if (result != 0)
			{
				ProcessFileError(result);
			}
		}

		/// <summary>
		/// Tests the ability to extract the current file without saving extracted data to disk
		/// </summary>
		public void Test()
		{
			int result = RARProcessFile(_archiveHandle, (int) Operation.Test, string.Empty, string.Empty);

			// Check result
			if (result != 0)
			{
				ProcessFileError(result);
			}
		}

		/// <summary>
		/// Extracts the current file to the default destination path
		/// </summary>
		public void Extract()
		{
			Extract(_destinationPath, string.Empty);
		}

		/// <summary>
		/// Extracts the current file to a specified destination path and filename
		/// </summary>
		/// <param name="destinationName">Path and name of extracted file</param>
		public void Extract(string destinationName)
		{
			Extract(string.Empty, destinationName);
		}

		/// <summary>
		/// Extracts the current file to a specified directory without renaming file
		/// </summary>
		/// <param name="destinationPath"></param>
		public void ExtractToDirectory(string destinationPath)
		{
			Extract(destinationPath, string.Empty);
		}

		#endregion

		#region Private Methods

		private void Extract(string destinationPath, string destinationName)
		{
			int result = RARProcessFile(_archiveHandle, (int) Operation.Extract, destinationPath, destinationName);

			// Check result
			if (result != 0)
			{
				ProcessFileError(result);
			}
		}

		private static DateTime FromMSDOSTime(uint dosTime)
		{
			ushort hiWord = (ushort) ((dosTime & 0xFFFF0000) >> 16);
			ushort loWord = (ushort) (dosTime & 0xFFFF);
			int year = ((hiWord & 0xFE00) >> 9) + 1980;
			int month = (hiWord & 0x01E0) >> 5;
			int day = hiWord & 0x1F;
			int hour = (loWord & 0xF800) >> 11;
			int minute = (loWord & 0x07E0) >> 5;
			int second = (loWord & 0x1F) << 1;
			return new DateTime(year, month, day, hour, minute, second);
		}

		private static void ProcessFileError(int result)
		{
			switch ((RarError) result)
			{
				case RarError.UnknownFormat:
					throw new OutOfMemoryException("Unknown archive format.");

				case RarError.BadData:
					throw new IOException("File CRC Error");

				case RarError.BadArchive:
					throw new IOException("File is not a valid archive.");

				case RarError.OpenError:
					throw new IOException("File could not be opened.");

				case RarError.CreateError:
					throw new IOException("File could not be created.");

				case RarError.CloseError:
					throw new IOException("File close error.");

				case RarError.ReadError:
					throw new IOException("File read error.");

				case RarError.WriteError:
					throw new IOException("File write error.");
			}
		}

		private int RARCallback(uint msg, int userData, IntPtr p1, int p2)
		{
			string volume = string.Empty;
			string newVolume = string.Empty;
			int result = -1;

			switch ((CallbackMessages) msg)
			{
				case CallbackMessages.VolumeChange:
					volume = Marshal.PtrToStringAnsi(p1);
					if ((VolumeMessage) p2 == VolumeMessage.Notify)
						result = OnNewVolume(volume);
					else if ((VolumeMessage) p2 == VolumeMessage.Ask)
					{
						newVolume = OnMissingVolume(volume);
						if (newVolume.Length == 0)
							result = -1;
						else
						{
							if (newVolume != volume)
							{
								for (int i = 0; i < newVolume.Length; i++)
								{
									Marshal.WriteByte(p1, i, (byte) newVolume[i]);
								}
								Marshal.WriteByte(p1, newVolume.Length, 0);
							}
							result = 1;
						}
					}
					break;

				case CallbackMessages.ProcessData:
					result = OnDataAvailable(p1, p2);
					break;

				case CallbackMessages.NeedPassword:
					result = OnPasswordRequired(p1, p2);
					break;
			}
			return result;
		}

		#endregion

		#region Protected Virtual (Overridable) Methods

		protected virtual void OnNewFile()
		{
			if (NewFile != null)
			{
				NewFile(this, new NewFileEventArgs(_currentFile));
			}
		}

		protected virtual int OnPasswordRequired(IntPtr p1, int p2)
		{
			int result = -1;
			if (PasswordRequired != null)
			{
				PasswordRequiredEventArgs e = new PasswordRequiredEventArgs();
				PasswordRequired(this, e);
				if (e.ContinueOperation && e.Password.Length > 0)
				{
					for (int i = 0; (i < e.Password.Length) && (i < p2); i++)
						Marshal.WriteByte(p1, i, (byte) e.Password[i]);
					Marshal.WriteByte(p1, e.Password.Length, 0);
					result = 1;
				}
			}
			else
			{
				throw new IOException("Password is required for extraction.");
			}
			return result;
		}

		protected virtual int OnDataAvailable(IntPtr p1, int p2)
		{
			int result = 1;
			if (_currentFile != null)
				_currentFile.BytesExtracted += p2;
			if (DataAvailable != null)
			{
				byte[] data = new byte[p2];
				Marshal.Copy(p1, data, 0, p2);
				DataAvailableEventArgs e = new DataAvailableEventArgs(data);
				DataAvailable(this, e);
				if (!e.ContinueOperation)
					result = -1;
			}
			if (ExtractionProgress != null && _currentFile != null)
			{
				ExtractionProgressEventArgs e = new ExtractionProgressEventArgs();
				e.FileName = _currentFile.FileName;
				e.FileSize = _currentFile.UnpackedSize;
				e.BytesExtracted = _currentFile.BytesExtracted;
				e.PercentComplete = _currentFile.PercentComplete;
				ExtractionProgress(this, e);
				if (!e.ContinueOperation)
					result = -1;
			}
			return result;
		}

		protected virtual int OnNewVolume(string volume)
		{
			while (Pause)
			{
				Thread.Sleep(10);
				if (Abort)
					return -1;
			}
			if (Abort)
				return -1;
			int result = 1;
			if (NewVolume != null)
			{
				NewVolumeEventArgs e = new NewVolumeEventArgs(volume);
				NewVolume(this, e);
				if (!e.ContinueOperation)
					result = -1;
			}
			return result;
		}

		protected virtual string OnMissingVolume(string volume)
		{
			string result = string.Empty;
			if (MissingVolume != null)
			{
				MissingVolumeEventArgs e = new MissingVolumeEventArgs(volume);
				MissingVolume(this, e);
				if (e.ContinueOperation)
					result = e.VolumeName;
			}
			return result;
		}

		#endregion
	}

	public class NewVolumeEventArgs
	{
		public string VolumeName;
		public bool ContinueOperation = true;

		public NewVolumeEventArgs(string volumeName)
		{
			VolumeName = volumeName;
		}
	}

	public class MissingVolumeEventArgs
	{
		public string VolumeName;
		public bool ContinueOperation = false;

		public MissingVolumeEventArgs(string volumeName)
		{
			VolumeName = volumeName;
		}
	}

	public class DataAvailableEventArgs
	{
		public readonly byte[] Data;
		public bool ContinueOperation = true;

		public DataAvailableEventArgs(byte[] data)
		{
			Data = data;
		}
	}

	public class PasswordRequiredEventArgs
	{
		public string Password = string.Empty;
		public bool ContinueOperation = true;
	}

	public class NewFileEventArgs
	{
		public RARFileInfo FileInfo;
		public NewFileEventArgs(RARFileInfo fileInfo)
		{
			FileInfo = fileInfo;
		}
	}

	public class ExtractionProgressEventArgs
	{
		public string FileName;
		public long FileSize;
		public long BytesExtracted;
		public double PercentComplete;
		public bool ContinueOperation = true;
	}

	public class RARFileInfo
	{
		public string FileName;
		public bool ContinuedFromPrevious = false;
		public bool ContinuedOnNext = false;
		public bool IsDirectory = false;
		public long PackedSize = 0;
		public long UnpackedSize = 0;
		public int HostOS = 0;
		public long FileCRC = 0;
		public DateTime FileTime;
		public int VersionToUnpack = 0;
		public int Method = 0;
		public int FileAttributes = 0;
		public long BytesExtracted = 0;

		public double PercentComplete
		{
			get
			{
				if (UnpackedSize != 0)
					return (((double) BytesExtracted / UnpackedSize) * 100.0);

				return 0;
			}
		}
	}
}