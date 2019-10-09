namespace UsefulApp.Localization.Configuration
{
    public class Language
    {
        public LocalizationType LocalizationTypeType
        {
            get
            {
                if (!string.IsNullOrEmpty(ShortName))
                {
                    switch (ShortName.ToUpper())
                    {
                        case "RU":
                            return LocalizationType.Ru;

                        case "EN":
                            return LocalizationType.En;
                    }
                }

                return LocalizationType.En;
            }
        }

        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}