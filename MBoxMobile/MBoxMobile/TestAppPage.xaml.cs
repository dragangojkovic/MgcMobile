using MBoxMobile.Views;
using System;
using Xamarin.Forms;

namespace MBoxMobile
{
    public partial class TestAppPage : ContentPage
    {
        public TestAppPage()
        {
            InitializeComponent();
        }

        private async void ButtonPhoto_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PhotoPage());
        }

        private async void ButtonBarcode_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BarcodePage());
        }

        private async void ButtonEnglish_Clicked(object sender, EventArgs e)
        {
            App.CurrentLanguage = Helpers.Languages.English;
            await Navigation.PushAsync(new TestMultiLanguagePage());
        }

        private async void ButtonSrpski_Clicked(object sender, EventArgs e)
        {
            App.CurrentLanguage = Helpers.Languages.LabelCheck;
            await Navigation.PushAsync(new TestMultiLanguagePage());
        }

        private async void ButtonUserType1_Clicked(object sender, EventArgs e)
        {
            App.UserType = 1;
            await Navigation.PushAsync(new Views.TestMainPage());
        }

        private async void ButtonUserType2_Clicked(object sender, EventArgs e)
        {
            App.UserType = 2;
            await Navigation.PushAsync(new Views.TestMainPage());
        }
    }
}
