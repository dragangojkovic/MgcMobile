﻿using System.Collections.Generic;

namespace MBoxMobile.Helpers
{
    public static class MultiLanguageSupport
    {
        public static Dictionary<int, string> GetLanguages()
        {
            Dictionary<int, string> languages = new Dictionary<int, string>();
            languages.Add(1, "English");
            languages.Add(2, "中文");
            languages.Add(3, "Deutsch");
            languages.Add(4, "Nederlands");
            languages.Add(5, "Français");
            languages.Add(6, "Español");
            languages.Add(7, "Khmer");
            languages.Add(8, "Bahasa");
            languages.Add(9, "Português");
            languages.Add(10, "LabelCheck");

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
            translation.Add("Common_FilterTimeTitle", "Personal Filter");
            translation.Add("Common_FilterTimeDescription", "Please choose a personal filter");
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
            translation.Add("Common_FilterTimeCancel", "Cancel");
            translation.Add("Common_FilterNotificationLast24Hours", "Last 24 Hours");
            translation.Add("Common_FilterNotificationYesterday", "Yesterday");
            translation.Add("Common_FilterNotificationLast48Hours", "Last 48 Hours");
            translation.Add("Common_FilterNotificationLast72Hours", "Last 72 Hours");
            translation.Add("Common_FilterNotificationLast7Days", "Last 7 Days");
            translation.Add("Common_FilterNotificationLast14Days", "Last 14 Days");
            translation.Add("Common_FilterNotificationLast30Days", "Last 30 Days");
            translation.Add("Common_FilterFilterOn", "Filter On");
            translation.Add("Common_FilterFilterOff", "Filter Off");
            translation.Add("Common_FilterAll", "All");
            translation.Add("Common_FilterOn", "On");
            translation.Add("Common_FilterOff", "Off");
            translation.Add("Common_FilterErrors", "Errors");
            translation.Add("Common_FilterHasWaste", "Has waste");
            translation.Add("Common_WorkingTimeOnly", "Working Time Only");
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
            translation.Add("UptimeDetails_Detail", "Detail");
            translation.Add("UptimeDetails_AuxiliaryEquipment", "Auxiliary equipment");
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
            translation.Add("Notification_Acknowledged", "Ack.");
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
