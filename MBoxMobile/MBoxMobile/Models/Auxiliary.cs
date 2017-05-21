using System.Collections.Generic;
using Newtonsoft.Json;

namespace MBoxMobile.Models
{
    public class AuxiliaryType
    {
        public int EquipmentType { get; set; }
        public string EquipmentTypeName { get; set; }
        public int? On { get; set; }
        public int Qty { get; set; }
    }

    public class AuxiliaryEquipment
    {
        public int MachineNumber { get; set; }
        public string EquipmentGroup { get; set; }
        public int? On { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string SubDepartment { get; set; }
        public int? SystemData { get; set; }
    }

    public class AuxiliaryTypeList
    {
        [JsonProperty("d")]
        public List<AuxiliaryType> AuxiliaryTypes { get; set; }
    }

    public class AuxiliaryEquipmentList
    {
        [JsonProperty("d")]
        public List<AuxiliaryEquipment> AuxiliaryEquipments { get; set; }
    }
}
