namespace UsefulApp.Localization
{
    public class LanguageChangeEventArgs : System.EventArgs
    {
    }

    public delegate void LanguageChangeEventHandler(object sender, LanguageChangeEventArgs e);
}