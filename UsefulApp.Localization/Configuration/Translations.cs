using System.Collections.Generic;
using System.Xml.Serialization;

namespace Client.Configuration
{
	[XmlRoot("translations")]
	public class Translations
	{
		[XmlElement("translation")]
		public List<Translation> Items { get; set; }
	}
}
