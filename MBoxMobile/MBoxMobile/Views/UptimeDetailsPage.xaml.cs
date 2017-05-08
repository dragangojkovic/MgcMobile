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
        double screenWidth = 0.0;
        double screenHeight = 0.0;
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

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["Filter4ButtonWidth"] = screenWidth * 0.18;
            Resources["Filter4ButtonHeight"] = screenWidth * 0.10;
            Resources["ContentMinHeight"] = screenHeight - 60.0;

            Resources["FilterLabelMargin"] = new Thickness(5, FilterSupport.GetFilterLabelMarginTop(screenWidth), 0, 0);
            Resources["Filter4FontSize"] = FilterSupport.GetFilter4FontSize(screenWidth);

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
            
            UptimeDetailsAccordion.AccordionWidth = screenWidth - 30;
            UptimeDetailsAccordion.AccordionHeight = 55.0;
            UptimeDetailsAccordion.DataSource = GetEmptyAccordion();
            UptimeDetailsAccordion.DataBind();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Resources["UptimeDetails_FilterLabel"] = App.CurrentTranslation["UptimeDetails_FilterLabel"];
            Resources["UptimeDetails_FilterAll"] = App.CurrentTranslation["UptimeDetails_FilterAll"];
            Resources["UptimeDetails_FilterOn"] = App.CurrentTranslation["UptimeDetails_FilterOn"];
            Resources["UptimeDetails_FilterOff"] = App.CurrentTranslation["UptimeDetails_FilterOff"];
            Resources["UptimeDetails_FilterErrors"] = App.CurrentTranslation["UptimeDetails_FilterErrors"];
            Resources["UptimeDetails_Detail"] = App.CurrentTranslation["UptimeDetails_Detail"];
            Resources["UptimeDetails_AuxiliaryEquipment"] = App.CurrentTranslation["UptimeDetails_AuxiliaryEquipment"];
            Resources["UptimeDetails_Close"] = App.CurrentTranslation["UptimeDetails_Close"];

            if (!AreTablesPopulated)
            {
                Resources["IsLoading"] = true;
                UptimeDetailsAccordion.AccordionWidth = screenWidth - 30;
                UptimeDetailsAccordion.AccordionHeight = 55.0;
                UptimeDetailsAccordion.DataSource = await GetAccordionData();
                UptimeDetailsAccordion.DataBind();
                Resources["IsLoading"] = false;

                AreTablesPopulated = true;
            }
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

            #region Details
            wvDetails = new WebView();
            wvDetails.WidthRequest = screenWidth - 35;
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
            wvAuxiliaryEquipments = new WebView();
            wvAuxiliaryEquipments.WidthRequest = screenWidth - 35;
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
            double wvHeight = 0;
            const double WV_ROW_Height = 31.75;

            switch (webViewName)
            {
                case "wvDetails":
                    IEnumerable<EfficiencyMachine> detailList = await MBoxApiCalls.GetEfficiencyByMachine(locationId, departmentId, subDepartmentId, filterId, periodId, equipmentTypeId, equipmentGroupId, (int)mode);
                    string htmlHeaderDetails = HtmlTableSupport.Uptime_Details_TableHeader();
                    string htmlBodyDetails = HtmlTableSupport.Uptime_Details_TableBody(detailList);
                    string htmlHtmlDetails = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderDetails, htmlBodyDetails);
                    wvDetails.Source = new HtmlWebViewSource { Html = htmlHtmlDetails };
                    wvHeight = (detailList.Count() + 2) * WV_ROW_Height + 7;
                    break;
                case "wvAuxiliaryEquipments":
                    IEnumerable<EfficiencyAuxiliaryEquipment> auxiliaryEquipmentsList = await MBoxApiCalls.GetEfficiencyByAuxMachine(locationId, departmentId, subDepartmentId, filterId, periodId, auxiliaryTypeId);
                    string htmlHeaderAuxiliaryEquipments = HtmlTableSupport.Uptime_AuxiliaryEquipments_TableHeader();
                    string htmlBodyAuxiliaryEquipments = HtmlTableSupport.Uptime_AuxiliaryEquipments_TableBody(auxiliaryEquipmentsList);
                    string htmlHtmlAuxiliaryEquipments = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderAuxiliaryEquipments, htmlBodyAuxiliaryEquipments);
                    wvAuxiliaryEquipments.Source = new HtmlWebViewSource { Html = htmlHtmlAuxiliaryEquipments };
                    wvHeight = (auxiliaryEquipmentsList.Count() + 1) * WV_ROW_Height + 10;
                    break;
            }

            return wvHeight;
        }
    }
}
