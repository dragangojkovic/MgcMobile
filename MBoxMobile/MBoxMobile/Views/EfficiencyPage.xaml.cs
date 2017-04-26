using MBoxMobile.CustomControls;
using MBoxMobile.Interfaces;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EfficiencyPage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;
        bool workingTimeOnly = false;

        public EfficiencyPage()
        {
            InitializeComponent();

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["CheckboxAreaWidth"] = screenWidth * 0.1;
            Resources["CheckboxSource"] = "emptyroundcheck50.png";

            Resources["Filter2ButtonWidth"] = screenWidth * 0.25;
            Resources["Filter2ButtonHeight"] = screenWidth * 0.12;
            Resources["FilterButtonWidth"] = screenWidth * 0.42;
            Resources["FilterButtonHeight"] = screenWidth * 0.12;
            Resources["FilterTimeButtonWidth"] = screenWidth * 0.4;
            Resources["FilterTimeButtonHeight"] = screenWidth * 0.1;
            Resources["ContentMinHeight"] = screenHeight - 60.0;

            EfficiencyAccordion.AccordionWidth = screenWidth * 0.94;
            EfficiencyAccordion.AccordionHeight = 55.0;
            EfficiencyAccordion.DataSource = GetAccordionData();
            EfficiencyAccordion.DataBind();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Resources["Efficiency_Title"] = App.CurrentTranslation["Efficiency_Title"];
            Resources["Efficiency_ViewDetail"] = App.CurrentTranslation["Efficiency_ViewDetail"];
            Resources["Efficiency_FilterOn"] = App.CurrentTranslation["Efficiency_FilterOn"];
            Resources["Efficiency_FilterOff"] = App.CurrentTranslation["Efficiency_FilterOff"];
            Resources["Efficiency_Filter"] = App.CurrentTranslation["Efficiency_Filter"];
            Resources["Efficiency_FilterTime"] = App.CurrentTranslation["Efficiency_FilterTimeLast24Hours"];
            Resources["Efficiency_WorkingTimeOnly"] = App.CurrentTranslation["Efficiency_WorkingTimeOnly"];
            Resources["Efficiency_Locations"] = App.CurrentTranslation["Efficiency_Locations"];
            Resources["Efficiency_Departments"] = App.CurrentTranslation["Efficiency_Departments"];
            Resources["Efficiency_SubDepartments"] = App.CurrentTranslation["Efficiency_SubDepartments"];
            Resources["Efficiency_Equipment"] = App.CurrentTranslation["Efficiency_Equipment"];
            Resources["Efficiency_EquipmentGroup"] = App.CurrentTranslation["Efficiency_EquipmentGroup"];
            Resources["Efficiency_AuxiliaryEquipment"] = App.CurrentTranslation["Efficiency_AuxiliaryEquipment"];
        }

        public void ViewDetailClicked(object sender, EventArgs e)
        {
            
        }

        public void FilterOnClicked(object sender, EventArgs e)
        {

        }

        public void FilterOffClicked(object sender, EventArgs e)
        {

        }

        public void FilterClicked(object sender, EventArgs e)
        {

        }

        public void FilterTimeClicked(object sender, EventArgs e)
        {

        }

        public void WorkingTimeTapped(object sender, EventArgs e)
        {
            workingTimeOnly = !workingTimeOnly;
            if (workingTimeOnly)
                Resources["CheckboxSource"] = "fullroundcheck50.png";
            else
                Resources["CheckboxSource"] = "emptyroundcheck50.png";
        }

        private List<AccordionSource> GetAccordionData()
        {
            var result = new List<AccordionSource>();
            
            var asLocations = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Efficiency_Locations"],
                ContentItems = new Label() { Text = "Put something here!"}
            };
            result.Add(asLocations);

            var asDepartments = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Efficiency_Departments"],
                ContentItems = new Label() { Text = "Put something here!" }
            };
            result.Add(asDepartments);

            var asSubDepartments = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Efficiency_SubDepartments"],
                ContentItems = new Label() { Text = "Put something here!" }
            };
            result.Add(asSubDepartments);

            var asEquipment = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Efficiency_Equipment"],
                ContentItems = new Label() { Text = "Put something here!" }
            };
            result.Add(asEquipment);

            var asEquipmentGroup = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Efficiency_EquipmentGroup"],
                ContentItems = new Label() { Text = "Put something here!" }
            };
            result.Add(asEquipmentGroup);

            var asAuxiliaryEquipment = new AccordionSource()
            {
                HeaderText = App.CurrentTranslation["Efficiency_AuxiliaryEquipment"],
                ContentItems = new Label() { Text = "Put something here!" }
            };
            result.Add(asAuxiliaryEquipment);

            return result;
        }
    }
}
