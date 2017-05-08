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
        bool AreTablesPopulated = false;

        bool filterOn = false;
        int? personalFilter = null;
        int timeFilter = 6274;
        bool workingTimeOnly = false;

        int? locationId = null;
        int? departmentId = null;
        int? subDepartmentId = null;
        int? equipmentId = null;
        int? equipmentGroupId = null;
        int? auxiliaryEquipmentId = null;

        WebView wvLocations, wvDepartments, wvSubDepartments, wvEquipments, wvEquipmentGroups, wvAuxiliaryEquipments;

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

            UptimeAccordion.AccordionWidth = screenWidth - 30;
            UptimeAccordion.AccordionHeight = 55.0;
            UptimeAccordion.DataSource = GetEmptyAccordion();
            UptimeAccordion.DataBind();
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

            if (!AreTablesPopulated)
            {
                Resources["IsLoading"] = true;
                UptimeAccordion.AccordionWidth = screenWidth - 30;
                UptimeAccordion.AccordionHeight = 55.0;
                UptimeAccordion.DataSource = await GetAccordionData();
                UptimeAccordion.DataBind();
                Resources["IsLoading"] = false;

                AreTablesPopulated = true;
            }
        }

        public async void ViewDetailClicked(object sender, EventArgs e)
        {
            if ((bool)Resources["IsLoading"] == false)
                await Navigation.PushModalAsync(new UptimeDetailsPage(timeFilter, personalFilter, locationId, departmentId, subDepartmentId, equipmentId, equipmentGroupId, auxiliaryEquipmentId));
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

        private List<AccordionSource> GetEmptyAccordion()
        {
            var result = new List<AccordionSource>();

            #region Locations
            var asLocations = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Uptime_Locations"]
            };
            result.Add(asLocations);
            #endregion

            #region Departments
            var asDepartments = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Uptime_Departments"]
            };
            result.Add(asDepartments);
            #endregion

            #region SubDepartments
            var asSubDepartments = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Uptime_SubDepartments"]
            };
            result.Add(asSubDepartments);
            #endregion

            #region Equipments
            var asEquipment = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Uptime_Equipment"]
            };
            result.Add(asEquipment);
            #endregion

            #region EquipmentGroups
            var asEquipmentGroup = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Uptime_EquipmentGroup"]
            };
            result.Add(asEquipmentGroup);
            #endregion

            #region AuxiliaryEquipments
            var asAuxiliaryEquipment = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Uptime_AuxiliaryEquipment"]
            };
            result.Add(asAuxiliaryEquipment);
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
                HeaderText = App.CurrentTranslation["Uptime_Locations"],
                ContentItems = wvLocations,
                ContentHeight = await PopulateWebView("wvLocations")
            };
            result.Add(asLocations);
            #endregion

            #region Departments
            wvDepartments = new WebView();
            wvDepartments.WidthRequest = screenWidth - 35;
            wvDepartments.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            wvDepartments.VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true);

            var asDepartments = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Uptime_Departments"],
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
                HeaderText = App.CurrentTranslation["Uptime_SubDepartments"],
                ContentItems = wvSubDepartments,
                ContentHeight = await PopulateWebView("wvSubDepartments")
            };
            result.Add(asSubDepartments);
            #endregion

            #region Equipments
            wvEquipments = new WebView();
            wvEquipments.WidthRequest = screenWidth - 35;
            wvEquipments.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            wvEquipments.VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true);

            var asEquipment = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Uptime_Equipment"],
                ContentItems = wvEquipments,
                ContentHeight = await PopulateWebView("wvEquipments")
            };
            result.Add(asEquipment);
            #endregion

            #region EquipmentGroups
            wvEquipmentGroups = new WebView();
            wvEquipmentGroups.WidthRequest = screenWidth - 35;
            wvEquipmentGroups.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            wvEquipmentGroups.VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true);

            var asEquipmentGroup = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Uptime_EquipmentGroup"],
                ContentItems = wvEquipmentGroups,
                ContentHeight = await PopulateWebView("wvEquipmentGroups")
            };
            result.Add(asEquipmentGroup);
            #endregion

            #region AuxiliaryEquipments
            wvAuxiliaryEquipments = new WebView();
            wvAuxiliaryEquipments.WidthRequest = screenWidth - 35;
            wvAuxiliaryEquipments.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            wvAuxiliaryEquipments.VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true);

            var asAuxiliaryEquipment = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Uptime_AuxiliaryEquipment"],
                ContentItems = wvAuxiliaryEquipments,
                ContentHeight = await PopulateWebView("wvAuxiliaryEquipments")
            };
            result.Add(asAuxiliaryEquipment);
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
                asDepartments.ContentHeight = await PopulateWebView("wvDepartments");
                asSubDepartments.ContentHeight = await PopulateWebView("wvSubDepartments");
                asEquipment.ContentHeight = await PopulateWebView("wvEquipments");
                asEquipmentGroup.ContentHeight = await PopulateWebView("wvEquipmentGroups");
                asAuxiliaryEquipment.ContentHeight = await PopulateWebView("wvAuxiliaryEquipments");
                Resources["IsLoading"] = false;
            };

            wvDepartments.Navigating += async (s, e) =>
            {
                if (e.Url != string.Empty)
                {
                    departmentId = int.Parse(e.Url.Split('=').LastOrDefault());
                }
                e.Cancel = true;

                Resources["IsLoading"] = true;
                asSubDepartments.ContentHeight = await PopulateWebView("wvSubDepartments");
                asEquipment.ContentHeight = await PopulateWebView("wvEquipments");
                asEquipmentGroup.ContentHeight = await PopulateWebView("wvEquipmentGroups");
                asAuxiliaryEquipment.ContentHeight = await PopulateWebView("wvAuxiliaryEquipments");
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
                asEquipment.ContentHeight = await PopulateWebView("wvEquipments");
                asEquipmentGroup.ContentHeight = await PopulateWebView("wvEquipmentGroups");
                asAuxiliaryEquipment.ContentHeight = await PopulateWebView("wvAuxiliaryEquipments");
                Resources["IsLoading"] = false;
            };

            wvEquipments.Navigating += (s, e) =>
            {
                if (e.Url != string.Empty)
                {
                    equipmentId = int.Parse(e.Url.Split('=').LastOrDefault());
                }
                e.Cancel = true;
            };
            
            wvEquipmentGroups.Navigating += (s, e) =>
            {
                if (e.Url != string.Empty)
                {
                    equipmentGroupId = int.Parse(e.Url.Split('=').LastOrDefault());
                }
                e.Cancel = true;
            };

            wvAuxiliaryEquipments.Navigating += (s, e) =>
            {
                if (e.Url != string.Empty)
                {
                    auxiliaryEquipmentId = int.Parse(e.Url.Split('=').LastOrDefault());
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
                    IEnumerable<EfficiencyModel> locationList = await MBoxApiCalls.GetEfficiencyByLocation(personalFilter, timeFilter);
                    string htmlHeaderLocations = HtmlTableSupport.Uptime_Locations_TableHeader();
                    string htmlBodyLocations = HtmlTableSupport.Uptime_Large_TableBody(locationList);
                    string htmlHtmlLocations = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderLocations, htmlBodyLocations);
                    wvLocations.Source = new HtmlWebViewSource { Html = htmlHtmlLocations };
                    wvHeight = (locationList.Count() + 2) * WV_ROW_Height + 7;
                    break;
                case "wvDepartments":
                    IEnumerable<EfficiencyModel> departmentsList = await MBoxApiCalls.GetEfficiencyByDepartment(locationId, personalFilter, timeFilter);
                    string htmlHeaderDepartments = HtmlTableSupport.Uptime_Large_TableHeader("Department");
                    string htmlBodyDepartments = HtmlTableSupport.Uptime_Large_TableBody(departmentsList);
                    string htmlHtmlDepartments = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderDepartments, htmlBodyDepartments);
                    wvDepartments.Source = new HtmlWebViewSource { Html = htmlHtmlDepartments };
                    wvHeight = (departmentsList.Count() + 1) * WV_ROW_Height + 7;
                    break;
                case "wvSubDepartments":
                    IEnumerable<EfficiencyModel> subDepartmentsList = await MBoxApiCalls.GetEfficiencyBySubDepartment(locationId, departmentId, personalFilter, timeFilter);
                    string htmlHeaderSubDepartments = HtmlTableSupport.Uptime_Large_TableHeader("SubDepartment");
                    string htmlBodySubDepartments = HtmlTableSupport.Uptime_Large_TableBody(subDepartmentsList);
                    string htmlHtmlSubDepartments = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderSubDepartments, htmlBodySubDepartments);
                    wvSubDepartments.Source = new HtmlWebViewSource { Html = htmlHtmlSubDepartments };
                    wvHeight = (subDepartmentsList.Count() + 1) * WV_ROW_Height + 7;
                    break;
                case "wvEquipments":
                    IEnumerable<EfficiencyModel> equipmentsList = await MBoxApiCalls.GetEfficiencyByEquipmentType(locationId, departmentId, subDepartmentId, personalFilter, timeFilter);
                    string htmlHeaderEquipments = HtmlTableSupport.Uptime_Large_TableHeader("Equipment");
                    string htmlBodyEquipments = HtmlTableSupport.Uptime_Large_TableBody(equipmentsList);
                    string htmlHtmlEquipments = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderEquipments, htmlBodyEquipments);
                    wvEquipments.Source = new HtmlWebViewSource { Html = htmlHtmlEquipments };
                    wvHeight = (equipmentsList.Count() + 1) * WV_ROW_Height + 7;
                    break;
                case "wvEquipmentGroups":
                    IEnumerable<EfficiencyModel> equipmentGroupsList = await MBoxApiCalls.GetEfficiencyByEquipmentGroup(locationId, departmentId, subDepartmentId, personalFilter, timeFilter);
                    string htmlHeaderEquipmentGroups = HtmlTableSupport.Uptime_Large_TableHeader("EquipmentGroup");
                    string htmlBodyEquipmentGroups = HtmlTableSupport.Uptime_Large_TableBody(equipmentGroupsList);
                    string htmlHtmlEquipmentGroups = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderEquipmentGroups, htmlBodyEquipmentGroups);
                    wvEquipmentGroups.Source = new HtmlWebViewSource { Html = htmlHtmlEquipmentGroups };
                    wvHeight = (equipmentGroupsList.Count() + 1) * WV_ROW_Height + 7;
                    break;
                case "wvAuxiliaryEquipments":
                    IEnumerable<EfficiencyModel> auxiliaryEquipmentsList = await MBoxApiCalls.GetEfficiencyByAuxiliaryType(locationId, departmentId, subDepartmentId, personalFilter, timeFilter);
                    string htmlHeaderAuxiliaryEquipments = HtmlTableSupport.Uptime_Small_TableHeader();
                    string htmlBodyAuxiliaryEquipments = HtmlTableSupport.Uptime_Small_TableBody(auxiliaryEquipmentsList);
                    string htmlHtmlAuxiliaryEquipments = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderAuxiliaryEquipments, htmlBodyAuxiliaryEquipments);
                    wvAuxiliaryEquipments.Source = new HtmlWebViewSource { Html = htmlHtmlAuxiliaryEquipments };
                    wvHeight = (auxiliaryEquipmentsList.Count() + 1) * WV_ROW_Height + 7;
                    break;
            }

            return wvHeight;
        }
    }
}
