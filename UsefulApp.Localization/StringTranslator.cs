using System.Linq;
using Arsis.AIS.UI.Common.Localization.Configuration;

namespace Arsis.AIS.UI.Common.Localization
{
    public static class StringTranslator
    {
        public static string GetTranslation(this string name, TranslationConfiguration configuration)
        {
            return GetTranslation(configuration.Translations, name);
        }

        public static string GetTranslation(this Translations translations, string name)
        {
            return translations.Items.FirstOrDefault(t => t.Name == name)?.Text;
        }
    }
}