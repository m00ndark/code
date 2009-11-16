using System;
using System.Security.Cryptography;

namespace UnpakkDaemon.SimpleFileVerification
{
	public class CRC32 : HashAlgorithm
	{
		private const UInt32 DEFAULT_POLYNOMIAL = 0xedb88320;
		private const UInt32 DEFAULT_SEED = 0xffffffff;

		private UInt32 _hash;
		private readonly UInt32 _seed;
		private readonly UInt32[] _table;
		private static UInt32[] _defaultTable;

		public CRC32()
		{
			_table = InitializeTable(DEFAULT_POLYNOMIAL);
			_seed = DEFAULT_SEED;
			Initialize();
		}

		public CRC32(UInt32 polynomial, UInt32 seed)
		{
			_table = InitializeTable(polynomial);
			_seed = seed;
			Initialize();
		}

		#region Overrides of HashAlgorithm

		public override void Initialize()
		{
			_hash = _seed;
		}

		protected override void HashCore(byte[] buffer, int start, int length)
		{
			_hash = CalculateHash(_table, _hash, buffer, start, length);
		}

		protected override byte[] HashFinal()
		{
			byte[] hashBuffer = UInt32ToBigEndianBytes(~_hash);
			HashValue = hashBuffer;
			return hashBuffer;
		}

		#endregion

		public override int HashSize
		{
			get { return 32; }
		}

		public static UInt32 Compute(byte[] buffer)
		{
			return ~CalculateHash(InitializeTable(DEFAULT_POLYNOMIAL), DEFAULT_SEED, buffer, 0, buffer.Length);
		}

		public static UInt32 Compute(UInt32 seed, byte[] buffer)
		{
			return ~CalculateHash(InitializeTable(DEFAULT_POLYNOMIAL), seed, buffer, 0, buffer.Length);
		}

		public static UInt32 Compute(UInt32 polynomial, UInt32 seed, byte[] buffer)
		{
			return ~CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
		}

		private static UInt32[] InitializeTable(UInt32 polynomial)
		{
			if (polynomial == DEFAULT_POLYNOMIAL && _defaultTable != null)
				return _defaultTable;

			UInt32[] createTable = new UInt32[256];
			for (int i = 0; i < 256; i++)
			{
				UInt32 entry = (UInt32) i;
				for (int j = 0; j < 8; j++)
				{
					if ((entry & 1) == 1)
						entry = (entry >> 1) ^ polynomial;
					else
						entry = entry >> 1;
				}
				createTable[i] = entry;
			}

			if (polynomial == DEFAULT_POLYNOMIAL)
				_defaultTable = createTable;

			return createTable;
		}

		private static UInt32 CalculateHash(UInt32[] table, UInt32 seed, byte[] buffer, int start, int size)
		{
			UInt32 crc = seed;
			for (int i = start; i < size; i++)
			{
				unchecked
				{
					crc = (crc >> 8) ^ table[buffer[i] ^ crc & 0xff];
				}
			}
			return crc;
		}

		private static byte[] UInt32ToBigEndianBytes(UInt32 x)
		{
			return new byte[]
				{
					(byte) ((x >> 24) & 0xff),
					(byte) ((x >> 16) & 0xff),
					(byte) ((x >> 8) & 0xff),
					(byte) (x & 0xff)
				};
		}
	}
}
