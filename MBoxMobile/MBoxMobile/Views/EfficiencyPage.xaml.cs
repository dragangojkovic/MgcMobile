using MBoxMobile.CustomControls;
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
    public partial class EfficiencyPage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;

        public EfficiencyPage()
        {
            InitializeComponent();

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            EfficiencyAccordion.AccordionWidth = DependencyService.Get<IDisplay>().Width * 0.94;
            EfficiencyAccordion.AccordionHeight = 60.0;
            EfficiencyAccordion.DataSource = GetAccordionData();
            EfficiencyAccordion.DataBind();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Resources["Efficiency_Title"] = App.CurrentTranslation["Efficiency_Title"];
            Resources["Efficiency_ViewDetail"] = App.CurrentTranslation["Efficiency_ViewDetail"];
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

        public void FilterTimeClicked(object sender, EventArgs e)
        {

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
