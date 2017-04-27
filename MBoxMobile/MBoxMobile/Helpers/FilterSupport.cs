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

            dict.Add(1, "Uptime_FilterTimeToday");
            dict.Add(2, "Uptime_FilterTimeLast24Hours");
            dict.Add(3, "Uptime_FilterTimeYesterday");
            dict.Add(4, "Uptime_FilterTimeCurrentWeek");
            dict.Add(5, "Uptime_FilterTimeLastWeek");
            dict.Add(6, "Uptime_FilterTimeCurrentMonth");
            dict.Add(7, "Uptime_FilterTimeLastMonth");
            dict.Add(8, "Uptime_FilterTimeCurrentQuarter");
            dict.Add(9, "Uptime_FilterTimeLastQuarter");
            dict.Add(10, "Uptime_FilterTimeCurrentYear");

            return dict;
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
