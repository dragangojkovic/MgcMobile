using MBoxMobile.Interfaces;
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
            Application.Current.MainPage = new SideView();
            //Application.Current.MainPage = new TestAccordionPage();
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
