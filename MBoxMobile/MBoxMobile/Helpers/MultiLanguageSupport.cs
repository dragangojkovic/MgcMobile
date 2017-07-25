using System.Collections.Generic;

namespace MBoxMobile.Helpers
{
    public static class MultiLanguageSupport
    {
        public static Dictionary<int, string> GetLanguages()
        {
            Dictionary<int, string> languages = new Dictionary<int, string>();
            languages.Add(1, "English");
            languages.Add(2, "中文");
            //languages.Add(3, "Deutsch");
            //languages.Add(4, "Nederlands");
            //languages.Add(5, "Français");
            //languages.Add(6, "Español");
            //languages.Add(7, "Khmer");
            //languages.Add(8, "Bahasa");
            //languages.Add(9, "Português");
            //languages.Add(10, "LabelCheck");

            return languages;
        }

        public static Dictionary<string, string> GetTranslations(Languages language)
        {
            Dictionary<string, string> dict;

            switch(language)
            {
                case Languages.English:
                    dict = GetTranslation_English();
                    break;
                case Languages.Chinese:
                    dict = GetTranslation_Chinese();
                    break;
                case Languages.German:
                    dict = GetTranslation_German();
                    break;
                case Languages.Dutch:
                    dict = GetTranslation_Dutch();
                    break;
                case Languages.French:
                    dict = GetTranslation_French();
                    break;
                case Languages.Spanish:
                    dict = GetTranslation_Spanish();
                    break;
                case Languages.Khmer:
                    dict = GetTranslation_Khmer();
                    break;
                case Languages.Bahasa:
                    dict = GetTranslation_Bahasa();
                    break;
                case Languages.Portuguese:
                    dict = GetTranslation_Portuguese();
                    break;
                case Languages.LabelCheck:
                    dict = GetTranslation_LabelCheck();
                    break;
                default:
                    dict = GetTranslation_English();
                    break;
            }

            return dict;
        }

        #region Translations

        // Dictionary key has format PageName_ElementName

