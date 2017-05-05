using System.Collections.Generic;

namespace MBoxMobile.Helpers
{
    public static class FilterSupport
    {
        public static Dictionary<int, string> GetPersonalFilters()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();

            //dict.Add(1, "Uptime_FilterTimeToday");
            //dict.Add(2, "Uptime_FilterTimeLast24Hours");
            //dict.Add(3, "Uptime_FilterTimeYesterday");
            //dict.Add(4, "Uptime_FilterTimeCurrentWeek");
            //dict.Add(5, "Uptime_FilterTimeLastWeek");
            //dict.Add(6, "Uptime_FilterTimeCurrentMonth");
            //dict.Add(7, "Uptime_FilterTimeLastMonth");
            //dict.Add(8, "Uptime_FilterTimeCurrentQuarter");
            //dict.Add(9, "Uptime_FilterTimeLastQuarter");
            //dict.Add(10, "Uptime_FilterTimeCurrentYear");

            return dict;
        }

        public static Dictionary<int, string> GetTimeFilters()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();

            dict.Add(6275, "Uptime_FilterTimeToday");
            dict.Add(6274, "Uptime_FilterTimeLast24Hours");
            dict.Add(6276, "Uptime_FilterTimeYesterday");
            dict.Add(6277, "Uptime_FilterTimeCurrentWeek");
            dict.Add(6278, "Uptime_FilterTimeLastWeek");
            dict.Add(6279, "Uptime_FilterTimeCurrentMonth");
            dict.Add(6280, "Uptime_FilterTimeLastMonth");
            dict.Add(6350, "Uptime_FilterTimeCurrentQuarter");
            dict.Add(6351, "Uptime_FilterTimeLastQuarter");
            dict.Add(6352, "Uptime_FilterTimeCurrentYear");

            return dict;
        }

        public static double GetFilter4FontSize(double screenWidth)
        {
            if (screenWidth <= 353)
                return 10;
            else
                return 14;
        }

        public static double GetFilter2FontSize(double screenWidth)
        {
            if (screenWidth <= 353)
                return 12;
            else
                return 14;
        }

        public static double GetFilterLabelMarginTop(double screenWidth)
        {
            if (screenWidth <= 353)
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
}
