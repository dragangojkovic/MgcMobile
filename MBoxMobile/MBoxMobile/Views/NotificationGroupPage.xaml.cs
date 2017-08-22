using MBoxMobile.CustomControls;
using MBoxMobile.Helpers;
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
    public partial class NotificationGroupPage : ContentPage
    {
        double ScreenWidth = 0.0;
        double ScreenHeight = 0.0;
        bool AreTablesPopulated = false;

        List<NotificationModel> NotificationList;
        List<NotificationGroupInfo> SubGroupsInfoList;
        List<SelectedCheckboxes> SelectedCheckBoxList;

        int selectedNotificationID = 0;
        int removedNotificationID = 0;
        List<int> removedBatchNotificationIds = new List<int>();

        public NotificationGroupPage(string notificationGroup, List<NotificationModel> notifications)
        {
            InitializeComponent();

            ScreenWidth = DependencyService.Get<IDisplay>().Width;
            ScreenHeight = DependencyService.Get<IDisplay>().Height;

            SetUpLayout();

            Resources["NotificationGroup_Title"] = notificationGroup;
            NotificationList = notifications;

            MessagingCenter.Subscribe<string>(this, "NotificationPopupClosedWithAction", (sender) =>
            {
                App.IsNotificationHandling = false;
                App.ShouldReloadNotifications = true;

                AreTablesPopulated = false;
                removedNotificationID = selectedNotificationID;
                //if (Navigation.ModalStack.Count > 0)
                //    await Navigation.PopModalAsync();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Resources["Common_Close"] = App.CurrentTranslation["Common_Close"];

            if (!AreTablesPopulated)
            {
                Resources["IsLoading"] = true;
                SubGroupsInfoList = GetSubGroupsInfoList();
                NotificationGroupsAccordion.AccordionWidth = ScreenWidth - 30;
                NotificationGroupsAccordion.AccordionHeight = 55.0;
                NotificationGroupsAccordion.DataSource = GetAccordionData();
                NotificationGroupsAccordion.DataBind();
                Resources["IsLoading"] = false;

                AreTablesPopulated = true;
            }
        }

        private void SetUpLayout()
        {
            Resources["ContentMinHeight"] = ScreenHeight - 60.0;
        }

        public async void CloseClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private List<AccordionSource> GetAccordionData()
        {
            var result = new List<AccordionSource>();

            foreach(NotificationGroupInfo ngi in SubGroupsInfoList)
            {
                double contentHeight;
                StackLayout content = new StackLayout();
                content.Children.Add(CreateThreeButtons(ngi));
                content.Children.Add(CreateAndPopulateWebViews(ngi.GroupName, out contentHeight));

                var asCurrent = new AccordionSource()
                {
                    HeaderText = string.Format("{0} ({1})", ngi.GroupName, ngi.GroupItemCount), // App.CurrentTranslation["????"],
                    ContentItems = content,
                    ContentHeight = -1 // contentHeight + 20
                };
                if (ScreenWidth <= 360) asCurrent.HeaderFontSize = 14;
                result.Add(asCurrent);
            }

            return result;
        }

        private View CreateThreeButtons(NotificationGroupInfo ngi)
        {
            StackLayout result = new StackLayout();

            if (ngi.TableType != TableLayoutType.NoCheckboxes)
            {
                result.Orientation = StackOrientation.Horizontal;

                var buttonSelectAll = new Button()
                {
                    ClassId = ngi.GroupName,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = (Color)Application.Current.Resources["BlueMedium"],
                    BorderColor = Color.Black,
                    BorderWidth = 1,
                    WidthRequest = 60,
                    HeightRequest = 40,
                    Text = App.CurrentTranslation["Notification_SelectAll"],
                    FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button))
                };
                buttonSelectAll.Clicked += SelectAllButton_Clicked;

                var buttonDeselectAll = new Button()
                {
                    ClassId = ngi.GroupName,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = (Color)Application.Current.Resources["BlueMedium"],
                    BorderColor = Color.Black,
                    BorderWidth = 1,
                    WidthRequest = 60,
                    HeightRequest = 40,
                    Text = App.CurrentTranslation["Notification_DeselectAll"],
                    FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button))
                };
                buttonDeselectAll.Clicked += DeselectAllButton_Clicked;

                var buttonSaveAll = new Button()
                {
                    ClassId = ngi.GroupName,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = (Color)Application.Current.Resources["BlueMedium"],
                    BorderColor = Color.Black,
                    BorderWidth = 1,
                    WidthRequest = 60,
                    HeightRequest = 40,
                    FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button))
                };

                if (ngi.TableType == TableLayoutType.AcknowledgeCheckboxes)
                {
                    buttonSaveAll.Text = App.CurrentTranslation["Notification_AcknowledgeAll"];
                    buttonSaveAll.Clicked += AcknowledgeAllButton_Clicked;
                }
                else
                {
                    buttonSaveAll.Text = App.CurrentTranslation["Notification_SaveAll"];
                    buttonSaveAll.Clicked += SaveAllButton_Clicked;
                }

                result.Children.Add(buttonSelectAll);
                result.Children.Add(buttonDeselectAll);
                result.Children.Add(buttonSaveAll);
            }

            return result;
        }

        private void SelectAllButton_Clicked(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            UpdateWebView(bt.ClassId, true);
        }

        private void DeselectAllButton_Clicked(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            UpdateWebView(bt.ClassId, false);
        }

        private void AcknowledgeAllButton_Clicked(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            PrepareSelectedCheckboxList(bt.ClassId, TableLayoutType.AcknowledgeCheckboxes);
            PrepareAndCallBatchApi(bt.ClassId, TableLayoutType.AcknowledgeCheckboxes);
        }

        private void SaveAllButton_Clicked(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            PrepareSelectedCheckboxList(bt.ClassId, TableLayoutType.SaveCheckboxes);
            PrepareAndCallBatchApi(bt.ClassId, TableLayoutType.SaveCheckboxes);
        }

        private void UpdateWebView(string subGroupName, bool? selectedAll, int notificationId = 0, string option = "", bool? state = null)
        {
            AccordionSource asource = NotificationGroupsAccordion.DataSource.Where(x => x.HeaderText.Contains(subGroupName)).FirstOrDefault();
            StackLayout stack = asource.ContentItems as StackLayout;
            WebView wv = (stack.Children[1] as StackLayout).Children[0] as WebView;

            double viewHeight = 10;
            const double WV_ROW_Height = 32;
            const double WV_ROW_Checkbox_Height = 34;
            const double WV_HEADER_Height = 30;
            const double WV_BIG_HEADER_Height = 49;

            List<NotificationModel> notifs = NotificationList.Where(x => x.AlterDescription == subGroupName).ToList();

            if (notificationId > 0)
            {
                NotificationModel nm = notifs.Where(x => x.ID == notificationId).FirstOrDefault();
                switch (option)
                {
                    case "ack":
                        nm.Acknowledge = (bool)state;
                        break;
                    case "app":
                        nm.Approved = (bool)state;
                        break;
                    case "rpt":
                        nm.NeedReport = (bool)state;
                        break;
                }
            }
                        
            string htmlHtmlDetails = string.Empty;
            int subTableCount = 0;

            //4th level of grouping - there can be more than one table in sub-group
            List<int> subSubGroups = notifs.Select(x => x.DataType).Distinct().ToList();

            // if DataType 2 exist, move it on first position, because of select / deselect buttons
            bool type2Exists = subSubGroups.Contains(2);
            if (type2Exists)
            {
                subSubGroups.Remove(2);
                subSubGroups.Insert(0, 2);
            }

            foreach (int tableType in subSubGroups)
            {
                if (selectedAll != null)
                {
                    if (tableType == 2 || tableType == 3)
                    {
                        foreach (NotificationModel nm in notifs)
                        {
                            nm.Acknowledge = (bool)selectedAll;
                        }
                    }
                    if (tableType == 5)
                    {
                        foreach (NotificationModel nm in notifs)
                        {
                            nm.Approved = (bool)selectedAll;
                        }
                    }
                }

                subTableCount++;

                // 1. get notifs for this table
                List<NotificationModel> data = notifs.Where(x => x.DataType == tableType).ToList();

                // 2. check what table look we should use here and populate
                string htmlButtons = string.Empty;
                string htmlHeader = string.Empty;
                string htmlContent = string.Empty;
                switch (tableType.ToString())
                {
                    case "2":
                        htmlHeader = HtmlTableSupport.NotificationDataType2_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType2_TableContent(data);
                        break;
                    case "3":   // can be found with type 2 in the same subgroup
                        htmlHeader = HtmlTableSupport.NotificationDataType3_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType3_TableContent(data);
                        break;
                    case "5":
                        htmlHeader = HtmlTableSupport.NotificationDataType5_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType5_TableContent(data);
                        break;
                }

                htmlHtmlDetails += HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeader, htmlContent) + "";
                if (tableType > 3)
                    viewHeight += WV_BIG_HEADER_Height;
                else
                    viewHeight += WV_HEADER_Height;

                if (tableType == 2 || tableType > 5)
                    viewHeight += data.Count() * WV_ROW_Checkbox_Height + 11;
                else
                    viewHeight += data.Count() * WV_ROW_Height + 11;
            }

            wv.Source = new HtmlWebViewSource { Html = htmlHtmlDetails };
            wv.HeightRequest = viewHeight;
        }

        private void PrepareSelectedCheckboxList(string subGroupName, TableLayoutType type)
        {
            SelectedCheckBoxList = new List<SelectedCheckboxes>();

            AccordionSource asource = NotificationGroupsAccordion.DataSource.Where(x => x.HeaderText.Contains(subGroupName)).FirstOrDefault();
            StackLayout stack = asource.ContentItems as StackLayout;
            WebView wv = (stack.Children[1] as StackLayout).Children[0] as WebView;

            HtmlWebViewSource currentSource = wv.Source as HtmlWebViewSource;
            string fullHtml = currentSource.Html;
            fullHtml = fullHtml.Replace("\r\n", "");
            fullHtml = fullHtml.Replace("\t", "");
            fullHtml = fullHtml.Trim();
            int start = fullHtml.IndexOf("<tbody class='mbox'>") + "<tbody class='mbox'>".Length;
            int end = fullHtml.IndexOf("</tbody>");
            string htmlForParse = fullHtml.Substring(start, end - start);
            htmlForParse = htmlForParse.Trim();

            string[] tableRows = htmlForParse.Split(new string[] { "</tr>" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string row in tableRows)
            {
                string[] tableData = row.Split(new string[] { "</td>" }, StringSplitOptions.RemoveEmptyEntries);
                SelectedCheckboxes scbox = new SelectedCheckboxes();
                string currentTD = tableData[0];
                string[] idHtml = currentTD.Split(new string[] { ">" }, StringSplitOptions.None);
                scbox.Id = idHtml[2];

                if (type == TableLayoutType.AcknowledgeCheckboxes)
                {
                    scbox.Acknowledge = tableData[4].Contains("checked");
                    scbox.Report = false;
                }

                if (type == TableLayoutType.SaveCheckboxes)
                {
                    scbox.Acknowledge = tableData[3].Contains("checked");
                    scbox.Report = tableData[4].Contains("checked");
                }

                SelectedCheckBoxList.Add(scbox);
            }
        }

        private async void PrepareAndCallBatchApi(string subGroupName, TableLayoutType type)
        {
            bool result = false;
            if (type == TableLayoutType.AcknowledgeCheckboxes)
            {
                string notificationIDList = string.Empty;
                string notificationParentIDList = string.Empty;

                Resources["IsLoading"] = true;
                foreach (SelectedCheckboxes scbox in SelectedCheckBoxList)
                {
                    if (scbox.Acknowledge)
                    {
                        int notifID = 0;
                        int.TryParse(scbox.Id, out notifID);
                        string sParentId = NotificationList.Where(x => x.ID == notifID).FirstOrDefault().ParentID.ToString();

                        if (scbox.Id != string.Empty && sParentId != string.Empty)
                            notificationParentIDList += "," + sParentId;
                        else
                            notificationIDList += "," + scbox.Id;

                        removedBatchNotificationIds.Add(notifID);
                    }
                }
                if (notificationIDList != string.Empty) notificationIDList = notificationIDList.Substring(1);
                if (notificationParentIDList != string.Empty) notificationParentIDList = notificationParentIDList.Substring(1);

                if (!(notificationIDList == string.Empty && notificationParentIDList == string.Empty))
                {
                    result = await MBoxApiCalls.BatchAcknowledge(notificationIDList, notificationParentIDList);
                    Resources["IsLoading"] = false;

                    if (!result)
                        await DisplayAlert(App.CurrentTranslation["NotificationReply_AcknowledgeButtonText"], App.CurrentTranslation["NotificationReply_ErrorMsgSubmitFailed"], App.CurrentTranslation["Common_OK"]);
                }
                else
                    Resources["IsLoading"] = false;
            }

            if (type == TableLayoutType.SaveCheckboxes)
            {
                string notificationIDListApprove = string.Empty;
                string notificationParentIDListApprove = string.Empty;
                string notificationIDListReport = string.Empty;
                string notificationParentIDListReport = string.Empty;

                Resources["IsLoading"] = true;
                foreach (SelectedCheckboxes scbox in SelectedCheckBoxList)
                {
                    int notifID = 0;
                    int.TryParse(scbox.Id, out notifID);
                    string sParentId = NotificationList.Where(x => x.ID == notifID).FirstOrDefault().ParentID.ToString();

                    if (scbox.Acknowledge)
                    {
                        if (scbox.Id != string.Empty && sParentId != string.Empty)
                            notificationParentIDListApprove += "," + sParentId;
                        else
                            notificationIDListApprove += "," + scbox.Id;

                        removedBatchNotificationIds.Add(notifID);
                    }
                    if (scbox.Report)
                    {
                        if (scbox.Id != string.Empty && sParentId != string.Empty)
                            notificationParentIDListReport += "," + sParentId;
                        else
                            notificationIDListReport += "," + scbox.Id;
                    }
                }
                if (notificationIDListApprove != string.Empty) notificationIDListApprove = notificationIDListApprove.Substring(1);
                if (notificationParentIDListApprove != string.Empty) notificationParentIDListApprove = notificationParentIDListApprove.Substring(1);
                if (notificationIDListReport != string.Empty) notificationIDListReport = notificationIDListReport.Substring(1);
                if (notificationParentIDListReport != string.Empty) notificationParentIDListReport = notificationParentIDListReport.Substring(1);

                result = true;
                if (!(notificationIDListApprove == string.Empty && notificationParentIDListApprove == string.Empty))
                {
                    result = await MBoxApiCalls.BatchApprove(notificationIDListApprove, notificationParentIDListApprove);
                }
                if (!result)
                {
                    Resources["IsLoading"] = false;
                    await DisplayAlert(App.CurrentTranslation["NotificationReply_ApproveButtonText"], App.CurrentTranslation["NotificationReply_ErrorMsgSubmitFailed"], App.CurrentTranslation["Common_OK"]);
                }
                else
                {
                    if (!(notificationIDListReport == string.Empty && notificationParentIDListReport == string.Empty))
                    {
                        result = await MBoxApiCalls.BatchNeedReport(notificationIDListApprove, notificationParentIDListApprove);
                    }
                    Resources["IsLoading"] = false;
                    if (!result)
                    {
                        await DisplayAlert(App.CurrentTranslation["NotificationReply_ReportButtonText"], App.CurrentTranslation["NotificationReply_ErrorMsgSubmitFailed"], App.CurrentTranslation["Common_OK"]);
                    }
                }   
            }

            if (result)
            {
                if (removedBatchNotificationIds.Count > 0)
                {
                    foreach (int id in removedBatchNotificationIds)
                    {
                        var item = NotificationList.FirstOrDefault(x => x.ID == id);
                        if (item != null) NotificationList.Remove(item);
                    }

                    removedBatchNotificationIds = new List<int>();

                    UpdateWebView(subGroupName, null);
                }

                MessagingCenter.Send<string>("NotificationHandler", "NotificationPopupClosedWithAction");
            }
        }

        private View CreateAndPopulateWebViews(string subGroupName, out double viewHeight)
        {
            viewHeight = 10;
            const double WV_ROW_Height = 32;
            const double WV_ROW_Checkbox_Height = 34;
            const double WV_HEADER_Height = 30;
            const double WV_BIG_HEADER_Height = 49;
            StackLayout result = new StackLayout();
            List<NotificationModel> notifs = NotificationList.Where(x => x.AlterDescription == subGroupName).ToList();

            // 0. create new table (vw)
            WebView wv = new WebView();
            wv.WidthRequest = ScreenWidth - 35;
            wv.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            wv.VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            wv.Margin = new Thickness(0, 0, 0, 5);
            wv.Navigating += (s, e) =>
            {
                if (e.Url != string.Empty)
                {
                    string sParameters = e.Url.Split('=')[1];
                    string[] parameterArray = sParameters.Split('&');
                    selectedNotificationID = int.Parse(parameterArray[0]);
                    if (parameterArray.Count() == 3)
                    {
                        string option = parameterArray[1];
                        bool state = bool.Parse(parameterArray[2]);

                        UpdateWebView(subGroupName, null, selectedNotificationID, option, state);
                    }
                    else
                    {
                        // call pop-up regarding notification type
                        NotificationModel currentNotification = NotificationList.Where(x => x.ID == selectedNotificationID).FirstOrDefault();
                        if (currentNotification.Popup >= 1 && currentNotification.Popup <= 7)
                        {
                            switch (currentNotification.Popup)
                            {
                                case 1:
                                    Navigation.PushModalAsync(new NotificationReplyType1Page(currentNotification));
                                    break;
                                case 2:
                                    Navigation.PushModalAsync(new NotificationReplyType2Page(currentNotification));
                                    break;
                                case 3:
                                    Navigation.PushModalAsync(new NotificationReplyType3Page(currentNotification));
                                    break;
                                case 4:
                                    Navigation.PushModalAsync(new NotificationReplyType4Page(currentNotification));
                                    break;
                                case 5:
                                    Navigation.PushModalAsync(new NotificationReplyType5Page(currentNotification));
                                    break;
                                case 6:
                                    Navigation.PushModalAsync(new NotificationReplyType6Page(currentNotification));
                                    break;
                                case 7:
                                    Navigation.PushModalAsync(new NotificationReplyType7Page(currentNotification));
                                    break;
                            }
                        }
                    }
                }
                e.Cancel = true;
            };
            string htmlHtmlDetails = string.Empty;
            int subTableCount = 0;

            //4th level of grouping - there can be more than one table in sub-group
            List<int> subSubGroups = notifs.Select(x => x.DataType).Distinct().ToList();

            // if DataType 2 exist, move it on first position, because of select / deselect buttons
            bool type2Exists = subSubGroups.Contains(2);
            if (type2Exists)
            {
                subSubGroups.Remove(2);
                subSubGroups.Insert(0, 2);
            }

            foreach (int tableType in subSubGroups)
            {
                subTableCount++;

                // 1. get notifs for this table
                List<NotificationModel> data = notifs.Where(x => x.DataType == tableType).ToList();

                // 2. check what table look we should use here and populate
                string htmlButtons = string.Empty;
                string htmlHeader = string.Empty;
                string htmlContent = string.Empty;
                switch (tableType.ToString())
                {
                    case "1":
                        htmlHeader = HtmlTableSupport.NotificationDataType1_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType1_TableContent(data);
                        break;
                    case "2":
                        htmlHeader = HtmlTableSupport.NotificationDataType2_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType2_TableContent(data);
                        break;
                    case "3":
                        htmlHeader = HtmlTableSupport.NotificationDataType3_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType3_TableContent(data);
                        break;
                    case "4":
                        htmlHeader = HtmlTableSupport.NotificationDataType4_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType4_TableContent(data);
                        break;
                    case "5":
                        htmlHeader = HtmlTableSupport.NotificationDataType5_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType5_TableContent(data);
                        break;
                    case "6":
                        htmlHeader = HtmlTableSupport.NotificationDataType6_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType6_TableContent(data);
                        break;
                    case "7":
                        htmlHeader = HtmlTableSupport.NotificationDataType7_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType7_TableContent(data);
                        break;
                    case "8":
                        htmlHeader = HtmlTableSupport.NotificationDataType8_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType8_TableContent(data);
                        break;
                    case "9":
                        htmlHeader = HtmlTableSupport.NotificationDataType9_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType9_TableContent(data);
                        break;
                    case "10":
                        htmlHeader = HtmlTableSupport.NotificationDataType10_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType10_TableContent(data);
                        break;
                    case "11":
                        htmlHeader = HtmlTableSupport.NotificationDataType11_TableHeader();
                        htmlContent = HtmlTableSupport.NotificationDataType11_TableContent(data);
                        break;
                }

                htmlHtmlDetails += HtmlTableSupport.InsertHeaderAndBodyToHtmlTable(htmlHeader, htmlContent) + "";
                if (tableType > 3)
                    viewHeight += WV_BIG_HEADER_Height;
                else
                    viewHeight += WV_HEADER_Height;

                if (tableType == 2 || tableType > 5)
                    viewHeight += data.Count() * WV_ROW_Checkbox_Height + 11;
                else
                    viewHeight += data.Count() * WV_ROW_Height + 11;
            }

            wv.Source = new HtmlWebViewSource { Html = htmlHtmlDetails };
            wv.HeightRequest = viewHeight;

            // 3. add to View
            result.Children.Add(wv);

            return result;
        }

        private List<NotificationGroupInfo> GetSubGroupsInfoList()
        {
            List<NotificationGroupInfo> descriptions = new List<NotificationGroupInfo>();

            // remove handled notification, if exists such
            if (removedNotificationID > 0)
            {
                var item = NotificationList.FirstOrDefault(x => x.ID == removedNotificationID);
                if (item != null) NotificationList.Remove(item);
                removedNotificationID = 0;
            }

            foreach (NotificationModel nm in NotificationList)
            {
                string description = nm.AlterDescription;
                if (descriptions.Count == 0 || descriptions.Where(d => d.GroupName == description).Count() == 0)
                {
                    TableLayoutType tableType = TableLayoutType.NoCheckboxes;
                    if (nm.DataType == 2) tableType = TableLayoutType.AcknowledgeCheckboxes;
                    if (nm.DataType == 5) tableType = TableLayoutType.SaveCheckboxes;

                    int notifCount = NotificationList.Where(x => x.AlterDescription == description).Count();
                    descriptions.Add(new NotificationGroupInfo() { GroupName = description, GroupItemCount = notifCount, TableType = tableType });
                }
            }

            // for the case when one sub-group have elements with same AlterDescription but different DataType and one DataType == 2
            foreach (NotificationGroupInfo ngi in descriptions)
            {
                if (ngi.TableType == TableLayoutType.NoCheckboxes)
                {
                    int type2Count = NotificationList.Where(x => x.AlterDescription == ngi.GroupName && x.DataType == 2).Count();
                    if (type2Count > 0) ngi.TableType = TableLayoutType.AcknowledgeCheckboxes;
                }
            }
            
            return descriptions;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (ScreenWidth != width || ScreenHeight != height)
            {
                ScreenWidth = width;
                ScreenHeight = height;
                SetUpLayout();

                NotificationGroupsAccordion.AccordionWidth = width - 30;
                NotificationGroupsAccordion.UpdateLayout();
            }
        }
    }
}
