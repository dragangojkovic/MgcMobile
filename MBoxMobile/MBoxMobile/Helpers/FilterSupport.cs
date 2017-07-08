using System.Collections.Generic;

namespace MBoxMobile.Helpers
{
    public static class FilterSupport
    {
        public static Dictionary<int, string> GetTimeFilters()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();

            dict.Add(6275, "Common_FilterTimeToday");
            dict.Add(6274, "Common_FilterTimeLast24Hours");
            dict.Add(6276, "Common_FilterTimeYesterday");
            dict.Add(6277, "Common_FilterTimeCurrentWeek");
            dict.Add(6278, "Common_FilterTimeLastWeek");
            dict.Add(6279, "Common_FilterTimeCurrentMonth");
            dict.Add(6280, "Common_FilterTimeLastMonth");
            dict.Add(6350, "Common_FilterTimeCurrentQuarter");
            dict.Add(6351, "Common_FilterTimeLastQuarter");
            dict.Add(6352, "Common_FilterTimeCurrentYear");

            return dict;
        }

        public static Dictionary<int, string> GetNotificationFilters()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();

            dict.Add(6566, "Common_FilterNotificationLast24Hours");
            dict.Add(6653, "Common_FilterNotificationYesterday");
            dict.Add(6560, "Common_FilterNotificationLast48Hours");
            dict.Add(6562, "Common_FilterNotificationLast72Hours");
            dict.Add(6563, "Common_FilterNotificationLast7Days");
            dict.Add(6564, "Common_FilterNotificationLast14Days");
            dict.Add(6565, "Common_FilterNotificationLast30Days");

            return dict;
        }

        public static double GetFilter4FontSize(double screenWidth)
        {
            if (screenWidth <= 360)
                return 10;
            else
                return 14;
        }

        public static double GetFilter2FontSize(double screenWidth)
        {
            if (screenWidth <= 360)
                return 12;
            else
                return 14;
        }

        public static double GetFilter2FontSizeNotificationPage(double screenWidth)
        {
            if (screenWidth <= 360)
                return 10;
            else
                return 12;
        }

        public static double GetWorkingHoursLabelFontSize(double screenWidth)
        {
            if (screenWidth <= 360)
                return 12;
            else
                return 15;
        }

        public static double GetFilterLabelMarginTop(double screenWidth)
        {
            if (screenWidth <= 360)
                return 5;
            else
                return 8;
        }
    }

    public enum Filter4State
    {
        All = 1,
        On,
        Off,
        Errors
    }

    public enum Filter3State
    {
        All = 1,
        On,
        HasWaste
    }
}