        private static Dictionary<string, string> GetTranslation_English()
        {            
            Dictionary<string, string> translation = new Dictionary<string, string>();

            //Common elements
            translation.Add("Common_Filter", "Select Filter");
            translation.Add("Common_FilterPersonalTitle", "Personal Filter");
            translation.Add("Common_FilterPersonalDescription", "Please choose a personal filter");
            translation.Add("Common_FilterTimeTitle", "Period Filter");
            translation.Add("Common_FilterTimeDescription", "Please choose a period filter");
            translation.Add("Common_FilterTimeToday", "Today");
            translation.Add("Common_FilterTimeLast24Hours", "Last 24 Hours");
            translation.Add("Common_FilterTimeYesterday", "Yesterday");
            translation.Add("Common_FilterTimeCurrentWeek", "Current Week");
            translation.Add("Common_FilterTimeLastWeek", "Last Week");
            translation.Add("Common_FilterTimeCurrentMonth", "Current Month");
            translation.Add("Common_FilterTimeLastMonth", "Last Month");
            translation.Add("Common_FilterTimeCurrentQuarter", "Current Quarter");
            translation.Add("Common_FilterTimeLastQuarter", "Last Quarter");
            translation.Add("Common_FilterTimeCurrentYear", "Current Year");
            translation.Add("Common_FilterCancel", "Cancel");
            translation.Add("Common_FilterNotificationTitle", "Notification Filter");
            translation.Add("Common_FilterNotificationDescription", "Please choose a notification filter");
            translation.Add("Common_FilterTimeLast48Hours", "Last 48 Hours");
            translation.Add("Common_FilterTimeLast72Hours", "Last 72 Hours");
            translation.Add("Common_FilterTimeLast7Days", "Last 7 Days");
            translation.Add("Common_FilterTimeLast14Days", "Last 14 Days");
            translation.Add("Common_FilterTimeLast30Days", "Last 30 Days");
            translation.Add("Common_FilterFilterOn", "Filter On");
            translation.Add("Common_FilterFilterOff", "Filter Off");
            translation.Add("Common_FilterAll", "All");
            translation.Add("Common_FilterOn", "On");
            translation.Add("Common_FilterOff", "Off");
            translation.Add("Common_FilterErrors", "Errors");
            translation.Add("Common_FilterHasWaste", "Has waste");
            translation.Add("Common_WorkingTimeOnly", "Working Time Only");
            translation.Add("Common_FilteredBy", "Filtered by: ");
            translation.Add("Common_FilterClear", "Clear");
            translation.Add("Common_ViewDetail", "View Detail");
            translation.Add("Common_Close", "Close");
            translation.Add("Common_OK", "OK");
            translation.Add("Common_ServerStatusCodeMsg", "Server returned status code ");
            translation.Add("Common_ErrorConnectionFailed", "Connection to the MBox server failed!");
            translation.Add("Common_ErrorMsgNoNetwork", "There is no network connection!");

            //Menu - Side view
            translation.Add("Menu_Home", "Home");
            translation.Add("Menu_Language", "Language");
            translation.Add("Menu_Logout", "Logout");
            translation.Add("Menu_Exit", "Exit");
            translation.Add("Menu_About", "About");

            //About
            translation.Add("About_Version", "Version:");
            translation.Add("About_Token", "Device token:");

            //Login page
            translation.Add("Login_Title", "Login");
            translation.Add("Login_SelectServer", "Select server");
            translation.Add("Login_Username", "Please input username");
            translation.Add("Login_Password", "Please input password");
            translation.Add("Login_RememberMe", "Remember me");
            translation.Add("Login_Login", "Login");
            translation.Add("Login_ForgotPassword", "Forgot password?");
            translation.Add("Login_ErrorUsername", "Username is required!");
            translation.Add("Login_ErrorPassword", "Password is required!");
            translation.Add("Login_ErrorPlatform", "Platform is required!");
            translation.Add("Login_ErrorDeviceToken", "Device token is required!");
            translation.Add("Login_ErrorInvalidLogin", "Invalid login!");
            translation.Add("Login_ErrorServer", "Server is required!");
            translation.Add("Login_ErrorInvalidServer", "Invalid server!");

            //ForgotPassword page
            translation.Add("Forgot_Title", "Forgot password?");
            translation.Add("Forgot_InfoText", "Registered email address");
            translation.Add("Forgot_Email", "Please input your email");
            translation.Add("Forgot_Send", "Request new password");

            //Main page
            translation.Add("Main_Uptime", "Uptime");
            translation.Add("Main_ElectricityUsage", "Electricity usage");
            translation.Add("Main_Production", "Production");
            translation.Add("Main_Notifications", "Notifications");
            translation.Add("Main_AuxiliaryEquipment", "Auxiliary equipment");

            //Language page
            translation.Add("Language_Title", "Language");
            translation.Add("Language_InfoText", "Select language from the list");
            translation.Add("Language_SelectLanguage", "Select language");
            translation.Add("Language_Update", "Update");
            translation.Add("Language_Cancel", "Cancel");
            translation.Add("Language_AlertMessage", "Nothing selected!");

            //Uptime page
            translation.Add("Uptime_Title", "Uptime");
            translation.Add("Uptime_Locations", "Locations");
            translation.Add("Uptime_Departments", "Departments");
            translation.Add("Uptime_SubDepartments", "Sub departments");
            translation.Add("Uptime_Equipment", "Equipment");
            translation.Add("Uptime_EquipmentGroup", "Equipment group");
            translation.Add("Uptime_AuxiliaryEquipment", "Auxiliary equipment");
            translation.Add("Uptime_LocationsTableEquipment", "Equipment (current status)");
            translation.Add("Uptime_Location", "Location");
            translation.Add("Uptime_Uptime", "Uptime");
            translation.Add("Uptime_On", "On");
            translation.Add("Uptime_Off", "Off");
            translation.Add("Uptime_Errors", "Errors");
            translation.Add("UptimeDetails_FilterLabel", "Filter:");
            translation.Add("UptimeDetails_Detail", "Equipment detail");
            translation.Add("UptimeDetails_AuxiliaryEquipment", "Auxiliary equipment detail");
            translation.Add("UptimeDetails_Name", "Name");
            translation.Add("UptimeDetails_Current", "Current");
            translation.Add("UptimeDetails_OffTime", "Off Time");
            translation.Add("UptimeDetails_Status", "Status");
            translation.Add("UptimeDetails_RunTime", "Time(DD:HH:MM)");
            translation.Add("UptimeDetails_Stops", "Stops");
            translation.Add("UptimeDetails_StopTime", "Time(DD:HH:MM)");
            translation.Add("UptimeDetails_Group", "Group");
            translation.Add("UptimeDetails_Department", "Department");
            translation.Add("UptimeDetails_SubDepartment", "Sub Department");
            translation.Add("UptimeDetails_Type", "Type");
            translation.Add("UptimeDetails_Remark", "Remark");
            translation.Add("UptimeDetails_SystemData", "System Data");

            //Electricity Usage
            translation.Add("ElectricityUsage_Title", "Electricity usage");
            translation.Add("ElectricityUsage_Locations", "Locations");
            translation.Add("ElectricityUsage_Areas", "Areas");
            translation.Add("ElectricityUsage_Departments", "Departments");
            translation.Add("ElectricityUsage_SubDepartments", "Sub departments");
            translation.Add("ElectricityUsage_ConsumingPower", "Equipment off but consuming power");
            translation.Add("ElectricityUsage_Current", "Current");
            translation.Add("ElectricityUsage_Total", "Total");
            translation.Add("ElectricityUsage_NoDetails", "No details");
            translation.Add("ElectricityUsage_Waste", "Waste");
            translation.Add("ElectricityUsage_Eff", "Eff.");
            translation.Add("ElectricityUsage_On", "On");
            translation.Add("ElectricityUsage_Off", "Off");
            translation.Add("ElectricityUsage_Errors", "Errors");
            translation.Add("ElectricityUsage_Group", "Group");
            translation.Add("ElectricityUsage_Status", "Status");
            translation.Add("ElectricityUsage_Location", "Location");
            translation.Add("ElectricityUsage_Department", "Department");
            translation.Add("ElectricityUsage_SubDepartment", "Sub Department");
            translation.Add("ElectricityUsage_SystemData", "System Data");

            //Production page
            translation.Add("Production_Title", "Production");
            translation.Add("Production_Equipment", "Equipment");
            translation.Add("Production_Eff", "Eff.");
            translation.Add("Production_On", "On");
            translation.Add("Production_Off", "Off");
            translation.Add("ProductionDetails_Current", "Current");
            translation.Add("ProductionDetails_OffTime", "Off time");
            translation.Add("ProductionDetails_EqGroup", "Equipment group");
            translation.Add("ProductionDetails_ProductionType", "Production type");
            translation.Add("ProductionDetails_Eff", "Eff.");
            translation.Add("ProductionDetails_Mould", "Mould");
            translation.Add("ProductionDetails_CycleTime", "Cycle time(s)");
            translation.Add("ProductionDetails_Status", "Status");
            translation.Add("ProductionDetails_RunTime", "Time (DD:HH:MM)");
            translation.Add("ProductionDetails_Stops", "Stops");
            translation.Add("ProductionDetails_StopTime", "Time (DD:HH:MM)");
            translation.Add("ProductionDetails_kWH", "kWH");
            translation.Add("ProductionDetails_Operator", "Operator");
            translation.Add("ProductionDetails_Location", "Location");
            translation.Add("ProductionDetails_Department", "Department");
            translation.Add("ProductionDetails_SubDepartment", "Sub department");
            translation.Add("ProductionDetails_SystemData", "System data");
            translation.Add("ProductionDetails_AverageWelds", "Average welds per hour");
            translation.Add("ProductionDetails_AverageTime", "Average time of weld(s)");
            translation.Add("ProductionDetails_Product", "Product");
            translation.Add("ProductionDetails_WireUseUnit", "Wire use unit");

            //Notification page
            translation.Add("Notification_Title", "Notifications");
            translation.Add("Notification_NonConfirmed", "Non confirmed");
            translation.Add("Notification_Solution", "Solution");
            translation.Add("Notification_ToBeApproved", "To be approved");
            translation.Add("Notification_AllReportedNotifications", "All reported notifications");
            translation.Add("Notification_AllApprovedNotifications", "All approved notifications");
            translation.Add("Notification_PersonalFilter", "Personal filter:");
            translation.Add("Notification_NotificationFilter", "Notification filter:");
            translation.Add("Notification_DateTime", "Date time");
            translation.Add("Notification_Notification", "Notification");
            translation.Add("Notification_kWh", "kWh");
            translation.Add("Notification_Operator", "Operator");
            translation.Add("Notification_Product", "Product");
            translation.Add("Notification_Location", "Location");
            translation.Add("Notification_Department", "Department");
            translation.Add("Notification_SubDepartment", "Sub-department");
            translation.Add("Notification_Acknowledged", "Acknowledged");
            translation.Add("Notification_Description", "Description");
            translation.Add("Notification_TimeToAcknowledge", "Time to acknowledge (DD:HH:MM)");
            translation.Add("Notification_Person", "Person");
            translation.Add("Notification_Approved", "Approved");
            translation.Add("Notification_Reported", "Reported");
            translation.Add("Notification_WasteCause", "Waste cause");
            translation.Add("Notification_ProblemCause", "Problem cause");
            translation.Add("Notification_TimeToSolution", "Time to solution (DD:HH:MM)");
            translation.Add("Notification_AcknowledgedBy", "Acknowledged by");
            translation.Add("Notification_SolvedBy", "Solved by");
            translation.Add("Notification_TimeToApprove", "Time to approve (DD:HH:MM)");
            translation.Add("Notification_Report", "Report");
            translation.Add("Notification_ApprovedBy", "Approved by");
            translation.Add("Notification_ReportedBy", "Reported by");
            translation.Add("Notification_TimeToSolve", "Time to solve (DD:HH:MM)");
            translation.Add("Notification_SelectAll", "Select all");
            translation.Add("Notification_DeselectAll", "Deselect all");
            translation.Add("Notification_AcknowledgeAll", "Acknowledge");
            translation.Add("Notification_SaveAll", "Save");

            //NotificationReply pages
            translation.Add("NotificationReplyType1_Title", "Non confirmed electricity");
            translation.Add("NotificationReplyType2_Title", "Non confirmed acknowledge");
            translation.Add("NotificationReplyType3_Title", "Non confirmed description");
            translation.Add("NotificationReplyType4_Title", "Solution");
            translation.Add("NotificationReplyType5_Title", "To be approved");
            translation.Add("NotificationReplyType6_Title", "Reported");
            translation.Add("NotificationReplyType7_07_Title", "Approved electricity");
            translation.Add("NotificationReplyType7_08_Title", "Approved acknowledge");
            translation.Add("NotificationReplyType7_09_Title", "Approved description");
            translation.Add("NotificationReplyType7_10_Title", "Approved description & solution");
            translation.Add("NotificationReplyType7_11_Title", "Approved solution");
            translation.Add("NotificationReply_CauseButtonText", "Tap to choose a cause");
            translation.Add("NotificationReply_NotificationButtonText", "Tap to choose a notification");
            translation.Add("NotificationReply_DescriptionPlaceholder", "Description");
            translation.Add("NotificationReply_SolutionPlaceholder", "Please describe your solution");
            translation.Add("NotificationReply_SubmitButtonText", "Submit");
            translation.Add("NotificationReply_AcknowledgeButtonText", "Acknowledge");
            translation.Add("NotificationReply_SaveButtonText", "Save");
            translation.Add("NotificationReply_SaveAndCloseButtonText", "Save & close");
            translation.Add("NotificationReply_ApproveButtonText", "Approve");
            translation.Add("NotificationReply_ApproveReportButtonText", "Approve & report");
            translation.Add("NotificationReply_ReportButtonText", "Report");
            translation.Add("NotificationReply_CancelButtonText", "Cancel");
            //translation.Add("NotificationReply_CloseButtonText", "Close");
            translation.Add("NotificationReply_DateTimeTitle", "Date time");
            translation.Add("NotificationReply_MachineNumberTitle", "M#");          ///fixed???
            translation.Add("NotificationReply_OperatorTitle", "Operator");
            translation.Add("NotificationReply_ProductTitle", "Product");
            translation.Add("NotificationReply_LocationTitle", "Location");
            translation.Add("NotificationReply_DepartmentTitle", "Department");
            translation.Add("NotificationReply_SubDepartmentTitle", "Subdepartment");
            translation.Add("NotificationReply_TypeTitle", "Type");
            translation.Add("NotificationReply_RemarkTitle", "Remark");
            translation.Add("NotificationReply_KwhTitle", "kWh");
            translation.Add("NotificationReply_NotificationTitle", "Notification");
            translation.Add("NotificationReply_WasteCauseTitle", "Waste cause");
            translation.Add("NotificationReply_CauseTitle", "Cause");
            translation.Add("NotificationReply_SolutionTitle", "Solution");
            translation.Add("NotificationReply_DescriptionTitle", "Description");
            translation.Add("NotificationReply_ReportedTitle", "Reported");
            translation.Add("NotificationReply_AknowledgeTimeTitle", "Aknowledge time");
            translation.Add("NotificationReply_AknowledgedByTitle", "Aknowledged by");
            translation.Add("NotificationReply_SolutionTimeTitle", "Solution time");
            translation.Add("NotificationReply_SolutionByTitle", "Solution by");
            translation.Add("NotificationReply_ApprovalTimeTitle", "Approval time");
            translation.Add("NotificationReply_ApprovalByTitle", "Approval by");
            translation.Add("NotificationReply_ReportTimeTitle", "Report time");
            translation.Add("NotificationReply_ReportedByTitle", "Reported by");
            translation.Add("NotificationReply_ReportTitle", "Report");
            //
            translation.Add("NotificationReply_CauseASTitle", "Cause");
            translation.Add("NotificationReply_CauseASDescription", "Tap to choose a cause");
            translation.Add("NotificationReply_CauseASCancel", "Cancel");
            //
            translation.Add("NotificationReply_NotificationASTitle", "Notification");
            translation.Add("NotificationReply_NotificationASDescription", "Tap to choose a notification");
            translation.Add("NotificationReply_NotificationASCancel", "Cancel");
            //
            translation.Add("NotificationReply_ErrorMsgChooseCause", "Please choose a cause!");
            translation.Add("NotificationReply_ErrorMsgChooseNotification", "Please input a notification!");
            translation.Add("NotificationReply_ErrorMsgInputDescription", "Please input a description!");
            translation.Add("NotificationReply_ErrorMsgInputSolution", "Please input a solution!");
            translation.Add("NotificationReply_ErrorMsgSubmitFailed", "Sending data to server is failed!");

            //Auxiliary equipment page
            translation.Add("AuxiliaryEquipment_Title", "Auxiliary equipment");
            translation.Add("AuxiliaryEquipment_Equipment", "Equipment");
            translation.Add("AuxiliaryEquipment_On", "On");
            translation.Add("AuxiliaryEquipment_QTY", "QTY");
            translation.Add("AuxiliaryEquipmentDetails_Group", "Equipment group");
            translation.Add("AuxiliaryEquipmentDetails_Location", "Location");
            translation.Add("AuxiliaryEquipmentDetails_Department", "Department");
            translation.Add("AuxiliaryEquipmentDetails_SubDepartment", "Sub department");
            translation.Add("AuxiliaryEquipmentDetails_SystemData", "System data");

            //testing
            translation.Add("TestMultiLanguage_Title", "TestMultiLanguage");
            translation.Add("TestMultiLanguage_Label1", "ContentPage");
            translation.Add("TestMultiLanguage_Label2", "ContentPage is the simplest type of page.");
            translation.Add("TestMultiLanguage_Label3", "The content of the ContentPage is generally a layout of some sort that can be a parent to multiple children.");
            translation.Add("TestMultiLanguage_Button", "Click Me");

            return translation;
        }

