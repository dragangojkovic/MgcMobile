using MBoxMobile.Helpers;
using MBoxMobile.Interfaces;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SideView : MasterDetailPage
    {
        bool isPresented;

        public SideView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            Resources["UserFullName"] = App.LoggedUser.login.FirstName;

            Dictionary<int, string> dictMenuItems = UserTypesSupport.GetMenuItems(App.UserType);
            foreach (KeyValuePair<int, string> pair in dictMenuItems)
            {
                switch (pair.Key)
                {
                    case 1:
                        Resources["ItemHomeVisible"] = true;
                        break;
                    case 2:
                        Resources["ItemLanguageVisible"] = true;
                        break;
                    case 3:
                        Resources["ItemLogoutVisible"] = true;
                        break;
                    case 4:
                        Resources["ItemExitVisible"] = true;
                        break;
                }
            }

            isPresented = false;
            MasterBehavior = MasterBehavior.Popover;

            double screenWidth = DependencyService.Get<IDisplay>().Width;
            double screenHeight = DependencyService.Get<IDisplay>().Height;
            Resources["HeaderHeight"] = (int)(screenHeight * 0.17);
            Resources["LabelNameMargin"] = new Thickness(0, screenHeight * 0.08, 0, 0);
            Resources["IconWidthHeight"] = 28;

            Detail = new NavigationPage(new MainPage()) { BarTextColor = Color.White, BarBackgroundColor = (Color)Application.Current.Resources["BlueMedium"] };

            this.IsPresentedChanged += SideView_IsPresentedChanged;
        }

        private void SideView_IsPresentedChanged(object sender, EventArgs e)
        {
            Resources["Menu_Home"] = App.CurrentTranslation["Menu_Home"];
            Resources["Menu_Language"] = App.CurrentTranslation["Menu_Language"];
            Resources["Menu_Logout"] = App.CurrentTranslation["Menu_Logout"];
            Resources["Menu_Exit"] = App.CurrentTranslation["Menu_Exit"];
            Resources["Menu_About"] = App.CurrentTranslation["Menu_About"];
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Resources["Menu_Home"] = App.CurrentTranslation["Menu_Home"];
            Resources["Menu_Language"] = App.CurrentTranslation["Menu_Language"];
            Resources["Menu_Logout"] = App.CurrentTranslation["Menu_Logout"];
            Resources["Menu_Exit"] = App.CurrentTranslation["Menu_Exit"];
            Resources["Menu_About"] = App.CurrentTranslation["Menu_About"];
        }

        public void HomeTapped(object sender, EventArgs e)
        {
            if (!(MasterBehavior == MasterBehavior.SplitOnLandscape) && Device.Idiom != TargetIdiom.Tablet)
                IsPresented = isPresented;

            Detail = new NavigationPage(new MainPage()) { BarTextColor = Color.White, BarBackgroundColor = (Color)Application.Current.Resources["BlueMedium"] };
        }

        public void LanguageTapped(object sender, EventArgs e)
        {
            if (!(MasterBehavior == MasterBehavior.SplitOnLandscape) && Device.Idiom != TargetIdiom.Tablet)
                IsPresented = isPresented;

            Page currentDetailPage = Detail;
            Detail = new NavigationPage(new LanguagePage(this, currentDetailPage)) { BarTextColor = Color.White, BarBackgroundColor = (Color)Application.Current.Resources["BlueMedium"] };
        }

        public void LogoutTapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new LoginPage());

            App.IsNotificationHandling = false;
            App.LoggedUser = null;
            App.LastErrorMessage = string.Empty;

            DependencyService.Get<ISecureStorage>().Delete();
        }

        public void ExitTapped(object sender, EventArgs e)
        {
            var closer = DependencyService.Get<ICloseApplication>();
            if (closer != null)
                closer.CloseApplicationHandler();
        }

        public void AboutTapped(object sender, EventArgs e)
        {
            if (!(MasterBehavior == MasterBehavior.SplitOnLandscape) && Device.Idiom != TargetIdiom.Tablet)
                IsPresented = isPresented;

            Detail = new NavigationPage(new AboutPage()) { BarTextColor = Color.White, BarBackgroundColor = (Color)Application.Current.Resources["BlueMedium"] };
        }
    }
}
