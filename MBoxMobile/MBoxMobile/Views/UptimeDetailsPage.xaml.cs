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
    public partial class UptimeDetailsPage : ContentPage
    {
        double ScreenWidth = 0.0;
        double ScreenHeight = 0.0;
        bool AreTablesPopulated = false;

        int periodId;
        int? filterId = null;
        int? locationId = null;
        int? departmentId = null;
        int? subDepartmentId = null;
        int? equipmentTypeId = null;
        int? equipmentGroupId = null;
        int? auxiliaryTypeId = null;
        Filter4State mode = Filter4State.All;

        WebView wvDetails, wvAuxiliaryEquipments;

        public UptimeDetailsPage(int perid, int? filtid, int? locid, int? depid, int? subdid, int? equipid, int? equipgroupid, int? auxid)
        {
            InitializeComponent();

            periodId = perid;
            filterId = filtid;
            locationId = locid;
            departmentId = depid;
            subDepartmentId = subdid;
            equipmentTypeId = equipid;
            equipmentGroupId = equipgroupid;
            auxiliaryTypeId = auxid;

            ScreenWidth = DependencyService.Get<IDisplay>().Width;
            ScreenHeight = DependencyService.Get<IDisplay>().Height;

            SetUpLayout();

            switch (mode)
            {
                case Filter4State.All:
                    Resources["FilterAllStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
                    Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["FilterErrorsStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    break;
                case Filter4State.On:
                    Resources["FilterAllStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
                    Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["FilterErrorsStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    break;
                case Filter4State.Off:
                    Resources["FilterAllStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
                    Resources["FilterErrorsStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    break;
                case Filter4State.Errors:
                    Resources["FilterAllStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["FilterErrorsStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
                    break;
            }
            
            UptimeDetailsAccordion.AccordionWidth = ScreenWidth - 30;
            UptimeDetailsAccordion.AccordionHeight = 55.0;
            UptimeDetailsAccordion.DataSource = GetEmptyAccordion();
            UptimeDetailsAccordion.DataBind();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Resources["UptimeDetails_FilterLabel"] = App.CurrentTranslation["UptimeDetails_FilterLabel"];
            Resources["Common_FilterAll"] = App.CurrentTranslation["Common_FilterAll"];
            Resources["Common_FilterOn"] = App.CurrentTranslation["Common_FilterOn"];
            Resources["Common_FilterOff"] = App.CurrentTranslation["Common_FilterOff"];
            Resources["Common_FilterErrors"] = App.CurrentTranslation["Common_FilterErrors"];
            Resources["UptimeDetails_Detail"] = App.CurrentTranslation["UptimeDetails_Detail"];
            Resources["UptimeDetails_AuxiliaryEquipment"] = App.CurrentTranslation["UptimeDetails_AuxiliaryEquipment"];
            Resources["Common_Close"] = App.CurrentTranslation["Common_Close"];

            if (!AreTablesPopulated)
            {
                Resources["IsLoading"] = true;
                UptimeDetailsAccordion.AccordionWidth = ScreenWidth - 30;
                UptimeDetailsAccordion.AccordionHeight = 55.0;
                UptimeDetailsAccordion.DataSource = await GetAccordionData();
                UptimeDetailsAccordion.DataBind();
                Resources["IsLoading"] = false;

                AreTablesPopulated = true;
            }
        }

        private void SetUpLayout()
        {
            Resources["Filter4ButtonWidth"] = ScreenWidth * 0.18;
            Resources["Filter4ButtonHeight"] = 36;
            Resources["ContentMinHeight"] = ScreenHeight - 60.0;

            Resources["FilterLabelMargin"] = new Thickness(5, FilterSupport.GetFilterLabelMarginTop(ScreenWidth), 0, 0);
            Resources["Filter4FontSize"] = FilterSupport.GetFilter4FontSize(ScreenWidth);
        }

        private void InitializeWebViews()
        {
            wvDetails = new WebView();
            wvAuxiliaryEquipments = new WebView();

            wvDetails.Navigating += (s, e) =>
            {
                e.Cancel = true;
            };
            wvAuxiliaryEquipments.Navigating += (s, e) =>
            {
                e.Cancel = true;
            };
        }

        public async void DetailCloseClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        public void FilterAllClicked(object sender, EventArgs e)
        {
            Resources["FilterAllStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
            Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterErrorsStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];

            mode = Filter4State.All;
            DoFiltering();
        }

        public void FilterOnClicked(object sender, EventArgs e)
        {
            Resources["FilterAllStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
            Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterErrorsStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];

            mode = Filter4State.On;
            DoFiltering();
        }

        public void FilterOffClicked(object sender, EventArgs e)
        {
            Resources["FilterAllStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
            Resources["FilterErrorsStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];

            mode = Filter4State.Off;
            DoFiltering();
        }

        public void FilterErrorsClicked(object sender, EventArgs e)
        {
            Resources["FilterAllStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterErrorsStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];

            mode = Filter4State.Errors;
            DoFiltering();
        }

        private async void DoFiltering()
        {
            Resources["IsLoading"] = true;
            UptimeDetailsAccordion.DataSource = await GetAccordionData();
            UptimeDetailsAccordion.DataBind();
            Resources["IsLoading"] = false;
        }

        private List<AccordionSource> GetEmptyAccordion()
        {
            var result = new List<AccordionSource>();

            var asDetails = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["UptimeDetails_Detail"]
            };
            result.Add(asDetails);

            var asAuxiliaryEquipment = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["UptimeDetails_AuxiliaryEquipment"]
            };
            result.Add(asAuxiliaryEquipment);

            return result;
        }

        private async Task<List<AccordionSource>> GetAccordionData()
        {
            var result = new List<AccordionSource>();
            InitializeWebViews();

            #region Details
            wvDetails.WidthRequest = ScreenWidth - 35;
            wvDetails.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            wvDetails.VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true);

            var asDetails = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["UptimeDetails_Detail"],
                ContentItems = wvDetails,
                ContentHeight = await PopulateWebView("wvDetails")
            };
            result.Add(asDetails);
            #endregion

            #region AuxiliaryEquipment
            wvAuxiliaryEquipments.WidthRequest = ScreenWidth - 35;
            wvAuxiliaryEquipments.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            wvAuxiliaryEquipments.VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true);

            var asAuxiliaryEquipment = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["UptimeDetails_AuxiliaryEquipment"],
                ContentItems = wvAuxiliaryEquipments,
                ContentHeight = await PopulateWebView("wvAuxiliaryEquipments")
            };
            result.Add(asAuxiliaryEquipment);
            #endregion

            return result;
        }

        private async Task<double> PopulateWebView(string webViewName)
        {
            double wvHeight = -1;
            const double WV_ROW_Height = 31.75;

            switch (webViewName)
            {
                case "wvDetails":
                    IEnumerable<EfficiencyMachine> detailList = await MBoxApiCalls.GetEfficiencyPerMachine(locationId, departmentId, subDepartmentId, filterId, periodId, equipmentTypeId, equipmentGroupId, (int)mode);
                    string htmlHeaderDetails = HtmlTableSupport.Uptime_Details_TableHeader();
                    string htmlContentDetails = HtmlTableSupport.Uptime_Details_TableContent(detailList);
                    string htmlHtmlDetails = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderDetails, htmlContentDetails);
                    wvDetails.Source = new HtmlWebViewSource { Html = htmlHtmlDetails };
                    //wvHeight = (detailList.Count() + 2) * WV_ROW_Height + 7;
                    break;
                case "wvAuxiliaryEquipments":
                    IEnumerable<EfficiencyAuxiliaryEquipment> auxiliaryEquipmentsList = await MBoxApiCalls.GetEfficiencyPerAuxMachine(locationId, departmentId, subDepartmentId, filterId, periodId, auxiliaryTypeId);
                    string htmlHeaderAuxiliaryEquipments = HtmlTableSupport.Uptime_AuxiliaryEquipments_TableHeader();
                    string htmlContentAuxiliaryEquipments = HtmlTableSupport.Uptime_AuxiliaryEquipments_TableContent(auxiliaryEquipmentsList);
                    string htmlHtmlAuxiliaryEquipments = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderAuxiliaryEquipments, htmlContentAuxiliaryEquipments);
                    wvAuxiliaryEquipments.Source = new HtmlWebViewSource { Html = htmlHtmlAuxiliaryEquipments };
                    //wvHeight = (auxiliaryEquipmentsList.Count() + 1) * WV_ROW_Height + 10;
                    break;
            }

            return wvHeight;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (ScreenWidth != width || ScreenHeight != height)
            {
                ScreenWidth = width;
                ScreenHeight = height;
                SetUpLayout();

                UptimeDetailsAccordion.AccordionWidth = width - 30;
                UptimeDetailsAccordion.UpdateLayout();
            }
        }
    }
}
