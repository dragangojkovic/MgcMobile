using MBoxMobile.Interfaces;
using Plugin.Settings;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;

        public AboutPage()
        {
            InitializeComponent();

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["LogoWidth"] = screenWidth * 0.6;
            Resources["LogoHeight"] = screenHeight * 0.264;

            Resources["AppVersion"] = DependencyService.Get<IVersion>().Version;
            Resources["AppToken"] = CrossSettings.Current.GetValueOrDefault("DEVICE_TOKEN", string.Empty);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Resources["About_Title"] = App.CurrentTranslation["Menu_About"];
            Resources["About_Version"] = App.CurrentTranslation["About_Version"];
            Resources["About_Token"] = App.CurrentTranslation["About_Token"];
        }
    }
}
