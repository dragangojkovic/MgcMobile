using MBoxMobile.Interfaces;
using MBoxMobile.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationReplyType7Page : ContentPage
    {
        double screenWidth = 0.0;
        double screenHeight = 0.0;
        NotificationModel NotificationModel;
        bool ShowReceivedNotification = false;

        public NotificationReplyType7Page(NotificationModel notificationModel, bool showReceived = false)
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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            switch(NotificationModel.DataType)
            {
                case 7:
                    Resources["NotificationReplyType7_Title"] = App.CurrentTranslation["NotificationReplyType7_07_Title"];
                    break;
                case 8:
                    Resources["NotificationReplyType7_Title"] = App.CurrentTranslation["NotificationReplyType7_08_Title"];
                    break;
                case 9:
                    Resources["NotificationReplyType7_Title"] = App.CurrentTranslation["NotificationReplyType7_09_Title"];
                    break;
                case 10:
                    Resources["NotificationReplyType7_Title"] = App.CurrentTranslation["NotificationReplyType7_10_Title"];
                    break;
                case 11:
                    Resources["NotificationReplyType7_Title"] = App.CurrentTranslation["NotificationReplyType7_11_Title"];
                    break;
            }            

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
            Resources["NotificationReply_NotificationTitle"] = App.CurrentTranslation["NotificationReply_NotificationTitle"];
            Resources["NotificationReply_NotificationValue"] = NotificationModel.AlterDescription;
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

            Resources["NotificationReply_AknowledgeTimeTitle"] = App.CurrentTranslation["NotificationReply_AknowledgeTimeTitle"];
            Resources["NotificationReply_AknowledgeTimeValue"] = NotificationModel.DesDateLocal;
            Resources["NotificationReply_AknowledgedByTitle"] = App.CurrentTranslation["NotificationReply_AknowledgedByTitle"];
            Resources["NotificationReply_AknowledgedByValue"] = NotificationModel.DesPerson;
            Resources["NotificationReply_SolutionTimeTitle"] = App.CurrentTranslation["NotificationReply_SolutionTimeTitle"];
            Resources["NotificationReply_SolutionTimeValue"] = NotificationModel.SolutionDateLocal;
            Resources["NotificationReply_SolutionByTitle"] = App.CurrentTranslation["NotificationReply_SolutionByTitle"];
            Resources["NotificationReply_SolutionByValue"] = NotificationModel.SoluPerson;
            Resources["NotificationReply_ApprovalTimeTitle"] = App.CurrentTranslation["NotificationReply_ApprovalTimeTitle"];
            Resources["NotificationReply_ApprovalTimeValue"] = NotificationModel.ApproveDateLocal;
            Resources["NotificationReply_ApprovalByTitle"] = App.CurrentTranslation["NotificationReply_ApprovalByTitle"];
            Resources["NotificationReply_ApprovalByValue"] = NotificationModel.ApprovePerson;
            Resources["NotificationReply_ReportTimeTitle"] = App.CurrentTranslation["NotificationReply_ReportTimeTitle"];
            Resources["NotificationReply_ReportTimeValue"] = NotificationModel.ReportDateLocal;
            Resources["NotificationReply_ReportedByTitle"] = App.CurrentTranslation["NotificationReply_ReportedByTitle"];
            Resources["NotificationReply_ReportedByValue"] = NotificationModel.ReportPerson;
            if (!string.IsNullOrEmpty(NotificationModel.Report))
            {
                Resources["NotificationReply_ReportTitle"] = App.CurrentTranslation["NotificationReply_ReportTitle"];
                Resources["NotificationReply_ReportValue"] = NotificationModel.Report;
            }
            else
            {
                ReportTitle.IsVisible = false;
                ReportValue.IsVisible = false;
            }

            Resources["NotificationReply_CancelButtonText"] = App.CurrentTranslation["NotificationReply_CancelButtonText"];
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
