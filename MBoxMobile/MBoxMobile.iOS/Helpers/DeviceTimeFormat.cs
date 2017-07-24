using Foundation;
using MBoxMobile.Helpers;
using MBoxMobile.Interfaces;
using MBoxMobile.iOS.Helpers;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceTimeFormat))]
namespace MBoxMobile.iOS.Helpers
{
    public class DeviceTimeFormat : IDeviceDateTimeFormat
    {
        public string ConvertToDeviceShortDateFormat(DateTime inputDateTime)
        {
            var timeInEpoch = DateTimeSupport.ConvertDateTimeToUnixTime(inputDateTime);

            if (timeInEpoch == null)
            {
                return string.Empty;
            }

            using (var dateInNsDate = NSDate.FromTimeIntervalSince1970((double)timeInEpoch))
            {
                using (var formatter = new NSDateFormatter
                {
                    TimeStyle = NSDateFormatterStyle.None,
                    DateStyle = NSDateFormatterStyle.Short,
                    Locale = NSLocale.CurrentLocale
                })
                {
                    return formatter.ToString(dateInNsDate);
                }
            }
        }

        public string ConvertToDeviceTimeFormat(DateTime inputDateTime)
        {
            var timeInEpoch = DateTimeSupport.ConvertDateTimeToUnixTime(inputDateTime);

            if (timeInEpoch == null)
            {
                return string.Empty;
            }

            using (var dateInNsDate = NSDate.FromTimeIntervalSince1970((double)timeInEpoch))
            {
                using (var formatter = new NSDateFormatter
                {
                    TimeStyle = NSDateFormatterStyle.Short,
                    DateStyle = NSDateFormatterStyle.None,
                    Locale = NSLocale.CurrentLocale
                })
                {
                    return formatter.ToString(dateInNsDate);
                }
            }
        }
    }
}
