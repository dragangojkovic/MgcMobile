using MBoxMobile.Helpers;
using MBoxMobile.Interfaces;
using MBoxMobile.Models;
using MBoxMobile.Services;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        int serverId = -1;
        bool rememberMe = false;

        LoginPageViewModel loginViewModel = new LoginPageViewModel();

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = loginViewModel;

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

        public async void LoginClicked(object sender, EventArgs e)
        {
            if (ServerPicker.SelectedIndex != -1)
            {
                string selectedServer = ServerPicker.Items[ServerPicker.SelectedIndex];
                serverId = App.Servers.FirstOrDefault(x => x.Value == selectedServer).Key;
            }
            else
            {
                serverId = -1;
            }

            CustomerDetail customer = new CustomerDetail();
            customer.ServerId = serverId;
            customer.Username = loginViewModel.Username != null ? loginViewModel.Username.Trim() : string.Empty;
            customer.Password = loginViewModel.Password != null ? loginViewModel.Password.Trim() : string.Empty;
            if (Device.OS == TargetPlatform.Android)
                customer.Platform = "Android";
            if (Device.OS == TargetPlatform.iOS)
                customer.Platform = "iOS";
            customer.DeviceToken = CrossSettings.Current.GetValueOrDefault("DEVICE_TOKEN", string.Empty);

            //TODO: remove after testing
            customer.DeviceToken = "TestToken12345";

            int status = await LoginCustomer.GetLoginStatus(customer);

            if (status == 10000)
            {
                if (rememberMe)
                {
                    DependencyService.Get<ISecureStorage>().Save(serverId.ToString(), customer.Username, customer.Password);
                }
                Application.Current.MainPage = new SideView();
            }
            else
            {
                await DisplayAlert(App.CurrentTranslation["Login_Title"], App.LastErrorMessage, App.CurrentTranslation["Common_OK"]);
            }
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
        private int serverId;
        public int ServerId
        {
            get { return serverId; }
            set
            {
                if (serverId != value)
                {
                    serverId = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ServerId"));
                    }
                }
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Username"));
                    }
                }
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Password"));
                    }
                }
            }
        }

        //private bool rememberMe;
        //public bool RememberMe
        //{
        //    get { return rememberMe; }
        //    set
        //    {
        //        if (rememberMe != value)
        //        {
        //            rememberMe = value;
        //            if (PropertyChanged != null)
        //            {
        //                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RememberMe"));
        //            }
        //        }
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
