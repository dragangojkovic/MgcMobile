using MBoxMobile.Interfaces;
using MBoxMobile.Models;
using MBoxMobile.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationReplyType5Page : ContentPage
    {
        double ScreenWidth = 0.0;
        double ScreenHeight = 0.0;
        NotificationModel NotificationModel;
        bool ShowReceivedNotification = false;

        public NotificationReplyType5Page(NotificationModel notificationModel, bool showReceived = false)
        {
            InitializeComponent();

            App.IsNotificationHandling = true;
            NotificationModel = notificationModel;
            ShowReceivedNotification = showReceived;

            ScreenWidth = DependencyService.Get<IDisplay>().Width;
            ScreenHeight = DependencyService.Get<IDisplay>().Height;

            Resources["IsLoading"] = false;
            Resources["PageContentMinHeight"] = ScreenHeight - 120.0;
            Resources["NotificationContentMinHeight"] = ScreenHeight - 200.0;
            Resources["ButtonWidth"] = (ScreenWidth - 24) / 2.0;
            Resources["ButtonLargeWidth"] = ScreenWidth - 20;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Resources["NotificationReplyType5_Title"] = App.CurrentTranslation["NotificationReplyType5_Title"];

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
            if (!string.IsNullOrEmpty(NotificationModel.Solution))
            {
                Resources["NotificationReply_SolutionTitle"] = App.CurrentTranslation["NotificationReply_SolutionTitle"];
                Resources["NotificationReply_SolutionValue"] = NotificationModel.Solution;
            }
            else
            {
                SolutionTitle.IsVisible = false;
                SolutionValue.IsVisible = false;
            }
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

            Resources["NotificationReply_CauseButtonText"] = App.CurrentTranslation["NotificationReply_CauseButtonText"];
            Resources["NotificationReply_DescriptionPlaceholder"] = App.CurrentTranslation["NotificationReply_DescriptionPlaceholder"];
            Resources["NotificationReply_ApproveButtonText"] = App.CurrentTranslation["NotificationReply_ApproveButtonText"];
            Resources["NotificationReply_ApproveReportButtonText"] = App.CurrentTranslation["NotificationReply_ApproveReportButtonText"];
            Resources["NotificationReply_SubmitButtonText"] = App.CurrentTranslation["NotificationReply_ReportButtonText"];
            Resources["NotificationReply_CancelButtonText"] = App.CurrentTranslation["NotificationReply_CancelButtonText"];
        }

        private static int CalculateNewDataType(int? alterReply)
        {
            int result = 0;
            if (alterReply != null)
            {
                switch (alterReply)
                {
                    case 6806:
                        result = 7;
                        break;
                    case 6551:
                        result = 8;
                        break;
                    case 6552:
                        result = 9;
                        break;
                    case 6553:
                        result = 10;
                        break;
                    case 6559:
                        result = 11;
                        break;
                }
            }

            return result;
        }

        public async void ApproveClicked(object sender, EventArgs e)
        {
            Resources["IsLoading"] = true;
            bool result = await MBoxApiCalls.ReplyApprove(NotificationModel.ID, NotificationModel.ParentID, CalculateNewDataType(NotificationModel.AlterReply));
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
                await DisplayAlert(App.CurrentTranslation["NotificationReplyType5_Title"], App.CurrentTranslation["NotificationReply_ErrorMsgSubmitFailed"], App.CurrentTranslation["Common_OK"]);
        }

        public async void ApproveReportClicked(object sender, EventArgs e)
        {
            Resources["IsLoading"] = true;
            bool result = await MBoxApiCalls.ReplyApproveAndReport(NotificationModel.ID, NotificationModel.ParentID, CalculateNewDataType(NotificationModel.AlterReply));
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
                await DisplayAlert(App.CurrentTranslation["NotificationReplyType5_Title"], App.CurrentTranslation["NotificationReply_ErrorMsgSubmitFailed"], App.CurrentTranslation["Common_OK"]);
        }

        public async void ReportClicked(object sender, EventArgs e)
        {
            Resources["IsLoading"] = true;
            bool result = await MBoxApiCalls.ReplyNeedReport(NotificationModel.ID, NotificationModel.ParentID);
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
                await DisplayAlert(App.CurrentTranslation["NotificationReplyType5_Title"], App.CurrentTranslation["NotificationReply_ErrorMsgSubmitFailed"], App.CurrentTranslation["Common_OK"]);
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

            if (ScreenWidth != width || ScreenHeight != height)
            {
                ScreenWidth = width;
                ScreenHeight = height;

                Resources["ButtonWidth"] = (ScreenWidth - 24) / 2.0;
                Resources["ButtonLargeWidth"] = ScreenWidth - 20;
            }
        }
    }
}
