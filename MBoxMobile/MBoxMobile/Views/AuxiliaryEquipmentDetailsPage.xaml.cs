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
    public partial class AuxiliaryEquipmentDetailsPage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;
        bool IsTablePopulated = false;

        int? filterId = null;
        int auxiliaryEquipmentId;

        public AuxiliaryEquipmentDetailsPage(int? filtid, int auxequipid)
        {
            InitializeComponent();

            filterId = filtid;
            auxiliaryEquipmentId = auxequipid;

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["WebViewWidth"] = screenWidth - 35;
            Resources["WebViewHeight"] = screenHeight - 10;
            Resources["ContentMinHeight"] = screenHeight - 60.0;

            AuxiliaryDetailsWebView.Navigating += (s, e) =>
            {
                e.Cancel = true;
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Resources["UptimeDetails_Detail"] = App.CurrentTranslation["UptimeDetails_Detail"];
            Resources["UptimeDetails_AuxiliaryEquipment"] = App.CurrentTranslation["UptimeDetails_AuxiliaryEquipment"];
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

        private async Task PopulateWebView()
        {
            IEnumerable<AuxiliaryEquipment> detailList = await MBoxApiCalls.GetAuxiliaryEquipments(filterId, auxiliaryEquipmentId);
            string htmlHeaderDetails = HtmlTableSupport.AuxiliaryEquipment_Details_TableHeader();
            string htmlContentDetails = HtmlTableSupport.AuxiliaryEquipment_Details_TableContent(detailList);
            string htmlHtmlDetails = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderDetails, htmlContentDetails);
            AuxiliaryDetailsWebView.Source = new HtmlWebViewSource { Html = htmlHtmlDetails };
        }
    }
}
