using Newtonsoft.Json;
using System.Collections.Generic;

namespace MBoxMobile.Models
{
    public class NotificationModel
    {
        public int ID { get; set; }
        public string DateTime { get; set; }
        public string MachineNumber { get; set; }
        public string Notification { get; set; }
        public string NotificationType { get; set; }
        public string Kwh { get; set; }
        public int? Cause { get; set; }
        public string AckDuration { get; set; }
        public string Description { get; set; }
        public string AckName { get; set; }
        public string Solution { get; set; }
        public string SoluDuration { get; set; }
        public string SoluType { get; set; }
        public string SoluName { get; set; }
        public string ApprDuration { get; set; }
        public string ApprName { get; set; }
        public string Operator { get; set; }
        public string Product { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string SubDepartment { get; set; }
        public string EquipmentTypeName { get; set; }
        public string EquipmentGroupName { get; set; }
        public string MachineName { get; set; }
        public int AlterReply { get; set; }
        public int AlterType { get; set; }
        public int NotType { get; set; }
        public bool Acknowledge { get; set; }
        public string AlterCauseText { get; set; }
        public bool IsPullDown { get; set; }
        public int AlterEquipmentType { get; set; }
    }

    public class NotificationFilter
    {
        public int FilterID { get; set; }
        public string FilterName { get; set; }
    }

    public class NotificationFilterModel
    {
        public List<NotificationFilter> FilterList { get; set; }
        public bool FilterOn { get; set; }
        public int SelectedFilterID { get; set; }
    }

    public class MaterialModel
    {
        public int MID { get; set; }
        public string Material { get; set; }
    }

    #region ListClasses
    public class MaterialModelList
    {
        [JsonProperty("d")]
        public List<MaterialModel> Materials { get; set; }
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