        private static Dictionary<string, string> GetTranslation_Chinese()
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();

            //Common elements
            translation.Add("Common_Filter", "选择筛选");
            translation.Add("Common_FilterPersonalTitle", "个人筛选");
            translation.Add("Common_FilterPersonalDescription", "请选择个人筛选");
            translation.Add("Common_FilterTimeTitle", "周期筛选");
            translation.Add("Common_FilterTimeDescription", "请选择周期筛选");
            translation.Add("Common_FilterTimeToday", "今天");
            translation.Add("Common_FilterTimeLast24Hours", "前24小时");
            translation.Add("Common_FilterTimeYesterday", "昨天");
            translation.Add("Common_FilterTimeCurrentWeek", "本周");
            translation.Add("Common_FilterTimeLastWeek", "上周");
            translation.Add("Common_FilterTimeCurrentMonth", "本月");
            translation.Add("Common_FilterTimeLastMonth", "上月");
            translation.Add("Common_FilterTimeCurrentQuarter", "本季度");
            translation.Add("Common_FilterTimeLastQuarter", "上个季度");
            translation.Add("Common_FilterTimeCurrentYear", "本年");
            translation.Add("Common_FilterCancel", "取消");
            translation.Add("Common_FilterNotificationTitle", "通知筛选");
            translation.Add("Common_FilterNotificationDescription", "请选择通知筛选");
            translation.Add("Common_FilterTimeLast48Hours", "前48小时");
            translation.Add("Common_FilterTimeLast72Hours", "前72小时");
            translation.Add("Common_FilterTimeLast7Days", "前7天");
            translation.Add("Common_FilterTimeLast14Days", "前14天");
            translation.Add("Common_FilterTimeLast30Days", "前30天");
            translation.Add("Common_FilterFilterOn", "打开筛选");
            translation.Add("Common_FilterFilterOff", "关闭筛选");
            translation.Add("Common_FilterAll", "全部");
            translation.Add("Common_FilterOn", "打开");
            translation.Add("Common_FilterOff", "关闭");
            translation.Add("Common_FilterErrors", "错误");
            translation.Add("Common_FilterHasWaste", "己浪费");
            translation.Add("Common_WorkingTimeOnly", "只有工作日");
            translation.Add("Common_FilteredBy", "Filtered byCH: ");  /////////////////
            translation.Add("Common_FilterClear", "ClearCH");         /////////////////
            translation.Add("Common_ViewDetail", "查看细节");
            translation.Add("Common_Close", "关闭");
            translation.Add("Common_OK", "可以");
            translation.Add("Common_ServerStatusCodeMsg", "服务器返回状态编码 ");
            translation.Add("Common_ErrorConnectionFailed", "连接MBOX失败");
            translation.Add("Common_ErrorMsgNoNetwork", "无网络连接");

