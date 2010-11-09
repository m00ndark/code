using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MediaGalleryExplorerCore.DataAccess
{
	public static class CryptoServiceHandler
	{
		public static string GenerateHash(string str)
		{
			byte[] hash = new SHA1CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(str));
			return hash.Select(b => b.ToString("X2")).Aggregate((a, b) => (a + b));
		}
	}
}
