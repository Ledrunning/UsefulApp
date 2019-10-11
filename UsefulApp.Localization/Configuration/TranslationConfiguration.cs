using System.Collections.Generic;

namespace UsefulApp.Localization.Configuration
{
    public class TranslationConfiguration
    {
        private Language _language;

        public Language Language
        {
            get => _language;
            set
            {
                if (_language != value)
                {
                    _language = value;
                    OnLanguageChangeEvent(new LanguageChangeEventArgs());
                }

                _language = value;
            }
        }

        public List<Language> Languages { get; set; }
        public Translations Translations { get; set; }

        public event LanguageChangeEventHandler LanguageChangeEvent;

        protected virtual void OnLanguageChangeEvent(LanguageChangeEventArgs e)
        {
            Translations = XmlHelper.DeserializeXml<Translations>($"{Language.ShortName}.xml");
            LanguageChangeEvent?.Invoke(this, e);
        }
    }
}