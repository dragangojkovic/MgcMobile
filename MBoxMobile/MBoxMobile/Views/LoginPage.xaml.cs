using MBoxMobile.Interfaces;
using MBoxMobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;
        bool rememberMe = false;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginPageViewModel();

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["LogoWidth"] = screenWidth * 0.5;
            Resources["LogoHeight"] = screenHeight * 0.22;
            Resources["CheckboxAreaWidth"] = screenWidth * 0.18;
            Resources["IconAreaWidth"] = 32;

            Resources["CheckboxSource"] = "emptycheck.png";
            Resources["IsLoading"] = false;
            Resources["IsFormVisible"] = true;

            if (ServerPicker.Items.Count == 0)
            {
                foreach (KeyValuePair<int, string> pair in App.Servers)
                {
                    ServerPicker.Items.Add(pair.Value);
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Resources["Login_Title"] = App.CurrentTranslation["Login_Title"];
            Resources["Login_SelectServer"] = App.CurrentTranslation["Login_SelectServer"];
            Resources["Login_Username"] = App.CurrentTranslation["Login_Username"];
            Resources["Login_Password"] = App.CurrentTranslation["Login_Password"];
            Resources["Login_RememberMe"] = App.CurrentTranslation["Login_RememberMe"];
            Resources["Login_Login"] = App.CurrentTranslation["Login_Login"];
            Resources["Login_ForgotPassword"] = App.CurrentTranslation["Login_ForgotPassword"];
        }

        public void LoginClicked(object sender, EventArgs e)
        {
            //ToDo: remove this static data and replace with response from Authenticate method
            UserInfo uInfo = new UserInfo();
            uInfo.status = 10000;
            uInfo.login = new UserLogin()
            {
                BelongToLocationID = "853,885,913,1981",
                BelongToTableID = 539,
                EfficiencyWorkHours = false.ToString(),
                EquipmentDepartmentFilter = "0",
                EquipmentLocationFilter = "0",
                FirstName = "Saša",
                FunctionFilter = "6069",
                IsFreeze1 = "4644",
                LoginID = "salemih@gmail.com",
                MainFilter = true.ToString(),
                MenuLanguage = "1",
                NotificationFilter = true.ToString(),
                RecordId = 9148,
                SelectedNotificationFilter = "0",
                SelectedPersonalFilter = "0",
                ServerIPAddress = "121.33.199.84",
                TInitials = "Mbox_developer",
                Title = "Customer",
                UserGroup = "Customer"
            };
            App.LoggedUser = uInfo;

            Application.Current.MainPage = new SideView();
        }

        public void RememberMeTapped(object sender, EventArgs e)
        {
            rememberMe = !rememberMe;
            if (rememberMe)
                Resources["CheckboxSource"] = "fullcheck.png";
            else
                Resources["CheckboxSource"] = "emptycheck.png";
        }

        public async void ForgotTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPasswordPage());
        }
    }

    class LoginPageViewModel : INotifyPropertyChanged
    {

        


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
