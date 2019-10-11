using System.IO;
using System.Xml.Serialization;
namespace UsefulApp.Localization
{
    public static class XmlHelper
    {
        public static T DeserializeXml<T>(string fileName)
        {
            var serializer = new XmlSerializer(typeof(T));
            var result = default(T);
            using (var reader = new StreamReader(fileName))
            {
                result = (T)serializer.Deserialize(reader);
                reader.Close();
            }

            return result;
        }

        public static T DeserializeXml<T>(string fileName, string rootElement)
        {
            var rootAttribute = new XmlRootAttribute { ElementName = rootElement, IsNullable = false };
            var serializer = new XmlSerializer(typeof(T), rootAttribute);
            var result = default(T);
            using (var reader = new StreamReader(fileName))
            {
                result = (T)serializer.Deserialize(reader);
                reader.Close();
            }
            return result;
        }
    }
}
