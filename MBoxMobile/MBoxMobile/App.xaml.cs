using MBoxMobile.Helpers;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MBoxMobile
{
    public partial class App : Application
    {
        private static Languages _currentLanguage;
        public static Languages CurrentLanguage
        {
            get { return _currentLanguage; }
            set
            {
                _currentLanguage = value;
                CurrentTranslation = MultiLanguageSupport.GetTranslations(CurrentLanguage);
            }
        }
        public static Dictionary<string, string> CurrentTranslation { get; set; }

        public static int UserType { get; set; }

        public App()
        {
            InitializeComponent();
            CurrentLanguage = Languages.English;
                        
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
