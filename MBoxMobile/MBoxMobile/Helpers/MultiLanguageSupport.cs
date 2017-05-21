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
            translation.Add("Common_FilterFilterOn", "Filter On");
            translation.Add("Common_FilterFilterOff", "Filter Off");
            translation.Add("Common_FilterAll", "All");
            translation.Add("Common_FilterOn", "On");
            translation.Add("Common_FilterOff", "Off");
            translation.Add("Common_FilterErrors", "Errors");
            translation.Add("Common_ViewDetail", "View Detail");
            translation.Add("Common_Close", "Close");

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
            translation.Add("Uptime_WorkingTimeOnly", "Working Time Only");
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

            //InputNotificationsKWH page
            translation.Add("InputNotificationsKWH_Title", "KWH");
            translation.Add("InputNotificationsKWH_CauseButtonText", "Tap to choose a cause");
            translation.Add("InputNotificationsKWH_OpinionPlaceholder", "Please describe your opinion");
            translation.Add("InputNotificationsKWH_SubmitButtonText", "Submit");
            translation.Add("InputNotificationsKWH_CancelButtonText", "Cancel");
            translation.Add("InputNotificationsKWH_MachineNumberTitle", "M#");          ///fixed???
            translation.Add("InputNotificationsKWH_MachineNameTitle", "Machine name");
            translation.Add("InputNotificationsKWH_EquipTypeNameTitle", "Equipment type");
            translation.Add("InputNotificationsKWH_EquipGroupNameTitle", "Equipment group");
            translation.Add("InputNotificationsKWH_KwhTitle", "kWh");
            translation.Add("InputNotificationsKWH_OperatorTitle", "Operator");
            translation.Add("InputNotificationsKWH_ProductTitle", "Product");
            translation.Add("InputNotificationsKWH_DateTimeTitle", "Date time");
            translation.Add("InputNotificationsKWH_NotificationTitle", "Notification");
            translation.Add("InputNotificationsKWH_LocationTitle", "Location");
            translation.Add("InputNotificationsKWH_DepartmentTitle", "Department");
            translation.Add("InputNotificationsKWH_SubDepartmentTitle", "Subdepartment");
            //
            translation.Add("InputNotificationsKWH_CauseASTitle", "Cause");
            translation.Add("InputNotificationsKWH_CauseASDescription", "Tap to choose a cause");
            translation.Add("InputNotificationsKWH_CauseASCancel", "Cancel");

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
