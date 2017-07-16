﻿using MBoxMobile.Interfaces;
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
    public partial class NotificationReplyType3Page : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;
        NotificationModel NotificationModel;
        bool ShowReceivedNotification = false;
        List<AlterDescriptionModel> AlterDescriptions = new List<AlterDescriptionModel>();
        int AlterDescriptionID = 0;

        public NotificationReplyType3Page(NotificationModel notificationModel, bool showReceived = false)
        {
            InitializeComponent();

            App.IsNotificationHandling = true;
            NotificationModel = notificationModel;
            ShowReceivedNotification = showReceived;

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["IsLoading"] = false;
            Resources["PageContentMinHeight"] = screenHeight - 60.0;
            Resources["NotificationContentMinHeight"] = screenHeight - 200.0;
            Resources["ButtonWidth"] = (screenWidth - 24) / 2.0;
            Resources["ButtonLargeWidth"] = screenWidth - 20;

            if (notificationModel.IsPullDown)
            {
                InitActionSheet();
                NotificationButton.IsEnabled = true;
            }
            else
            {
                NotificationButton.IsEnabled = false;
            }
        }

        private async void InitActionSheet()
        {
            Resources["IsLoading"] = true;
            AlterDescriptions = await MBoxApiCalls.GetAlterDescriptionList();
            if (AlterDescriptions.Count > 0)
                AlterDescriptions = AlterDescriptions.Where(x => x.EquipmentType == (int)NotificationModel.AlterButton).ToList();
            Resources["IsLoading"] = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Resources["NotificationReplyType3_Title"] = App.CurrentTranslation["NotificationReplyType3_Title"];

            Resources["NotificationReply_DateTimeTitle"] = App.CurrentTranslation["NotificationReply_DateTimeTitle"];
            Resources["NotificationReply_DateTimeValue"] = NotificationModel.RecordDate;
            Resources["NotificationReply_MachineNumberTitle"] = App.CurrentTranslation["NotificationReply_MachineNumberTitle"];
            Resources["NotificationReply_MachineNumberValue"] = NotificationModel.MachineNumber;
            Resources["NotificationReply_OperatorTitle"] = App.CurrentTranslation["NotificationReply_OperatorTitle"];
            Resources["NotificationReply_OperatorValue"] = NotificationModel.Operator;
            Resources["NotificationReply_ProductTitle"] = App.CurrentTranslation["NotificationReply_ProductTitle"];
            Resources["NotificationReply_ProductValue"] = NotificationModel.Product;
            Resources["NotificationReply_LocationTitle"] = App.CurrentTranslation["NotificationReply_LocationTitle"];
            Resources["NotificationReply_LocationValue"] = NotificationModel.SentToCompany;
            Resources["NotificationReply_DepartmentTitle"] = App.CurrentTranslation["NotificationReply_DepartmentTitle"];
            Resources["NotificationReply_DepartmentValue"] = NotificationModel.Department;
            Resources["NotificationReply_SubDepartmentTitle"] = App.CurrentTranslation["NotificationReply_SubDepartmentTitle"];
            Resources["NotificationReply_SubDepartmentValue"] = NotificationModel.DepartmentSubName;

            Resources["NotificationReply_TypeTitle"] = App.CurrentTranslation["NotificationReply_TypeTitle"];
            Resources["NotificationReply_TypeValue"] = NotificationModel.EquipmentType;
            Resources["NotificationReply_RemarkTitle"] = App.CurrentTranslation["NotificationReply_RemarkTitle"];
            Resources["NotificationReply_RemarkValue"] = NotificationModel.MainCharacterization;

            Resources["NotificationReply_NotificationButtonText"] = App.CurrentTranslation["NotificationReply_NotificationButtonText"];
            Resources["NotificationReply_DescriptionPlaceholder"] = App.CurrentTranslation["NotificationReply_DescriptionPlaceholder"];
            Resources["NotificationReply_SubmitButtonText"] = App.CurrentTranslation["NotificationReply_SaveButtonText"];
            Resources["NotificationReply_CancelButtonText"] = App.CurrentTranslation["NotificationReply_CancelButtonText"];
        }

        public async void NotificationClicked(object sender, EventArgs e)
        {
            if (AlterDescriptions.Count > 0)
            {
                string[] items = new string[AlterDescriptions.Count];

                for (int i = 0; i < AlterDescriptions.Count; i++)
                {
                    items[i] = AlterDescriptions[i].Material;
                }

                var action = await DisplayActionSheet(App.CurrentTranslation["NotificationReply_NotificationASDescription"], App.CurrentTranslation["NotificationReply_NotificationASCancel"], null, items);
                if (action != App.CurrentTranslation["NotificationReply_NotificationASCancel"])
                {
                    NotificationButton.Text = action;
                    AlterDescriptionID = AlterDescriptions.Where(x => x.Material == action).FirstOrDefault().MID;
                }
            }
        }

        public async void SubmitClicked(object sender, EventArgs e)
        {
            if (Description.Text == null) Description.Text = string.Empty;

            if (NotificationModel.IsPullDown && AlterDescriptionID == 0)
            {
                await DisplayAlert(App.CurrentTranslation["NotificationReplyType3_Title"], App.CurrentTranslation["NotificationReply_ErrorMsgChooseNotification"], App.CurrentTranslation["Common_OK"]);
            }
            else if (NotificationModel.NeedDesc && string.IsNullOrEmpty(Description.Text) && string.IsNullOrEmpty(Description.Text.Trim()))
            {
                await DisplayAlert(App.CurrentTranslation["NotificationReplyType3_Title"], App.CurrentTranslation["NotificationReply_ErrorMsgInputDescription"], App.CurrentTranslation["Common_OK"]);
            }
            else
            {
                int popup = 5;
                if (NotificationModel.AlterReply == 6553)
                    popup = 4;
                int? newAlterDescriptionID = null;
                if (AlterDescriptionID > 0)
                    newAlterDescriptionID = AlterDescriptionID;
                Resources["IsLoading"] = true;
                bool result = await MBoxApiCalls.ReplyDescription(NotificationModel.ID, NotificationModel.ParentID, Description.Text.Trim(), newAlterDescriptionID, popup);
                Resources["IsLoading"] = false;

                if (result)
                {
                    if (ShowReceivedNotification)
                        MessagingCenter.Send<string>("NotificationHandler", "NotificationPopupClosed");
                    else
                        MessagingCenter.Send<string>("NotificationHandler", "NotificationPopupClosedWithAction");

                    await Navigation.PopModalAsync();
                }
                else
                    await DisplayAlert(App.CurrentTranslation["NotificationReplyType3_Title"], App.CurrentTranslation["NotificationReply_ErrorMsgSubmitFailed"], App.CurrentTranslation["Common_OK"]);
            }
        }

        public async void CancelClicked(object sender, EventArgs e)
        {
            if (ShowReceivedNotification)
                MessagingCenter.Send<string>("NotificationHandler", "NotificationPopupClosed");
            else
                App.IsNotificationHandling = false;

            await Navigation.PopModalAsync();
        }
    }
}