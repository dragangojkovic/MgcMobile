using MBoxMobile.CustomControls;
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

            Resources["LogoWidth"] = screenWidth * 0.6;
            Resources["LogoHeight"] = screenHeight * 0.264;
            Resources["ButtonWidth"] = (screenWidth - 26) / 2.0;
                        
            MessagingCenter.Subscribe<string>(this, "NotificationPopupClosed", (sender) => {
                App.IsNotificationHandling = false;
                App.NotificationsForHandling.Remove(App.NotificationsForHandling[0]);

                HandleReceivedNotifications();
            });

            MessagingCenter.Subscribe<string>(this, "ErrorOccured", async (sender) =>
            {
                if (!App.NoConnectivityMsgShown)
                {
                    App.NoConnectivityMsgShown = true;
                    await DisplayAlert("M-Box", App.LastErrorMessage, App.CurrentTranslation["Common_OK"]);
                    App.NoConnectivityMsgShown = false;
                }
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

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

            HandleReceivedNotifications();
        }

        private async void Button1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UptimePage());
        }

        private async void Button2_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ElectricityUsagePage());
        }

        private async void Button3_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductionPage());
        }

        private async void Button4_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotificationPage());
        }

        private async void Button5_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AuxiliaryEquipmentPage());
        }

        private void HandleReceivedNotifications()
        {
            if (App.NotificationsForHandling != null && App.NotificationsForHandling.Count > 0 && !App.IsNotificationHandling)
            {
                //NotificationModel currentNotification = App.NotificationsForHandling[0];
                //if (currentNotification.Popup >= 1 && currentNotification.Popup <= 7)
                //{
                //    switch (currentNotification.Popup)
                //    {
                //        case 1:
                //            Navigation.PushModalAsync(new NotificationReplyType1Page(currentNotification, true));
                //            break;
                //        case 2:
                //            Navigation.PushModalAsync(new NotificationReplyType2Page(currentNotification, true));
                //            break;
                //        case 3:
                //            Navigation.PushModalAsync(new NotificationReplyType3Page(currentNotification, true));
                //            break;
                //        case 4:
                //            Navigation.PushModalAsync(new NotificationReplyType4Page(currentNotification, true));
                //            break;
                //        case 5:
                //            Navigation.PushModalAsync(new NotificationReplyType5Page(currentNotification, true));
                //            break;
                //        case 6:
                //            Navigation.PushModalAsync(new NotificationReplyType6Page(currentNotification, true));
                //            break;
                //        case 7:
                //            Navigation.PushModalAsync(new NotificationReplyType7Page(currentNotification, true));
                //            break;
                //    }
                //}
                App.NotificationsForHandling.Remove(App.NotificationsForHandling[0]);
                Navigation.PushAsync(new NotificationPage());
            }
        }

    }
}
