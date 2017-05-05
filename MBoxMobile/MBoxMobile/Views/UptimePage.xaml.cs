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
    public partial class UptimePage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;

        bool filterOn = false;
        int personalFilter = 1;
        int timeFilter = 6274;
        bool workingTimeOnly = false;

        public UptimePage()
        {
            InitializeComponent();

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["CheckboxAreaWidth"] = screenWidth * 0.08;
            Resources["CheckboxSource"] = "emptyroundcheck50.png";

            Resources["Filter2ButtonWidth"] = screenWidth * 0.25;
            Resources["Filter2ButtonHeight"] = screenWidth * 0.12;
            Resources["FilterButtonWidth"] = screenWidth * 0.4;
            Resources["FilterButtonHeight"] = screenWidth * 0.12;
            Resources["FilterTimeButtonWidth"] = screenWidth * 0.38;
            Resources["FilterTimeButtonHeight"] = screenWidth * 0.1;
            Resources["ContentMinHeight"] = screenHeight - 60.0;

            Resources["Filter2FontSize"] = FilterSupport.GetFilter2FontSize(screenWidth);
            Resources["FilterWorkingHoursLabelFontSize"] = FilterSupport.GetWorkingHoursLabelFontSize(screenWidth);

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
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            string currentTimeFilter = FilterSupport.GetTimeFilters()[timeFilter];

            Resources["Uptime_Title"] = App.CurrentTranslation["Uptime_Title"];
            Resources["Uptime_ViewDetail"] = App.CurrentTranslation["Uptime_ViewDetail"];
            Resources["Uptime_FilterOn"] = App.CurrentTranslation["Uptime_FilterOn"];
            Resources["Uptime_FilterOff"] = App.CurrentTranslation["Uptime_FilterOff"];
            Resources["Uptime_Filter"] = App.CurrentTranslation["Uptime_Filter"];                     //this can be any value from GetPersonalFilters()
            Resources["Uptime_FilterTime"] = App.CurrentTranslation[currentTimeFilter];
            Resources["Uptime_WorkingTimeOnly"] = App.CurrentTranslation["Uptime_WorkingTimeOnly"];
            Resources["Uptime_Locations"] = App.CurrentTranslation["Uptime_Locations"];
            Resources["Uptime_Departments"] = App.CurrentTranslation["Uptime_Departments"];
            Resources["Uptime_SubDepartments"] = App.CurrentTranslation["Uptime_SubDepartments"];
            Resources["Uptime_Equipment"] = App.CurrentTranslation["Uptime_Equipment"];
            Resources["Uptime_EquipmentGroup"] = App.CurrentTranslation["Uptime_EquipmentGroup"];
            Resources["Uptime_AuxiliaryEquipment"] = App.CurrentTranslation["Uptime_AuxiliaryEquipment"];

            Resources["IsLoading"] = true;
            UptimeAccordion.AccordionWidth = screenWidth - 30;
            UptimeAccordion.AccordionHeight = 55.0;
            UptimeAccordion.DataSource = await GetAccordionData();
            UptimeAccordion.DataBind();
            Resources["IsLoading"] = false;
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
            filterOn = true;
        }

        public void FilterOffClicked(object sender, EventArgs e)
        {
            Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
            Resources["FilterIsEnabled"] = false;
            filterOn = false;
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
            string derivedKey = App.CurrentTranslation.FirstOrDefault(x => x.Value == action).Key;
            timeFilter = FilterSupport.GetTimeFilters().FirstOrDefault(x => x.Value == derivedKey).Key;

            // do filtering
            Resources["IsLoading"] = true;
            UptimeAccordion.DataSource = await GetAccordionData();
            UptimeAccordion.DataBind();
            Resources["IsLoading"] = false;
        }

        public void WorkingTimeTapped(object sender, EventArgs e)
        {
            workingTimeOnly = !workingTimeOnly;
            if (workingTimeOnly)
                Resources["CheckboxSource"] = "fullroundcheck50.png";
            else
                Resources["CheckboxSource"] = "emptyroundcheck50.png";
        }

        private async Task<List<AccordionSource>> GetAccordionData()
        {
            var result = new List<AccordionSource>();

            #region Locations
            WebView wvLocations = new WebView();
            wvLocations.WidthRequest = screenWidth - 35;
            wvLocations.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            wvLocations.VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true);

            //List<EfficiencyLocation> locationList = await MBoxApiCalls.GetEfficiencyByLocation("853,885,913,1981", "1", "1");
            List<EfficiencyLocation> locationList = await MBoxApiCalls.GetEfficiencyByLocation(App.LoggedUser.login.BelongToLocationID, personalFilter.ToString(), timeFilter.ToString());

            string htmlHeaderLocations = HtmlTableSupport.Uptime_Locations_TableHeader();
            string htmlBodyLocations = HtmlTableSupport.Uptime_Locations_TableBody(locationList);
            string htmlHtmlLocations = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderLocations, htmlBodyLocations);
            wvLocations.Source = new HtmlWebViewSource { Html = htmlHtmlLocations };
            
            var asLocations = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Uptime_Locations"],
                ContentItems = wvLocations
            };
            result.Add(asLocations);
            #endregion

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
