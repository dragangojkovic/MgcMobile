using MBoxMobile.Interfaces;
using MBoxMobile.Models;
using MBoxMobile.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationReplyType6Page : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;
        NotificationModel NotificationModel;
        bool ShowReceivedNotification = false;

        public NotificationReplyType6Page(NotificationModel notificationModel, bool showReceived = false)
        {
            InitializeComponent();

            App.IsNotificationHandling = true;
            NotificationModel = notificationModel;
            ShowReceivedNotification = showReceived;

            screenWidth = DependencyService.Get<IDisplay>().Width;
            screenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["IsLoading"] = false;
            Resources["PageContentMinHeight"] = screenHeight - 120.0;
            Resources["NotificationContentMinHeight"] = screenHeight - 200.0;
            Resources["ButtonWidth"] = (screenWidth - 24) / 2.0;
            Resources["ButtonLargeWidth"] = screenWidth - 20;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Resources["NotificationReplyType6_Title"] = App.CurrentTranslation["NotificationReplyType6_Title"];

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
            if (NotificationModel.Kwh != null)
            {
                Resources["NotificationReply_KwhTitle"] = App.CurrentTranslation["NotificationReply_KwhTitle"];
                Resources["NotificationReply_KwhValue"] = NotificationModel.Kwh;
            }
            else
            {
                KwhTitle.IsVisible = false;
                KwhValue.IsVisible = false;
            }
            if (!string.IsNullOrEmpty(NotificationModel.ElecCause))
            {
                Resources["NotificationReply_WasteCauseTitle"] = App.CurrentTranslation["NotificationReply_WasteCauseTitle"];
                Resources["NotificationReply_WasteCauseValue"] = NotificationModel.ElecCause;
            }
            else
            {
                WasteCauseTitle.IsVisible = false;
                WasteCauseValue.IsVisible = false;
            }
            Resources["NotificationReply_ReportedTitle"] = App.CurrentTranslation["NotificationReply_ReportedTitle"];
            Resources["NotificationReply_ReportedValue"] = NotificationModel.NeedReport.ToString();
            if (!string.IsNullOrEmpty(NotificationModel.AlterDescription))
            {
                Resources["NotificationReply_NotificationTitle"] = App.CurrentTranslation["NotificationReply_NotificationTitle"];
                Resources["NotificationReply_NotificationValue"] = NotificationModel.AlterDescription;
            }
            else
            {
                NotificationTitle.IsVisible = false;
                NotificationValue.IsVisible = false;
            }
            if (!string.IsNullOrEmpty(NotificationModel.AlterCause))
            {
                Resources["NotificationReply_CauseTitle"] = App.CurrentTranslation["NotificationReply_CauseTitle"];
                Resources["NotificationReply_CauseValue"] = NotificationModel.AlterCause;
            }
            else
            {
                CauseTitle.IsVisible = false;
                CauseValue.IsVisible = false;
            }
            Resources["NotificationReply_SolutionTitle"] = App.CurrentTranslation["NotificationReply_SolutionTitle"];
            Resources["NotificationReply_SolutionValue"] = NotificationModel.Solution;
            if (!string.IsNullOrEmpty(NotificationModel.Description))
            {
                Resources["NotificationReply_DescriptionTitle"] = App.CurrentTranslation["NotificationReply_DescriptionTitle"];
                Resources["NotificationReply_DescriptionValue"] = NotificationModel.Description;
            }
            else
            {
                DescriptionTitle.IsVisible = false;
                DescriptionValue.IsVisible = false;
            }

            Resources["NotificationReply_ReportPlaceholder"] = App.CurrentTranslation["NotificationReply_ReportPlaceholder"];
            Resources["NotificationReply_SaveButtonText"] = App.CurrentTranslation["NotificationReply_SaveButtonText"];
            Resources["NotificationReply_SaveAndCloseButtonText"] = App.CurrentTranslation["NotificationReply_SaveAndCloseButtonText"];
            Resources["NotificationReply_CancelButtonText"] = App.CurrentTranslation["NotificationReply_CancelButtonText"];
        }

        public async void SaveClicked(object sender, EventArgs e)
        {
            Resources["IsLoading"] = true;
            bool result = await MBoxApiCalls.ReplyReport(NotificationModel.ID, NotificationModel.ParentID, Report.Text.Trim());
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
                await DisplayAlert(App.CurrentTranslation["NotificationReplyType6_Title"], App.CurrentTranslation["NotificationReply_ErrorMsgSubmitFailed"], App.CurrentTranslation["Common_OK"]);
        }

        public async void SaveAndCloseClicked(object sender, EventArgs e)
        {
            Resources["IsLoading"] = true;
            bool result = await MBoxApiCalls.ReplyReportAndRemove(NotificationModel.ID, NotificationModel.ParentID, Report.Text.Trim());
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
                await DisplayAlert(App.CurrentTranslation["NotificationReplyType6_Title"], App.CurrentTranslation["NotificationReply_ErrorMsgSubmitFailed"], App.CurrentTranslation["Common_OK"]);
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
