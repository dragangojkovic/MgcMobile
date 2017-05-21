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
    public partial class AuxiliaryEquipmentPage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;
        bool IsTablePopulated = false;

        bool filterOn = false;
        int? personalFilter = null;
        
        int auxiliaryEquipmentId = 0;

        public AuxiliaryEquipmentPage()
        {
            InitializeComponent();

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["Filter2ButtonWidth"] = screenWidth * 0.25;
            Resources["Filter2ButtonHeight"] = screenWidth * 0.12;
            Resources["FilterButtonWidth"] = screenWidth * 0.4;
            Resources["FilterButtonHeight"] = screenWidth * 0.12;
            Resources["WebViewWidth"] = screenWidth - 35;
            Resources["WebViewHeight"] = screenHeight - 10;
            Resources["ContentMinHeight"] = screenHeight;

            Resources["Filter2FontSize"] = FilterSupport.GetFilter2FontSize(screenWidth);

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

            AuxiliaryWebView.Navigating += (s, e) =>
            {
                if (e.Url != string.Empty)
                {
                    auxiliaryEquipmentId = int.Parse(e.Url.Split('=').LastOrDefault());
                }
                e.Cancel = true;

                if (auxiliaryEquipmentId != 0)
                    Navigation.PushModalAsync(new AuxiliaryEquipmentDetailsPage(personalFilter, auxiliaryEquipmentId));
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Resources["AuxiliaryEquipment_Title"] = App.CurrentTranslation["AuxiliaryEquipment_Title"];
            Resources["Common_FilterFilterOn"] = App.CurrentTranslation["Common_FilterFilterOn"];
            Resources["Common_FilterFilterOff"] = App.CurrentTranslation["Common_FilterFilterOff"];
            Resources["Common_Filter"] = App.CurrentTranslation["Common_Filter"];                     //this can be any value from GetPersonalFilters()
            
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

        private async Task PopulateWebView()
        {
            IEnumerable<AuxiliaryType> auxiliaryTypesList = await MBoxApiCalls.GetAuxiliaryTypes(personalFilter);
            string htmlHeaderAuxiliaryTypes = HtmlTableSupport.AuxiliaryEquipment_Type_TableHeader();
            string htmlContentAuxiliaryTypes = HtmlTableSupport.AuxiliaryEquipment_Type_TableContent(auxiliaryTypesList);
            string htmlHtmlAuxiliaryTypes = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderAuxiliaryTypes, htmlContentAuxiliaryTypes);
            AuxiliaryWebView.Source = new HtmlWebViewSource { Html = htmlHtmlAuxiliaryTypes };
        }
    }
}
