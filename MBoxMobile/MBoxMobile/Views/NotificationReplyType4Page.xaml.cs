﻿using MBoxMobile.Interfaces;
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
    public partial class NotificationReplyType4Page : ContentPage
    {
        double ScreenWidth = 0.0;
        double ScreenHeight = 0.0;
        NotificationModel NotificationModel;
        bool ShowReceivedNotification = false;
        List<SolutionCauseModel> SolutionCauses = new List<SolutionCauseModel>();
        int CauseID = 0;

        public NotificationReplyType4Page(NotificationModel notificationModel, bool showReceived = false)
        {
            InitializeComponent();

            App.IsNotificationHandling = true;
            NotificationModel = notificationModel;
            ShowReceivedNotification = showReceived;

            ScreenWidth = DependencyService.Get<IDisplay>().Width;
            ScreenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["IsLoading"] = false;
            Resources["PageContentMinHeight"] = ScreenHeight - 60.0;
            Resources["NotificationContentMinHeight"] = ScreenHeight - 200.0;
            Resources["ButtonWidth"] = (ScreenWidth - 24) / 2.0;
            Resources["ButtonLargeWidth"] = ScreenWidth - 20;

            SendButton.IsVisible = false;
            Resources["SolutionWidth"] = ScreenWidth - 20;
            Solution.Focused += Solution_Focused;
            Solution.Unfocused += Solution_Unfocused;

            InitActionSheet();
        }

        private void Solution_Unfocused(object sender, FocusEventArgs e)
        {
            SendButton.IsVisible = false;
            Resources["SolutionWidth"] = ScreenWidth - 30;
        }

        private void Solution_Focused(object sender, FocusEventArgs e)
        {
            SendButton.IsVisible = true;
            Resources["SolutionWidth"] = ScreenWidth - 95;
        }

        private async void InitActionSheet()
        {
            Resources["IsLoading"] = true;
            SolutionCauses = await MBoxApiCalls.GetSolutionCauseList();
            Resources["IsLoading"] = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Resources["NotificationReplyType4_Title"] = App.CurrentTranslation["NotificationReplyType4_Title"];

            Resources["NotificationReply_DateTimeTitle"] = App.CurrentTranslation["NotificationReply_DateTimeTitle"];
            Resources["NotificationReply_DateTimeValue"] = NotificationModel.RecordDateLocal;
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
            Resources["NotificationReply_NotificationTitle"] = App.CurrentTranslation["NotificationReply_NotificationTitle"];
            Resources["NotificationReply_NotificationValue"] = NotificationModel.AlterDescription;
            Resources["NotificationReply_DescriptionTitle"] = App.CurrentTranslation["NotificationReply_DescriptionTitle"];
            Resources["NotificationReply_DescriptionValue"] = NotificationModel.Description;

            Resources["NotificationReply_CauseButtonText"] = App.CurrentTranslation["NotificationReply_CauseButtonText"];
            Resources["NotificationReply_SolutionPlaceholder"] = App.CurrentTranslation["NotificationReply_SolutionPlaceholder"];
            Resources["NotificationReply_SendButtonText"] = App.CurrentTranslation["NotificationReply_SendButtonText"];
            Resources["NotificationReply_SubmitButtonText"] = App.CurrentTranslation["NotificationReply_SaveButtonText"];
            Resources["NotificationReply_CancelButtonText"] = App.CurrentTranslation["NotificationReply_CancelButtonText"];
        }

        public async void CauseClicked(object sender, EventArgs e)
        {
            if (SolutionCauses.Count > 0)
            {
                string[] items = new string[SolutionCauses.Count];

                for (int i = 0; i < SolutionCauses.Count; i++)
                {
                    items[i] = SolutionCauses[i].Material;
                }

                var action = await DisplayActionSheet(App.CurrentTranslation["NotificationReply_CauseASDescription"], App.CurrentTranslation["NotificationReply_CauseASCancel"], null, items);
                if (action != App.CurrentTranslation["NotificationReply_CauseASCancel"])
                {
                    CauseButton.Text = action;
                    CauseID = SolutionCauses.Where(x => x.Material == action).FirstOrDefault().MID;
                                        
                    Solution.Focus();
                }
            }
        }

        public async void SendClicked(object sender, EventArgs e)
        {
            await HandlingSubmit();
        }

        public async void SubmitClicked(object sender, EventArgs e)
        {
            await HandlingSubmit();
        }

        private async Task HandlingSubmit()
        {
            if (Solution.Text == null) Solution.Text = string.Empty;

            if (CauseID == 0)
            {
                await DisplayAlert(App.CurrentTranslation["NotificationReplyType4_Title"], App.CurrentTranslation["NotificationReply_ErrorMsgChooseCause"], App.CurrentTranslation["Common_OK"]);
            }
            else if (string.IsNullOrEmpty(Solution.Text.Trim()))
            {
                await DisplayAlert(App.CurrentTranslation["NotificationReplyType4_Title"], App.CurrentTranslation["NotificationReply_ErrorMsgInputSolution"], App.CurrentTranslation["Common_OK"]);
            }
            else
            {
                Resources["IsLoading"] = true;
                bool result = await MBoxApiCalls.ReplySolution(NotificationModel.ID, NotificationModel.ParentID, Solution.Text.Trim(), CauseID);
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
                    await DisplayAlert(App.CurrentTranslation["NotificationReplyType4_Title"], App.CurrentTranslation["NotificationReply_ErrorMsgSubmitFailed"], App.CurrentTranslation["Common_OK"]);
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

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (Solution.IsFocused)
                Solution.Unfocus();

            if (ScreenWidth != width || ScreenHeight != height)
            {
                ScreenWidth = width;
                ScreenHeight = height;

                Resources["ButtonWidth"] = (ScreenWidth - 24) / 2.0;
                Resources["ButtonLargeWidth"] = ScreenWidth - 20;

                if (SendButton.IsVisible)
                    Resources["SolutionWidth"] = ScreenWidth - 95;
                else
                    Resources["SolutionWidth"] = ScreenWidth - 30;
            }
        }
    }
}
