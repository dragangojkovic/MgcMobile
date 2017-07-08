﻿using MBoxMobile.Helpers;
using MBoxMobile.Models;
using MBoxMobile.Views;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MBoxMobile.CustomControls
{
    public class NotificationGroupButton : ContentView
    {
        public List<NotificationModel> RelatedNotifications { get; set; }
        private string GroupName { get; set; }

        public NotificationGroupButton(string groupName, int count, double buttonWidth, double buttonHeight)
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Padding = new Thickness(10, 0, 0, 0);

            GroupName = groupName;

            var vButton = new Button()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = (Color)Application.Current.Resources["BlueMedium"],
                BorderColor = Color.Black,
                BorderRadius = 5,
                BorderWidth = 1,
                WidthRequest = buttonWidth - 40,
                HeightRequest = buttonHeight,
                Text = groupName
            };
            vButton.Clicked += Button_Clicked;

            var vNameLabel = new Label()
            {
                Text = groupName,
                BackgroundColor = (Color)Application.Current.Resources["BlueMedium"],
                VerticalTextAlignment = TextAlignment.Center,
                Style = (Style)Application.Current.Resources["LabelMediumStyle"]
            };

            var vCountLabel = new Label()
            {
                Text = count.ToString(),
                BackgroundColor = (Color)Application.Current.Resources["BlueMedium"],
                HorizontalTextAlignment = TextAlignment.End,
                VerticalTextAlignment = TextAlignment.Center,
                Style = (Style)Application.Current.Resources["LabelMediumStyle"]
            };

            var vMainLayout = new StackLayout();
            RelativeLayout relativeLayout = new RelativeLayout();
            relativeLayout.Children.Add(vButton, Constraint.RelativeToParent((parent) => { return 0; }));
            relativeLayout.Children.Add(vNameLabel,
                Constraint.RelativeToView(vButton, (parent, sibling) => { return sibling.X + 15; }),
                Constraint.RelativeToView(vButton, (parent, sibling) => { return sibling.Y + 10; }),
                Constraint.RelativeToView(vButton, (parent, sibling) => { return sibling.Width * .7; }),
                Constraint.RelativeToView(vButton, (parent, sibling) => { return sibling.Height - 20; }));
            relativeLayout.Children.Add(vCountLabel,
                Constraint.RelativeToView(vButton, (parent, sibling) => { return sibling.X + 10 + sibling.Width * .7; }),
                Constraint.RelativeToView(vButton, (parent, sibling) => { return sibling.Y + 10; }),
                Constraint.RelativeToView(vButton, (parent, sibling) => { return sibling.Width * .3 - 45; }),
                Constraint.RelativeToView(vButton, (parent, sibling) => { return sibling.Height - 20; }));

            vMainLayout.Children.Add(relativeLayout);
            Content = vMainLayout;
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NotificationGroupPage(GroupName, RelatedNotifications));
        }
    }
}
