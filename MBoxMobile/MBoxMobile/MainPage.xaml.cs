using MBoxMobile.Views;
using System;
using Xamarin.Forms;

namespace MBoxMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
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
    }
}
