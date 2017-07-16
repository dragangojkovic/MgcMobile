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

        PersonalFilter personalFilter = null;
        bool filterOn = false;
        int? personalFilterId = null;
        int? resultedPersonalFilterId = null;

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

            //filter default values 
            Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
            Resources["FilterIsEnabled"] = false;

            AuxiliaryWebView.Navigating += (s, e) =>
            {
                if (e.Url != string.Empty)
                {
                    auxiliaryEquipmentId = int.Parse(e.Url.Split('=').LastOrDefault());
                }
                e.Cancel = true;

                if (auxiliaryEquipmentId != 0)
                    Navigation.PushModalAsync(new AuxiliaryEquipmentDetailsPage(personalFilterId, auxiliaryEquipmentId));
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Resources["AuxiliaryEquipment_Title"] = App.CurrentTranslation["AuxiliaryEquipment_Title"];
            Resources["Common_FilterFilterOn"] = App.CurrentTranslation["Common_FilterFilterOn"];
            Resources["Common_FilterFilterOff"] = App.CurrentTranslation["Common_FilterFilterOff"];
            Resources["Common_Filter"] = App.CurrentTranslation["Common_Filter"];
            
            if (!IsTablePopulated)
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
                await PopulateWebView();
                Resources["IsLoading"] = false;

                IsTablePopulated = true;
            }
        }

        private async void DoFiltering()
        {
            Resources["IsLoading"] = true;
            await PopulateWebView();
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
            for (int i = 0; i < filters.Count; i++)
            {
                items[i] = filters[i].FilterName;
            }

            var action = await DisplayActionSheet(App.CurrentTranslation["Common_FilterPersonalDescription"], App.CurrentTranslation["Common_FilterCancel"], null, items);
            if (action != App.CurrentTranslation["Common_FilterCancel"])
            {
                bool result = await MBoxApiCalls.SetSelectedPersonalFilter((int)personalFilterId);
                if (result)
                {
                    FilterButton.Text = action;
                    personalFilterId = filters.Where(x => x.FilterName == action).FirstOrDefault().FilterID;
                    resultedPersonalFilterId = personalFilterId;

                    DoFiltering();
                }
            }
        }

        private async Task PopulateWebView()
        {
            IEnumerable<AuxiliaryType> auxiliaryTypesList = await MBoxApiCalls.GetAuxiliaryTypes(resultedPersonalFilterId);
            string htmlHeaderAuxiliaryTypes = HtmlTableSupport.AuxiliaryEquipment_Type_TableHeader();
            string htmlContentAuxiliaryTypes = HtmlTableSupport.AuxiliaryEquipment_Type_TableContent(auxiliaryTypesList);
            string htmlHtmlAuxiliaryTypes = HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeaderAuxiliaryTypes, htmlContentAuxiliaryTypes);
            AuxiliaryWebView.Source = new HtmlWebViewSource { Html = htmlHtmlAuxiliaryTypes };
        }
    }
}
