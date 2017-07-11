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
        // helper field for switching IsPresented property on phone/tablet devices
        // we don't want side bar be always open on phones, but we do for tablets
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
                        Resources["Menu_Home"] = App.CurrentTranslation["Menu_Home"];
                        Resources["ItemHomeVisible"] = true;
                        break;
                    case 2:
                        Resources["Menu_Language"] = App.CurrentTranslation["Menu_Language"];
                        Resources["ItemLanguageVisible"] = true;
                        break;
                    case 3:
                        Resources["Menu_Logout"] = App.CurrentTranslation["Menu_Logout"];
                        Resources["ItemLogoutVisible"] = true;
                        break;
                    case 4:
                        Resources["Menu_Exit"] = App.CurrentTranslation["Menu_Exit"];
                        Resources["ItemExitVisible"] = true;
                        break;
                }
            }

            isPresented = false;
            MasterBehavior = MasterBehavior.Popover;

            //if (Device.Idiom == TargetIdiom.Phone)
            //{
            //    MasterBehavior = MasterBehavior.Popover;
            //}
            //else if (Device.Idiom == TargetIdiom.Tablet)
            //{
            //    MasterBehavior = MasterBehavior.SplitOnLandscape;
            //    isPresented = true;
            //}
            //else
            //{
            //    MasterBehavior = MasterBehavior.Popover;
            //}

            double screenWidth = DependencyService.Get<IDisplay>().Width;
            double screenHeight = DependencyService.Get<IDisplay>().Height;
            Resources["HeaderHeight"] = (int)(screenHeight / 5);
            Resources["LabelNameMargin"] = new Thickness(0, screenHeight / 10, 0, 0);
            Resources["IconWidthHeight"] = 28;

            Detail = new NavigationPage(new MainPage()) { BarTextColor = Color.White, BarBackgroundColor = (Color)Application.Current.Resources["BlueMedium"] };
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
    }
}
