using Newtonsoft.Json;
using System.Collections.Generic;

namespace MBoxMobile.Models
{
    public abstract class ElectricityModel
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Current { get; set; }
		public string Total { get; set; }
        public string NoDetails { get; set; }
        public string Waste { get; set; }
		public string WastePer { get; set; }
	}

    public class ElectricityLocation : ElectricityModel { }
	public class ElectricityArea : ElectricityModel { }
	public class ElectricityDepartment : ElectricityModel { }
	public class ElectricitySubDepartment : ElectricityModel { }

	public class ElectricityMachine
	{
		public int MachineNumber { get; set; }
		public string MachineGroupName { get; set; }
		public string EfficiencyPer { get; set; }
		public string StatusText { get; set; }
		public string Current { get; set; }
		public string Total { get; set; }
		public string Waste { get; set; }
		public string WastePer { get; set; }
		public string Location { get; set; }
		public string DepartmentName { get; set; }
		public string SubDepartmentName { get; set; }
		public string SystemData { get; set; }
	}

    public class ElectricityLocationList
    {
        [JsonProperty("d")]
        public List<ElectricityLocation> ElectricityLocations { get; set; }
    }

    public class ElectricityAreaList
    {
        [JsonProperty("d")]
        public List<ElectricityArea> ElectricityAreas { get; set; }
    }

    public class ElectricityDepartmentList
    {
        [JsonProperty("d")]
        public List<ElectricityDepartment> ElectricityDepartments { get; set; }
    }

    public class ElectricitySubDepartmentList
    {
        [JsonProperty("d")]
        public List<ElectricitySubDepartment> ElectricitySubDepartments { get; set; }
    }

    public class ElectricityMachineList
    {
        [JsonProperty("d")]
        public List<ElectricityMachine> ElectricityMachines { get; set; }
    }
}
