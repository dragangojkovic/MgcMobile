using MBoxMobile.CustomControls;
using MBoxMobile.Helpers;
using MBoxMobile.Interfaces;
using MBoxMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationGroupPage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;
        bool AreTablesPopulated = false;

        List<NotificationModel> NotificationList;
        List<NotificationGroupInfo> SubGroupsInfoList;

        int selectedNotificationID = -1;

        public NotificationGroupPage(string notificationGroup, List<NotificationModel> notifications)
        {
            InitializeComponent();

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["ContentMinHeight"] = screenHeight - 60.0;
            Resources["NotificationGroup_Title"] = notificationGroup;
            NotificationList = notifications;

            SubGroupsInfoList = GetSubGroupsInfoList();

            MessagingCenter.Subscribe<string>(this, "NotificationPopupClosedWithAction", async (sender) =>
            {
                App.IsNotificationHandling = false;
                App.ShouldReloadNotifications = true;

                if (Navigation.ModalStack.Count > 0)
                    await Navigation.PopModalAsync();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Resources["Common_Close"] = App.CurrentTranslation["Common_Close"];

            if (!AreTablesPopulated)
            {
                Resources["IsLoading"] = true;
                NotificationGroupsAccordion.AccordionWidth = screenWidth - 30;
                NotificationGroupsAccordion.AccordionHeight = 55.0;
                NotificationGroupsAccordion.DataSource = GetAccordionData();
                NotificationGroupsAccordion.DataBind();
                Resources["IsLoading"] = false;

                AreTablesPopulated = true;
            }
        }

        public async void CloseClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private List<AccordionSource> GetAccordionData()
        {
            var result = new List<AccordionSource>();

            foreach(NotificationGroupInfo ngi in SubGroupsInfoList)
            {
                double contentHeight;
                View content = CreateAndPopulateWebViews(ngi, out contentHeight);

                var asCurrent = new AccordionSource()
                {
                    HeaderText = string.Format("{0} ({1})", ngi.GroupName, ngi.GroupItemCount), // App.CurrentTranslation["????"],
                    ContentItems = content,
                    ContentHeight = contentHeight
                };
                if (screenWidth <= 360) asCurrent.HeaderFontSize = 14;
                result.Add(asCurrent);
            }

            return result;
        }

        private View CreateAndPopulateWebViews(NotificationGroupInfo subGroup, out double viewHeight)
        {
            viewHeight = 0;
            const double WV_ROW_Height = 32.75;
            StackLayout result = new StackLayout();
            List<NotificationModel> notifs = NotificationList.Where(x => x.AlterDescription == subGroup.GroupName).ToList();

            //4th level of grouping - there can be more than one table in sub-group
            List<int> subSubGroups = notifs.Select(x => x.DataType).Distinct().ToList();
            foreach(int tableType in subSubGroups)
            {
                // 0. create new table (vw)
                WebView wv = new WebView();
                wv.WidthRequest = screenWidth - 35;
                wv.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
                wv.VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
                wv.Margin = new Thickness(0, 0, 0, 5);
                wv.Navigating += (s, e) =>
                {
                    if (e.Url != string.Empty)
                    {
                        selectedNotificationID = int.Parse(e.Url.Split('=').LastOrDefault());
                        // call pop-up regarding notification type
                        NotificationModel currentNotification = NotificationList.Where(x => x.ID == selectedNotificationID).FirstOrDefault();
                        if (currentNotification.Popup >= 1 && currentNotification.Popup <= 7)
                        {
                            switch (currentNotification.Popup)
                            {
                                case 1:
                                    Navigation.PushModalAsync(new NotificationReplyType1Page(currentNotification));
                                    break;
                                case 2:
                                    Navigation.PushModalAsync(new NotificationReplyType2Page(currentNotification));
                                    break;
                                case 3:
                                    Navigation.PushModalAsync(new NotificationReplyType3Page(currentNotification));
                                    break;
                                case 4:
                                    Navigation.PushModalAsync(new NotificationReplyType4Page(currentNotification));
                                    break;
                                case 5:
                                    Navigation.PushModalAsync(new NotificationReplyType5Page(currentNotification));
                                    break;
                                case 6:
                                    Navigation.PushModalAsync(new NotificationReplyType6Page(currentNotification));
                                    break;
                                case 7:
                                    Navigation.PushModalAsync(new NotificationReplyType7Page(currentNotification));
                                    break;
                            }
                        }
                    }
                    e.Cancel = true;
                };

                // 1. get notifs for this table
                List<NotificationModel> data = notifs.Where(x => x.DataType == tableType).ToList();

                // 2. check what table look we should use here and populate
                string htmlHeader = string.Empty;
                string htmlContent = string.Empty;
                switch (tableType.ToString())
                {
                    case "1":
                        htmlHeader = HtmlTableSupport.NotificationDataType1_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType1_TableContent(data);
                        break;
                    case "2":
                        htmlHeader = HtmlTableSupport.NotificationDataType2_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType2_TableContent(data);
                        break;
                    case "3":
                        htmlHeader = HtmlTableSupport.NotificationDataType3_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType3_TableContent(data);
                        break;
                    case "4":
                        htmlHeader = HtmlTableSupport.NotificationDataType4_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType4_TableContent(data);
                        break;
                    case "5":
                        htmlHeader = HtmlTableSupport.NotificationDataType5_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType5_TableContent(data);
                        break;
                    case "6":
                        htmlHeader = HtmlTableSupport.NotificationDataType6_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType6_TableContent(data);
                        break;
                    case "7":
                        htmlHeader = HtmlTableSupport.NotificationDataType7_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType7_TableContent(data);
                        break;
                    case "8":
                        htmlHeader = HtmlTableSupport.NotificationDataType8_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType8_TableContent(data);
                        break;
                    case "9":
                        htmlHeader = HtmlTableSupport.NotificationDataType9_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType9_TableContent(data);
                        break;
                    case "10":
                        htmlHeader = HtmlTableSupport.NotificationDataType10_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType10_TableContent(data);
                        break;
                    case "11":
                        htmlHeader = HtmlTableSupport.NotificationDataType11_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType11_TableContent(data);
                        break;
                }

                string htmlHtmlDetails = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeader, htmlContent);
                wv.Source = new HtmlWebViewSource { Html = htmlHtmlDetails };
                viewHeight += (data.Count() + 1) * WV_ROW_Height + 10;
                wv.HeightRequest = viewHeight;

                // 3. add to View
                result.Children.Add(wv);
            }

            return result;
        }

        private List<NotificationGroupInfo> GetSubGroupsInfoList()
        {
            List<NotificationGroupInfo> descriptions = new List<NotificationGroupInfo>();
            
            foreach(NotificationModel nm in NotificationList)
            {
                string description = nm.AlterDescription;
                if (descriptions.Count == 0 || descriptions.Where(d => d.GroupName == description).Count() == 0)
                {
                    int notifCount = NotificationList.Where(x => x.AlterDescription == description).Count();
                    descriptions.Add(new NotificationGroupInfo() { GroupName = description, GroupItemCount = notifCount });
                }
            }
            
            return descriptions;
        }
    }
}