            //Menu - Side view
            translation.Add("Menu_Home", "主页");
            translation.Add("Menu_Language", "语言");
            translation.Add("Menu_Logout", "退出账户");
            translation.Add("Menu_Exit", "退出");
            translation.Add("Menu_About", "AboutCH");             /////////////////
            
            //About
            translation.Add("About_Version", "VersionCH:");       /////////////////
            translation.Add("About_Token", "Device tokenCH:");    /////////////////

            //Login page
            translation.Add("Login_Title", "登陆");
            translation.Add("Login_SelectServer", "选择服务器");
            translation.Add("Login_Username", "请输入用户名");
            translation.Add("Login_Password", "请输入密码");
            translation.Add("Login_RememberMe", "记住我");
            translation.Add("Login_Login", "登陆");
            translation.Add("Login_ForgotPassword", "忘记密码");
            translation.Add("Login_ErrorUsername", "用户名必填");
            translation.Add("Login_ErrorPassword", "密码必填");
            translation.Add("Login_ErrorPlatform", "平台必填");
            translation.Add("Login_ErrorDeviceToken", "设备标识必填");
            translation.Add("Login_ErrorInvalidLogin", "无效登陆");
            translation.Add("Login_ErrorServer", "服务器必填");
            translation.Add("Login_ErrorInvalidServer", "无效服务器");

