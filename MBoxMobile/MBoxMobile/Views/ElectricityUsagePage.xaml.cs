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
    public partial class ElectricityUsagePage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;
        bool AreTablesPopulated = false;

        bool filterOn = false;
        int? personalFilter = null;
        int timeFilter = 6274;
        bool workingTimeOnly = false;

        int? locationId = null;
        int? areaId = null;
        int? departmentId = null;
        int? subDepartmentId = null;
        int? consumingPowerEquipmentId = null;

        WebView wvLocations, wvAreas, wvDepartments, wvSubDepartments, wvConsumingPowerEquipments;

        public ElectricityUsagePage()
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

            ElectricityUsageAccordion.AccordionWidth = screenWidth - 30;
            ElectricityUsageAccordion.AccordionHeight = 55.0;
            ElectricityUsageAccordion.DataSource = GetEmptyAccordion();
            ElectricityUsageAccordion.DataBind();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            string currentTimeFilter = FilterSupport.GetTimeFilters()[timeFilter];

            Resources["ElectricityUsage_Title"] = App.CurrentTranslation["ElectricityUsage_Title"];
            Resources["Common_ViewDetail"] = App.CurrentTranslation["Common_ViewDetail"];
            Resources["Common_FilterFilterOn"] = App.CurrentTranslation["Common_FilterFilterOn"];
            Resources["Common_FilterFilterOff"] = App.CurrentTranslation["Common_FilterFilterOff"];
            Resources["Common_Filter"] = App.CurrentTranslation["Common_Filter"];                     //this can be any value from GetPersonalFilters()
            Resources["Common_FilterTime"] = App.CurrentTranslation[currentTimeFilter];
            Resources["Common_WorkingTimeOnly"] = App.CurrentTranslation["Common_WorkingTimeOnly"];

            if (!AreTablesPopulated)
            {
                Resources["IsLoading"] = true;
                ElectricityUsageAccordion.AccordionWidth = screenWidth - 30;
                ElectricityUsageAccordion.AccordionHeight = 55.0;
                ElectricityUsageAccordion.DataSource = await GetAccordionData();
                ElectricityUsageAccordion.DataBind();
                Resources["IsLoading"] = false;

                AreTablesPopulated = true;
            }
        }

        public async void ViewDetailClicked(object sender, EventArgs e)
        {
            if ((bool)Resources["IsLoading"] == false)
                await Navigation.PushModalAsync(new ElectricityUsageDetailsPage(locationId, departmentId, subDepartmentId, personalFilter));
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
            var action = await DisplayActionSheet(App.CurrentTranslation["Common_FilterTimeDescription"], App.CurrentTranslation["Common_FilterTimeCancel"], null,
                App.CurrentTranslation["Common_FilterTimeToday"], App.CurrentTranslation["Common_FilterTimeLast24Hours"],
                App.CurrentTranslation["Common_FilterTimeYesterday"], App.CurrentTranslation["Common_FilterTimeCurrentWeek"],
                App.CurrentTranslation["Common_FilterTimeLastWeek"], App.CurrentTranslation["Common_FilterTimeCurrentMonth"],
                App.CurrentTranslation["Common_FilterTimeLastMonth"], App.CurrentTranslation["Common_FilterTimeCurrentQuarter"],
                App.CurrentTranslation["Common_FilterTimeLastQuarter"], App.CurrentTranslation["Common_FilterTimeCurrentYear"]);

            if (action != App.CurrentTranslation["Common_FilterTimeCancel"])
            {
                FilterTimeButton.Text = action;
                string derivedKey = App.CurrentTranslation.FirstOrDefault(x => x.Value == action).Key;
                timeFilter = FilterSupport.GetTimeFilters().FirstOrDefault(x => x.Value == derivedKey).Key;

                // do filtering
                Resources["IsLoading"] = true;
                ElectricityUsageAccordion.DataSource = await GetAccordionData();
                ElectricityUsageAccordion.DataBind();
                Resources["IsLoading"] = false;
            }
        }

        public void WorkingTimeTapped(object sender, EventArgs e)
        {
            workingTimeOnly = !workingTimeOnly;
            if (workingTimeOnly)
                Resources["CheckboxSource"] = "fullroundcheck50.png";
            else
                Resources["CheckboxSource"] = "emptyroundcheck50.png";
        }

        private List<AccordionSource> GetEmptyAccordion()
        {
            var result = new List<AccordionSource>();

            #region Locations
            var asLocations = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["ElectricityUsage_Locations"]
            };
            result.Add(asLocations);
            #endregion

            #region Areas
            var asArea = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["ElectricityUsage_Areas"]
            };
            result.Add(asArea);
            #endregion

            #region Departments
            var asDepartments = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["ElectricityUsage_Departments"]
            };
            result.Add(asDepartments);
            #endregion

            #region SubDepartments
            var asSubDepartments = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["ElectricityUsage_SubDepartments"]
            };
            result.Add(asSubDepartments);
            #endregion

            #region ConsumingPowerEquipments
            var asConsumingPowerEquipment = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["ElectricityUsage_ConsumingPower"]
            };
            result.Add(asConsumingPowerEquipment);
            #endregion


            return result;
        }

        private async Task<List<AccordionSource>> GetAccordionData()
        {
            var result = new List<AccordionSource>();

            #region Locations
            wvLocations = new WebView();
            wvLocations.WidthRequest = screenWidth - 35;
            wvLocations.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            wvLocations.VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true);

            var asLocations = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["ElectricityUsage_Locations"],
                ContentItems = wvLocations,
                ContentHeight = await PopulateWebView("wvLocations")
            };
            result.Add(asLocations);
            #endregion

            #region Areas
            wvAreas = new WebView();
            wvAreas.WidthRequest = screenWidth - 35;
            wvAreas.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            wvAreas.VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true);

            var asArea = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["ElectricityUsage_Areas"],
                ContentItems = wvAreas,
                ContentHeight = await PopulateWebView("wvAreas")
            };
            result.Add(asArea);
            #endregion

            #region Departments
            wvDepartments = new WebView();
            wvDepartments.WidthRequest = screenWidth - 35;
            wvDepartments.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            wvDepartments.VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true);

            var asDepartments = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["ElectricityUsage_Departments"],
                ContentItems = wvDepartments,
                ContentHeight = await PopulateWebView("wvDepartments")
            };
            result.Add(asDepartments);
            #endregion

            #region SubDepartments
            wvSubDepartments = new WebView();
            wvSubDepartments.WidthRequest = screenWidth - 35;
            wvSubDepartments.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            wvSubDepartments.VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true);

            var asSubDepartments = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["ElectricityUsage_SubDepartments"],
                ContentItems = wvSubDepartments,
                ContentHeight = await PopulateWebView("wvSubDepartments")
            };
            result.Add(asSubDepartments);
            #endregion

            #region ConsumingPowerEquipments
            wvConsumingPowerEquipments = new WebView();
            wvConsumingPowerEquipments.WidthRequest = screenWidth - 35;
            wvConsumingPowerEquipments.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            wvConsumingPowerEquipments.VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true);

            var asConsumingPowerEquipment = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["ElectricityUsage_ConsumingPower"],
                ContentItems = wvConsumingPowerEquipments,
                ContentHeight = await PopulateWebView("wvConsumingPowerEquipments"),
                HeaderFontSize = 14
            };
            result.Add(asConsumingPowerEquipment);
            #endregion
            
            #region Navigating handlers

            wvLocations.Navigating += async (s, e) =>
            {
                if (e.Url != string.Empty)
                {
                    locationId = int.Parse(e.Url.Split('=').LastOrDefault());
                }
                e.Cancel = true;

                Resources["IsLoading"] = true;
                //asArea.ContentHeight = await PopulateWebView("wvAreas");
                asDepartments.ContentHeight = await PopulateWebView("wvDepartments");
                asSubDepartments.ContentHeight = await PopulateWebView("wvSubDepartments");
                asConsumingPowerEquipment.ContentHeight = await PopulateWebView("wvConsumingPowerEquipments");
                Resources["IsLoading"] = false;
            };

            wvAreas.Navigating += (s, e) =>
            {
                if (e.Url != string.Empty)
                {
                    areaId = int.Parse(e.Url.Split('=').LastOrDefault());
                }
                e.Cancel = true;
            };

            wvDepartments.Navigating += async (s, e) =>
            {
                if (e.Url != string.Empty)
                {
                    departmentId = int.Parse(e.Url.Split('=').LastOrDefault());
                }
                e.Cancel = true;

                Resources["IsLoading"] = true;
                //asArea.ContentHeight = await PopulateWebView("wvAreas");
                asSubDepartments.ContentHeight = await PopulateWebView("wvSubDepartments");
                asConsumingPowerEquipment.ContentHeight = await PopulateWebView("wvConsumingPowerEquipments");
                Resources["IsLoading"] = false;
            };

            wvSubDepartments.Navigating += async (s, e) =>
            {
                if (e.Url != string.Empty)
                {
                    subDepartmentId = int.Parse(e.Url.Split('=').LastOrDefault());
                }
                e.Cancel = true;

                Resources["IsLoading"] = true;
                //asArea.ContentHeight = await PopulateWebView("wvAreas");
                asConsumingPowerEquipment.ContentHeight = await PopulateWebView("wvConsumingPowerEquipments");
                Resources["IsLoading"] = false;
            };

            wvConsumingPowerEquipments.Navigating += (s, e) =>
            {
                if (e.Url != string.Empty)
                {
                    consumingPowerEquipmentId = int.Parse(e.Url.Split('=').LastOrDefault());
                }
                e.Cancel = true;
            };

            #endregion

            return result;
        }

        private async Task<double> PopulateWebView(string webViewName)
        {
            double wvHeight = 0;
            const double WV_ROW_Height = 31.75;

            switch (webViewName)
            {
                case "wvLocations":
                    IEnumerable<ElectricityModel> locationList = await MBoxApiCalls.GetElectricityUsagePerLocation(personalFilter);
                    string htmlHeaderLocations = HtmlTableSupport.ElectricityUsage_Medium_TableHeader("Location");
                    string htmlContentLocations = HtmlTableSupport.ElectricityUsage_Medium_TableContent(locationList, "Location");
                    string htmlHtmlLocations = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderLocations, htmlContentLocations);
                    wvLocations.Source = new HtmlWebViewSource { Html = htmlHtmlLocations };
                    wvHeight = (locationList.Count() + 1) * WV_ROW_Height + 7;
                    break;
                case "wvAreas":
                    IEnumerable<ElectricityModel> areasList = await MBoxApiCalls.GetElectricityUsagePerArea(locationId, personalFilter);
                    string htmlHeaderAreas = HtmlTableSupport.ElectricityUsage_Medium_TableHeader("Area");
                    string htmlContentAreas = HtmlTableSupport.ElectricityUsage_Medium_TableContent(areasList, "Area");
                    string htmlHtmlAreas = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderAreas, htmlContentAreas);
                    wvAreas.Source = new HtmlWebViewSource { Html = htmlHtmlAreas };
                    wvHeight = (areasList.Count() + 1) * WV_ROW_Height + 7;
                    break;
                case "wvDepartments":
                    IEnumerable<ElectricityModel> departmentsList = await MBoxApiCalls.GetElectricityUsagePerDepartment(locationId, personalFilter);
                    string htmlHeaderDepartments = HtmlTableSupport.ElectricityUsage_Medium_TableHeader("Department");
                    string htmlContentDepartments = HtmlTableSupport.ElectricityUsage_Medium_TableContent(departmentsList, "Department");
                    string htmlHtmlDepartments = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderDepartments, htmlContentDepartments);
                    wvDepartments.Source = new HtmlWebViewSource { Html = htmlHtmlDepartments };
                    wvHeight = (departmentsList.Count() + 1) * WV_ROW_Height + 7;
                    break;
                case "wvSubDepartments":
                    IEnumerable<ElectricityModel> subDepartmentsList = await MBoxApiCalls.GetElectricityUsagePerSubDepartment(locationId, departmentId, personalFilter);
                    string htmlHeaderSubDepartments = HtmlTableSupport.ElectricityUsage_Medium_TableHeader("SubDepartment");
                    string htmlContentSubDepartments = HtmlTableSupport.ElectricityUsage_Medium_TableContent(subDepartmentsList, "SubDepartment");
                    string htmlHtmlSubDepartments = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderSubDepartments, htmlContentSubDepartments);
                    wvSubDepartments.Source = new HtmlWebViewSource { Html = htmlHtmlSubDepartments };
                    wvHeight = (subDepartmentsList.Count() + 1) * WV_ROW_Height + 7;
                    break;
                case "wvConsumingPowerEquipments":
                    IEnumerable<ElectricityMachine> ConsumingPowerEquipmentsList = await MBoxApiCalls.GetElectricityUsagePerWasting(locationId, departmentId, subDepartmentId, personalFilter);
                    string htmlHeaderConsumingPowerEquipments = HtmlTableSupport.ElectricityUsage_Large_TableHeader();
                    string htmlContentConsumingPowerEquipments = HtmlTableSupport.ElectricityUsage_Large_TableContent(ConsumingPowerEquipmentsList);
                    string htmlHtmlConsumingPowerEquipments = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderConsumingPowerEquipments, htmlContentConsumingPowerEquipments);
                    wvConsumingPowerEquipments.Source = new HtmlWebViewSource { Html = htmlHtmlConsumingPowerEquipments };
                    wvHeight = (ConsumingPowerEquipmentsList.Count() + 1) * WV_ROW_Height + 7;
                    break;
            }

            return wvHeight;
        }
    }
}
