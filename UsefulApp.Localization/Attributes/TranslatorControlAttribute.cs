using System;

namespace Client.Attributes
{
	public class TranslatorControlAttribute : Attribute
	{
		public string Name { get; set; }

		public TranslatorControlAttribute(string name)
		{
			Name = name;
		}
	}
}
