using MBoxMobile.Interfaces;
using MBoxMobile.Models;
using MBoxMobile.Services;
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
    public partial class InputNotificationsKWhPage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;
        NotificationModel NotificationModel;
        List<MaterialModel> Materials;
        bool IsPosted = false;

        public InputNotificationsKWhPage(NotificationModel notificationModel)
        {
            InitializeComponent();

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["PageContentMinHeight"] = screenHeight - 60.0;
            Resources["NotificationContentMinHeight"] = screenHeight - 200.0;
            Resources["ButtonWidth"] = (screenWidth - 24) / 2.0;

            InitActionSheet();

            //NotificationModel = notificationModel;
            NotificationModel = new NotificationModel();    //testing only
            //NotificationModel.ID = int.Parse(apsInfo.Inputstable_AlterID);
            //NotificationModel.MachineNumber = apsInfo.machine_num;
            NotificationModel.MachineName = "Super cool machine";
            NotificationModel.DateTime = "10.05.2017 22:51";
            //NotificationModel.EquipmentTypeName = "";
            //NotificationModel.EquipmentGroupName = apsInfo.EquipGroupName;
            //NotificationModel.Kwh = apsInfo.Kwh;
            //NotificationModel.Operator = apsInfo.Operator;
            //NotificationModel.Product = apsInfo.Product;
            //NotificationModel.Notification = apsInfo.Notification;
            //NotificationModel.Location = apsInfo.Location;
            //NotificationModel.Department = apsInfo.Department;
            //NotificationModel.SubDepartment = apsInfo.SubDepartment;
            //NotificationModel.AlterType = 55;// int.Parse(apsInfo.AlterType);
            //NotificationModel.AlterReply = 6551;// int.Parse(apsInfo.AlterReply);
            //NotificationModel.NotType = int.Parse(apsInfo.NotType);

            if (string.IsNullOrEmpty(notificationModel.AlterCauseText))
            {
                IsPosted = false;
            }
            else if (notificationModel.AlterCauseText.Contains("Other see remark") && string.IsNullOrWhiteSpace(notificationModel.Description))
            {
                IsPosted = false;
            }
            else
            {
                IsPosted = true;
            }

            if (IsPosted)
            {
                CauseButton.IsEnabled = false;
                CauseButton.Text = notificationModel.AlterCauseText;
                Opinion.IsEnabled = false;
                Opinion.Text = notificationModel.Description;
                CancelButton.IsVisible = true;
            }
            else
            {
                CancelButton.IsVisible = false;
            }
        }

        private async void InitActionSheet()
        {
            Resources["IsLoading"] = true;
            Materials = await MBoxApiCalls.GetElectricityWasteCauseList();
            Resources["IsLoading"] = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Resources["InputNotificationsKWH_Title"] = App.CurrentTranslation["InputNotificationsKWH_Title"];
            Resources["InputNotificationsKWH_CauseButtonText"] = App.CurrentTranslation["InputNotificationsKWH_CauseButtonText"];
            Resources["InputNotificationsKWH_OpinionPlaceholder"] = App.CurrentTranslation["InputNotificationsKWH_OpinionPlaceholder"];
            Resources["InputNotificationsKWH_SubmitButtonText"] = App.CurrentTranslation["InputNotificationsKWH_SubmitButtonText"];
            Resources["InputNotificationsKWH_CancelButtonText"] = App.CurrentTranslation["InputNotificationsKWH_CancelButtonText"];

            Resources["InputNotificationsKWH_MachineNumberTitle"] = App.CurrentTranslation["InputNotificationsKWH_MachineNumberTitle"];
            Resources["InputNotificationsKWH_MachineNumberValue"] = NotificationModel.MachineNumber;
            Resources["InputNotificationsKWH_MachineNameTitle"] = App.CurrentTranslation["InputNotificationsKWH_MachineNameTitle"];
            Resources["InputNotificationsKWH_MachineNameValue"] = NotificationModel.MachineName;
            Resources["InputNotificationsKWH_EquipTypeNameTitle"] = App.CurrentTranslation["InputNotificationsKWH_EquipTypeNameTitle"];
            Resources["InputNotificationsKWH_EquipTypeNameValue"] = NotificationModel.EquipmentTypeName;
            Resources["InputNotificationsKWH_EquipGroupNameTitle"] = App.CurrentTranslation["InputNotificationsKWH_EquipGroupNameTitle"];
            Resources["InputNotificationsKWH_EquipGroupNameValue"] = NotificationModel.EquipmentGroupName;

            Resources["InputNotificationsKWH_KwhTitle"] = App.CurrentTranslation["InputNotificationsKWH_KwhTitle"];
            Resources["InputNotificationsKWH_KwhValue"] = NotificationModel.Kwh;
            Resources["InputNotificationsKWH_OperatorTitle"] = App.CurrentTranslation["InputNotificationsKWH_OperatorTitle"];
            Resources["InputNotificationsKWH_OperatorValue"] = NotificationModel.Operator;
            Resources["InputNotificationsKWH_ProductTitle"] = App.CurrentTranslation["InputNotificationsKWH_ProductTitle"];
            Resources["InputNotificationsKWH_ProductValue"] = NotificationModel.Product;
            Resources["InputNotificationsKWH_DateTimeTitle"] = App.CurrentTranslation["InputNotificationsKWH_DateTimeTitle"];
            Resources["InputNotificationsKWH_DateTimeValue"] = NotificationModel.DateTime;

            Resources["InputNotificationsKWH_NotificationTitle"] = App.CurrentTranslation["InputNotificationsKWH_NotificationTitle"];
            Resources["InputNotificationsKWH_NotificationValue"] = NotificationModel.Notification;
            Resources["InputNotificationsKWH_LocationTitle"] = App.CurrentTranslation["InputNotificationsKWH_LocationTitle"];
            Resources["InputNotificationsKWH_LocationValue"] = NotificationModel.Location;
            Resources["InputNotificationsKWH_DepartmentTitle"] = App.CurrentTranslation["InputNotificationsKWH_DepartmentTitle"];
            Resources["InputNotificationsKWH_DepartmentValue"] = NotificationModel.Department;
            Resources["InputNotificationsKWH_SubDepartmentTitle"] = App.CurrentTranslation["InputNotificationsKWH_SubDepartmentTitle"];
            Resources["InputNotificationsKWH_SubDepartmentValue"] = NotificationModel.SubDepartment;
        }

        public async void CauseClicked(object sender, EventArgs e)
        {
            string[] items = new string[Materials.Count];

            for (int i = 0; i < Materials.Count; i++)
            {
                items[i] = Materials[i].Material;
            }

            var action = await DisplayActionSheet(App.CurrentTranslation["InputNotificationsKWH_CauseASDescription"], App.CurrentTranslation["InputNotificationsKWH_CauseASCancel"], null, items);
            if (action == App.CurrentTranslation["InputNotificationsKWH_CauseASCancel"])
                action = App.CurrentTranslation["InputNotificationsKWH_CauseASDescription"];

            CauseButton.Text = action;
        }

        public async void SubmitClicked(object sender, EventArgs e)
        {

        }

        public async void CancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
