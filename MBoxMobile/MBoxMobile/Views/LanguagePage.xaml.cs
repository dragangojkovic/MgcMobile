using MBoxMobile.Helpers;
using MBoxMobile.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LanguagePage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;
        Dictionary<int, string> languages;

        SideView pageMaster;
        Page pageToReturn;

        public LanguagePage(SideView master, Page fromPage)
        {
            InitializeComponent();

            pageMaster = master;
            pageToReturn = fromPage;

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["LogoWidth"] = screenWidth * 0.5;
            Resources["LogoHeight"] = screenHeight * 0.22;

            Resources["Language_Title"] = App.CurrentTranslation["Language_Title"];
            Resources["Language_InfoText"] = App.CurrentTranslation["Language_InfoText"];
            Resources["Language_SelectLanguage"] = App.CurrentTranslation["Language_SelectLanguage"];
            Resources["Language_Update"] = App.CurrentTranslation["Language_Update"];
            Resources["Language_Cancel"] = App.CurrentTranslation["Language_Cancel"];

            languages = MultiLanguageSupport.GetLanguages();
            if (LanguagePicker.Items.Count == 0)
            {
                foreach (KeyValuePair<int, string> pair in languages)
                {
                    LanguagePicker.Items.Add(pair.Value);
                }
            }

            LanguagePicker.SelectedIndexChanged += LanguagePicker_SelectedIndexChanged;
        }

        private void LanguagePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LanguagePicker.SelectedIndex != -1)
            {
                string selectedLanguage = LanguagePicker.Items[LanguagePicker.SelectedIndex];
                int languageId = languages.FirstOrDefault(x => x.Value == selectedLanguage).Key;

                Dictionary<string, string> currentTranslation = MultiLanguageSupport.GetTranslations((Languages)languageId);
                Resources["Language_InfoText"] = currentTranslation["Language_InfoText"];
                Resources["Language_Update"] = currentTranslation["Language_Update"];
                Resources["Language_Cancel"] = currentTranslation["Language_Cancel"];
            }
        }

        public async void UpdateClicked(object sender, EventArgs e)
        {
            if (LanguagePicker.SelectedIndex != -1)
            {
                string selectedLanguage = LanguagePicker.Items[LanguagePicker.SelectedIndex];
                int languageId = languages.FirstOrDefault(x => x.Value == selectedLanguage).Key;
                App.CurrentLanguage = (Languages)languageId;
                Plugin.Settings.CrossSettings.Current.AddOrUpdateValue("LANGUAGE", languageId.ToString());

                pageMaster.Detail = pageToReturn;
            }
            else
            {
                await DisplayAlert("M-Box", App.CurrentTranslation["Language_AlertMessage"], "OK");
            }
        }

        public void CancelClicked(object sender, EventArgs e)
        {
            pageMaster.Detail = pageToReturn;
        }
    }
}
