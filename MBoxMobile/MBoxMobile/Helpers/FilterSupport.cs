using System.Collections.Generic;

namespace MBoxMobile.Helpers
{
    public static class FilterSupport
    {
        public static Dictionary<int, string> GetPersonalFilters()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();

            //dict.Add(1, "Common_FilterTimeToday");
            //dict.Add(2, "Common_FilterTimeLast24Hours");
            //dict.Add(3, "Common_FilterTimeYesterday");
            //dict.Add(4, "Common_FilterTimeCurrentWeek");
            //dict.Add(5, "Common_FilterTimeLastWeek");
            //dict.Add(6, "Common_FilterTimeCurrentMonth");
            //dict.Add(7, "Common_FilterTimeLastMonth");
            //dict.Add(8, "Common_FilterTimeCurrentQuarter");
            //dict.Add(9, "Common_FilterTimeLastQuarter");
            //dict.Add(10, "Common_FilterTimeCurrentYear");

            return dict;
        }

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

        public static double GetWorkingHoursLabelFontSize(double screenWidth)
        {
            if (screenWidth <= 353)
                return 12;
            else
                return 15;
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
