using MBoxMobile.CustomControls;
using MBoxMobile.Helpers;
using MBoxMobile.Interfaces;
using MBoxMobile.Models;
using MBoxMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationPage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;
        bool AreTablesPopulated = false;

        PersonalFilter personalFilter = null;
        bool personalFilterOn = false;
        int? personalFilterId = null;
        NotificationFilter notificationFilter = null;
        bool notificationFilterOn = false;
        int? notificationFilterId = null;
        int timeFilterId = 6566;

        List<NotificationModel> AllNotifications = new List<NotificationModel>();
        List<NotificationModel> NonConfirmedNotifications = new List<NotificationModel>();
        List<NotificationModel> SolutionNotifications = new List<NotificationModel>();
        List<NotificationModel> ToBeApprovedNotifications = new List<NotificationModel>();
        List<NotificationModel> AllReportedNotifications = new List<NotificationModel>();
        List<NotificationModel> AllApprovedNotifications = new List<NotificationModel>();

        int NonConfirmedCount, SolutionCount, ToBeApprovedCount, AllReportedCount, AllApprovedCount;
        StackLayout vNonConfirmed = new StackLayout();
        StackLayout vSolution = new StackLayout();
        StackLayout vToBeApproved = new StackLayout();
        StackLayout vAllReported = new StackLayout();
        StackLayout vAllApproved = new StackLayout();

        public NotificationPage()
        {
            InitializeComponent();

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["Filter2ButtonWidth"] = screenWidth * 0.14;
            Resources["Filter2ButtonHeight"] = screenWidth * 0.09;
            Resources["PersonalFilterButtonWidth"] = screenWidth * 0.4;
            Resources["NotificationFilterButtonWidth"] = screenWidth * 0.37;
            Resources["FilterButtonHeight"] = screenWidth * 0.09;
            Resources["FilterTimeButtonWidth"] = screenWidth - 30;
            Resources["FilterTimeButtonHeight"] = screenWidth * 0.1;
            Resources["ContentMinHeight"] = screenHeight; // - 60.0;

            Resources["Filter2FontSize"] = FilterSupport.GetFilter2FontSizeNotificationPage(screenWidth);

            NotificationAccordion.AccordionWidth = screenWidth - 30;
            NotificationAccordion.AccordionHeight = 55.0;
            NotificationAccordion.DataSource = GetEmptyAccordion();
            NotificationAccordion.DataBind();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (App.ShouldReloadNotifications)
            {
                AreTablesPopulated = false;
                App.ShouldReloadNotifications = false;
            }

            string currentTimeFilter = FilterSupport.GetNotificationFilters()[timeFilterId];

            Resources["Notification_Title"] = App.CurrentTranslation["Notification_Title"];
            Resources["Notification_PersonalFilter"] = App.CurrentTranslation["Notification_PersonalFilter"];
            Resources["Notification_NotificationFilter"] = App.CurrentTranslation["Notification_NotificationFilter"];
            Resources["Common_FilterOn"] = App.CurrentTranslation["Common_FilterOn"];
            Resources["Common_FilterOff"] = App.CurrentTranslation["Common_FilterOff"];
            Resources["Common_Filter"] = App.CurrentTranslation["Common_Filter"];
            Resources["Common_FilterTime"] = App.CurrentTranslation[currentTimeFilter];

            if (!AreTablesPopulated)
            {
                Resources["IsLoading"] = true;
                personalFilter = await MBoxApiCalls.GetPersonalFilter();
                personalFilterOn = personalFilter.FilterOn;
                if (personalFilterOn)
                {
                    Resources["PersonalFilterOnStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
                    Resources["PersonalFilterOffStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["PersonalFilterIsEnabled"] = true;
                    Resources["Common_Filter"] = personalFilter.FilterList.Where(x => x.FilterID == personalFilter.SelectedFilterID).FirstOrDefault().FilterName;
                }
                else
                {
                    Resources["PersonalFilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["PersonalFilterOffStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
                    Resources["PersonalFilterIsEnabled"] = false;
                }

                notificationFilter = await MBoxApiCalls.GetNotificationFilter();
                notificationFilterOn = notificationFilter.FilterOn;
                if (notificationFilterOn)
                {
                    Resources["NotificationFilterOnStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
                    Resources["NotificationFilterOffStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["NotificationFilterIsEnabled"] = true;
                    Resources["Notification_Filter"] = notificationFilter.FilterList.Where(x => x.FilterID == notificationFilter.SelectedFilterID).FirstOrDefault().FilterName;
                }
                else
                {
                    Resources["NotificationFilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["NotificationFilterOffStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
                    Resources["NotificationFilterIsEnabled"] = false;
                }

                NotificationAccordion.AccordionWidth = screenWidth - 30;
                NotificationAccordion.AccordionHeight = 55.0;
                NotificationAccordion.DataSource = await GetAccordionData();
                NotificationAccordion.DataBind();
                Resources["IsLoading"] = false;

                AreTablesPopulated = true;
            }
        }

        public async void PersonalFilterOnClicked(object sender, EventArgs e)
        {
            Resources["PersonalFilterOnStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
            Resources["PersonalFilterOffStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["PersonalFilterIsEnabled"] = true;
            personalFilterOn = true;
            await MBoxApiCalls.SetPersonalFilterOnOff(true);
        }

        public async void PersonalFilterOffClicked(object sender, EventArgs e)
        {
            Resources["PersonalFilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["PersonalFilterOffStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
            Resources["PersonalFilterIsEnabled"] = false;
            personalFilterOn = false;
            await MBoxApiCalls.SetPersonalFilterOnOff(false);
        }

        public async void PersonalFilterClicked(object sender, EventArgs e)
        {
            List<Filter> filters = personalFilter.FilterList;
            int itemCount = 0;

            if (filters != null)
                itemCount = filters.Count;

            string[] items = new string[itemCount];
            for (int i = 0; i < itemCount; i++)
            {
                items[i] = filters[i].FilterName;
            }

            var action = await DisplayActionSheet(App.CurrentTranslation["Common_FilterTimeDescription"], App.CurrentTranslation["Common_FilterTimeCancel"], null, items);
            if (action != App.CurrentTranslation["Common_FilterTimeCancel"])
            {
                PersonalFilterButton.Text = action;
                personalFilterId = filters.Where(x => x.FilterName == action).FirstOrDefault().FilterID;

                // do filtering
                Resources["IsLoading"] = true;
                NotificationAccordion.DataSource = await GetAccordionData();
                NotificationAccordion.DataBind();
                Resources["IsLoading"] = false;

                await MBoxApiCalls.SetSelectedPersonalFilter((int)personalFilterId);
            }
        }

        public async void NotificationFilterOnClicked(object sender, EventArgs e)
        {
            Resources["NotificationFilterOnStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
            Resources["NotificationFilterOffStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["NotificationFilterIsEnabled"] = true;
            notificationFilterOn = true;
            await MBoxApiCalls.SetNotificationFilterOnOff(true);
        }

        public async void NotificationFilterOffClicked(object sender, EventArgs e)
        {
            Resources["NotificationFilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["NotificationFilterOffStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
            Resources["NotificationFilterIsEnabled"] = false;
            notificationFilterOn = false;
            await MBoxApiCalls.SetNotificationFilterOnOff(false);
        }

        public async void NotificationFilterClicked(object sender, EventArgs e)
        {
            List<Filter> filters = notificationFilter.FilterList;
            int itemCount = 0;

            if (filters != null)
                itemCount = filters.Count;

            string[] items = new string[itemCount];
            for (int i = 0; i < itemCount; i++)
            {
                items[i] = filters[i].FilterName;
            }

            var action = await DisplayActionSheet(App.CurrentTranslation["Common_FilterTimeDescription"], App.CurrentTranslation["Common_FilterTimeCancel"], null, items);
            if (action != App.CurrentTranslation["Common_FilterTimeCancel"])
            {
                NotificationFilterButton.Text = action;
                notificationFilterId = filters.Where(x => x.FilterName == action).FirstOrDefault().FilterID;

                // do filtering
                Resources["IsLoading"] = true;
                NotificationAccordion.DataSource = await GetAccordionData();
                NotificationAccordion.DataBind();
                Resources["IsLoading"] = false;

                await MBoxApiCalls.SetSelectedNotificationFilter((int)notificationFilterId);
            }
        }

        public async void FilterTimeClicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet(App.CurrentTranslation["Common_FilterTimeDescription"], App.CurrentTranslation["Common_FilterTimeCancel"], null,
                App.CurrentTranslation["Common_FilterNotificationLast24Hours"], App.CurrentTranslation["Common_FilterNotificationYesterday"],
                App.CurrentTranslation["Common_FilterNotificationLast48Hours"], App.CurrentTranslation["Common_FilterNotificationLast72Hours"],
                App.CurrentTranslation["Common_FilterNotificationLast7Days"], App.CurrentTranslation["Common_FilterNotificationLast14Days"],
                App.CurrentTranslation["Common_FilterNotificationLast30Days"]);

            if (action != App.CurrentTranslation["Common_FilterTimeCancel"])
            {
                FilterTimeButton.Text = action;
                string derivedKey = App.CurrentTranslation.FirstOrDefault(x => x.Value == action).Key;
                timeFilterId = FilterSupport.GetNotificationFilters().FirstOrDefault(x => x.Value == derivedKey).Key;

                // do filtering
                Resources["IsLoading"] = true;
                NotificationAccordion.DataSource = await GetAccordionData();
                NotificationAccordion.DataBind();
                Resources["IsLoading"] = false;
            }
        }

        private List<AccordionSource> GetEmptyAccordion()
        {
            var result = new List<AccordionSource>();

            var asNonConfirmed = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Notification_NonConfirmed"]
            };
            result.Add(asNonConfirmed);

            var asSolution = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Notification_Solution"]
            };
            result.Add(asSolution);

            var asToBeApproved = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Notification_ToBeApproved"]
            };
            result.Add(asToBeApproved);

            var asAllReportedNotifications = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Notification_AllReportedNotifications"]
            };
            result.Add(asAllReportedNotifications);

            var asAllApprovedNotifications = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Notification_AllApprovedNotifications"]
            };
            result.Add(asAllApprovedNotifications);
            
            return result;
        }

        private async Task<List<AccordionSource>> GetAccordionData()
        {
            var result = new List<AccordionSource>();
            await PopulateNotificationGroups();

            var asNonConfirmed = new AccordionSource()
            {
                HeaderText = string.Format("{0} ({1})", App.CurrentTranslation["Notification_NonConfirmed"], NonConfirmedCount),
                ContentItems = vNonConfirmed,
                ContentHeight = vNonConfirmed.Height
            };
            result.Add(asNonConfirmed);

            var asSolution = new AccordionSource()
            {
                HeaderText = string.Format("{0} ({1})", App.CurrentTranslation["Notification_Solution"], SolutionCount),
                ContentItems = vSolution,
                ContentHeight = vSolution.Height
            };
            result.Add(asSolution);

            var asToBeApproved = new AccordionSource()
            {
                HeaderText = string.Format("{0} ({1})", App.CurrentTranslation["Notification_ToBeApproved"], ToBeApprovedCount),
                ContentItems = vToBeApproved,
                ContentHeight = vToBeApproved.Height
            };
            result.Add(asToBeApproved);

            var asAllReportedNotifications = new AccordionSource()
            {
                HeaderText = string.Format("{0} ({1})", App.CurrentTranslation["Notification_AllReportedNotifications"], AllReportedCount),
                ContentItems = vAllReported,
                ContentHeight = vAllReported.Height
            };
            result.Add(asAllReportedNotifications);

            var asAllApprovedNotifications = new AccordionSource()
            {
                HeaderText = string.Format("{0} ({1})", App.CurrentTranslation["Notification_AllApprovedNotifications"], AllApprovedCount),
                ContentItems = vAllApproved,
                ContentHeight = vAllApproved.Height
            };
            result.Add(asAllApprovedNotifications);

            return result;
        }

        private async Task PopulateNotificationGroups()
        {
            //reinitialize layout
            vNonConfirmed = new StackLayout();
            vSolution = new StackLayout();
            vToBeApproved = new StackLayout();
            vAllReported = new StackLayout();
            vAllApproved = new StackLayout();

            AllNotifications = await MBoxApiCalls.GetNotifications(personalFilterId, notificationFilterId, timeFilterId);
            NonConfirmedNotifications = AllNotifications.Where(x => x.DataType == 1 || x.DataType == 2 || x.DataType == 3).ToList();
            SolutionNotifications = AllNotifications.Where(x => x.DataType == 4).ToList();
            ToBeApprovedNotifications = AllNotifications.Where(x => x.DataType == 5).ToList();
            AllReportedNotifications = AllNotifications.Where(x => x.NeedReport == true).ToList();
            AllApprovedNotifications = AllNotifications.Where(x => x.DataType == 7 || x.DataType == 8 || x.DataType == 9 || x.DataType == 10 || x.DataType == 11).ToList();

            NonConfirmedCount = NonConfirmedNotifications.Count();
            SolutionCount = SolutionNotifications.Count();
            ToBeApprovedCount = ToBeApprovedNotifications.Count();
            AllReportedCount = AllReportedNotifications.Count();
            AllApprovedCount = AllApprovedNotifications.Count();

            if (NonConfirmedCount > 0)
            {
                AddNotificationGroupButtons(ref NonConfirmedNotifications, ref vNonConfirmed);
            }

            if (SolutionCount > 0)
            {
                AddNotificationGroupButtons(ref SolutionNotifications, ref vSolution);
            }

            if (ToBeApprovedCount > 0)
            {
                AddNotificationGroupButtons(ref ToBeApprovedNotifications, ref vToBeApproved);
            }

            if (AllReportedCount > 0)
            {
                AddNotificationGroupButtons(ref AllReportedNotifications, ref vAllReported);
            }

            if (AllApprovedCount > 0)
            {
                AddNotificationGroupButtons(ref AllApprovedNotifications, ref vAllApproved);
            }

            AllNotifications.Clear();
        }

        private void AddNotificationGroupButtons(ref List<NotificationModel> notifications, ref StackLayout layout)
        {
            List<string> alterTypes = NotificationSupport.GetAlterTypeDisplayNames();
            foreach (string subGroupName in alterTypes)
            {
                int subGroupNotifCount = notifications.Where(x => x.AlterTypeText == subGroupName).Count();
                if (subGroupNotifCount > 0)
                {
                    NotificationGroupButton ngButton = new NotificationGroupButton(subGroupName, subGroupNotifCount, screenWidth, 60);
                    ngButton.RelatedNotifications = notifications.Where(x => x.AlterTypeText == subGroupName).ToList();
                    layout.Children.Add(ngButton);
                }
            }
        }
    }
}
