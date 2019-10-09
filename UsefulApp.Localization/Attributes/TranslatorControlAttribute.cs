using System;

namespace UsefulApp.Localization.Attributes
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
