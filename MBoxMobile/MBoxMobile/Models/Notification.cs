using Newtonsoft.Json;
using System.Collections.Generic;

namespace MBoxMobile.Models
{
    public class NotificationModel
    {
        public int ID { get; set; }
        public string RecordDate { get; set; }
        public string MachineNumber { get; set; }
        public string AlterDescription { get; set; }
        public string Description { get; set; }
        public bool NeedReport { get; set; }
        public string DesDate { get; set; }
        public string ApproveDate { get; set; }
        public string Report { get; set; }
        public bool Acknowledge { get; set; }
        public int? AlterCauseID { get; set; }
        public int Popup { get; set; }
        public string SolutionDate { get; set; }
        public string ReportDate { get; set; }
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
        public List<SolutionCauseModel> SolutionCauseModels { get; set; }
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
