using System.Collections.Generic;

namespace MBoxMobile.Helpers
{
    public static class MultiLanguageSupport
    {
        public static List<string> SupportedLanguageDisplayNames = new List<string>()
        {
           "English", "中文", "Deutsch", "Nederlands", "Français", "Español", "Khmer", "Bahasa", "Português", "LabelCheck"
        };

        public static Dictionary<string, string> GetTranslations(Languages language)
        {
            Dictionary<string, string> dict;

            switch(language)
            {
                case Languages.English:
                    dict = GetTranslation_English();
                    break;
                case Languages.Chinese:
                    dict = GetTranslation_Chinese();
                    break;
                case Languages.German:
                    dict = GetTranslation_German();
                    break;
                case Languages.Dutch:
                    dict = GetTranslation_Dutch();
                    break;
                case Languages.French:
                    dict = GetTranslation_French();
                    break;
                case Languages.Spanish:
                    dict = GetTranslation_Spanish();
                    break;
                case Languages.Khmer:
                    dict = GetTranslation_Khmer();
                    break;
                case Languages.Bahasa:
                    dict = GetTranslation_Bahasa();
                    break;
                case Languages.Portuguese:
                    dict = GetTranslation_Portuguese();
                    break;
                case Languages.LabelCheck:
                    dict = GetTranslation_LabelCheck();
                    break;
                default:
                    dict = GetTranslation_English();
                    break;
            }

            return dict;
        }

        #region Translations

        // Dictionary key have format PageName_ElementName

        private static Dictionary<string, string> GetTranslation_English()
        {
            //testing
            Dictionary<string, string> translation = new Dictionary<string, string>();
            translation.Add("TestMultiLanguage_Title", "TestMultiLanguage");
            translation.Add("TestMultiLanguage_Label1", "ContentPage");
            translation.Add("TestMultiLanguage_Label2", "ContentPage is the simplest type of page.");
            translation.Add("TestMultiLanguage_Label3", "The content of the ContentPage is generally a layout of some sort that can be a parent to multiple children.");
            translation.Add("TestMultiLanguage_Button", "Click Me");

            return translation;
        }

        private static Dictionary<string, string> GetTranslation_Chinese()
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();
            
            return translation;
        }

        private static Dictionary<string, string> GetTranslation_German()
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();

            return translation;
        }

        private static Dictionary<string, string> GetTranslation_Dutch()
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();

            return translation;
        }

        private static Dictionary<string, string> GetTranslation_French()
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();

            return translation;
        }

        private static Dictionary<string, string> GetTranslation_Spanish()
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();

            return translation;
        }

        private static Dictionary<string, string> GetTranslation_Khmer()
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();

            return translation;
        }

        private static Dictionary<string, string> GetTranslation_Bahasa()
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();

            return translation;
        }

        private static Dictionary<string, string> GetTranslation_Portuguese()
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();

            return translation;
        }

        private static Dictionary<string, string> GetTranslation_LabelCheck()
        {
            //testing
            Dictionary<string, string> translation = new Dictionary<string, string>();
            translation.Add("TestMultiLanguage_Title", "Test višejezičnosti");
            translation.Add("TestMultiLanguage_Label1", "Obična strana");
            translation.Add("TestMultiLanguage_Label2", "Obična strana je najjednostavniji tip strane.");
            translation.Add("TestMultiLanguage_Label3", "Sadržaj obične strane je generalno... kako ovo glupo zvuči kad se prevede :)");
            translation.Add("TestMultiLanguage_Button", "Pritisni me");

            return translation;
        }

        #endregion
    }

    public enum Languages
    {
        English = 1,
        Chinese,
        German,
        Dutch,
        French,
        Spanish,
        Khmer,
        Bahasa,
        Portuguese,
        LabelCheck
    }
}