            //ForgotPassword page
            translation.Add("Forgot_Title", "忘记密码");
            translation.Add("Forgot_InfoText", "注册邮箱地址");
            translation.Add("Forgot_Email", "请输入你的邮箱");
            translation.Add("Forgot_Send", "要求新密码");

            //Main page
            translation.Add("Main_Uptime", "正常运行时间");
            translation.Add("Main_ElectricityUsage", "耗电量");
            translation.Add("Main_Production", "生产");
            translation.Add("Main_Notifications", "通知");
            translation.Add("Main_AuxiliaryEquipment", "辅助设备");

            //Language page
            translation.Add("Language_Title", "语言");
            translation.Add("Language_InfoText", "从列表中选择语言");
            translation.Add("Language_SelectLanguage", "选择语言");
            translation.Add("Language_Update", "更新");
            translation.Add("Language_Cancel", "取消");
            translation.Add("Language_AlertMessage", "未选中");

            //Uptime page
            translation.Add("Uptime_Title", "正常运行时间");
            translation.Add("Uptime_Locations", "位置");
            translation.Add("Uptime_Departments", "部门");
            translation.Add("Uptime_SubDepartments", "分部门");
            translation.Add("Uptime_Equipment", "设备");
            translation.Add("Uptime_EquipmentGroup", "设备组");
            translation.Add("Uptime_AuxiliaryEquipment", "辅助设备");
            translation.Add("Uptime_LocationsTableEquipment", "设备(目前状态)");
            translation.Add("Uptime_Location", "位置");
            translation.Add("Uptime_Uptime", "正常运行时间");
            translation.Add("Uptime_On", "打开");
            translation.Add("Uptime_Off", "关闭");
            translation.Add("Uptime_Errors", "错误");
            translation.Add("UptimeDetails_FilterLabel", "筛选器");
            translation.Add("UptimeDetails_Detail", "细节");                  ////////////
            translation.Add("UptimeDetails_AuxiliaryEquipment", "辅助设备");  /////////////
            translation.Add("UptimeDetails_Name", "名称");
            translation.Add("UptimeDetails_Current", "当前的");
            translation.Add("UptimeDetails_OffTime", "关闭时间");
            translation.Add("UptimeDetails_Status", "状态");
            translation.Add("UptimeDetails_RunTime", "时间");
            translation.Add("UptimeDetails_Stops", "停止");
            translation.Add("UptimeDetails_StopTime", "时间：");
            translation.Add("UptimeDetails_Group", "组");
            translation.Add("UptimeDetails_Department", "部");
            translation.Add("UptimeDetails_SubDepartment", "子部门");
            translation.Add("UptimeDetails_Type", "型");
            translation.Add("UptimeDetails_Remark", "备注");
            translation.Add("UptimeDetails_SystemData", "系统数据");

