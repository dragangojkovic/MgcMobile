﻿using MBoxMobile.CustomControls;
using MBoxMobile.Helpers;
using MBoxMobile.Interfaces;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;

        public MainPage()
        {
            InitializeComponent();

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["LogoWidth"] = screenWidth * 0.5;
            Resources["LogoHeight"] = screenHeight * 0.22;
            Resources["ButtonWidth"] = (screenWidth - 26) / 2.0;

            Dictionary<int, string> dictButtons = UserTypesSupport.GetButtons(App.UserType);

            // add empty rows - one is already added in xaml
            for (int i = 1; i < dictButtons.Count; i++)
            {
                GridButtons.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            // add buttons in grid rows, max 2 per row
            int dictOrdinal = 0;
            foreach (KeyValuePair<int, string> pair in dictButtons)
            {
                int rowOrdinal = dictOrdinal / 2;
                int left;
                if (dictOrdinal % 2 == 0)
                    left = 0;
                else
                    left = 1;

                //Button b = new Button { Text = App.CurrentTranslation[pair.Value], Style = (Style)Application.Current.Resources["ButtonBlueStyle"] };
                RoundedButton b = new RoundedButton { Text = App.CurrentTranslation[pair.Value], Style = (Style)Application.Current.Resources["ButtonBlueStyle"] };
                switch (pair.Key)
                {
                    case 1:
                        b.Clicked += Button1_Clicked; break;
                    case 2:
                        b.Clicked += Button2_Clicked; break;
                    case 3:
                        b.Clicked += Button3_Clicked; break;
                    case 4:
                        b.Clicked += Button4_Clicked; break;
                    case 5:
                        b.Clicked += Button5_Clicked; break;
                }

                GridButtons.Children.Add(b, left, rowOrdinal);
                dictOrdinal++;
            }
        }

        private async void Button1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UptimePage());
        }

        private async void Button2_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Alert", "You have clicked Electricity usage", "OK");
        }

        private async void Button3_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Alert", "You have clicked Production", "OK");
        }

        private async void Button4_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Alert", "You have clicked Notifications", "OK");
        }

        private async void Button5_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Alert", "You have clicked Auxiliary equipment", "OK");
        }
    }
}
