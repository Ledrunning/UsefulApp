using System.Xml.Serialization;

namespace Client.Configuration
{
	public class Translation
	{
		[XmlAttribute("name")]
		public string Name { get; set; }
		[XmlElement("text")]
		public string Text { get; set; }
	}
}
