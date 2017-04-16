using MBoxMobile.Helpers;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestMultiLanguagePage : ContentPage
    {
        public TestMultiLanguagePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Resources["TestPageTitle"] = App.CurrentTranslation["TestMultiLanguage_Title"];
            Resources["Label1Text"] = App.CurrentTranslation["TestMultiLanguage_Label1"];
            Resources["Label2Text"] = App.CurrentTranslation["TestMultiLanguage_Label2"];
            Resources["Label3Text"] = App.CurrentTranslation["TestMultiLanguage_Label3"];
            Resources["ButtonText"] = App.CurrentTranslation["TestMultiLanguage_Button"];
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
