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
    public partial class ProductionDetailsPage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;
        bool IsTablePopulated = false;

        int periodId;
        int? filterId = null;
        int equipmentTypeId;
        string equipmentName = string.Empty;
        string descCN = string.Empty;
        Filter4State mode = Filter4State.All;

        public ProductionDetailsPage(int perid, int? filtid, int equipid, string equipname, string desccn)
        {
            InitializeComponent();

            periodId = perid;
            filterId = filtid;
            equipmentTypeId = equipid;
            equipmentName = equipname;
            descCN = desccn;

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["ProductionDetails_Title"] = equipmentName;

            Resources["Filter4ButtonWidth"] = (screenWidth - 20) * 0.23;
            Resources["Filter4ButtonHeight"] = screenWidth * 0.10;

            Resources["WebViewWidth"] = screenWidth - 35;
            Resources["WebViewHeight"] = screenHeight - 70;
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

            ProductionDetailsWebView.Navigating += (s, e) =>
            {
                e.Cancel = true;
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Resources["Common_FilterAll"] = App.CurrentTranslation["Common_FilterAll"];
            Resources["Common_FilterOn"] = App.CurrentTranslation["Common_FilterOn"];
            Resources["Common_FilterOff"] = App.CurrentTranslation["Common_FilterOff"];
            Resources["Common_FilterErrors"] = App.CurrentTranslation["Common_FilterErrors"];
            Resources["Common_Close"] = App.CurrentTranslation["Common_Close"];

            if (!IsTablePopulated)
            {
                Resources["IsLoading"] = true;
                await PopulateWebView();
                Resources["IsLoading"] = false;

                IsTablePopulated = true;
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
            await PopulateWebView();
            Resources["IsLoading"] = false;
        }

        private async Task PopulateWebView()
        {
            IEnumerable<ProductionDetail> detailList = await MBoxApiCalls.GetProductionPerMachine(filterId, periodId, descCN, equipmentTypeId, (int)mode);
            string htmlHeaderDetails, htmlContentDetails;
            switch (descCN)
            {
                case "Moulding":
                    htmlHeaderDetails = HtmlTableSupport.ProductionDetail_Moulding_TableHeader();
                    htmlContentDetails = HtmlTableSupport.ProductionDetail_Moulding_TableContent(detailList);
                    break;
                case "CNC":
                    htmlHeaderDetails = HtmlTableSupport.ProductionDetail_CNC_TableHeader();
                    htmlContentDetails = HtmlTableSupport.ProductionDetail_CNC_TableContent(detailList);
                    break;
                case "Welding":
                    htmlHeaderDetails = HtmlTableSupport.ProductionDetail_Welding_TableHeader();
                    htmlContentDetails = HtmlTableSupport.ProductionDetail_Welding_TableContent(detailList);
                    break;
                case "EDM wire cut":
                    htmlHeaderDetails = HtmlTableSupport.ProductionDetail_EDMWireCut_TableHeader();
                    htmlContentDetails = HtmlTableSupport.ProductionDetail_EDMWireCut_TableContent(detailList);
                    break;
                default:
                    htmlHeaderDetails = "";
                    htmlContentDetails = "";
                    break;
            }
            string htmlHtmlDetails = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderDetails, htmlContentDetails);
            ProductionDetailsWebView.Source = new HtmlWebViewSource { Html = htmlHtmlDetails };
        }
    }
}
