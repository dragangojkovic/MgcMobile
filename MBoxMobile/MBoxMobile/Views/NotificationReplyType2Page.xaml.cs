using MBoxMobile.Interfaces;
using MBoxMobile.Models;
using MBoxMobile.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationReplyType2Page : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;
        NotificationModel NotificationModel;
        bool ShowReceivedNotification = false;

        public NotificationReplyType2Page(NotificationModel notificationModel, bool showReceived = false)
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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Resources["NotificationReplyType2_Title"] = App.CurrentTranslation["NotificationReplyType2_Title"];

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

            Resources["NotificationReply_DescriptionPlaceholder"] = App.CurrentTranslation["NotificationReply_DescriptionPlaceholder"];
            Resources["NotificationReply_SubmitButtonText"] = App.CurrentTranslation["NotificationReply_AcknowledgeButtonText"];
            Resources["NotificationReply_CancelButtonText"] = App.CurrentTranslation["NotificationReply_CancelButtonText"];
        }

        public async void SubmitClicked(object sender, EventArgs e)
        {
            if (Description.Text == null) Description.Text = string.Empty;

            if (NotificationModel.NeedDesc && string.IsNullOrEmpty(Description.Text.Trim()))
            {
                await DisplayAlert(App.CurrentTranslation["NotificationReplyType2_Title"], App.CurrentTranslation["NotificationReply_ErrorMsgInputDescription"], App.CurrentTranslation["Common_OK"]);
            }
            else
            {
                Resources["IsLoading"] = true;
                bool result = await MBoxApiCalls.ReplyAcknowledge(NotificationModel.ID, NotificationModel.ParentID, Description.Text.Trim());
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
                    await DisplayAlert(App.CurrentTranslation["NotificationReplyType2_Title"], App.CurrentTranslation["NotificationReply_ErrorMsgSubmitFailed"], App.CurrentTranslation["Common_OK"]);
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
