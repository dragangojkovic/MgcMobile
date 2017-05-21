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
    public partial class ProductionPage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;
        bool IsTablePopulated = false;

        bool filterOn = false;
        int? personalFilter = null;
        int timeFilter = 6274;
        bool workingTimeOnly = false;

        int equipmentId = 0;
        string equipmentName = string.Empty;
        string descCN = null;
        string equipmentIdAndDescCN = string.Empty;
        string[] equipParameters;

        public ProductionPage()
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

            Resources["WebViewWidth"] = screenWidth - 35;
            Resources["WebViewHeight"] = screenHeight - 70;
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

            ProductionWebView.Navigating += (s, e) =>
            {
                if (e.Url != string.Empty)
                {
                    equipmentIdAndDescCN = e.Url.Split('=').LastOrDefault();
                    equipParameters = equipmentIdAndDescCN.Split('#');
                    equipmentId = int.Parse(equipParameters[0]);
                    equipmentName = equipParameters[1];
                    descCN = equipParameters[2];
                }
                e.Cancel = true;

                if (equipmentId != 0 && !string.IsNullOrEmpty(descCN))
                {
                    Navigation.PushModalAsync(new ProductionDetailsPage(timeFilter, personalFilter, equipmentId, equipmentName, descCN));
                }
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            string currentTimeFilter = FilterSupport.GetTimeFilters()[timeFilter];

            Resources["Production_Title"] = App.CurrentTranslation["Production_Title"];
            Resources["Common_FilterFilterOn"] = App.CurrentTranslation["Common_FilterFilterOn"];
            Resources["Common_FilterFilterOff"] = App.CurrentTranslation["Common_FilterFilterOff"];
            Resources["Common_Filter"] = App.CurrentTranslation["Common_Filter"];                     //this can be any value from GetPersonalFilters()
            Resources["Common_FilterTime"] = App.CurrentTranslation[currentTimeFilter];
            Resources["Uptime_WorkingTimeOnly"] = App.CurrentTranslation["Uptime_WorkingTimeOnly"];

            if (!IsTablePopulated)
            {
                Resources["IsLoading"] = true;
                await PopulateWebView();
                Resources["IsLoading"] = false;

                IsTablePopulated = true;
            }
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
                await PopulateWebView();
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

        private async Task PopulateWebView()
        {
            IEnumerable<ProductionGeneral> productionGeneralList = await MBoxApiCalls.GetProductionPerEquipmentType(personalFilter, timeFilter);
            string htmlHeaderProductionGenerals = HtmlTableSupport.Production_Equipment_TableHeader();
            string htmlContentProductionGenerals = HtmlTableSupport.Production_Equipment_TableContent(productionGeneralList);
            string htmlHtmlProductionGenerals = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderProductionGenerals, htmlContentProductionGenerals);
            ProductionWebView.Source = new HtmlWebViewSource { Html = htmlHtmlProductionGenerals };
        }
    }
}
