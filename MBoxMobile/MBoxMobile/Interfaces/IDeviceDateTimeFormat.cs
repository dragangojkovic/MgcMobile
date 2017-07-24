using System;

namespace MBoxMobile.Interfaces
{
    public interface IDeviceDateTimeFormat
    {
        string ConvertToDeviceShortDateFormat(DateTime inputDateTime);
        string ConvertToDeviceTimeFormat(DateTime inputDateTime);
    }
}
