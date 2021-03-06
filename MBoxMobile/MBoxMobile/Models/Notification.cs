﻿using MBoxMobile.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MBoxMobile.Models
{
    public class NotificationModel
    {
        public int ID { get; set; }
        public string RecordDate { get; set; }
        public string RecordDateLocal
        {
            get
            {
                if (RecordDate != string.Empty)
                {
                    DateTime dt;
                    if (DateTime.TryParse(RecordDate, out dt))
                        return string.Format("{0} {1}", DependencyService.Get<IDeviceDateTimeFormat>().ConvertToDeviceShortDateFormat(dt), DependencyService.Get<IDeviceDateTimeFormat>().ConvertToDeviceTimeFormat(dt));
                    else
                        return string.Empty;
                }
                else
                    return string.Empty;
            }
        }
        public string MachineNumber { get; set; }
        public string AlterDescription { get; set; }
        public string Description { get; set; }
        public bool NeedReport { get; set; }
        public string DesDate { get; set; }
        public string DesDateLocal
        {
            get
            {
                if (DesDate != string.Empty)
                {
                    DateTime dt;
                    if (DateTime.TryParse(DesDate, out dt))
                        return string.Format("{0} {1}", DependencyService.Get<IDeviceDateTimeFormat>().ConvertToDeviceShortDateFormat(dt), DependencyService.Get<IDeviceDateTimeFormat>().ConvertToDeviceTimeFormat(dt));
                    else
                        return string.Empty;
                }
                else
                    return string.Empty;
            }
        }
        public string ApproveDate { get; set; }
        public string ApproveDateLocal
        {
            get
            {
                if (ApproveDate != string.Empty)
                {
                    DateTime dt;
                    if (DateTime.TryParse(ApproveDate, out dt))
                        return string.Format("{0} {1}", DependencyService.Get<IDeviceDateTimeFormat>().ConvertToDeviceShortDateFormat(dt), DependencyService.Get<IDeviceDateTimeFormat>().ConvertToDeviceTimeFormat(dt));
                    else
                        return string.Empty;
                }
                else
                    return string.Empty;
            }
        }
        public string Report { get; set; }
        public bool Acknowledge { get; set; }
        public int? AlterCauseID { get; set; }
        public int Popup { get; set; }
        public string SolutionDate { get; set; }
        public string SolutionDateLocal
        {
            get
            {
                if (SolutionDate != string.Empty)
                {
                    DateTime dt;
                    if (DateTime.TryParse(SolutionDate, out dt))
                        return string.Format("{0} {1}", DependencyService.Get<IDeviceDateTimeFormat>().ConvertToDeviceShortDateFormat(dt), DependencyService.Get<IDeviceDateTimeFormat>().ConvertToDeviceTimeFormat(dt));
                    else
                        return string.Empty;
                }
                else
                    return string.Empty;
            }
        }
        public string ReportDate { get; set; }
        public string ReportDateLocal
        {
            get
            {
                if (ReportDate != string.Empty)
                {
                    DateTime dt;
                    if (DateTime.TryParse(ReportDate, out dt))
                        return string.Format("{0} {1}", DependencyService.Get<IDeviceDateTimeFormat>().ConvertToDeviceShortDateFormat(dt), DependencyService.Get<IDeviceDateTimeFormat>().ConvertToDeviceTimeFormat(dt));
                    else
                        return string.Empty;
                }
                else
                    return string.Empty;
            }
        }
        public string SoluPerson { get; set; }
        public string DesPerson { get; set; }
        public int LocalTimeZone { get; set; }
        public string Operator { get; set; }
        public string ReportPerson { get; set; }
        public string ApprovePerson { get; set; }
        public int DataType { get; set; }
        public string SentToCompany { get; set; }
        public string Department { get; set; }
        public string DepartmentSubName { get; set; }
        public string Product { get; set; }
        public int AlterType { get; set; }
        public int AlterDescriptionID { get; set; }
        public string Solution { get; set; }
        public string AlterButtonDesc { get; set; }
        public string AlterTypeText { get; set; }
        public float? Kwh { get; set; }
        public bool NeedDesc { get; set; }
        public bool IsPullDown { get; set; }
        public int? AlterReply { get; set; }
        public int? AlterButton { get; set; }
        public string ElecCause { get; set; }
        public int? ElecCauseID { get; set; }
        public string AlterCause { get; set; }
        public bool Approved { get; set; }
        public string MainCharacterization { get; set; }
        public string EquipTypeText { get; set; }
        public string EquipGroup { get; set; }
        public int? AddressID { get; set; }
        public int? MachineGroupNameID { get; set; }
        public int? CardType { get; set; }
        public int? EquipmentType { get; set; }
        public int? ParentID { get; set; }

        public string CalculateDateTime(string firstDT, string secondDT)
        {
            if (firstDT != string.Empty && secondDT != string.Empty)
            {
                DateTime dtFirst, dtSecond;
                if (DateTime.TryParse(firstDT, out dtFirst) && DateTime.TryParse(secondDT, out dtSecond))
                {
                    int days = (dtSecond - dtFirst).Days;
                    int hours = (dtSecond - dtFirst).Hours;
                    int minutes = (dtSecond - dtFirst).Minutes;

                    return string.Format("{0:00}:{1:00}:{2:00}", days, hours, minutes);
                }
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }
    }   

    public class NotificationFilter
    {
        public List<Filter> FilterList { get; set; }
        public bool FilterOn { get; set; }
        public int SelectedFilterID { get; set; }
    }

    public class NotificationFilterWrapper
    {
        [JsonProperty("d")]
        public NotificationFilter MyNotificationFilter { get; set; }
    }

    public class WasteCauseModel
    {
        public int MID { get; set; }
        public string Material { get; set; }
        public string DescCH { get; set; }
    }

    public class AlterDescriptionModel
    {
        public int MID { get; set; }
        public string Material { get; set; }
        public int EquipmentType { get; set; }
        public bool NeedDesc { get; set; }
    }

    public class SolutionCauseModel
    {
        public int MID { get; set; }
        public string Material { get; set; }
        public string Subgroup { get; set; }
    }

    #region ListClasses
    public class NotificationModelList
    {
        [JsonProperty("d")]
        public List<NotificationModel> Notifications { get; set; }
    }

    public class WasteCauseModelList
    {
        [JsonProperty("d")]
        public List<WasteCauseModel> WasteCauses { get; set; }
    }

    public class AlterDescriptionModelList
    {
        [JsonProperty("d")]
        public List<AlterDescriptionModel> AlterDescriptions { get; set; }
    }

    public class SolutionCauseModelList
    {
        [JsonProperty("d")]
        public List<SolutionCauseModel> SolutionCauses { get; set; }
    }
    #endregion

    public class NotificationPayload
    {
        public string machine_num { get; set; }
        public string at { get; set; }
        public string record_date { get; set; }
        public string material { get; set; }
        public string Inputstable_AlterID { get; set; }
        public string AlterEquipType { get; set; }
        public string MachineName { get; set; }
        public string EquipTypeName { get; set; }
        public string EquipGroupName { get; set; }
        public string Kwh { get; set; }
        public string Operator { get; set; }
        public string Product { get; set; }
        public string Notification { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string SubDepartment { get; set; }
        public string AlterType { get; set; }
        public string NotType { get; set; }
        public string AlterReply { get; set; }
    }
}
