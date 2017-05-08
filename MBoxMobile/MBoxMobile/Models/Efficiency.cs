using Newtonsoft.Json;
using System.Collections.Generic;

namespace MBoxMobile.Models
{
    public abstract class EfficiencyModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float? Efficiency { get; set; }
        public float? EfficiencyPercent
        {
            get
            {
                return Efficiency.HasValue ? float.Parse(Efficiency.Value.ToString("n2")) * 100 : (float?)null;
            }
        }
        public int? On { get; set; }
        public int? Off { get; set; }
        public int? Errors { get; set; }
    }

    public class EfficiencyLocation : EfficiencyModel { }
    public class EfficiencyDepartment : EfficiencyModel { }
    public class EfficiencySubDepartment : EfficiencyModel { }
    public class EfficiencyEquipmentType : EfficiencyModel { }
    public class EfficiencyEquipmentGroup : EfficiencyModel { }
    public class EfficiencyAuxiliary : EfficiencyModel { }

    public class EfficiencyMachine
    {
        public string MachineNumber { get; set; }
        public string EquipmentGroup { get; set; }
        public float? Efficiency { get; set; }
        public float? EfficiencyPercent
        {
            get
            {
                return Efficiency.HasValue ? float.Parse(Efficiency.Value.ToString("n2")) * 100 : (float?)null;
            }
        }
        public string Status { get; set; }
        public string RunTime { get; set; }
        public string Stops { get; set; }
        public string StopTime { get; set; }
        public string Worker { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string DepartmentSubName { get; set; }
        public string SystemData { get; set; }

    }

    public class EfficiencyAuxiliaryEquipment
    {
        public string MachineNumber { get; set; }
        public string MachineName { get; set; }
        public string MachineType { get; set; }
        public string MachineGroup { get; set; }
        public float? Efficiency { get; set; }
        public float? EfficiencyPercent
        {
            get
            {
                return Efficiency.HasValue ? float.Parse(Efficiency.Value.ToString("n2")) * 100 : (float?)null;
            }
        }
        public string Location { get; set; }
        public string Department { get; set; }
        public string SubDepartment { get; set; }
        public string SystemData { get; set; }
        public string Stoptime { get; set; }
    }

    #region ListClasses

    public class EfficiencyLocationList
    {
        [JsonProperty("d")]
        public List<EfficiencyLocation> EfficiencyLocations { get; set; }
    }

    public class EfficiencyDepartmentList
    {
        [JsonProperty("d")]
        public List<EfficiencyDepartment> EfficiencyDepartments { get; set; }
    }

    public class EfficiencySubDepartmentList
    {
        [JsonProperty("d")]
        public List<EfficiencySubDepartment> EfficiencySubDepartments { get; set; }
    }

    public class EfficiencyEquipmentTypeList
    {
        [JsonProperty("d")]
        public List<EfficiencyEquipmentType> EfficiencyEquipmentTypes { get; set; }
    }

    public class EfficiencyEquipmentGroupList
    {
        [JsonProperty("d")]
        public List<EfficiencyEquipmentGroup> EfficiencyEquipmentGroups { get; set; }
    }

    public class EfficiencyAuxiliaryList
    {
        [JsonProperty("d")]
        public List<EfficiencyAuxiliary> EfficiencyAuxiliaries { get; set; }
    }

    public class EfficiencyMachineList
    {
        [JsonProperty("d")]
        public List<EfficiencyMachine> EfficiencyMachines { get; set; }
    }

    public class EfficiencyAuxiliaryEquipmentList
    {
        [JsonProperty("d")]
        public List<EfficiencyAuxiliaryEquipment> EfficiencyAuxiliaryEquipments { get; set; }
    }

    #endregion
}
