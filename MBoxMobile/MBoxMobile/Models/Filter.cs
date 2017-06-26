using Newtonsoft.Json;
using System.Collections.Generic;

namespace MBoxMobile.Models
{
    public class Filter
    {
        public int Id { get; set; }
        public int FilterID { get; set; }
        public string FilterName { get; set; }
    }

    public class PersonalFilter
    {
        public List<Filter> FilterList { get; set; }
        public bool FilterOn { get; set; }
        public int SelectedFilterID { get; set; }
    }

    public class PersonalFilterWrapper
    {
        [JsonProperty("d")]
        public PersonalFilter MyPersonalFilter { get; set; }
    }
}
