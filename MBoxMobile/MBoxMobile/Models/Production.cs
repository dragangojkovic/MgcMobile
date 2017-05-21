using Newtonsoft.Json;
using System.Collections.Generic;

namespace MBoxMobile.Models
{
    public class ProductionGeneral
    {
        public int EquipmentType { get; set; }
        public string EquipmentTypeName { get; set; }
        public float? Efficiency { get; set; }
        public int? On { get; set; }
        public int? Off { get; set; }
        public string DescCH { get; set; }
    }

    public class ProductionDetail
    {
        public int MachineID { get; set; }
        public string MachineNumber { get; set; }
        public string MachineGroupName { get; set; }
        public string ProductionType { get; set; }
        public float? Efficiency { get; set; }
        public string SpecCode { get; set; }
        public int? CycleTime { get; set; }
        public int? AverageWeld { get; set; }
        public int? AverageWeldTime { get; set; }
        public int? AverageWireUse { get; set; }
        public string Status { get; set; }
        public string Runtime { get; set; }
        public int? CurrentStop { get; set; }
        public string TotalStop { get; set; }
        public float? Kwh { get; set; }
        public string Operator { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string SubDepartment { get; set; }
        public string WireUnit { get; set; }
        public int? SystemData { get; set; }
    }

    public class ProductionGeneralList
    {
        [JsonProperty("d")]
        public List<ProductionGeneral> ProductionGenerals { get; set; }
    }

    public class ProductionDetailList
    {
        [JsonProperty("d")]
        public List<ProductionDetail> ProductionDetails { get; set; }
    }
}
