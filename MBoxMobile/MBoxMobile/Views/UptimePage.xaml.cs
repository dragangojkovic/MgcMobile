﻿using MBoxMobile.CustomControls;
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
        double ScreenWidth = 0.0;
        double ScreenHeight = 0.0;
        bool AreTablesPopulated = false;

        PersonalFilter personalFilter = null;
        bool filterOn = false;
        int? personalFilterId = null;
        int? resultedPersonalFilterId = null;
        int timeFilterId = 6274;
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

            ScreenWidth = DependencyService.Get<IDisplay>().Width;
            ScreenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["CheckboxAreaWidth"] = ScreenWidth * 0.08;
            Resources["CheckboxSource"] = "emptyroundcheck50.png";

            SetUpLayout();

            //filter default values 
            Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
            Resources["FilterIsEnabled"] = false;

            Resources["Uptime_FilterInfo"] = string.Empty;
            Resources["Uptime_IsFilterInfoVisible"] = false;

            UptimeAccordion.AccordionWidth = ScreenWidth - 30;
            UptimeAccordion.AccordionHeight = 55.0;
            UptimeAccordion.DataSource = GetEmptyAccordion();
            UptimeAccordion.DataBind();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            string currentTimeFilter = FilterSupport.GetTimeFilters()[timeFilterId];

            Resources["Uptime_Title"] = App.CurrentTranslation["Uptime_Title"];
            Resources["Common_ViewDetail"] = App.CurrentTranslation["Common_ViewDetail"];
            Resources["Common_FilterFilterOn"] = App.CurrentTranslation["Common_FilterFilterOn"];
            Resources["Common_FilterFilterOff"] = App.CurrentTranslation["Common_FilterFilterOff"];
            Resources["Common_Filter"] = App.CurrentTranslation["Common_Filter"];
            Resources["Common_FilterTime"] = App.CurrentTranslation[currentTimeFilter];
            Resources["Uptime_WorkingTimeOnly"] = App.CurrentTranslation["Common_WorkingTimeOnly"];

            Resources["Uptime_Locations"] = App.CurrentTranslation["Uptime_Locations"];
            Resources["Uptime_Departments"] = App.CurrentTranslation["Uptime_Departments"];
            Resources["Uptime_SubDepartments"] = App.CurrentTranslation["Uptime_SubDepartments"];
            Resources["Uptime_Equipment"] = App.CurrentTranslation["Uptime_Equipment"];
            Resources["Uptime_EquipmentGroup"] = App.CurrentTranslation["Uptime_EquipmentGroup"];
            Resources["Uptime_AuxiliaryEquipment"] = App.CurrentTranslation["Uptime_AuxiliaryEquipment"];
            Resources["Common_FilterClear"] = App.CurrentTranslation["Common_FilterClear"];

            if (!AreTablesPopulated)
            {
                Resources["IsLoading"] = true;
                personalFilter = await MBoxApiCalls.GetPersonalFilter();
                filterOn = personalFilter.FilterOn;
                if (filterOn)
                {
                    Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
                    Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["FilterIsEnabled"] = true;
                    Resources["Common_Filter"] = personalFilter.FilterList.Where(x => x.FilterID == personalFilter.SelectedFilterID).FirstOrDefault().FilterName;
                    personalFilterId = personalFilter.SelectedFilterID > 0 ? personalFilter.SelectedFilterID : (int?)null;
                    resultedPersonalFilterId = personalFilterId;
                }
                else
                {
                    Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
                    Resources["FilterIsEnabled"] = false;
                }
                UptimeAccordion.AccordionWidth = ScreenWidth - 30;
                UptimeAccordion.AccordionHeight = 55.0;
                UptimeAccordion.DataSource = await GetAccordionData();
                UptimeAccordion.DataBind();
                Resources["IsLoading"] = false;

                AreTablesPopulated = true;
            }
        }

        private void SetUpLayout()
        {
            Resources["Filter2ButtonWidth"] = ScreenWidth * 0.25;
            Resources["Filter2ButtonHeight"] = 42; // screenWidth * 0.12;
            Resources["FilterButtonWidth"] = ScreenWidth * 0.4;
            Resources["FilterButtonHeight"] = 42; // screenWidth * 0.12;
            Resources["FilterTimeButtonWidth"] = ScreenWidth - 30; // screenWidth * 0.38;
            Resources["FilterTimeButtonHeight"] = 36; // screenWidth * 0.1;
            Resources["ContentMinHeight"] = ScreenHeight - 60.0;
            Resources["FilterInfoWidth"] = (ScreenWidth - 30) * 0.5;

            Resources["Filter2FontSize"] = FilterSupport.GetFilter2FontSize(ScreenWidth);
            Resources["FilterWorkingHoursLabelFontSize"] = FilterSupport.GetWorkingHoursLabelFontSize(ScreenWidth);
        }

        public async void ViewDetailClicked(object sender, EventArgs e)
        {
            if ((bool)Resources["IsLoading"] == false)
                await Navigation.PushModalAsync(new UptimeDetailsPage(timeFilterId, resultedPersonalFilterId, locationId, departmentId, subDepartmentId, equipmentId, equipmentGroupId, auxiliaryEquipmentId));
        }

        private async void DoFiltering()
        {
            Resources["IsLoading"] = true;
            UptimeAccordion.DataSource = await GetAccordionData();
            UptimeAccordion.DataBind();
            Resources["IsLoading"] = false;
        }

        public async void FilterOnClicked(object sender, EventArgs e)
        {
            if (!filterOn)
            {
                Resources["IsLoading"] = true;
                bool result = await MBoxApiCalls.SetPersonalFilterOnOff(true);
                Resources["IsLoading"] = false;

                if (result)
                {
                    Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
                    Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["FilterIsEnabled"] = true;
                    filterOn = true;
                    resultedPersonalFilterId = personalFilterId;

                    if (resultedPersonalFilterId != null)
                        DoFiltering();
                }
            }
        }

        public async void FilterOffClicked(object sender, EventArgs e)
        {
            if (filterOn)
            {
                Resources["IsLoading"] = true;
                bool result = await MBoxApiCalls.SetPersonalFilterOnOff(false);
                Resources["IsLoading"] = false;

                if (result)
                {
                    Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
                    Resources["FilterIsEnabled"] = false;
                    filterOn = false;
                    resultedPersonalFilterId = null;

                    if (personalFilterId != null)
                        DoFiltering();
                }
            }
        }

        public async void FilterClicked(object sender, EventArgs e)
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

            var action = await DisplayActionSheet(App.CurrentTranslation["Common_FilterPersonalDescription"], App.CurrentTranslation["Common_FilterCancel"], null, items);
            if (action != App.CurrentTranslation["Common_FilterCancel"])
            {
                personalFilterId = filters.Where(x => x.FilterName == action).FirstOrDefault().FilterID;
                bool result = await MBoxApiCalls.SetSelectedPersonalFilter((int)personalFilterId);
                if (result)
                {
                    FilterButton.Text = action;
                    resultedPersonalFilterId = personalFilterId;

                    DoFiltering();
                }
            }
        }

        public async void FilterTimeClicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet(App.CurrentTranslation["Common_FilterTimeDescription"], App.CurrentTranslation["Common_FilterCancel"], null,
                App.CurrentTranslation["Common_FilterTimeToday"], App.CurrentTranslation["Common_FilterTimeLast24Hours"],
                App.CurrentTranslation["Common_FilterTimeYesterday"], App.CurrentTranslation["Common_FilterTimeCurrentWeek"],
                App.CurrentTranslation["Common_FilterTimeLastWeek"], App.CurrentTranslation["Common_FilterTimeCurrentMonth"],
                App.CurrentTranslation["Common_FilterTimeLastMonth"], App.CurrentTranslation["Common_FilterTimeCurrentQuarter"],
                App.CurrentTranslation["Common_FilterTimeLastQuarter"], App.CurrentTranslation["Common_FilterTimeCurrentYear"]);

            if (action != App.CurrentTranslation["Common_FilterCancel"])
            {
                FilterTimeButton.Text = action;
                string derivedKey = App.CurrentTranslation.FirstOrDefault(x => x.Value == action).Key;
                timeFilterId = FilterSupport.GetTimeFilters().FirstOrDefault(x => x.Value == derivedKey).Key;

                DoFiltering();
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

        public void FilterClearClicked(object sender, EventArgs e)
        {
            locationId = null;
            departmentId = null;
            subDepartmentId = null;
            equipmentId = null;
            equipmentGroupId = null;
            auxiliaryEquipmentId = null;

            //Resources["LabelFilterLocationsStyle"] = (Style)Application.Current.Resources["LabelSmallStyleGray"];
            //Resources["LabelFilterDepartmentsStyle"] = (Style)Application.Current.Resources["LabelSmallStyleGray"];
            //Resources["LabelFilterSubDepartmentsStyle"] = (Style)Application.Current.Resources["LabelSmallStyleGray"];
            //Resources["LabelFilterEquipmentStyle"] = (Style)Application.Current.Resources["LabelSmallStyleGray"];
            //Resources["LabelFilterEquipmentGroupStyle"] = (Style)Application.Current.Resources["LabelSmallStyleGray"];
            //Resources["LabelFilterAuxiliaryEquipmentStyle"] = (Style)Application.Current.Resources["LabelSmallStyleGray"];
            //Resources["Uptime_IsClearEnabled"] = false;
            Resources["Uptime_FilterInfo"] = string.Empty;
            Resources["Uptime_IsFilterInfoVisible"] = false;

            DoFiltering();
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
            wvLocations.WidthRequest = ScreenWidth - 35;
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
            wvDepartments.WidthRequest = ScreenWidth - 35;
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
            wvSubDepartments.WidthRequest = ScreenWidth - 35;
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
            wvEquipments.WidthRequest = ScreenWidth - 35;
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
            wvEquipmentGroups.WidthRequest = ScreenWidth - 35;
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
            wvAuxiliaryEquipments.WidthRequest = ScreenWidth - 35;
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
                    departmentId = null;
                    subDepartmentId = null;
                    equipmentId = null;
                    equipmentGroupId = null;
                    auxiliaryEquipmentId = null;
                    Resources["Uptime_FilterInfo"] = SetFilterInfoText();
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
                    subDepartmentId = null;
                    equipmentId = null;
                    equipmentGroupId = null;
                    auxiliaryEquipmentId = null;
                    Resources["Uptime_FilterInfo"] = SetFilterInfoText();
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
                    equipmentId = null;
                    equipmentGroupId = null;
                    auxiliaryEquipmentId = null;
                    Resources["Uptime_FilterInfo"] = SetFilterInfoText();
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
                    equipmentGroupId = null;
                    auxiliaryEquipmentId = null;
                    Resources["Uptime_FilterInfo"] = SetFilterInfoText();
                }
                e.Cancel = true;
            };
            
            wvEquipmentGroups.Navigating += (s, e) =>
            {
                if (e.Url != string.Empty)
                {
                    equipmentGroupId = int.Parse(e.Url.Split('=').LastOrDefault());
                    auxiliaryEquipmentId = null;
                    Resources["Uptime_FilterInfo"] = SetFilterInfoText();
                }
                e.Cancel = true;
            };

            wvAuxiliaryEquipments.Navigating += (s, e) =>
            {
                if (e.Url != string.Empty)
                {
                    auxiliaryEquipmentId = int.Parse(e.Url.Split('=').LastOrDefault());
                    Resources["Uptime_FilterInfo"] = SetFilterInfoText();
                }
                e.Cancel = true;
            };

            #endregion

            return result;
        }

        private async Task<double> PopulateWebView(string webViewName)
        {
            double wvHeight = 0;
            const double WV_ROW_Height = 32;
            const double WV_HEADER_Height = 30;
            double languageCorrection = 0;

            if (App.CurrentLanguage == Languages.Chinese)
                languageCorrection = 19;

            switch (webViewName)
            {
                case "wvLocations":
                    IEnumerable<EfficiencyModel> locationList = await MBoxApiCalls.GetEfficiencyPerLocation(resultedPersonalFilterId, timeFilterId);
                    string htmlHeaderLocations = HtmlTableSupport.Uptime_Locations_TableHeader();
                    string htmlContentLocations = HtmlTableSupport.Uptime_Medium_TableContent(locationList);
                    string htmlHtmlLocations = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderLocations, htmlContentLocations);
                    wvLocations.Source = new HtmlWebViewSource { Html = htmlHtmlLocations };
                    wvHeight = 2 * WV_HEADER_Height + locationList.Count() * WV_ROW_Height + 21 + languageCorrection;
                    break;
                case "wvDepartments":
                    IEnumerable<EfficiencyModel> departmentsList = await MBoxApiCalls.GetEfficiencyPerDepartment(locationId, resultedPersonalFilterId, timeFilterId);
                    string htmlHeaderDepartments = HtmlTableSupport.Uptime_Medium_TableHeader("Department");
                    string htmlContentDepartments = HtmlTableSupport.Uptime_Medium_TableContent(departmentsList);
                    string htmlHtmlDepartments = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderDepartments, htmlContentDepartments);
                    wvDepartments.Source = new HtmlWebViewSource { Html = htmlHtmlDepartments };
                    wvHeight = WV_HEADER_Height + departmentsList.Count() * WV_ROW_Height + 21 + languageCorrection;
                    break;
                case "wvSubDepartments":
                    IEnumerable<EfficiencyModel> subDepartmentsList = await MBoxApiCalls.GetEfficiencyPerSubDepartment(locationId, departmentId, resultedPersonalFilterId, timeFilterId);
                    string htmlHeaderSubDepartments = HtmlTableSupport.Uptime_Medium_TableHeader("SubDepartment");
                    string htmlContentSubDepartments = HtmlTableSupport.Uptime_Medium_TableContent(subDepartmentsList);
                    string htmlHtmlSubDepartments = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderSubDepartments, htmlContentSubDepartments);
                    wvSubDepartments.Source = new HtmlWebViewSource { Html = htmlHtmlSubDepartments };
                    wvHeight = WV_HEADER_Height + subDepartmentsList.Count() * WV_ROW_Height + 21 + languageCorrection;
                    break;
                case "wvEquipments":
                    IEnumerable<EfficiencyModel> equipmentsList = await MBoxApiCalls.GetEfficiencyPerEquipmentType(locationId, departmentId, subDepartmentId, resultedPersonalFilterId, timeFilterId);
                    string htmlHeaderEquipments = HtmlTableSupport.Uptime_Medium_TableHeader("Equipment");
                    string htmlContentEquipments = HtmlTableSupport.Uptime_Medium_TableContent(equipmentsList);
                    string htmlHtmlEquipments = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderEquipments, htmlContentEquipments);
                    wvEquipments.Source = new HtmlWebViewSource { Html = htmlHtmlEquipments };
                    wvHeight = WV_HEADER_Height + equipmentsList.Count() * WV_ROW_Height + 21 + languageCorrection;
                    break;
                case "wvEquipmentGroups":
                    IEnumerable<EfficiencyModel> equipmentGroupsList = await MBoxApiCalls.GetEfficiencyPerEquipmentGroup(locationId, departmentId, subDepartmentId, resultedPersonalFilterId, timeFilterId);
                    string htmlHeaderEquipmentGroups = HtmlTableSupport.Uptime_Medium_TableHeader("EquipmentGroup");
                    string htmlContentEquipmentGroups = HtmlTableSupport.Uptime_Medium_TableContent(equipmentGroupsList);
                    string htmlHtmlEquipmentGroups = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderEquipmentGroups, htmlContentEquipmentGroups);
                    wvEquipmentGroups.Source = new HtmlWebViewSource { Html = htmlHtmlEquipmentGroups };
                    wvHeight = WV_HEADER_Height + equipmentGroupsList.Count() * WV_ROW_Height + 21 + languageCorrection;
                    break;
                case "wvAuxiliaryEquipments":
                    IEnumerable<EfficiencyModel> auxiliaryEquipmentsList = await MBoxApiCalls.GetEfficiencyPerAuxiliaryType(locationId, departmentId, subDepartmentId, resultedPersonalFilterId, timeFilterId);
                    string htmlHeaderAuxiliaryEquipments = HtmlTableSupport.Uptime_Small_TableHeader();
                    string htmlContentAuxiliaryEquipments = HtmlTableSupport.Uptime_Small_TableContent(auxiliaryEquipmentsList);
                    string htmlHtmlAuxiliaryEquipments = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderAuxiliaryEquipments, htmlContentAuxiliaryEquipments);
                    wvAuxiliaryEquipments.Source = new HtmlWebViewSource { Html = htmlHtmlAuxiliaryEquipments };
                    wvHeight = WV_HEADER_Height + auxiliaryEquipmentsList.Count() * WV_ROW_Height + 21;
                    break;
            }

            return wvHeight;
        }

        private string SetFilterInfoText()
        {
            Resources["Uptime_IsFilterInfoVisible"] = true;
            string text = App.CurrentTranslation["Common_FilteredBy"];

            if (locationId != null && locationId > 0) text += App.CurrentTranslation["Uptime_Locations"] + ", ";
            if (departmentId != null && departmentId > 0) text += App.CurrentTranslation["Uptime_Departments"] + ", ";
            if (subDepartmentId != null && subDepartmentId > 0) text += App.CurrentTranslation["Uptime_SubDepartments"] + ", ";
            if (equipmentId != null && equipmentId > 0) text += App.CurrentTranslation["Uptime_Equipment"] + ", ";
            if (equipmentGroupId != null && equipmentGroupId > 0) text += App.CurrentTranslation["Uptime_EquipmentGroup"] + ", ";
            if (auxiliaryEquipmentId != null && auxiliaryEquipmentId > 0) text += App.CurrentTranslation["Uptime_AuxiliaryEquipment"] + ", ";
            if (text.EndsWith(", ")) text = text.Substring(0, text.Length - 2);

            return text;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (ScreenWidth != width || ScreenHeight != height)
            {
                ScreenWidth = width;
                ScreenHeight = height;
                SetUpLayout();

                UptimeAccordion.AccordionWidth = width - 30;
                UptimeAccordion.UpdateLayout();
            }
        }
    }
}
