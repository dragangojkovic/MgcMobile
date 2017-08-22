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
    public partial class NotificationReplyType1Page : ContentPage
    {
        double ScreenWidth = 0.0;
        double ScreenHeight = 0.0;
        NotificationModel NotificationModel;
        bool ShowReceivedNotification = false;
        List<WasteCauseModel> WasteCauses = new List<WasteCauseModel>();
        int CauseID = 0;

        public NotificationReplyType1Page(NotificationModel notificationModel, bool showReceived = false)
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
            Resources["DescriptionWidth"] = ScreenWidth - 20;
            Description.Focused += Description_Focused;
            Description.Unfocused += Description_Unfocused;

            InitActionSheet();
        }

        private void Description_Unfocused(object sender, FocusEventArgs e)
        {
            SendButton.IsVisible = false;
            Resources["DescriptionWidth"] = ScreenWidth - 30;
        }

        private void Description_Focused(object sender, FocusEventArgs e)
        {
            SendButton.IsVisible = true;
            Resources["DescriptionWidth"] = ScreenWidth - 95;
        }

        private async void InitActionSheet()
        {
            Resources["IsLoading"] = true;
            WasteCauses = await MBoxApiCalls.GetElectricityWasteCauseList();
            Resources["IsLoading"] = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Resources["NotificationReplyType1_Title"] = App.CurrentTranslation["NotificationReplyType1_Title"];

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
            Resources["NotificationReply_KwhTitle"] = App.CurrentTranslation["NotificationReply_KwhTitle"];
            Resources["NotificationReply_KwhValue"] = NotificationModel.Kwh;
            Resources["NotificationReply_NotificationTitle"] = App.CurrentTranslation["NotificationReply_NotificationTitle"];
            Resources["NotificationReply_NotificationValue"] = NotificationModel.AlterDescription;

            Resources["NotificationReply_CauseButtonText"] = App.CurrentTranslation["NotificationReply_CauseButtonText"];
            Resources["NotificationReply_DescriptionPlaceholder"] = App.CurrentTranslation["NotificationReply_DescriptionPlaceholder"];
            Resources["NotificationReply_SendButtonText"] = App.CurrentTranslation["NotificationReply_SendButtonText"];
            Resources["NotificationReply_SubmitButtonText"] = App.CurrentTranslation["NotificationReply_AcknowledgeButtonText"];
            Resources["NotificationReply_CancelButtonText"] = App.CurrentTranslation["NotificationReply_CancelButtonText"];
        }

        public async void CauseClicked(object sender, EventArgs e)
        {
            if (WasteCauses.Count > 0)
            {
                string[] items = new string[WasteCauses.Count];

                for (int i = 0; i < WasteCauses.Count; i++)
                {
                    items[i] = WasteCauses[i].Material;
                }

                var action = await DisplayActionSheet(App.CurrentTranslation["NotificationReply_CauseASDescription"], App.CurrentTranslation["NotificationReply_CauseASCancel"], null, items);
                if (action != App.CurrentTranslation["NotificationReply_CauseASCancel"])
                {
                    CauseButton.Text = action;
                    CauseID = WasteCauses.Where(x => x.Material == action).FirstOrDefault().MID;

                    string wcDescription = string.Empty;
                    if (CauseID != 0) wcDescription = WasteCauses.Where(x => x.MID == CauseID).FirstOrDefault().DescCH;
                    if (string.IsNullOrEmpty(wcDescription))
                        Description.Focus();
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
            string wcDescription = string.Empty;
            if (CauseID != 0) wcDescription = WasteCauses.Where(x => x.MID == CauseID).FirstOrDefault().DescCH;
            if (Description.Text == null) Description.Text = string.Empty;

            if (CauseID == 0)
            {
                await DisplayAlert(App.CurrentTranslation["NotificationReplyType1_Title"], App.CurrentTranslation["NotificationReply_ErrorMsgChooseCause"], App.CurrentTranslation["Common_OK"]);
            }
            else if (string.IsNullOrEmpty(wcDescription) && string.IsNullOrEmpty(Description.Text.Trim()))
            {
                await DisplayAlert(App.CurrentTranslation["NotificationReplyType1_Title"], App.CurrentTranslation["NotificationReply_ErrorMsgInputDescription"], App.CurrentTranslation["Common_OK"]);
            }
            else
            {
                Resources["IsLoading"] = true;
                bool result = await MBoxApiCalls.ReplyElectricity(NotificationModel.ID, NotificationModel.ParentID, Description.Text.Trim(), CauseID);
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
                    await DisplayAlert(App.CurrentTranslation["NotificationReplyType1_Title"], App.CurrentTranslation["NotificationReply_ErrorMsgSubmitFailed"], App.CurrentTranslation["Common_OK"]);
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

            if (Description.IsFocused)
                Description.Unfocus();

            if (ScreenWidth != width || ScreenHeight != height)
            {
                ScreenWidth = width;
                ScreenHeight = height;

                Resources["ButtonWidth"] = (ScreenWidth - 24) / 2.0;
                Resources["ButtonLargeWidth"] = ScreenWidth - 20;

                if (SendButton.IsVisible)
                    Resources["DescriptionWidth"] = ScreenWidth - 95;
                else
                    Resources["DescriptionWidth"] = ScreenWidth - 30;
            }
        }
    }
}
