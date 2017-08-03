using System.Collections.Generic;

namespace MBoxMobile.Helpers
{
    public static class NotificationSupport
    {
        public static List<string> GetAlterTypeDisplayNames()
        {
            List<string> list = new List<string>();

            list.Add("Electricity waste");
            list.Add("Equipment problem");
            list.Add("Operational");
            list.Add("Quality control");
            list.Add("Quality problem");
            list.Add("Planning");
            list.Add("System notification");

            return list;
        }
    }

    public class NotificationGroupInfo
    {
        public string GroupName { get; set; }
        public int GroupItemCount { get; set; }
        public TableLayoutType TableType { get; set; }
    }

    public enum NotificationType
    {
        NonConfirmed = 1,
        Solution,
        ToBeApproved,
        AllReported,
        AllApproved
    }

    public enum NotificationAlterType
    {
        ElectricityWaste,
        EquipmentProblem,
        Operational,
        QualityControl,
        QualityProblem,
        Planning,
        SystemNotification
    }

    public enum TableLayoutType
    {
        NoCheckboxes = 1,
        AcknowledgeCheckboxes,
        SaveCheckboxes
    }

    public class SelectedCheckboxes
    {
        public string Id { get; set; }
        public bool Acknowledge { get; set; }
        public bool Report { get; set; }
    }
}
