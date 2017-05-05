namespace MBoxMobile.Models
{
    public class UserInfo
    {
        public UserLogin login { get; set; }
        public int status { get; set; }
    }

    public class UserLogin
    {
        public int RecordId { get; set; }
        public string __type { get; set; }
        public string UserGroup { get; set; }
        public string BelongToLocationID { get; set; }
        public int BelongToTableID { get; set; }
        public string TInitials { get; set; }
        public string IsFreeze1 { get; set; }
        public string Title { get; set; }
        public string MenuLanguage { get; set; }
        public string TimePeriodFilter { get; set; }
        public string LocationFilter { get; set; }
        public string DepartmentFilter { get; set; }
        public string DepartmentSubFilter { get; set; }
        public string FunctionFilter { get; set; }
        public string EquipmentLocationFilter { get; set; }
        public string EquipmentDepartmentFilter { get; set; }
        public string EquipmentTypeFilter { get; set; }
        public string EfficiencyWorkHours { get; set; }
        public string FirstName { get; set; }
        public string LoginID { get; set; }
        public string MachineGroupFilter { get; set; }
        public string MainFilter { get; set; }
        //
        public string NotificationFilter { get; set; }
        public string SelectedNotificationFilter { get; set; }
        public string SelectedPersonalFilter { get; set; }
        public string ServerIPAddress { get; set; }
    }
}
