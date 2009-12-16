using System.Xml.Serialization;

namespace UnpakkDaemon.DataObjects
{
	public interface IRecord : IXmlSerializable
	{
		string Name { get; set; }
		int Size { get; set; }
	}
}