            //Electricity Usage
            translation.Add("ElectricityUsage_Title", "Electricity usage");
            translation.Add("ElectricityUsage_Locations", "Locations");
            translation.Add("ElectricityUsage_Areas", "Areas");
            translation.Add("ElectricityUsage_Departments", "Departments");
            translation.Add("ElectricityUsage_SubDepartments", "Sub departments");
            translation.Add("ElectricityUsage_ConsumingPower", "Equipment off but consuming power");
            translation.Add("ElectricityUsage_Current", "Current");
            translation.Add("ElectricityUsage_Total", "Total");
            translation.Add("ElectricityUsage_NoDetails", "No details");
            translation.Add("ElectricityUsage_Waste", "Waste");
            translation.Add("ElectricityUsage_Eff", "Eff.");
            translation.Add("ElectricityUsage_On", "On");
            translation.Add("ElectricityUsage_Off", "Off");
            translation.Add("ElectricityUsage_Errors", "Errors");
            translation.Add("ElectricityUsage_Group", "Group");
            translation.Add("ElectricityUsage_Status", "Status");
            translation.Add("ElectricityUsage_Location", "Location");
            translation.Add("ElectricityUsage_Department", "Department");
            translation.Add("ElectricityUsage_SubDepartment", "Sub Department");
            translation.Add("ElectricityUsage_SystemData", "System Data");

            //Production page
            translation.Add("Production_Title", "Production");
            translation.Add("Production_Equipment", "Equipment");
            translation.Add("Production_Eff", "Eff.");
            translation.Add("Production_On", "On");
            translation.Add("Production_Off", "Off");
            translation.Add("ProductionDetails_Current", "Current");
            translation.Add("ProductionDetails_OffTime", "Off time");
            translation.Add("ProductionDetails_EqGroup", "Equipment group");
            translation.Add("ProductionDetails_ProductionType", "Production type");
            translation.Add("ProductionDetails_Eff", "Eff.");
            translation.Add("ProductionDetails_Mould", "Mould");
            translation.Add("ProductionDetails_CycleTime", "Cycle time(s)");
            translation.Add("ProductionDetails_Status", "Status");
            translation.Add("ProductionDetails_RunTime", "Time (DD:HH:MM)");
            translation.Add("ProductionDetails_Stops", "Stops");
            translation.Add("ProductionDetails_StopTime", "Time (DD:HH:MM)");
            translation.Add("ProductionDetails_kWH", "kWH");
            translation.Add("ProductionDetails_Operator", "Operator");
            translation.Add("ProductionDetails_Location", "Location");
            translation.Add("ProductionDetails_Department", "Department");
            translation.Add("ProductionDetails_SubDepartment", "Sub department");
            translation.Add("ProductionDetails_SystemData", "System data");
            translation.Add("ProductionDetails_AverageWelds", "Average welds per hour");
            translation.Add("ProductionDetails_AverageTime", "Average time of weld(s)");
            translation.Add("ProductionDetails_Product", "Product");
            translation.Add("ProductionDetails_WireUseUnit", "Wire use unit");

