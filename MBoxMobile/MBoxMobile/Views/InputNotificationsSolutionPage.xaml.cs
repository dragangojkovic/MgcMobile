using MBoxMobile.Interfaces;
using MBoxMobile.Models;
using MBoxMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputNotificationsSolutionPage : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;
        NotificationModel NotificationModel;
        List<MaterialModel> Materials;
        int CauseID = 0;
        bool IsProcessed = false;

        public InputNotificationsSolutionPage(NotificationModel notificationModel)
        {
            InitializeComponent();

            App.IsNotificationHandling = true;
            NotificationModel = notificationModel;

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["PageContentMinHeight"] = screenHeight - 60.0;
            Resources["NotificationContentMinHeight"] = screenHeight - 200.0;
            Resources["ButtonWidth"] = (screenWidth - 24) / 2.0;
            Resources["ButtonLargeWidth"] = screenWidth - 20;

            InitActionSheet();

            IsProcessed = !string.IsNullOrWhiteSpace(notificationModel.Solution);

            // Set Optional Buttons
            SubmitButton.IsVisible = !IsProcessed;
            CancelButton.IsVisible = !IsProcessed;
            CloseButton.IsVisible = IsProcessed;

            if (IsProcessed)
            {
                CauseButton.IsEnabled = false;
                CauseButton.Text = notificationModel.Solution;
                Opinion.Text = notificationModel.Description;
                Opinion.IsEnabled = false;

                CloseButton.IsVisible = true;
            }
        }

        private async void InitActionSheet()
        {
            Resources["IsLoading"] = true;
            Materials = await MBoxApiCalls.GetSolutionCauseList(NotificationModel.ID);
            Resources["IsLoading"] = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Resources["InputNotificationsSolution_Title"] = App.CurrentTranslation["InputNotificationsSolution_Title"];
            Resources["InputNotifications_CauseButtonText"] = App.CurrentTranslation["InputNotifications_CauseButtonText"];
            Resources["InputNotifications_OpinionPlaceholder"] = App.CurrentTranslation["InputNotifications_OpinionPlaceholder"];
            Resources["InputNotifications_SubmitButtonText"] = App.CurrentTranslation["InputNotifications_SubmitButtonText"];
            Resources["InputNotifications_CancelButtonText"] = App.CurrentTranslation["InputNotifications_CancelButtonText"];
            Resources["InputNotifications_CloseButtonText"] = App.CurrentTranslation["InputNotifications_CloseButtonText"];

            Resources["InputNotifications_MachineNumberTitle"] = App.CurrentTranslation["InputNotifications_MachineNumberTitle"];
            Resources["InputNotifications_MachineNumberValue"] = NotificationModel.MachineNumber;
            Resources["InputNotifications_MachineNameTitle"] = App.CurrentTranslation["InputNotifications_MachineNameTitle"];
            Resources["InputNotifications_MachineNameValue"] = NotificationModel.MachineName;
            Resources["InputNotifications_EquipTypeNameTitle"] = App.CurrentTranslation["InputNotifications_EquipTypeNameTitle"];
            Resources["InputNotifications_EquipTypeNameValue"] = NotificationModel.EquipmentTypeName;
            Resources["InputNotifications_EquipGroupNameTitle"] = App.CurrentTranslation["InputNotifications_EquipGroupNameTitle"];
            Resources["InputNotifications_EquipGroupNameValue"] = NotificationModel.EquipmentGroupName;

            Resources["InputNotifications_OperatorTitle"] = App.CurrentTranslation["InputNotifications_OperatorTitle"];
            Resources["InputNotifications_OperatorValue"] = NotificationModel.Operator;
            Resources["InputNotifications_ProductTitle"] = App.CurrentTranslation["InputNotifications_ProductTitle"];
            Resources["InputNotifications_ProductValue"] = NotificationModel.Product;
            Resources["InputNotifications_DateTimeTitle"] = App.CurrentTranslation["InputNotifications_DateTimeTitle"];
            Resources["InputNotifications_DateTimeValue"] = NotificationModel.DateTime;

            Resources["InputNotifications_NotificationTitle"] = App.CurrentTranslation["InputNotifications_NotificationTitle"];
            Resources["InputNotifications_NotificationValue"] = NotificationModel.Notification;
            Resources["InputNotifications_LocationTitle"] = App.CurrentTranslation["InputNotifications_LocationTitle"];
            Resources["InputNotifications_LocationValue"] = NotificationModel.Location;
            Resources["InputNotifications_DepartmentTitle"] = App.CurrentTranslation["InputNotifications_DepartmentTitle"];
            Resources["InputNotifications_DepartmentValue"] = NotificationModel.Department;
            Resources["InputNotifications_SubDepartmentTitle"] = App.CurrentTranslation["InputNotifications_SubDepartmentTitle"];
            Resources["InputNotifications_SubDepartmentValue"] = NotificationModel.SubDepartment;
        }

        public async void CauseClicked(object sender, EventArgs e)
        {
            string[] items = new string[Materials.Count];

            for (int i = 0; i < Materials.Count; i++)
            {
                items[i] = Materials[i].Material;
            }

            var action = await DisplayActionSheet(App.CurrentTranslation["InputNotifications_CauseASDescription"], App.CurrentTranslation["InputNotifications_CauseASCancel"], null, items);
            if (action != App.CurrentTranslation["InputNotifications_CauseASCancel"])
            {
                CauseButton.Text = action;
                CauseID = Materials.Where(x => x.Material == action).FirstOrDefault().MID;
            }
        }

        public async void SubmitClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Opinion.Text.Trim()))
            {
                await DisplayAlert(App.CurrentTranslation["InputNotificationsKWH_Title"], App.CurrentTranslation["InputNotifications_ErrorMsgChooseOpinion"], App.CurrentTranslation["Common_OK"]);
            }
            else
            {
                Resources["IsLoading"] = true;
                bool result = await MBoxApiCalls.InputSolution(NotificationModel.ID, Opinion.Text, CauseID == 0 ? (int?)null : CauseID);
                Resources["IsLoading"] = false;

                if (result)
                {
                    MessagingCenter.Send<string>("NotificationHandler", "NotificationPopupClosed");
                    await Navigation.PopModalAsync();
                }
                else
                    await DisplayAlert(App.CurrentTranslation["InputNotificationsKWH_Title"], App.CurrentTranslation["InputNotifications_ErrorMsgSubmitFailed"], App.CurrentTranslation["Common_OK"]);
            }
        }

        public async void CancelClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<string>("NotificationHandler", "NotificationPopupClosed");
            await Navigation.PopModalAsync();
        }

        public async void CloseClicked(object sender, EventArgs e)
        {
            App.IsNotificationHandling = false;
            await Navigation.PopModalAsync();
        }
    }
}
