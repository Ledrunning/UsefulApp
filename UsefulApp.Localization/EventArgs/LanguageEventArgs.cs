using UsefulApp.Localization.Configuration;

namespace UsefulApp.Localization.EventArgs
{
    public delegate void LanguageButtonEventHandler(object sender, LanguageEventArgs e);

    public class LanguageEventArgs //: BaseButtonEventArgs
    {
        public LanguageEventArgs(Language language, bool isSelected) //: base(isSelected)
        {
            Language = language;
        }

        public Language Language { get; set; }
    }
}