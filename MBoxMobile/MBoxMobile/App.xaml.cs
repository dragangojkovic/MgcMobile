using MBoxMobile.Helpers;
using MBoxMobile.Interfaces;
using MBoxMobile.Models;
using MBoxMobile.Views;
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
            set { _currentLanguage = value; CurrentTranslation = MultiLanguageSupport.GetTranslations(CurrentLanguage); }
        }
        public static Dictionary<string, string> CurrentTranslation { get; set; }
        public static int UserType { get; set; }
        public static Dictionary<int, string> Servers { get; set; }

        public static UserInfo LoggedUser { get; set; }
        public static string LastErrorMessage { get; set; }
        public static bool NoConnectivityMsgShown { get; set; }
        public static bool IsNotificationHandling { get; set; }
        public static List<NotificationModel> NotificationsForHandling { get; set; }
        public static bool ShouldReloadNotifications { get; set; }
        public static string PlayServiceStatus { get; set; }

        public App()
        {
            InitializeComponent();

            CurrentLanguage = Languages.English;
            UserType = 1;

            Servers = new Dictionary<int, string>();
            Servers.Add(1, "s10.monitor-box.com");
            Servers.Add(2, "s12.monitor-box.com");

            NoConnectivityMsgShown = false;
            IsNotificationHandling = false;
            ShouldReloadNotifications = false;

            CheckUserLoginStatus();
        }

        private void CheckUserLoginStatus()
        {
            string savedCredentials = DependencyService.Get<ISecureStorage>().Get();
            if (savedCredentials == null)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                string[] credentials = savedCredentials.Split('#');
                MainPage = new LoginCheckPage(credentials[2], credentials[0], credentials[1]);
            }
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
