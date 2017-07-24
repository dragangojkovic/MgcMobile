using System;
using MBoxMobile.Interfaces;
using MBoxMobile.Droid.Helpers;
using MBoxMobile.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceDateTimeFormat))]
namespace MBoxMobile.Droid.Helpers
{
    public class DeviceDateTimeFormat : IDeviceDateTimeFormat
    {
        public string ConvertToDeviceShortDateFormat(DateTime inputDateTime)
        {
            var dateFormat = Android.Text.Format.DateFormat.GetDateFormat(Android.App.Application.Context);
            var epochDateTime = DateTimeSupport.ConvertDateTimeToUnixTime(inputDateTime, true);

            if (epochDateTime == null)
            {
                return string.Empty;
            }

            using (var javaDate = new Java.Util.Date((long)epochDateTime))
            {
                return dateFormat.Format(javaDate);
            }
        }

        public string ConvertToDeviceTimeFormat(DateTime inputDateTime)
        {
            var timeFormat = Android.Text.Format.DateFormat.GetTimeFormat(Android.App.Application.Context);
            var epochDateTime = DateTimeSupport.ConvertDateTimeToUnixTime(inputDateTime, true);

            if (epochDateTime == null)
            {
                return string.Empty;
            }

            using (var javaDate = new Java.Util.Date((long)epochDateTime))
            {
                return timeFormat.Format(javaDate);
            }
        }
    }
}