            //Notification page
            translation.Add("Notification_Title", "通知");
            translation.Add("Notification_NonConfirmed", "未确认");
            translation.Add("Notification_Solution", "解决方案");
            translation.Add("Notification_ToBeApproved", "被批准");
            translation.Add("Notification_AllReportedNotifications", "所有报告的通知");
            translation.Add("Notification_AllApprovedNotifications", "所有批准的通知");
            translation.Add("Notification_PersonalFilter", "个人筛选");
            translation.Add("Notification_NotificationFilter", "通知筛选");
            translation.Add("Notification_DateTime", "日期时间");
            translation.Add("Notification_Notification", "通知");
            translation.Add("Notification_kWh", "千瓦时");
            translation.Add("Notification_Operator", "操作员");
            translation.Add("Notification_Product", "产品");
            translation.Add("Notification_Location", "位置");
            translation.Add("Notification_Department", "部");
            translation.Add("Notification_SubDepartment", "子部门");
            translation.Add("Notification_Acknowledged", "命令正确应答");
            translation.Add("Notification_Description", "描述");
            translation.Add("Notification_TimeToAcknowledge", "承认时间（DD：mm）");
            translation.Add("Notification_Person", "人员");
            translation.Add("Notification_Approved", "经核准的");
            translation.Add("Notification_Reported", "报道");
            translation.Add("Notification_WasteCause", "浪费的原因");
            translation.Add("Notification_ProblemCause", "问题的原因");
            translation.Add("Notification_TimeToSolution", "解决问题的时间");
            translation.Add("Notification_AcknowledgedBy", "确认于");
            translation.Add("Notification_SolvedBy", "解决于");
            translation.Add("Notification_TimeToApprove", "批准时间（DD：mm）");
            translation.Add("Notification_Report", "报告");
            translation.Add("Notification_ApprovedBy", "经审核");
            translation.Add("Notification_ReportedBy", "经报告");
            translation.Add("Notification_TimeToSolve", "解决时间（DD：mm）");
            translation.Add("Notification_SelectAll", "Select_all_CH");         ////////////
            translation.Add("Notification_DeselectAll", "Deselect all_CH");     ////////////
            translation.Add("Notification_AcknowledgeAll", "AcknowledgeCH");    ////////////
            translation.Add("Notification_SaveAll", "SaveCH");                  ////////////

