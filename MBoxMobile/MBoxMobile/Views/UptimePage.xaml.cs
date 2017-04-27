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
    public partial class UptimePage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;

        bool filterOn = false;
        bool workingTimeOnly = false;

        public UptimePage()
        {
            InitializeComponent();

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["CheckboxAreaWidth"] = screenWidth * 0.1;
            Resources["CheckboxSource"] = "emptyroundcheck50.png";

            Resources["Filter2ButtonWidth"] = screenWidth * 0.25;
            Resources["Filter2ButtonHeight"] = screenWidth * 0.12;
            Resources["FilterButtonWidth"] = screenWidth * 0.42;
            Resources["FilterButtonHeight"] = screenWidth * 0.12;
            Resources["FilterTimeButtonWidth"] = screenWidth * 0.4;
            Resources["FilterTimeButtonHeight"] = screenWidth * 0.1;
            Resources["ContentMinHeight"] = screenHeight - 60.0;

            if (filterOn)
            {
                Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
                Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                Resources["FilterIsEnabled"] = true;
            }
            else
            {
                Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
                Resources["FilterIsEnabled"] = false;
            }

            UsageAccordion.AccordionWidth = screenWidth * 0.94;
            UsageAccordion.AccordionHeight = 55.0;
            UsageAccordion.DataSource = GetAccordionData();
            UsageAccordion.DataBind();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Resources["Uptime_Title"] = App.CurrentTranslation["Uptime_Title"];
            Resources["Uptime_ViewDetail"] = App.CurrentTranslation["Uptime_ViewDetail"];
            Resources["Uptime_FilterOn"] = App.CurrentTranslation["Uptime_FilterOn"];
            Resources["Uptime_FilterOff"] = App.CurrentTranslation["Uptime_FilterOff"];
            Resources["Uptime_Filter"] = App.CurrentTranslation["Uptime_Filter"];                     //this can be any value from GetPersonalFilters()
            Resources["Uptime_FilterTime"] = App.CurrentTranslation["Uptime_FilterTimeLast24Hours"];  //this can be any value from GetTimeFilters()
            Resources["Uptime_WorkingTimeOnly"] = App.CurrentTranslation["Uptime_WorkingTimeOnly"];
            Resources["Uptime_Locations"] = App.CurrentTranslation["Uptime_Locations"];
            Resources["Uptime_Departments"] = App.CurrentTranslation["Uptime_Departments"];
            Resources["Uptime_SubDepartments"] = App.CurrentTranslation["Uptime_SubDepartments"];
            Resources["Uptime_Equipment"] = App.CurrentTranslation["Uptime_Equipment"];
            Resources["Uptime_EquipmentGroup"] = App.CurrentTranslation["Uptime_EquipmentGroup"];
            Resources["Uptime_AuxiliaryEquipment"] = App.CurrentTranslation["Uptime_AuxiliaryEquipment"];
        }

        public async void ViewDetailClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new UptimeDetailsPage());
        }

        public void FilterOnClicked(object sender, EventArgs e)
        {
            Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
            Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterIsEnabled"] = true;
        }

        public void FilterOffClicked(object sender, EventArgs e)
        {
            Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
            Resources["FilterIsEnabled"] = false;
        }

        public void FilterClicked(object sender, EventArgs e)
        {
            
        }

        public async void FilterTimeClicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet(App.CurrentTranslation["Uptime_FilterTimeDescription"], App.CurrentTranslation["Uptime_FilterTimeCancel"], null,
                App.CurrentTranslation["Uptime_FilterTimeToday"], App.CurrentTranslation["Uptime_FilterTimeLast24Hours"],
                App.CurrentTranslation["Uptime_FilterTimeYesterday"], App.CurrentTranslation["Uptime_FilterTimeCurrentWeek"],
                App.CurrentTranslation["Uptime_FilterTimeLastWeek"], App.CurrentTranslation["Uptime_FilterTimeCurrentMonth"],
                App.CurrentTranslation["Uptime_FilterTimeLastMonth"], App.CurrentTranslation["Uptime_FilterTimeCurrentQuarter"],
                App.CurrentTranslation["Uptime_FilterTimeLastQuarter"], App.CurrentTranslation["Uptime_FilterTimeCurrentYear"]);

            FilterTimeButton.Text = action;

            // do filtering
        }

        public void WorkingTimeTapped(object sender, EventArgs e)
        {
            workingTimeOnly = !workingTimeOnly;
            if (workingTimeOnly)
                Resources["CheckboxSource"] = "fullroundcheck50.png";
            else
                Resources["CheckboxSource"] = "emptyroundcheck50.png";
        }

        private List<AccordionSource> GetAccordionData()
        {
            var result = new List<AccordionSource>();
            
            var asLocations = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Uptime_Locations"],
                ContentItems = new Label() { Text = "Put something here!"}
            };
            result.Add(asLocations);

            var asDepartments = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Uptime_Departments"],
                ContentItems = new Label() { Text = "Put something here!" }
            };
            result.Add(asDepartments);

            var asSubDepartments = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Uptime_SubDepartments"],
                ContentItems = new Label() { Text = "Put something here!" }
            };
            result.Add(asSubDepartments);

            var asEquipment = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Uptime_Equipment"],
                ContentItems = new Label() { Text = "Put something here!" }
            };
            result.Add(asEquipment);

            var asEquipmentGroup = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Uptime_EquipmentGroup"],
                ContentItems = new Label() { Text = "Put something here!" }
            };
            result.Add(asEquipmentGroup);

            var asAuxiliaryEquipment = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Uptime_AuxiliaryEquipment"],
                ContentItems = new Label() { Text = "Put something here!" }
            };
            result.Add(asAuxiliaryEquipment);

            return result;
        }
    }
}
