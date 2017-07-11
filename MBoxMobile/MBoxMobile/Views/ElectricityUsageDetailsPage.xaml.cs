using MBoxMobile.Helpers;
using MBoxMobile.Interfaces;
using MBoxMobile.Models;
using MBoxMobile.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ElectricityUsageDetailsPage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;
        bool IsTablePopulated = false;

        int? locationId = null;
        int? departmentId = null;
        int? subDepartmentId = null;
        int? filterId = null;
        Filter3State mode = Filter3State.All;

        public ElectricityUsageDetailsPage(int? locid, int? depid, int? subdepid, int? filtid)
        {
            InitializeComponent();

            locationId = locid;
            departmentId = depid;
            subDepartmentId = subdepid;
            filterId = filtid;

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["Filter3ButtonWidth"] = (screenWidth - 20) * 0.31;
            Resources["Filter3ButtonHeight"] = screenWidth * 0.10;

            Resources["WebViewWidth"] = screenWidth - 35;
            Resources["WebViewHeight"] = screenHeight - 70;
            Resources["ContentMinHeight"] = screenHeight - 60.0;

            Resources["Filter4FontSize"] = FilterSupport.GetFilter4FontSize(screenWidth);

            switch (mode)
            {
                case Filter3State.All:
                    Resources["FilterAllStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
                    Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["FilterHasWasteStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    break;
                case Filter3State.On:
                    Resources["FilterAllStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
                    Resources["FilterHasWasteStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    break;
                case Filter3State.HasWaste:
                    Resources["FilterAllStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
                    Resources["FilterHasWasteStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
                    break;
            }

            ElectricityUsageDetailsWebView.Navigating += (s, e) =>
            {
                e.Cancel = true;
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Resources["Common_FilterAll"] = App.CurrentTranslation["Common_FilterAll"];
            Resources["Common_FilterOn"] = App.CurrentTranslation["Common_FilterOn"];
            Resources["Common_FilterHasWaste"] = App.CurrentTranslation["Common_FilterHasWaste"];
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
            Resources["FilterHasWasteStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];

            mode = Filter3State.All;
            DoFiltering();
        }

        public void FilterOnClicked(object sender, EventArgs e)
        {
            Resources["FilterAllStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
            Resources["FilterHasWasteStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];

            mode = Filter3State.On;
            DoFiltering();
        }

        public void FilterOffClicked(object sender, EventArgs e)
        {
            Resources["FilterAllStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterHasWasteStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];

            mode = Filter3State.HasWaste;
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
            List<ElectricityMachine> detailList = await MBoxApiCalls.GetElectricityUsagePerMachine((int)mode, locationId, departmentId, subDepartmentId, filterId);
            string htmlHeaderDetails = HtmlTableSupport.ElectricityUsage_Large_TableHeader();
            string htmlContentDetails = HtmlTableSupport.ElectricityUsage_Large_TableContent(detailList);
            string htmlHtmlDetails = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderDetails, htmlContentDetails);
            ElectricityUsageDetailsWebView.Source = new HtmlWebViewSource { Html = htmlHtmlDetails };
            ElectricityUsageDetailsWebView.HeightRequest = (detailList.Count + 1) * 31 + 10;
        }
    }
}
