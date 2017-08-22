using MBoxMobile.Interfaces;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginNotificationPage : ContentPage
    {
        double ScreenWidth = 0.0;
        double ScreenHeight = 0.0;
        bool AreNotificationsEnabled = true;

        LoginNotificationPageViewModel loginNotifsViewModel = new LoginNotificationPageViewModel();

        public LoginNotificationPage()
        {
            InitializeComponent();
            BindingContext = loginNotifsViewModel;

            ScreenWidth = DependencyService.Get<IDisplay>().Width;
            ScreenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["LogoWidth"] = ScreenWidth * 0.5;
            Resources["LogoHeight"] = ScreenHeight * 0.22;
            Resources["IsLoading"] = false;

            Resources["RadioBoxAreaWidth"] = ScreenWidth * 0.09;
            Resources["RadioBoxSourceYes"] = "fullradio50.png";
            Resources["RadioBoxSourceNo"] = "emptyradio50.png";
            Resources["IsDeviceNameVisible"] = true;
            Resources["IsSubmitEnabled"] = false;

            DeviceName.TextChanged += (s, e) =>
            {
                if (DeviceName.Text.Trim().Length > 0)
                    Resources["IsSubmitEnabled"] = true;
                else
                    Resources["IsSubmitEnabled"] = false;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Resources["LoginNotification_InfoText1"] = App.CurrentTranslation["LoginNotification_InfoText1"];
            Resources["LoginNotification_InfoText2"] = App.CurrentTranslation["LoginNotification_InfoText2"];
            Resources["LoginNotification_YesText"] = App.CurrentTranslation["LoginNotification_YesText"];
            Resources["LoginNotification_NoText"] = App.CurrentTranslation["LoginNotification_NoText"];
            Resources["LoginNotification_DeviceName"] = App.CurrentTranslation["LoginNotification_DeviceName"];
            Resources["LoginNotification_Submit"] = App.CurrentTranslation["LoginNotification_Submit"];
        }

        public async void SubmitClicked(object sender, EventArgs e)
        {
            //there is no API for sending device name and enabling / disabling notifications
            string deviceName = loginNotifsViewModel.DeviceName;

            Application.Current.MainPage = new SideView();
        }

        public void ButtonYesTapped(object sender, EventArgs e)
        {
            AreNotificationsEnabled = true;
            Resources["RadioBoxSourceYes"] = "fullradio50.png";
            Resources["RadioBoxSourceNo"] = "emptyradio50.png";
            Resources["IsDeviceNameVisible"] = true;
            if (DeviceName.Text != null && DeviceName.Text.Trim().Length > 0)
                Resources["IsSubmitEnabled"] = true;
            else
                Resources["IsSubmitEnabled"] = false;
        }

        public void ButtonNoTapped(object sender, EventArgs e)
        {
            AreNotificationsEnabled = false;
            Resources["RadioBoxSourceYes"] = "emptyradio50.png";
            Resources["RadioBoxSourceNo"] = "fullradio50.png";
            Resources["IsDeviceNameVisible"] = false;
            Resources["IsSubmitEnabled"] = true;
        }
    }

    class LoginNotificationPageViewModel : INotifyPropertyChanged
    {
        private string deviceName;
        public string DeviceName
        {
            get { return deviceName; }
            set
            {
                if (deviceName != value)
                {
                    deviceName = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("DeviceName"));
                    }
                }
            }
        }
                
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