            //NotificationReply pages
            translation.Add("NotificationReplyType1_Title", "未确认电量");
            translation.Add("NotificationReplyType2_Title", "未确认审核");
            translation.Add("NotificationReplyType3_Title", "未确认的描述");
            translation.Add("NotificationReplyType4_Title", "解决方案");
            translation.Add("NotificationReplyType5_Title", "被批准");
            translation.Add("NotificationReplyType6_Title", "己报告");
            translation.Add("NotificationReplyType7_07_Title", "己批准电量");
            translation.Add("NotificationReplyType7_08_Title", "批准确认");
            translation.Add("NotificationReplyType7_09_Title", "通过描述");
            translation.Add("NotificationReplyType7_10_Title", "批准说明和解决方案");
            translation.Add("NotificationReplyType7_11_Title", "批准的解决方案");
            translation.Add("NotificationReply_CauseButtonText", "选择原因");
            translation.Add("NotificationReply_NotificationButtonText", "点击选择通知");
            translation.Add("NotificationReply_DescriptionPlaceholder", "描述");
            translation.Add("NotificationReply_SolutionPlaceholder", "请描述一下你的解决方案。");
            translation.Add("NotificationReply_SubmitButtonText", "提交");
            translation.Add("NotificationReply_AcknowledgeButtonText", "审核");
            translation.Add("NotificationReply_SaveButtonText", "保存");
            translation.Add("NotificationReply_SaveAndCloseButtonText", "保存并关闭");
            translation.Add("NotificationReply_ApproveButtonText", "批准");
            translation.Add("NotificationReply_ApproveReportButtonText", "批准和报告");
            translation.Add("NotificationReply_ReportButtonText", "报告");
            translation.Add("NotificationReply_CancelButtonText", "取消");
            //translation.Add("NotificationReply_CloseButtonText", "关闭");
            translation.Add("NotificationReply_DateTimeTitle", "日期时间");
            translation.Add("NotificationReply_MachineNumberTitle", "M#");
            translation.Add("NotificationReply_OperatorTitle", "操作员");
            translation.Add("NotificationReply_ProductTitle", "产品");
            translation.Add("NotificationReply_LocationTitle", "位置");
            translation.Add("NotificationReply_DepartmentTitle", "部");
            translation.Add("NotificationReply_SubDepartmentTitle", "子部门");
            translation.Add("NotificationReply_TypeTitle", "类型");
            translation.Add("NotificationReply_RemarkTitle", "备注");
            translation.Add("NotificationReply_KwhTitle", "千瓦时");
            translation.Add("NotificationReply_NotificationTitle", "通知");
            translation.Add("NotificationReply_WasteCauseTitle", "浪费的原因");
            translation.Add("NotificationReply_CauseTitle", "原因");
            translation.Add("NotificationReply_SolutionTitle", "解决方案");
            translation.Add("NotificationReply_DescriptionTitle", "描述");
            translation.Add("NotificationReply_ReportedTitle", "报告");
            translation.Add("NotificationReply_AknowledgeTimeTitle", "审核时间");
            translation.Add("NotificationReply_AknowledgedByTitle", "被…审核");
            translation.Add("NotificationReply_SolutionTimeTitle", "解决方案的时间");
            translation.Add("NotificationReply_SolutionByTitle", "解决方案");
            translation.Add("NotificationReply_ApprovalTimeTitle", "审批时间");
            translation.Add("NotificationReply_ApprovalByTitle", "批准");
            translation.Add("NotificationReply_ReportTimeTitle", "报告时间");
            translation.Add("NotificationReply_ReportedByTitle", "报告");
            translation.Add("NotificationReply_ReportTitle", "报告");
            //
            translation.Add("NotificationReply_CauseASTitle", "原因");
            translation.Add("NotificationReply_CauseASDescription", "选择原因");
            translation.Add("NotificationReply_CauseASCancel", "取消");
            //
            translation.Add("NotificationReply_NotificationASTitle", "通知");
            translation.Add("NotificationReply_NotificationASDescription", "点击选择通知");
            translation.Add("NotificationReply_NotificationASCancel", "取消");
            //
            translation.Add("NotificationReply_ErrorMsgChooseCause", "选择原因！");
            translation.Add("NotificationReply_ErrorMsgChooseNotification", "请输入通知！");
            translation.Add("NotificationReply_ErrorMsgInputDescription", "请输入描述！");
            translation.Add("NotificationReply_ErrorMsgInputSolution", "请输入解决方案！");
            translation.Add("NotificationReply_ErrorMsgSubmitFailed", "将数据发送到服务器失败！");

            //Auxiliary equipment page
            translation.Add("AuxiliaryEquipment_Title", "Auxiliary equipment");
            translation.Add("AuxiliaryEquipment_Equipment", "Equipment");
            translation.Add("AuxiliaryEquipment_On", "On");
            translation.Add("AuxiliaryEquipment_QTY", "QTY");
            translation.Add("AuxiliaryEquipmentDetails_Group", "Equipment group");
            translation.Add("AuxiliaryEquipmentDetails_Location", "Location");
            translation.Add("AuxiliaryEquipmentDetails_Department", "Department");
            translation.Add("AuxiliaryEquipmentDetails_SubDepartment", "Sub department");
            translation.Add("AuxiliaryEquipmentDetails_SystemData", "System data");

            return translation;
        }

        private static Dictionary<string, string> GetTranslation_German()
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();

            return translation;
        }

        private static Dictionary<string, string> GetTranslation_Dutch()
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();

            return translation;
        }

        private static Dictionary<string, string> GetTranslation_French()
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();

            return translation;
        }

        private static Dictionary<string, string> GetTranslation_Spanish()
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();

            return translation;
        }

        private static Dictionary<string, string> GetTranslation_Khmer()
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();

            return translation;
        }

        private static Dictionary<string, string> GetTranslation_Bahasa()
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();

            return translation;
        }

        private static Dictionary<string, string> GetTranslation_Portuguese()
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();

            return translation;
        }

        private static Dictionary<string, string> GetTranslation_LabelCheck()
        {
            //testing
            Dictionary<string, string> translation = new Dictionary<string, string>();
            translation.Add("TestMultiLanguage_Title", "Test višejezičnosti");
            translation.Add("TestMultiLanguage_Label1", "Obična strana");
            translation.Add("TestMultiLanguage_Label2", "Obična strana je najjednostavniji tip strane.");
            translation.Add("TestMultiLanguage_Label3", "Sadržaj obične strane je generalno... kako ovo glupo zvuči kad se prevede :)");
            translation.Add("TestMultiLanguage_Button", "Pritisni me");

            return translation;
        }

        #endregion
    }

    public enum Languages
    {
        English = 1,
        Chinese,
        German,
        Dutch,
        French,
        Spanish,
        Khmer,
        Bahasa,
        Portuguese,
        LabelCheck
    }
}
