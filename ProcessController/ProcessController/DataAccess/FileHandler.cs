using System.IO;
using System.Xml.Serialization;

namespace ProcessController.DataAccess
{
    public static class FileHandler
    {
        public static void Serialize(string filePath, IXmlSerializable obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            using (StreamWriter writer = new StreamWriter(filePath))
                serializer.Serialize(writer, obj);
        }

        public static T Deserialize<T>(string filePath) where T : IXmlSerializable
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader reader = new StreamReader(filePath))
                return (T) serializer.Deserialize(reader);
        }
    }
}
