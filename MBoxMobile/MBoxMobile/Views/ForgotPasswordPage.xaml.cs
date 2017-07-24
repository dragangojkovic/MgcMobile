using MBoxMobile.Interfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;

        public ForgotPasswordPage()
        {
            InitializeComponent();

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["LogoWidth"] = screenWidth * 0.5;
            Resources["LogoHeight"] = screenHeight * 0.22;
            
            Resources["IsLoading"] = false;
            Resources["IsFormVisible"] = true;

            Resources["Forgot_Title"] = App.CurrentTranslation["Forgot_Title"];
            Resources["Forgot_InfoText"] = App.CurrentTranslation["Forgot_InfoText"];
            Resources["Forgot_Email"] = App.CurrentTranslation["Forgot_Email"];
            Resources["Forgot_Send"] = App.CurrentTranslation["Forgot_Send"];
        }

        public async void SendClicked(object sender, EventArgs e)
        {
            await DisplayAlert("M-Box", "Not implemented yet!", "OK");
        }
    }
}
