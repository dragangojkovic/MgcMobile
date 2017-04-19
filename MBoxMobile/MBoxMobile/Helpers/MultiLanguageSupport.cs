using System.Collections.Generic;

namespace MBoxMobile.Helpers
{
    public static class MultiLanguageSupport
    {
        public static Dictionary<int, string> GetLanguages()
        {
            Dictionary<int, string> languages = new Dictionary<int, string>();
            languages.Add(1, "English");
            languages.Add(2, "中文");
            languages.Add(3, "Deutsch");
            languages.Add(4, "Nederlands");
            languages.Add(5, "Français");
            languages.Add(6, "Español");
            languages.Add(7, "Khmer");
            languages.Add(8, "Bahasa");
            languages.Add(9, "Português");
            languages.Add(10, "LabelCheck");

            return languages;
        }

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
            Dictionary<string, string> translation = new Dictionary<string, string>();

            //Menu - Side view
            translation.Add("Menu_Home", "Home");
            translation.Add("Menu_Language", "Language");
            translation.Add("Menu_Logout", "Logout");
            translation.Add("Menu_Exit", "Exit");

            //Login page
            translation.Add("Login_Title", "Login");
            translation.Add("Login_SelectServer", "Select server");
            translation.Add("Login_Username", "Please input username");
            translation.Add("Login_Password", "Please input password");
            translation.Add("Login_RememberMe", "Remember me");
            translation.Add("Login_Login", "Login");
            translation.Add("Login_ForgotPassword", "Forgot password?");

            //ForgotPassword page
            translation.Add("Forgot_Title", "Forgot password?");
            translation.Add("Forgot_InfoText", "Registered email address");
            translation.Add("Forgot_Email", "Please input your email");
            translation.Add("Forgot_Send", "Request new password");

            //Main page
            translation.Add("Main_Efficiency", "Efficiency");
            translation.Add("Main_ElectricityUsage", "Electricity usage");
            translation.Add("Main_Production", "Production");
            translation.Add("Main_Notifications", "Notifications");
            translation.Add("Main_AuxiliaryEquipment", "Auxiliary equipment");

            //Language page
            translation.Add("Language_Title", "Language");
            translation.Add("Language_InfoText", "Select language from the list");
            translation.Add("Language_SelectLanguage", "Select language");
            translation.Add("Language_Update", "Update");
            translation.Add("Language_Cancel", "Cancel");
            translation.Add("Language_AlertMessage", "Nothing selected!");

            //testing
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
