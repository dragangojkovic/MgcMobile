using MBoxMobile.Helpers;
using MBoxMobile.Interfaces;
using MBoxMobile.Models;
using Plugin.Settings;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginCheckPage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;

        string Server, Username, Password;

        public LoginCheckPage(string server, string username, string password)
        {
            InitializeComponent();

            Server = server;
            Username = username;
            Password = password;

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["LogoWidth"] = screenWidth * 0.5;
            Resources["LogoHeight"] = screenHeight * 0.22;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            CustomerDetail customer = new CustomerDetail();
            customer.ServerId = int.Parse(Server);
            customer.Username = Username;
            customer.Password = Password;
            if (Device.OS == TargetPlatform.Android)
                customer.Platform = "Android";
            if (Device.OS == TargetPlatform.iOS)
                customer.Platform = "iOS";
            customer.DeviceToken = CrossSettings.Current.GetValueOrDefault("DEVICE_TOKEN", string.Empty);
            if (customer.DeviceToken == "")
                customer.DeviceToken = "default_device_token";

                int status = await LoginCustomer.GetLoginStatus(customer);

            if (status == 10000)
            {
                Application.Current.MainPage = new SideView();
            }
            else
            {
                await DisplayAlert(App.CurrentTranslation["Login_Title"], App.LastErrorMessage, App.CurrentTranslation["Common_OK"]);
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}
