using MBoxMobile.CustomControls;
using MBoxMobile.Helpers;
using MBoxMobile.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UptimeDetailsPage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;

        Filter4State filterState = Filter4State.All;

        public UptimeDetailsPage()
        {
            InitializeComponent();

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["Filter4ButtonWidth"] = screenWidth * 0.18;
            Resources["Filter4ButtonHeight"] = screenWidth * 0.10;
            Resources["ContentMinHeight"] = screenHeight - 60.0;

            Resources["FilterLabelMargin"] = new Thickness(5, FilterSupport.GetFilterLabelMarginTop(screenWidth), 0, 0);
            Resources["Filter4FontSize"] = FilterSupport.GetFilter4FontSize(screenWidth);

            switch (filterState)
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
            UptimeDetailsAccordion.DataSource = GetAccordionData();
            UptimeDetailsAccordion.DataBind();
        }

        protected override void OnAppearing()
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
        }

        public void FilterOnClicked(object sender, EventArgs e)
        {
            Resources["FilterAllStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
            Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterErrorsStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
        }

        public void FilterOffClicked(object sender, EventArgs e)
        {
            Resources["FilterAllStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
            Resources["FilterErrorsStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
        }

        public void FilterErrorsClicked(object sender, EventArgs e)
        {
            Resources["FilterAllStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterOnStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterOffStyle"] = (Style)Application.Current.Resources["FilterNotSelectedStyle"];
            Resources["FilterErrorsStyle"] = (Style)Application.Current.Resources["FilterSelectedStyle"];
        }

        private List<AccordionSource> GetAccordionData()
        {
            var result = new List<AccordionSource>();

            var asDetails = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["UptimeDetails_Detail"],
                ContentItems = new Label() { Text = "Put something here!" }
            };
            result.Add(asDetails);

            var asAuxiliaryEquipment = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["UptimeDetails_AuxiliaryEquipment"],
                ContentItems = new Label() { Text = "Put something here!" }
            };
            result.Add(asAuxiliaryEquipment);

            return result;
        }
    }
}
